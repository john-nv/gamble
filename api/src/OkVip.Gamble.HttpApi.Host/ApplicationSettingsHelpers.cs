using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace OkVip.Gamble
{
	public static class ApplicationSettingsHelpers
	{
		public static void Configure(ILogger logger = null)
		{
			Configure(new ConfigurationBuilder(), logger);
		}

		public static void Configure(IConfigurationBuilder configurationBuilder, ILogger logger = null)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower()?.Trim();

			configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
								.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
								.AddEnvironmentVariables();

			if (!string.IsNullOrEmpty(environment) && File.Exists(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environment}.json")))
			{
				configurationBuilder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
			}

			configurationBuilder.Build();

			if (logger != null)
			{
				logger.Information($"Environment: {environment}");
			}
		}
	}
}