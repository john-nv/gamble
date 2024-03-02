using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using OkVip.Gamble.Games;
using OkVip.Gamble.Hubs;
using OkVip.Gamble.Rounds;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace OkVip.Gamble.Workers
{
	public class LuckySpaceshipRoundInitializeWorker : AsyncPeriodicBackgroundWorkerBase
	{
		protected IUnitOfWorkManager UnitOfWorkManager { get; private set; }

		protected IAbpDistributedLock DistributedLock { get; private set; }

		protected IRepository<GameLock, Guid> GameLockRepository { get; private set; }

		protected IClock Clock { get; private set; }

		protected IHubContext<LuckySpaceshipHub> HubContext { get; private set; }

		protected IRoundRepository RoundRepository { get; private set; }

		protected Guid LuckySpaceshipGameId => Guid.Parse("3a108428-6d41-b27d-955e-29486d671bc1");

		public LuckySpaceshipRoundInitializeWorker(
			AbpAsyncTimer timer,
			IServiceScopeFactory serviceScopeFactory,
			IUnitOfWorkManager unitOfWorkManager,
			IClock clock,
			IAbpDistributedLock distributedLock,
			IHubContext<LuckySpaceshipHub> hubContext,
			IRoundRepository roundRepository,
			IRepository<GameLock, Guid> gameLockRepository
		) : base(timer, serviceScopeFactory)
		{
			Timer.Period = 1000;
			Clock = clock;
			GameLockRepository = gameLockRepository;
			UnitOfWorkManager = unitOfWorkManager;
			DistributedLock = distributedLock;
			HubContext = hubContext;
			RoundRepository = roundRepository;
		}

		protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
		{
			await using (var handle = await DistributedLock.TryAcquireAsync("LuckySpaceshipRoundInitializeWorker"))
			{
				if (handle != null)
				{
					var currentNow = Clock.Now;
					using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
					{
						var lockGame = await GameLockRepository.FirstOrDefaultAsync(e => e.GameId == LuckySpaceshipGameId);

						if (lockGame == null)
						{
							return;
						}

						if (!(currentNow >= lockGame.From && currentNow <= lockGame.To))
						{
							await GameLockRepository.HardDeleteAsync(e => e.GameId == LuckySpaceshipGameId);
							await RoundRepository.InitNewRoundAsync(LuckySpaceshipGameId);
							await HubContext.Clients.Group(LuckySpaceshipGameId.ToString()).SendAsync("RenewGame");
						}
						else
						{
							await HubContext.Clients
											.Group(LuckySpaceshipGameId.ToString())
											.SendAsync("WaitingFor", new
											{
												WaitInSeconds = (lockGame.To - currentNow).TotalSeconds,
											});
						}

						await uow.CompleteAsync();
					}
				}
			}
		}
	}
}