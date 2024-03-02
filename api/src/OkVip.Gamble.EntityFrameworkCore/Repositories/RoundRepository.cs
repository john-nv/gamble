using Microsoft.Extensions.Logging;
using OkVip.Gamble.EntityFrameworkCore;
using OkVip.Gamble.Games;
using OkVip.Gamble.Rounds;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Timing;

namespace OkVip.Gamble.Repositories
{
	public class RoundRepository : EfCoreRepository<GambleDbContext, Round, Guid>, IRoundRepository
	{
		public IRepository<Game, Guid> GameRepository { get; set; }

		public IClock Clock { get; set; }

		public ILogger<RoundRepository> Logger { get; set; }

		public RoundRepository(IDbContextProvider<GambleDbContext> dbContextProvider) : base(dbContextProvider)
		{
		}

		public virtual async Task<Round> InitNewRoundAsync(Guid gameId)
		{
			Game game = await GameRepository.GetAsync(gameId);

			Round entity = new Round
			{
				Code = Clock.Now.ToString("yyyyMMddHHmmss"),
				GameId = gameId,
				StartTime = Clock.Now,
				EndTime = Clock.Now.AddMinutes(5),
				Status = RoundStatusConsts.New
			};

			await InsertAsync(entity, true);

			game.CurrentRoundId = entity.Id;

			Logger.LogInformation($"ROUND OF GAME LUCKY SPACESHIP IS INITIALED ");

			await GameRepository.UpdateAsync(game, true);

			return entity;
		}
	}
}
