using System;

namespace OkVip.Gamble.Categories
{
	public class CategoryOutputDto : BaseEntityDto<Guid>
	{
		public string Name { get; set; }

		public string? Description { get; set; }
	}
}
