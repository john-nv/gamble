using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Tickets;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
    public static class TicketMapping
    {
        public static void ConfigureTicket(this ModelBuilder builder)
        {
            builder.Entity<Ticket>(entity =>
            {
                entity.ToTable(GambleDbProperties.DbTablePrefix + "Tickets");
                entity.ConfigureByConvention();
            });

            builder.Entity<TicketLog>(entity =>
            {
                entity.ToTable(GambleDbProperties.DbTablePrefix + "TicketLogs");
                entity.ConfigureByConvention();
            });
        }
    }
}
