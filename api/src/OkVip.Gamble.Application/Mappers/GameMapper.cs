using AutoMapper;
using OkVip.Gamble.Games;

namespace OkVip.Gamble.Mappers
{
	public class GameMapper : Profile
	{
		public GameMapper()
		{
			CreateMap<Game, GameOutputDto>();
			CreateMap<Game, PublicGameOutputDto>()
				.ForMember(e => e.CurrentRound, config => config.MapFrom(e => e.CurrentRound));

			CreateMap<GameCreateOrUpdateDto, Game>();
		}
	}
}
