using System.Collections.Generic;

namespace OkVip.Gamble.Tickets
{
    public static class TicketStatusConsts
    {
        public const string New = "New";

        public const string Approved = "Approved";

        public const string Rejected = "Rejected";

        public static Dictionary<string, string> Translate => new Dictionary<string, string>
        {
            { New, "Gởi yêu cầu"},
            { Approved, "Đã duyệt"},
            { Rejected, "Từ chối"}
        };
    }
}