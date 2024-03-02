using FluentValidation;

namespace OkVip.Gamble.IdentityUsers
{
    public class CreateWithdrawPasswordInputValidator : AbstractValidator<CreateWithdrawPasswordInputDto>
    {
        public CreateWithdrawPasswordInputValidator()
        {
            RuleFor(e => e.WithdrawPassword).NotEmpty();
        }
    }
}
