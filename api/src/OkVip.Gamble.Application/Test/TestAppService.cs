using Microsoft.Extensions.Logging;
using OkVip.Gamble.Games;
using OkVip.Gamble.Rounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Json;

namespace OkVip.Gamble.Test
{
	public class TestAppService : ApplicationService, ITransientDependency
	{
		public IRepository<RoundDetail, Guid> RoundDetailRepository { get; set; }

		public IRepository<Round, Guid> RoundRepository { get; set; }

		public IRepository<Game, Guid> GameRepository { get; set; }

		public IJsonSerializer JsonSerializer { get; set; }

		public virtual async Task ExecuteAsync(Guid id)
		{
			var round = await RoundRepository.FindAsync(e => e.Id == id);

			if (round == null) return;

			if (round.Status != RoundStatusConsts.Finished) return;

			var game = await GameRepository.FindAsync(e => e.Id == round.GameId);

			if (game == null) return;

			Logger.LogInformation("BEGIN HANDLE WINER OR LOSER");

			var blockResults = JsonSerializer.Deserialize<List<int>>(round.ExtraProperties["blockResults"].ToString());
			var results = blockResults.Chunk(2).ToList();
			var details = await RoundDetailRepository.GetListAsync(e => e.RoundId == id);
			var terms = JsonSerializer.Deserialize<GameTerm>(game.ExtraProperties["terms"]!.ToString());
			var blocks = JsonSerializer.Deserialize<List<GameBlock>>(game.ExtraProperties["blocks"]!.ToString());

			foreach (var detail in details)
			{
				var jsonExtraProperties = JsonSerializer.Serialize(detail.ExtraProperties);
				var betting = JsonSerializer.Deserialize<RoundDetailExtraPropertyDto>(jsonExtraProperties);
				var totalEarnedAmount = 0m;

				foreach (var bet in betting.Bets)
				{
					var blockSelelected = blocks.FirstOrDefault(block => block.Id == bet.Block.Id);

					if (blockSelelected == null) continue;

					var index = blockSelelected.Id - 1;
					var resultSelected = blockResults[index];
					var isMatchMinMax = terms[bet.Term].MinMax.Any() && (resultSelected >= terms[bet.Term].MinMax[0] && resultSelected <= terms[bet.Term].MinMax[1]);
					var isMatchRange = terms[bet.Term].Range.Any() && terms[bet.Term].Range.Contains(resultSelected);

					if (isMatchRange || isMatchMinMax)
					{
						totalEarnedAmount += bet.BetAmount * terms[bet.Term].Rate;
					}
					else
					{
						totalEarnedAmount -= bet.BetAmount * terms[bet.Term].Rate;
					}
				}

				detail.AmountEarned = totalEarnedAmount;
				detail.Status = detail.AmountEarned > 0 ? RoundDetailStatusConst.Win : RoundDetailStatusConst.Lose;

				if (detail.AmountEarned == 0)
					detail.Status = RoundDetailStatusConst.Tier;

				await RoundDetailRepository.UpdateAsync(detail, true);
			}
		}
	}
}