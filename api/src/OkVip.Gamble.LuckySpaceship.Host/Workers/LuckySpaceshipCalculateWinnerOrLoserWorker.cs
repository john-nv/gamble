using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OkVip.Gamble.Games;
using OkVip.Gamble.Hubs;
using OkVip.Gamble.Rounds;
using OkVip.Gamble.Settings;
using OkVip.Gamble.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.Settings;
using Volo.Abp.Threading;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace OkVip.Gamble.Workers
{
    public class LuckySpaceshipCalculateWinnerOrLoserWorker : AsyncPeriodicBackgroundWorkerBase
    {
        protected IUnitOfWorkManager UnitOfWorkManager { get; private set; }

        protected IAbpDistributedLock DistributedLock { get; private set; }

        protected IRepository<RoundDetail, Guid> RoundDetailRepository { get; private set; }

        protected IRepository<RoundCalculation, Guid> RoundCalculationRepository { get; private set; }

        protected IRepository<Transaction, Guid> TransactionRepository { get; private set; }

        protected IHubContext<LuckySpaceshipHub> HubContext { get; private set; }

        protected IRepository<Round, Guid> RoundRepository { get; private set; }

        protected IRepository<Game, Guid> GameRepository { get; private set; }

        protected IRepository<GameLock, Guid> GameLockRepository { get; private set; }

        protected IJsonSerializer JsonSerializer { get; private set; }

        protected IClock Clock { get; private set; }

        protected IGuidGenerator GuidGenerator { get; private set; }
        protected ISettingProvider SettingProvider { get; private set; }

        public LuckySpaceshipCalculateWinnerOrLoserWorker(
            AbpAsyncTimer timer,
            IServiceScopeFactory serviceScopeFactory,
            IRepository<RoundDetail, Guid> roundDetailRepository,
            IRepository<RoundCalculation, Guid> roundCalculationRepository,
            IHubContext<LuckySpaceshipHub> hubContext,
            IRepository<Round, Guid> roundRepository,
            IRepository<Game, Guid> gameRepository,
            IRepository<GameLock, Guid> gameLockRepository,
            IRepository<Transaction, Guid> transactionRepository,
            IJsonSerializer jsonSerializer,
            IUnitOfWorkManager unitOfWorkManager,
            IAbpDistributedLock distributedLock,
            IGuidGenerator guidGenerator,
            IClock clock,
            ISettingProvider settingProvider
        ) : base(timer, serviceScopeFactory)
        {
            Timer.Period = 1000;
            RoundDetailRepository = roundDetailRepository;
            HubContext = hubContext;
            RoundRepository = roundRepository;
            GameRepository = gameRepository;
            GameLockRepository = gameLockRepository;
            JsonSerializer = jsonSerializer;
            Clock = clock;
            RoundCalculationRepository = roundCalculationRepository;
            UnitOfWorkManager = unitOfWorkManager;
            DistributedLock = distributedLock;
            TransactionRepository = transactionRepository;
            GuidGenerator = guidGenerator;
            SettingProvider = settingProvider;
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {


            await using (var handle = await DistributedLock.TryAcquireAsync("LuckySpaceshipCalculateWinnerOrLoserWorker"))
            {
                if (handle != null)
                {
                    using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
                    {
                        var roundCalculations = await RoundCalculationRepository.GetListAsync();

                        Logger.LogInformation($"FOUND {roundCalculations.Count} ROUND CALCULATIONS");

                        foreach (var item in roundCalculations)
                        {
                            var round = await RoundRepository.FindAsync(e => e.Id == item.RoundId);

                            if (round == null)
                            {
                                continue;
                            }

                            if (round.Status != RoundStatusConsts.Finished)
                            {
                                continue;
                            }

                            var game = await GameRepository.FindAsync(e => e.Id == round.GameId);

                            if (game == null)
                            {
                                continue;
                            }

                            Logger.LogInformation($"BEGIN HANDLE WINER OR LOSER ROUND: {item.RoundId}");

                            var blockResults = JsonSerializer.Deserialize<List<int>>(round.ExtraProperties["blockResults"].ToString());
                            var details = await RoundDetailRepository.GetListAsync(e => e.RoundId == item.RoundId);
                            var terms = JsonSerializer.Deserialize<GameTerm>(game.ExtraProperties["terms"]!.ToString());
                            var blocks = JsonSerializer.Deserialize<List<GameBlock>>(game.ExtraProperties["blocks"]!.ToString());

                            foreach (var detail in details)
                            {
                                string jsonExtraProperties = JsonSerializer.Serialize(detail.ExtraProperties);
                                var betting = JsonSerializer.Deserialize<RoundDetailExtraPropertyDto>(jsonExtraProperties);
                                decimal totalEarnedAmount = 0m;

                                var betWinTransaction = new Transaction(GuidGenerator.Create())
                                {
                                    Amount = 0m,
                                    Status = TransactionStatusConsts.Success,
                                    Type = TransactionTypeConsts.Pay,
                                    UserId = detail.UserId,
                                };

                                foreach (BetItem bet in betting.Bets)
                                {
                                    var blockSelelected = blocks.FirstOrDefault(block => block.Id == bet.Block.Id);

                                    if (blockSelelected == null)
                                    {
                                        continue;
                                    }

                                    int index = blockSelelected.Id - 1;
                                    int resultSelected = blockResults[index];
                                    bool isMatchMinMax = terms[bet.Term].MinMax.Any() && (resultSelected >= terms[bet.Term].MinMax[0] && resultSelected <= terms[bet.Term].MinMax[1]);
                                    bool isMatchRange = terms[bet.Term].Range.Any() && terms[bet.Term].Range.Contains(resultSelected);

                                    if (isMatchRange || isMatchMinMax)
                                    {
                                        var rate = terms[bet.Term].Rate;

                                        if (blockSelelected.Id != 1 && new[] { "long", "phung" }.Contains(bet.Term))
                                        {
                                            rate = await SettingProvider.GetAsync(GambleSettings.LuckspaceshipDefaultRate, BetRateConsts.DefaultRate);
                                        }

                                        var earnedAmount = bet.BetAmount * rate;
                                        totalEarnedAmount += earnedAmount;

                                        betWinTransaction.Amount += earnedAmount;
                                    }
                                }

                                await TransactionRepository.InsertAsync(betWinTransaction, true);
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
                            await RoundCalculationRepository.HardDeleteAsync(item, true);

                            await HubContext.Clients.Groups(game.Id.ToString())
                                                    .SendAsync("Result", new
                                                    {
                                                        item.RoundId,
                                                        GameId = game.Id
                                                    });

                            Logger.LogInformation($"FINISH HANDLE WINER OR LOSER ROUND: {item.RoundId}");
                        }

                        await uow.CompleteAsync();
                    }
                }
            }
        }
    }
}