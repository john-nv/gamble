using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Localization;
using OkVip.Gamble.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.Categories
{
	[Authorize(GamblePermissions.Category.Default)]
	public class CategoryAppService : CrudAppService<Category, CategoryOutputDto, Guid, CategoryGetListInputDto, CategoryCreateOrUpdateDto>, ICategoryAppService
	{
		public CategoryAppService(IRepository<Category, Guid> repository) : base(repository)
		{
			LocalizationResource = typeof(GambleResource);
		}

		[Authorize(GamblePermissions.Category.Create)]
		public override Task<CategoryOutputDto> CreateAsync(CategoryCreateOrUpdateDto input)
		{
			return base.CreateAsync(input);
		}

		[Authorize(GamblePermissions.Category.Update)]
		public override Task<CategoryOutputDto> UpdateAsync(Guid id, CategoryCreateOrUpdateDto input)
		{
			return base.UpdateAsync(id, input);
		}

		[Authorize(GamblePermissions.Category.Delete)]
		public override Task DeleteAsync(Guid id)
		{
			return base.DeleteAsync(id);
		}
	}
}