using System;

namespace OkVip.Gamble.Transactions
{
    public class TransactionOutputDto : BaseEntityDto<Guid>
    {
        public decimal Amount { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public string TypeName { get; set; } = string.Empty;
    }
}