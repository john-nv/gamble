using FluentValidation;

namespace OkVip.Gamble.Transactions
{
    public class TransactionDepositCreateOrUpdateValidator : AbstractValidator<TransactionDepositCreateOrUpdateDto>
    {
        public TransactionDepositCreateOrUpdateValidator()
        {
            RuleFor(e => e.UserId).NotEmpty();
            RuleFor(e => e.Amount).GreaterThan(0);
        }
    }
}
