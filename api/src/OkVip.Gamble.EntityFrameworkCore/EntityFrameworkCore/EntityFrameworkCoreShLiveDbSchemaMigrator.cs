using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OkVip.Gamble.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OkVip.Gamble.EntityFrameworkCore;

public class EntityFrameworkCoreGambleDbSchemaMigrator
	: IGambleDbSchemaMigrator, ITransientDependency
{
	private readonly IServiceProvider _serviceProvider;

	public EntityFrameworkCoreGambleDbSchemaMigrator(
		IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task MigrateAsync()
	{
		/* We intentionally resolve the GambleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

		await _serviceProvider
			.GetRequiredService<GambleDbContext>()
			.Database
			.MigrateAsync();
	}
}