using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;

namespace OkVip.Gamble.Games
{
    [Authorize]
    public class PublicGameAppService : ReadOnlyAppService<Game, PublicGameOutputDto, Guid, GameGetListInputDto>, IPublicGameAppService
    {
        public ISettingProvider SettingProvider { get; set; }

        public PublicGameAppService(IReadOnlyRepository<Game, Guid> repository) : base(repository)
        {

        }

        public virtual async Task<LuckySpaceshipSetting> GetLuckySpaceshipSettingAsync()
        {
            var defaultRate = await SettingProvider.GetAsync(GambleSettings.LuckspaceshipDefaultRate, BetRateConsts.DefaultRate);
            var amountInVndUnit = await SettingProvider.GetAsync(GambleSettings.AmountInVndUnit, 24000m);
            return new LuckySpaceshipSetting
            {
                DefaultRate = defaultRate,
                AmountInVndUnit = amountInVndUnit
            };
        }

        public override async Task<PublicGameOutputDto> GetAsync(Guid id)
        {
            var queryable = await Repository.WithDetailsAsync(e => e.CurrentRound);
            var entity = queryable.FirstOrDefault(e => e.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(Game), id);
            }

            return await MapToGetOutputDtoAsync(entity);
        }

        protected override IQueryable<Game> ApplyPaging(IQueryable<Game> query, GameGetListInputDto input)
        {
            return query;
        }
    }
}