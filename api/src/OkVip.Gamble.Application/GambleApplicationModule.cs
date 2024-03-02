using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace OkVip.Gamble;

[DependsOn(
	typeof(GambleDomainModule),
	typeof(AbpAccountApplicationModule),
	typeof(GambleApplicationContractsModule),
	typeof(AbpIdentityApplicationModule),
	typeof(AbpPermissionManagementApplicationModule),
	typeof(AbpTenantManagementApplicationModule),
	typeof(AbpFeatureManagementApplicationModule),
	typeof(AbpSettingManagementApplicationModule),
	typeof(AbpHttpClientModule)
)]
public class GambleApplicationModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddMaps<GambleApplicationModule>();
		});
	}
}