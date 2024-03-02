using FluentValidation;

namespace OkVip.Gamble.Tickets
{
    public class TicketRejectInputValidator : AbstractValidator<TicketRejectInputDto>
    {
        public TicketRejectInputValidator()
        {
            RuleFor(e => e.Note).NotEmpty();
        }
    }
}
