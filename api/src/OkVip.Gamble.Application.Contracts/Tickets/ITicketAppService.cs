using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Tickets
{
    public interface ITicketAppService : ICrudAppService<TicketOutputDto, Guid, TicketGetListInputDto, TicketCreateOrUpdateDto>
    {
        Task<TicketOutputDto> CreateWithdrawAsync(WithdrawTicketCreateOrUpdateDto input);

        Task<TicketOutputDto> CreateAsync(TicketCreateOrUpdateDto input);

        Task<TicketOutputDto> RejectAsync(Guid id, TicketRejectInputDto input);
    }
}