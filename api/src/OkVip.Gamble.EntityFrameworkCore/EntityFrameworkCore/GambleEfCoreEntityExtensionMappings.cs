using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.IdentityUsers;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace OkVip.Gamble.EntityFrameworkCore;

public static class GambleEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        GambleGlobalFeatureConfigurator.Configure();
        GambleModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                  .MapEfCoreProperty<IdentityUser, decimal>(nameof(ICustomizeIdentityUser.AccountBalance), (entityBuilder, propertyBuilder) =>
                  {
                      propertyBuilder.HasDefaultValue(0);
                  })
                  .MapEfCoreProperty<IdentityUser, string>(nameof(ICustomizeIdentityUser.WithdrawPassword));
        });
    }
}