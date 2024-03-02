using System.Collections.Generic;

namespace OkVip.Gamble.Tickets
{
    public static class TicketTypeConsts
    {
        public const string Withdraw = "Withdraw";

        public static Dictionary<string, string> Translate => new Dictionary<string, string>
        {
            { Withdraw, "Đơn rút tiền"}
        };
    }
}
