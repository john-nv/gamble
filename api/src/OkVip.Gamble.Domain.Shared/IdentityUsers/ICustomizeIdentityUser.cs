namespace OkVip.Gamble.IdentityUsers
{
    public interface ICustomizeIdentityUser
    {
        string AccountBalance { get; set; }

        string WithdrawPassword { get; set; }
    }
}
