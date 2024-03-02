using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.BankAccounts
{
	public interface IBankAccountAppService : ICrudAppService<BankAccountOutputDto, Guid, BankAccountGetListInputDto, BankAccountCreateOrUpdateDto>
	{
		Task<IList<BankAccountOutputDto>> GetMyAccountsAsync();
	}
}
