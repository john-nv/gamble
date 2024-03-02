using OkVip.Gamble.IdentityUsers;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace OkVip.Gamble;

public static class GambleModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {

    }

    private static void ConfigureExtraProperties()
    {
        ObjectExtensionManager.Instance.Modules()
             .ConfigureIdentity(identity =>
             {
                 identity.ConfigureUser(user =>
                 {
                     user.AddOrUpdateProperty<decimal>(nameof(ICustomizeIdentityUser.AccountBalance), configure =>
                     {
                         configure.DefaultValue = 0;
                     });

                     user.AddOrUpdateProperty<string>(nameof(ICustomizeIdentityUser.WithdrawPassword));
                 });
             });
    }
}