using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Rounds;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
	public static class RoundMapping
	{
		public static void ConfigureRound(this ModelBuilder builder)
		{
			builder.Entity<Round>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "Rounds");
				entity.ConfigureByConvention();
				entity.HasIndex(e => e.Code);
				entity.HasIndex(e => e.Status);
			});

			builder.Entity<RoundDetail>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "RoundDetails");

				entity.ConfigureByConvention();
				entity.Property(e => e.Rate).HasDefaultValue(RateEarnConsts.Default);
				entity.HasIndex(e => e.Status);
			});

			builder.Entity<RoundCalculation>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "RoundCalculations");
				entity.ConfigureByConvention();
			});
		}
	}
}
