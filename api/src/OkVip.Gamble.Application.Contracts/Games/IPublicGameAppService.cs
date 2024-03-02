using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Games
{
    public interface IPublicGameAppService : IReadOnlyAppService<PublicGameOutputDto, Guid, GameGetListInputDto>
    {
        Task<LuckySpaceshipSetting> GetLuckySpaceshipSettingAsync();

    }
}
