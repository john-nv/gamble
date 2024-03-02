using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Chips;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
	public static class ChipMapping
	{
		public static void ConfigureChip(this ModelBuilder builder)
		{
			builder.Entity<Chip>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "Chips");
				entity.ConfigureByConvention();
			});
		}
	}
}
