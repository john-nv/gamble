using System;

namespace OkVip.Gamble.Transactions
{
    public class Transaction : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string StatusName
        {
            get
            {
                if (string.IsNullOrEmpty(Status))
                {
                    return string.Empty;
                }

                return TransactionStatusConsts.Translate[Status];
            }
        }

        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(Type))
                {
                    return string.Empty;
                }

                return TransactionTypeConsts.Translate[Type];
            }
        }

        public Transaction()
        {
        }

        public Transaction(Guid id)
        {
            Id = id;
        }
    }
}