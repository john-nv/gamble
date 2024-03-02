using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OkVip.Gamble.EntityFrameworkCore;
using OkVip.Gamble.MultiTenancy;
using OkVip.Gamble.Workers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Timing;

namespace OkVip.Gamble;

[DependsOn(
	typeof(AbpAutofacModule),
	typeof(GambleApplicationModule),
	typeof(AbpCachingStackExchangeRedisModule),
	typeof(AbpDistributedLockingModule),
	typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
	typeof(GambleEntityFrameworkCoreModule),
	typeof(AbpAspNetCoreSerilogModule),
	typeof(AbpSwashbuckleModule),
	typeof(AbpBackgroundWorkersModule),
	typeof(AbpBackgroundJobsModule),
	typeof(AbpHttpClientModule),
	typeof(AbpAspNetCoreSignalRModule)
)]
public class GambleLuckySpaceshipHostModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();
		var hostingEnvironment = context.Services.GetHostingEnvironment();

		ConfigureConventionalControllers();
		ConfigureAuthentication(context, configuration);
		ConfigureCache(configuration);
		ConfigureVirtualFileSystem(context);
		ConfigureDataProtection(context, configuration, hostingEnvironment);
		ConfigureDistributedLocking(context, configuration);
		ConfigureCors(context, configuration);
		ConfigureSwaggerServices(context, configuration);
		ConfigureTimezone();
		ConfigureModelFilters(context, configuration);
		ConfigureBackgroundJob();
	}

	public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
	{
		await base.OnApplicationInitializationAsync(context);

		await context.AddBackgroundWorkerAsync<LuckySpaceshipResultCalculationWorker>();
		await context.AddBackgroundWorkerAsync<LuckySpaceshipRoundInitializeWorker>();
		await context.AddBackgroundWorkerAsync<LuckySpaceshipCalculateWinnerOrLoserWorker>();
	}

	public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
	{
		await context.ServiceProvider
					 .GetRequiredService<GambleDbContext>()
					 .Database
					 .MigrateAsync();
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
		app.UseCorrelationId();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseCors();
		app.UseAuthentication();

		if (MultiTenancyConsts.IsEnabled)
		{
			app.UseMultiTenancy();
		}

		app.UseUnitOfWork();
		app.UseDynamicClaims();
		app.UseAuthorization();

		app.UseSwagger();
		app.UseAbpSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gamble API");

			var configuration = context.GetConfiguration();
			options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
			options.OAuthScopes("Gamble");
		});

		app.UseAuditing();
		app.UseAbpSerilogEnrichers();
		app.UseConfiguredEndpoints();
	}


	private void ConfigureBackgroundJob()
	{
		Configure<AbpBackgroundJobWorkerOptions>(options =>
		{
			options.DefaultTimeout = 864000;
			options.JobPollPeriod = 1000;
		});
	}
	private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
	{
		context.Services.AddAbpSwaggerGenWithOAuth(
			configuration["AuthServer:Authority"]!,
			new Dictionary<string, string>
			{
					{"Gamble", "Gamble API"}
			},
			options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gamble API", Version = "v1" });
				options.DocInclusionPredicate((docName, description) => true);
				options.CustomSchemaIds(type => type.FullName);
			});
	}

	private void ConfigureModelFilters(ServiceConfigurationContext context, IConfiguration configuration)
	{
		//context.Services.Configure<ModelFilterOptions>(configuration.GetSection("RequestOptions"));
	}

	private void ConfigureCache(IConfiguration configuration)
	{
		Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Gamble:"; });
	}

	private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();
	}

	private void ConfigureTimezone()
	{
		Configure<AbpClockOptions>(options =>
		{
			options.Kind = DateTimeKind.Utc;
		});
	}

	private void ConfigureConventionalControllers()
	{
		Configure<AbpAspNetCoreMvcOptions>(options =>
		{
			options.ConventionalControllers.Create(typeof(GambleApplicationModule).Assembly, actions =>
			{
				actions.RootPath = "gamble";
			});
		});
	}

	private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
	{
		context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.Authority = configuration["AuthServer:Authority"];
				options.RequireHttpsMetadata = configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata");
				options.Audience = "Gamble";
			});

		context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
		{
			options.IsDynamicClaimsEnabled = true;
		});
	}

	private void ConfigureDataProtection(
		ServiceConfigurationContext context,
		IConfiguration configuration,
		IWebHostEnvironment hostingEnvironment)
	{
		var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Gamble");
		if (!hostingEnvironment.IsDevelopment())
		{
			var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Gamble-Protection-Keys");
		}
	}

	private void ConfigureDistributedLocking(
		ServiceConfigurationContext context,
		IConfiguration configuration)
	{
		context.Services.AddSingleton<IDistributedLockProvider>(sp =>
		{
			var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
			return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
		});
	}

	private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
	{
		context.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder
					.WithOrigins(configuration["App:CorsOrigins"]!.Split(","))
					.WithAbpExposedHeaders()
					.SetIsOriginAllowedToAllowWildcardSubdomains()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
		});
	}
}