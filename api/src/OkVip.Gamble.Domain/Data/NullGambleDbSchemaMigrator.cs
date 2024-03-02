using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OkVip.Gamble.Data;

/* This is used if database provider does't define
 * IGambleDbSchemaMigrator implementation.
 */

public class NullGambleDbSchemaMigrator : IGambleDbSchemaMigrator, ITransientDependency
{
	public Task MigrateAsync()
	{
		return Task.CompletedTask;
	}
}