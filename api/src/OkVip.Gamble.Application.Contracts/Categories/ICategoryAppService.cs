using System;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Categories
{
	public interface ICategoryAppService : ICrudAppService<CategoryOutputDto, Guid, CategoryGetListInputDto, CategoryCreateOrUpdateDto>
	{
	}
}
