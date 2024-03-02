using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.Announcements
{
    [Authorize]
    public class AnnouncementAppService : CrudAppService<Announcement, AnnouncementOutputDto, Guid, AnnouncementGetListInput, AnnouncementCreateOrUpdateDto>
    {
        public AnnouncementAppService(IRepository<Announcement, Guid> repository) : base(repository)
        {
        }

        public override Task<PagedResultDto<AnnouncementOutputDto>> GetListAsync(AnnouncementGetListInput input)
        {
            return base.GetListAsync(input);
        }

        [Authorize(GamblePermissions.Announcement.Create)]
        public override Task<AnnouncementOutputDto> CreateAsync(AnnouncementCreateOrUpdateDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(GamblePermissions.Announcement.Update)]
        public override Task<AnnouncementOutputDto> UpdateAsync(Guid id, AnnouncementCreateOrUpdateDto input)
        {
            return base.UpdateAsync(id, input);
        }

        [Authorize(GamblePermissions.Announcement.Delete)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }
    }
}