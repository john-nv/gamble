using Volo.Abp.Modularity;

namespace OkVip.Gamble;

/* Inherit from this class for your domain layer tests. */

public abstract class GambleDomainTestBase<TStartupModule> : GambleTestBase<TStartupModule>
	where TStartupModule : IAbpModule
{
}