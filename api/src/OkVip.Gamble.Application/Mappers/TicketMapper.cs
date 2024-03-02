using AutoMapper;
using OkVip.Gamble.Tickets;

namespace OkVip.Gamble.Mappers
{
	public class TicketMapper : Profile
	{
		public TicketMapper()
		{
			CreateMap<Ticket, TicketOutputDto>()
				.ForMember(e => e.CreatorName, config => config.MapFrom(e => e.Creator.Name))
				.ForMember(e => e.CreatorUsername, config => config.MapFrom(b => b.Creator.UserName))
				.ForMember(e => e.CreatorEmail, config => config.MapFrom(b => b.Creator.Email));

			CreateMap<TicketCreateOrUpdateDto, Ticket>();
			CreateMap<TicketLog, TicketLogOutputDto>();
		}
	}
}
