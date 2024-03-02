using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OkVip.Gamble.Games;
using OkVip.Gamble.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace OkVip.Gamble.Rounds
{
    [Authorize]
    public class RoundAppService : GambleAppService, IRoundAppService
    {
        public IRepository<RoundDetail, Guid> RoundDetailRepository { get; set; }

        public IRepository<Game, Guid> GameRepository { get; set; }

        public IRepository<Round, Guid> RoundRepository { get; set; }

        public IRepository<IdentityUser, Guid> IdentityUserRepository { get; set; }

        public IAbpDistributedLock DistributedLock { get; set; }

        [HttpGet("/api/gamble/game/{gameId}/histories")]
        public virtual async Task<IList<PublicRoundHistoryOutputDto>> GetHistoriesAsync(Guid gameId, int? limit = 100)
        {
            limit ??= 100;

            var queryable = await RoundRepository.GetQueryableAsync();
            var histories = queryable.Where(e => e.Status == RoundStatusConsts.Finished)
                                     .Where(e => e.GameId == gameId)
                                     .OrderByDescending(e => e.CreationTime)
                                     .Take(limit.Value)
                                     .ToList();

            return ObjectMapper.Map<IList<Round>, IList<PublicRoundHistoryOutputDto>>(histories);
        }

        [HttpGet("/api/gamble/game/{gameId}/prev-round")]
        public virtual async Task<PublicRoundOutputDto> GetPreviousRoundAsync(Guid gameId)
        {
            var queryable = await RoundRepository.GetQueryableAsync();
            var entity = queryable.Where(e => e.GameId == gameId && e.Status == RoundStatusConsts.Finished)
                                  .OrderBy(e => e.CreationTime)
                                  .LastOrDefault();

            return ObjectMapper.Map<Round, PublicRoundOutputDto>(entity);
        }

        [HttpGet("/api/gamble/game/{gameId}/round/{roundId}/detail")]
        public virtual async Task<PublicRoundDetailOutputDto> GetAsync(Guid gameId, Guid roundId)
        {
            var entity = await RoundDetailRepository.FirstOrDefaultAsync(e =>
                e.GameId == gameId &&
                e.RoundId == roundId &&
                e.UserId == CurrentUser.Id.Value
            );

            return ObjectMapper.Map<RoundDetail, PublicRoundDetailOutputDto>(entity);
        }

        [HttpPost("/api/gamble/game/{gameId}/round/{roundId}/detail")]
        public virtual async Task<PublicRoundDetailOutputDto> CreateAsync(Guid gameId, Guid roundId, RoundDetailCreateDto input)
        {
            await using (var handle = await DistributedLock.TryAcquireAsync($"OkVip.Gamble.Rounds.Create.{gameId}.{roundId}.{CurrentUser.Id}"))
            {
                if (handle != null)
                {
                    string jsonExtraProperties = JsonSerializer.Serialize(input.ExtraProperties);
                    var betting = JsonSerializer.Deserialize<RoundDetailExtraPropertyDto>(jsonExtraProperties);

                    if (!betting.Bets.Any())
                    {
                        throw new UserFriendlyException("Đặt cược không hợp lệ!");
                    }

                    using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
                    {
                        var identityUser = await IdentityUserRepository.GetAsync(CurrentUser.Id.Value);
                        var gameEntity = await GameRepository.GetAsync(gameId);
                        var roundEntity = await RoundRepository.GetAsync(roundId);
                        var accountBalance = identityUser.GetAccountBalance();

                        if (accountBalance < input.TotalBetAmount)
                        {
                            throw new UserFriendlyException(message: "Số dư không đủ");
                        }

                        if (!roundEntity.IsLive(Clock.Now))
                        {
                            throw new UserFriendlyException(message: "Vòng quay số đã hết hạn");
                        }

                        if (await RoundDetailRepository.AnyAsync(e => e.GameId == gameId && e.RoundId == roundId && e.UserId == CurrentUser.Id.Value))
                        {
                            throw new UserFriendlyException(message: "Vòng quay số đã được đặt cược");
                        }

                        await RoundDetailRepository.InsertAsync(new RoundDetail
                        {
                            GameId = gameId,
                            RoundId = roundId,
                            UserId = CurrentUser.Id.Value,
                            TotalBetAmount = input.TotalBetAmount,
                            ExtraProperties = input.ExtraProperties,
                            Status = RoundDetailStatusConst.Unknow
                        });

                        await uow.CompleteAsync();
                    }

                    using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
                    {
                        return await GetAsync(gameId, roundId);
                    }
                }
                else
                {
                    throw new UserFriendlyException("Đang xử lý");
                }
            }
        }
    }
}