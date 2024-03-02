using Volo.Abp.Modularity;

namespace OkVip.Gamble;

[DependsOn(
	typeof(GambleDomainModule),
	typeof(GambleTestBaseModule)
)]
public class GambleDomainTestModule : AbpModule
{
}