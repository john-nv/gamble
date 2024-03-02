using AutoMapper;
using OkVip.Gamble.Announcements;

namespace OkVip.Gamble.Mappers
{
    public class AnnouncementMapper : Profile
    {
        public AnnouncementMapper()
        {
            CreateMap<Announcement, AnnouncementOutputDto>();
            CreateMap<AnnouncementCreateOrUpdateDto, Announcement>();
        }
    }
}
