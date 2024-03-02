using Volo.Abp.Modularity;

namespace OkVip.Gamble;

[DependsOn(
	typeof(GambleApplicationModule),
	typeof(GambleDomainTestModule)
)]
public class GambleApplicationTestModule : AbpModule
{
}