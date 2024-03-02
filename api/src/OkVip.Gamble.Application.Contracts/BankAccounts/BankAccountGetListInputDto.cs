using System;
using Volo.Abp.Application.Dtos;

namespace OkVip.Gamble.BankAccounts
{
    public class BankAccountGetListInputDto : PagedAndSortedResultRequestDto
    {
        public Guid? CreatorId { get; set; }
    }
}
