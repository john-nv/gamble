using FluentValidation;

namespace OkVip.Gamble.IdentityUsers
{
    public class UpdateWithdrawPasswordInputValidator : AbstractValidator<UpdateWithdrawPasswordInputDto>
    {
        public UpdateWithdrawPasswordInputValidator()
        {
            RuleFor(e => e.SigninPassword).NotEmpty();
            RuleFor(e => e.WithdrawPassword).NotEmpty();
        }
    }
}
