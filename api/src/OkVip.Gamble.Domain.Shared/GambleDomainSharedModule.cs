using OkVip.Gamble.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace OkVip.Gamble;

[DependsOn(
	typeof(AbpAuditLoggingDomainSharedModule),
	typeof(AbpBackgroundJobsDomainSharedModule),
	typeof(AbpFeatureManagementDomainSharedModule),
	typeof(AbpIdentityDomainSharedModule),
	typeof(AbpOpenIddictDomainSharedModule),
	typeof(AbpPermissionManagementDomainSharedModule),
	typeof(AbpSettingManagementDomainSharedModule),
	typeof(AbpTenantManagementDomainSharedModule)
	)]
public class GambleDomainSharedModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		GambleGlobalFeatureConfigurator.Configure();
		GambleModuleExtensionConfigurator.Configure();
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<GambleDomainSharedModule>();
		});

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Add<GambleResource>("vi")
				.AddBaseTypes(typeof(AbpValidationResource))
				.AddVirtualJson("/Localization/Gamble");

			options.DefaultResourceType = typeof(GambleResource);
		});

		Configure<AbpExceptionLocalizationOptions>(options =>
		{
			options.MapCodeNamespace("Gamble", typeof(GambleResource));
		});
	}
}