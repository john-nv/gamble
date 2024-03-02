using AutoMapper;
using OkVip.Gamble.IdentityUsers;
using Volo.Abp.Identity;

namespace OkVip.Gamble.Mappers
{
    public class IdentityUserMapper : Profile
    {
        public IdentityUserMapper()
        {
            CreateMap<IdentityUser, IdentityUserCustomDto>()
                .MapExtraPropertiesToRegularProperties();
        }
    }
}