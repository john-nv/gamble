using OkVip.Gamble.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace OkVip.Gamble.DbMigrator;

[DependsOn(
	typeof(AbpAutofacModule),
	typeof(AbpCachingStackExchangeRedisModule),
	typeof(GambleEntityFrameworkCoreModule),
	typeof(GambleApplicationContractsModule)
	)]
public class GambleDbMigratorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Gamble:"; });
	}
}