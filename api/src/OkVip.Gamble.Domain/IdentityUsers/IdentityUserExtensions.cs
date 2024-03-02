using Volo.Abp.Identity;

namespace OkVip.Gamble.IdentityUsers
{
    public static class IdentityUserExtensions
    {
        public static void SetAccountBalance(this IdentityUser identityUser, decimal amount)
        {
            identityUser.ExtraProperties[nameof(ICustomizeIdentityUser.AccountBalance)] = amount;
        }

        public static void SetWithdrawPassword(this IdentityUser identityUser, string password)
        {
            identityUser.ExtraProperties[nameof(ICustomizeIdentityUser.WithdrawPassword)] = password;
        }

        public static decimal GetAccountBalance(this IdentityUser identityUser)
        {
            if (!identityUser.ExtraProperties.ContainsKey(nameof(ICustomizeIdentityUser.AccountBalance)))
                return 0;

            return decimal.Parse(identityUser.ExtraProperties[nameof(ICustomizeIdentityUser.AccountBalance)].ToString());
        }

        public static string GetWithdrawPassword(this IdentityUser identityUser)
        {
            if (!identityUser.ExtraProperties.ContainsKey(nameof(ICustomizeIdentityUser.WithdrawPassword)))
                return string.Empty;

            return identityUser.ExtraProperties[nameof(ICustomizeIdentityUser.WithdrawPassword)].ToString();
        }
    }
}
