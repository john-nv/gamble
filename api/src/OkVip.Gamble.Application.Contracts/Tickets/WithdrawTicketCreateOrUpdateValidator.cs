using FluentValidation;

namespace OkVip.Gamble.Tickets
{
    public class WithdrawTicketCreateOrUpdateValidator : AbstractValidator<WithdrawTicketCreateOrUpdateDto>
    {
        public WithdrawTicketCreateOrUpdateValidator()
        {
            RuleFor(e => e.Amount).NotEmpty().Must(e => e.Value > 0);
            RuleFor(e => e.WithdrawPassword).NotEmpty();
        }
    }
}
