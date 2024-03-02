using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OkVip.Gamble.EntityFrameworkCore
{
	public static class ModelBuilderExtensions
	{
		public static void ToTableName<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string name) where TEntity : class
		{
			entityTypeBuilder.ToTable(GambleDbProperties.DbTablePrefix + name);
		}
	}
}