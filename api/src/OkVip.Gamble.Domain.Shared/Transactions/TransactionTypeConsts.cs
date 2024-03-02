using System.Collections.Generic;

namespace OkVip.Gamble.Transactions
{
    public static class TransactionTypeConsts
    {
        public const string Deposit = "Deposit";

        public const string Revoke = "Revoke";

        public const string Withdraw = "Withdraw";

        public const string Betting = "Betting";

        public const string Refund = "Refund";

        public const string Pay = "Pay";

        public static Dictionary<string, string> Translate => new Dictionary<string, string>
        {
            { Deposit, "Nạp tiền" },
            { Withdraw, "Rút tiền" },
            { Betting, "Đặt cược" },
            { Refund, "Hoàn trả" },
            { Pay, "Thanh toán" },
            { Revoke, "Thu hồi" },
        };
    }
}
