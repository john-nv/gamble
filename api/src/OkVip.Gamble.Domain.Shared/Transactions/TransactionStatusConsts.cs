using System.Collections.Generic;

namespace OkVip.Gamble.Transactions
{
    public static class TransactionStatusConsts
    {
        public const string Success = "Success";

        public const string Failed = "Failed";

        public const string Pending = "Pending";

        public static Dictionary<string, string> Translate => new Dictionary<string, string>
        {
            { Success, "Thành công" },
            { Failed, "Thất bại" },
            { Pending, "Đang xử lý" }
        };
    }
}
