using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OkVip.Gamble.Games;
using OkVip.Gamble.Hubs;
using OkVip.Gamble.Rounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Json;
using Volo.Abp.Timing;

namespace OkVip.Gamble.Backgrounds
{
	public class CalculateWinnerOrLoserBgJob : AsyncBackgroundJob<CalculateWinnerOrLoserArgs>, ITransientDependency
	{
		public IRepository<RoundDetail, Guid> RoundDetailRepository { get; set; }

		public IHubContext<LuckySpaceshipHub> HubContext { get; set; }

		public IRepository<Round, Guid> RoundRepository { get; set; }

		public IRepository<Game, Guid> GameRepository { get; set; }

		public IRepository<GameLock, Guid> GameLockRepository { get; set; }

		public IJsonSerializer JsonSerializer { get; set; }

		public IClock Clock { get; set; }

		public override async Task ExecuteAsync(CalculateWinnerOrLoserArgs args)
		{
			var round = await RoundRepository.FindAsync(e => e.Id == args.RoundId);

			if (round == null) return;

			if (round.Status != RoundStatusConsts.Finished) return;

			var game = await GameRepository.FindAsync(e => e.Id == round.GameId);

			if (game == null) return;

			Logger.LogInformation("BEGIN HANDLE WINER OR LOSER");

			var blockResults = JsonSerializer.Deserialize<List<int>>(round.ExtraProperties["blockResults"].ToString());
			var details = await RoundDetailRepository.GetListAsync(e => e.RoundId == args.RoundId);
			var terms = JsonSerializer.Deserialize<GameTerm>(game.ExtraProperties["terms"]!.ToString());
			var blocks = JsonSerializer.Deserialize<List<GameBlock>>(game.ExtraProperties["blocks"]!.ToString());

			foreach (RoundDetail detail in details)
			{
				string jsonExtraProperties = JsonSerializer.Serialize(detail.ExtraProperties);
				RoundDetailExtraPropertyDto betting = JsonSerializer.Deserialize<RoundDetailExtraPropertyDto>(jsonExtraProperties);
				decimal totalEarnedAmount = 0m;

				foreach (BetItem bet in betting.Bets)
				{
					var blockSelelected = blocks.FirstOrDefault(block => block.Id == bet.Block.Id);

					if (blockSelelected == null) continue;

					int index = blockSelelected.Id - 1;
					int resultSelected = blockResults[index];
					bool isMatchMinMax = terms[bet.Term].MinMax.Any() && (resultSelected >= terms[bet.Term].MinMax[0] && resultSelected <= terms[bet.Term].MinMax[1]);
					bool isMatchRange = terms[bet.Term].Range.Any() && terms[bet.Term].Range.Contains(resultSelected);

					if (isMatchRange || isMatchMinMax)
					{
						totalEarnedAmount += bet.BetAmount * terms[bet.Term].Rate;
					}
				}

				detail.AmountEarned = totalEarnedAmount;
				detail.Status = detail.AmountEarned > detail.TotalBetAmount ? RoundDetailStatusConst.Win : RoundDetailStatusConst.Lose;

				if (detail.AmountEarned == detail.TotalBetAmount)
				{
					detail.Status = RoundDetailStatusConst.Tier;
				}

				if (detail.Status == RoundDetailStatusConst.Lose)
				{
					detail.AmountEarned = detail.TotalBetAmount * -1m;
				}

				await RoundDetailRepository.UpdateAsync(detail, true);
			}

			await GameRepository.UpdateAsync(game, true);

			await HubContext.Clients.Groups(game.Id.ToString()).SendAsync("Result", new { RoundId = args.RoundId, GameId = game.Id });

			Logger.LogInformation("FINISH HANDLE WINER OR LOSER");
		}
	}
}