using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Transactions;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
    public static class TransactionMapping
    {
        public static void ConfigureTransaction(this ModelBuilder builder)
        {
            builder.Entity<Transaction>(entity =>
            {
                entity.ToTable(GambleDbProperties.DbTablePrefix + "Transactions");
                entity.ConfigureByConvention();
                entity.Ignore(e => e.StatusName);
                entity.Ignore(e => e.TypeName);
                entity.HasIndex(e => e.Status);
            });
        }
    }
}
