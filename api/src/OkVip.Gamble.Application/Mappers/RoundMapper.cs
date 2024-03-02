using AutoMapper;
using OkVip.Gamble.Rounds;

namespace OkVip.Gamble.Mappers
{
	public class RoundMapper : Profile
	{
		public RoundMapper()
		{
			CreateMap<Round, PublicRoundOutputDto>();
			CreateMap<Round, PublicRoundHistoryOutputDto>()
				.MapExtraPropertiesToRegularProperties();

			CreateMap<RoundDetail, PublicRoundDetailOutputDto>();
		}
	}
}
