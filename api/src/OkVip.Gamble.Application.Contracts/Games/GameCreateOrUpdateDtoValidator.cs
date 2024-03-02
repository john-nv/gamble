using FluentValidation;

namespace OkVip.Gamble.Games
{
	public class GameCreateOrUpdateDtoValidator : AbstractValidator<GameCreateOrUpdateDto>
	{
		public GameCreateOrUpdateDtoValidator()
		{
			RuleFor(e => e.Name).NotEmpty();
			RuleFor(e => e.CategoryId).NotEmpty();
		}
	}
}