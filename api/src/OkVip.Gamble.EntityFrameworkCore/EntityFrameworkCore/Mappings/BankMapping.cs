using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Banks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
	public static class BankMapping
	{
		public static void ConfigureBank(this ModelBuilder builder)
		{
			builder.Entity<Bank>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "Banks");
				entity.ConfigureByConvention();
			});
		}
	}
}
