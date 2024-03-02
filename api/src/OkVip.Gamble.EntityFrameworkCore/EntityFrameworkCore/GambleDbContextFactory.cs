using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace OkVip.Gamble.EntityFrameworkCore;

public class GambleDbContextFactory : IDesignTimeDbContextFactory<GambleDbContext>
{
	public GambleDbContext CreateDbContext(string[] args)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		GambleEfCoreEntityExtensionMappings.Configure();

		var configuration = BuildConfiguration();

		var builder = new DbContextOptionsBuilder<GambleDbContext>()
			.UseNpgsql(configuration.GetConnectionString("Default"));

		return new GambleDbContext(builder.Options);
	}

	private static IConfigurationRoot BuildConfiguration()
	{
		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		var builder = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OkVip.Gamble.HttpApi.Host/"))
			.AddJsonFile("appsettings.json", optional: false);

		if (!string.IsNullOrEmpty(environment))
		{
			builder.AddJsonFile($"appsettings.{environment}.json", optional: true);
		}

		return builder.Build();
	}
}