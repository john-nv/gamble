using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Permissions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.Chips
{
    [Authorize]
    public class ChipAppService : CrudAppService<Chip, ChipOutputDto, Guid, ChipGetListInput, ChipCreateOrUpdateDto>
    {
        public ChipAppService(IRepository<Chip, Guid> repository) : base(repository)
        {
        }

        public override Task<PagedResultDto<ChipOutputDto>> GetListAsync(ChipGetListInput input)
        {
            return base.GetListAsync(input);
        }

        [Authorize(GamblePermissions.Chip.Create)]
        public override Task<ChipOutputDto> CreateAsync(ChipCreateOrUpdateDto input)
        {
            return base.CreateAsync(input);
        }

        [Authorize(GamblePermissions.Chip.Update)]
        public override Task<ChipOutputDto> UpdateAsync(Guid id, ChipCreateOrUpdateDto input)
        {
            return base.UpdateAsync(id, input);
        }

        [Authorize(GamblePermissions.Chip.Delete)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        protected override IQueryable<Chip> ApplyPaging(IQueryable<Chip> query, ChipGetListInput input)
        {
            return query;
        }

        protected override IQueryable<Chip> ApplyDefaultSorting(IQueryable<Chip> query)
        {
            return query.OrderBy(e => e.Amount);
        }
    }
}