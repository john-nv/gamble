using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Games;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
	public static class GameMapping
	{
		public static void ConfigureGame(this ModelBuilder builder)
		{
			builder.Entity<Game>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "Games");

				entity.HasOne(e => e.CurrentRound)
					  .WithMany()
					  .HasForeignKey(e => e.CurrentRoundId);

				entity.ConfigureByConvention();
			});

			builder.Entity<GameLock>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "GameLocks");
				entity.ConfigureByConvention();
			});
		}
	}
}
