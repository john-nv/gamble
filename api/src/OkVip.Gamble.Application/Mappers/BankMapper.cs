using AutoMapper;
using OkVip.Gamble.Banks;

namespace OkVip.Gamble.Mappers
{
	public class BankMapper : Profile
	{
		public BankMapper()
		{
			CreateMap<Bank, BankOutputDto>();
			CreateMap<BankCreateOrUpdateDto, Bank>();
		}
	}
}
