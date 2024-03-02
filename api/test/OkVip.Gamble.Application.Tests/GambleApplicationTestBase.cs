using Volo.Abp.Modularity;

namespace OkVip.Gamble;

public abstract class GambleApplicationTestBase<TStartupModule> : GambleTestBase<TStartupModule>
	where TStartupModule : IAbpModule
{
}