using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.IdentityUsers;
using OkVip.Gamble.Tickets;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace OkVip.Gamble.Profiles
{
    [Authorize]
    public class ProfileAppService : GambleAppService, IProfileAppService
    {
        public IRepository<IdentityUser, Guid> IdentityUserRepository { get; set; }

        public IRepository<Ticket, Guid> TicketRepository { get; set; }

        public virtual async Task<IdentityUserCustomDto> GetAsync()
        {
            var identity = await IdentityUserRepository.GetAsync(CurrentUser.Id.Value);
            var identityDto = ObjectMapper.Map<IdentityUser, IdentityUserCustomDto>(identity);

            identityDto.HasWithdrawPassword = !string.IsNullOrEmpty(identity.GetWithdrawPassword());
            identityDto.AllowCreateTicket = !await TicketRepository.AnyAsync(e => e.Status == TicketStatusConsts.New && e.CreatorId == identity.Id);
            return identityDto;
        }

        public virtual async Task<IdentityUserCustomDto> CreateWithdrawPasswordAsync(CreateWithdrawPasswordInputDto input)
        {
            using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
            {
                var identity = await IdentityUserRepository.GetAsync(CurrentUser.Id.Value);

                identity.SetWithdrawPassword(input.WithdrawPassword);

                await IdentityUserRepository.UpdateAsync(identity);

                await uow.CompleteAsync();
            }

            return await GetAsync();
        }

        public virtual async Task<IdentityUserCustomDto> UpdateWithdrawPasswordAsync(UpdateWithdrawPasswordInputDto input)
        {
            using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
            {
                var identity = await IdentityUserRepository.GetAsync(CurrentUser.Id.Value);

                identity.SetWithdrawPassword(input.WithdrawPassword);

                await IdentityUserRepository.UpdateAsync(identity);

                await uow.CompleteAsync();
            }

            return await GetAsync();
        }
    }
}
