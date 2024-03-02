using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Categories;

namespace OkVip.Gamble.EntityFrameworkCore.Mappings
{
    public static class CategoryMapping
    {
        public static void ConfigureCategory(this ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity.ToTable(GambleDbProperties.DbTablePrefix + "Categories");
            });
        }
    }
}
