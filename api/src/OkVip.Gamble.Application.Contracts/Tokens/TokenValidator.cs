using FluentValidation;

namespace OkVip.Gamble.Tokens
{
	public class TokenValidator : AbstractValidator<TokenInputDto>
	{
		public TokenValidator()
		{
			RuleFor(e => e.Username)
				.NotEmpty();

			RuleFor(e => e.Password)
				.NotEmpty();
		}
	}
}
