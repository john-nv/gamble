using Localization.Resources.AbpUi;
using OkVip.Gamble.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace OkVip.Gamble;

[DependsOn(
	typeof(GambleApplicationContractsModule),
	typeof(AbpAccountHttpApiModule),
	typeof(AbpIdentityHttpApiModule),
	typeof(AbpPermissionManagementHttpApiModule),
	typeof(AbpTenantManagementHttpApiModule),
	typeof(AbpFeatureManagementHttpApiModule),
	typeof(AbpSettingManagementHttpApiModule)
	)]
public class GambleHttpApiModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ConfigureLocalization();
	}

	private void ConfigureLocalization()
	{
		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Get<GambleResource>()
				.AddBaseTypes(
					typeof(AbpUiResource)
				);
		});
	}
}