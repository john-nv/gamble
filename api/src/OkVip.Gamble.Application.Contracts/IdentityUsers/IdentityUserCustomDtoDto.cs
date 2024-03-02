using Volo.Abp.Identity;

namespace OkVip.Gamble.IdentityUsers
{
    public class IdentityUserCustomDto : IdentityUserDto
    {
        public decimal AccountBalance { get; set; }

        public bool HasWithdrawPassword { get; set; }

        public bool AllowCreateTicket { get; set; }
    }
}