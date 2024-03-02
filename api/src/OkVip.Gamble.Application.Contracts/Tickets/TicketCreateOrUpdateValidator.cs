using FluentValidation;

namespace OkVip.Gamble.Tickets
{
    public class TicketCreateOrUpdateValidator : AbstractValidator<TicketCreateOrUpdateDto>
    {
        public TicketCreateOrUpdateValidator()
        {
            RuleFor(e => e.TicketType).NotEmpty();
            RuleFor(e => e.Title).NotEmpty();
        }
    }
}
