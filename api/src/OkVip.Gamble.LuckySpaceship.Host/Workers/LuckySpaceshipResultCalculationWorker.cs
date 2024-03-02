using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OkVip.Gamble.Games;
using OkVip.Gamble.Hubs;
using OkVip.Gamble.Rounds;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Threading;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace OkVip.Gamble.Workers
{
	public class LuckySpaceshipResultCalculationWorker : AsyncPeriodicBackgroundWorkerBase
	{
		protected IRoundRepository RoundRepository { get; private set; }

		protected IRepository<RoundDetail, Guid> RoundDetailRepository { get; private set; }

		protected IRepository<Game, Guid> GameRepository { get; private set; }

		protected IRepository<GameLock, Guid> GameLockRepository { get; private set; }

		protected IRepository<RoundCalculation, Guid> RoundCalculationRepository { get; private set; }

		protected IUnitOfWorkManager UnitOfWorkManager { get; private set; }

		protected IAbpDistributedLock DistributedLock { get; private set; }

		protected IClock Clock { get; private set; }

		protected IGuidGenerator GuidGenerator { get; private set; }

		protected IHubContext<LuckySpaceshipHub> HubContext { get; private set; }

		protected IBackgroundJobManager BackgroundJobManager { get; private set; }

		protected Guid LuckySpaceshipGameId => Guid.Parse("3a108428-6d41-b27d-955e-29486d671bc1");

		public LuckySpaceshipResultCalculationWorker(
			AbpAsyncTimer timer,
			IServiceScopeFactory serviceScopeFactory,
			IRoundRepository roundRepository,
			IRepository<RoundDetail, Guid> roundDetailRepository,
			IRepository<Game, Guid> gameRepository,
			IRepository<GameLock, Guid> gameLockRepository,
			IUnitOfWorkManager unitOfWorkManager,
			IGuidGenerator guidGenerator,
			IAbpDistributedLock distributedLock,
			IHubContext<LuckySpaceshipHub> hubContext,
			IRepository<RoundCalculation, Guid> roundCalculationRepository,
			IClock clock,
			IBackgroundJobManager backgroundJobManager
		) : base(timer, serviceScopeFactory)
		{
			Timer.Period = 1000;
			GuidGenerator = guidGenerator;
			RoundRepository = roundRepository;
			GameRepository = gameRepository;
			GameLockRepository = gameLockRepository;
			UnitOfWorkManager = unitOfWorkManager;
			DistributedLock = distributedLock;
			HubContext = hubContext;
			RoundDetailRepository = roundDetailRepository;
			Clock = clock;
			RoundCalculationRepository = roundCalculationRepository;
			BackgroundJobManager = backgroundJobManager;
		}

		protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
		{
			await using (var handle = await DistributedLock.TryAcquireAsync("LuckySpaceshipResultCalculationWorker"))
			{
				if (handle != null)
				{
					using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
					{
						var queryable = await RoundRepository.GetQueryableAsync();

						if (!await RoundRepository.AnyAsync())
						{
							await RoundRepository.InitNewRoundAsync(LuckySpaceshipGameId);
							await HubContext.Clients.Group(LuckySpaceshipGameId.ToString()).SendAsync("RenewGame");

							await uow.CompleteAsync();
							return;
						}

						var newRounds = await RoundRepository.GetListAsync(e => e.Status == RoundStatusConsts.New);

						foreach (var lastRound in newRounds)
						{
							Logger.LogInformation($"Now: {Clock.Now:dd/MM/yyyy HH:mm:ss}");
							Logger.LogInformation($"Round live From: {lastRound.StartTime:dd/MM/yyyy HH:mm:ss}");
							Logger.LogInformation($"Round live To: {lastRound.EndTime:dd/MM/yyyy HH:mm:ss}");

							if (!lastRound.IsLive(Clock.Now))
							{
								Logger.LogInformation("LuckySpaceshipResultCalculationWorker START");

								lastRound.Status = RoundStatusConsts.Finished;
								lastRound.GenerateResult();

								await RoundRepository.UpdateAsync(lastRound, true);
								await RoundCalculationRepository.InsertAsync(new RoundCalculation { RoundId = lastRound.Id, GameId = LuckySpaceshipGameId }, true);

								await HubContext.Clients
												.Group(LuckySpaceshipGameId.ToString())
												.SendAsync("GenerateResult", lastRound.GetResult().Chunk(2).Select(e => string.Join(string.Empty, e)));

								await GameLockRepository.HardDeleteAsync(e => e.GameId == LuckySpaceshipGameId);
								await GameLockRepository.InsertAsync(new GameLock
								{
									From = Clock.Now,
									To = Clock.Now.AddSeconds(30),
									GameId = LuckySpaceshipGameId
								}, true);

								Logger.LogInformation("LuckySpaceshipResultCalculationWorker END");
							}
						}

						await uow.CompleteAsync();
					}
				}
			}
		}
	}
}