using System;
using Volo.Abp.Data;

namespace OkVip.Gamble.Games
{
	public class GameOutputDto : BaseEntityDto<Guid>, IHasExtraProperties
	{
		public string Name { get; set; }

		public string? Description { get; set; }

		public bool IsActive { get; set; }

		public Guid CategoryId { get; set; }
	}
}
