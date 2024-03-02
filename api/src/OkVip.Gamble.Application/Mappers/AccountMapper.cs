using AutoMapper;
using OkVip.Gamble.Accounts;
using OkVip.Gamble.BankAccounts;

namespace OkVip.Gamble.Mappers
{
	public class BankAccountMapper : Profile
	{
		public BankAccountMapper()
		{
			CreateMap<Account, BankAccountOutputDto>();
			CreateMap<BankAccountCreateOrUpdateDto, Account>();
		}
	}
}