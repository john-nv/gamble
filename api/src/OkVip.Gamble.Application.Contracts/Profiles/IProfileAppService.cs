using OkVip.Gamble.IdentityUsers;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Profiles
{
    public interface IProfileAppService : IApplicationService
    {
        Task<IdentityUserCustomDto> GetAsync();

        Task<IdentityUserCustomDto> CreateWithdrawPasswordAsync(CreateWithdrawPasswordInputDto input);

        Task<IdentityUserCustomDto> UpdateWithdrawPasswordAsync(UpdateWithdrawPasswordInputDto input);
    }
}
