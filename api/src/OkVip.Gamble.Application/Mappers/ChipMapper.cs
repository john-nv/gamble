using AutoMapper;
using OkVip.Gamble.Chips;

namespace OkVip.Gamble.Mappers
{
    public class ChipMapper : Profile
    {
        public ChipMapper()
        {
            CreateMap<Chip, ChipOutputDto>();
            CreateMap<ChipCreateOrUpdateDto, Chip>();
        }
    }
}
