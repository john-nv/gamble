using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Accounts;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
	public static class AccountMapping
	{
		public static void ConfigureAccount(this ModelBuilder builder)
		{
			builder.Entity<Account>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "Accounts");
				entity.ConfigureByConvention();
			});
		}
	}
}
