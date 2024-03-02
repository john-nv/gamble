using Localization.Resources.AbpUi;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OkVip.Gamble.EntityFrameworkCore;
using OkVip.Gamble.Localization;
using OkVip.Gamble.MultiTenancy;
using StackExchange.Redis;
using System;
using System.IO;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace OkVip.Gamble;

[DependsOn(
	typeof(AbpAutofacModule),
	//typeof(AbpCachingStackExchangeRedisModule),
	//typeof(AbpDistributedLockingModule),
	typeof(AbpAccountWebOpenIddictModule),
	typeof(AbpAccountApplicationModule),
	typeof(AbpAccountHttpApiModule),
	typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
	typeof(GambleEntityFrameworkCoreModule),
	typeof(AbpAspNetCoreSerilogModule)
	)]
public class GambleAuthServerModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		PreConfigure<OpenIddictServerBuilder>(builder =>
		{
			builder.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(30));
			builder.SetAccessTokenLifetime(TimeSpan.FromDays(30));
			builder.SetIdentityTokenLifetime(TimeSpan.FromDays(30));
			builder.SetRefreshTokenLifetime(TimeSpan.FromDays(30));
		});

		PreConfigure<OpenIddictBuilder>(builder =>
		{
			builder.AddValidation(options =>
			{
				options.AddAudiences("Gamble");
				options.UseLocalServer();
				options.UseAspNetCore();
			});

			builder.AddServer(options =>
			{
				options.UseAspNetCore().DisableTransportSecurityRequirement();
			});
		});
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Get<GambleResource>()
				.AddBaseTypes(
					typeof(AbpUiResource),
					typeof(AccountResource)
				);
		});

		Configure<AbpBundlingOptions>(options =>
		{
			options.StyleBundles.Configure(
				LeptonXLiteThemeBundles.Styles.Global,
				bundle =>
				{
					bundle.AddFiles("/global-styles.css");
				}
			);
		});

		Configure<AbpAuditingOptions>(options =>
		{
			//options.IsEnabledForGetRequests = true;
			options.ApplicationName = "AuthServer";
		});

		if (hostingEnvironment.IsDevelopment())
		{
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<GambleDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}OkVip.Gamble.Domain.Shared"));
				options.FileSets.ReplaceEmbeddedByPhysical<GambleDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}OkVip.Gamble.Domain"));
			});
		}

		Configure<AppUrlOptions>(options =>
		{
			options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
			options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

			options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
			options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
		});

		Configure<AbpBackgroundJobOptions>(options =>
		{
			options.IsJobExecutionEnabled = false;
		});

		Configure<AbpDistributedCacheOptions>(options =>
		{
			options.KeyPrefix = "Gamble:";
		});

		var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Gamble");
		if (!hostingEnvironment.IsDevelopment())
		{
			var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Gamble-Protection-Keys");
		}

		context.Services.AddSingleton<IDistributedLockProvider>(sp =>
		{
			var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
		});

		context.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder
					.AllowAnyOrigin()
					.WithAbpExposedHeaders()
					.SetIsOriginAllowedToAllowWildcardSubdomains()
					.AllowAnyHeader()
					.AllowAnyMethod();
			});
		});

		context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
		{
			options.IsDynamicClaimsEnabled = true;
		});
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var env = context.GetEnvironment();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseAbpRequestLocalization();

		if (!env.IsDevelopment())
		{
			app.UseErrorPage();
		}

		app.UseCorrelationId();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseCors();
		app.UseAuthentication();
		app.UseAbpOpenIddictValidation();

		if (MultiTenancyConsts.IsEnabled)
		{
			app.UseMultiTenancy();
		}

		app.UseUnitOfWork();
		app.UseDynamicClaims();
		app.UseAuthorization();

		app.UseAuditing();
		app.UseAbpSerilogEnrichers();
		app.UseConfiguredEndpoints();
	}
}