using AutoMapper;
using OkVip.Gamble.Categories;

namespace OkVip.Gamble.Mappers
{
	public class CategoryMapper : Profile
	{
		public CategoryMapper()
		{
			CreateMap<Category, CategoryOutputDto>();
			CreateMap<CategoryCreateOrUpdateDto, Category>();
		}
	}
}
