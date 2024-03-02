using System;

namespace OkVip.Gamble.Transactions
{
    public class TransactionDepositCreateOrUpdateDto
    {
        public Guid? UserId { get; set; }

        public decimal Amount { get; set; }

        public bool IsRevoke { get; set; }
    }
}
