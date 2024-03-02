using FluentValidation;

namespace OkVip.Gamble.Categories
{
	public class CategoryCreateOrUpdateValidator : AbstractValidator<CategoryCreateOrUpdateDto>
	{
		public CategoryCreateOrUpdateValidator()
		{
			RuleFor(e => e.Name).NotEmpty();
		}
	}
}
