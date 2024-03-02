using AutoMapper;
using Volo.Abp.Data;

namespace OkVip.Gamble
{
	public static class AutoMapperExtensions
	{
		public static IMappingExpression<TSource, TDest> MapExtraPropertiesToRegularProperties<TSource, TDest>(this IMappingExpression<TSource, TDest> expression) where TDest : IHasExtraProperties where TSource : IHasExtraProperties
		{
			expression.AfterMap((source, destination, context) => destination.SetExtraPropertiesToRegularProperties());
			return expression;
		}
	}
}