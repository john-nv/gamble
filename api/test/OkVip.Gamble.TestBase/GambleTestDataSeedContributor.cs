using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace OkVip.Gamble;

public class GambleTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
	public Task SeedAsync(DataSeedContext context)
	{
		/* Seed additional test data... */

		return Task.CompletedTask;
	}
}