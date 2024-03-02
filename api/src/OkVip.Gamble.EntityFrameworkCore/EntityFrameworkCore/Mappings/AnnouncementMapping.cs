using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Announcements;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
	public static class AnnouncementMapping
	{
		public static void ConfigureAnnouncement(this ModelBuilder builder)
		{
			builder.Entity<Announcement>(entity =>
			{
				entity.ToTable(GambleDbProperties.DbTablePrefix + "Announcements");
				entity.ConfigureByConvention();
			});
		}
	}
}
