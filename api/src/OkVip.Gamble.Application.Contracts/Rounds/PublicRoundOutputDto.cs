using System;

namespace OkVip.Gamble.Rounds
{
	public class PublicRoundOutputDto : BaseEntityDto<Guid>
	{
		public string Code { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public string Status { get; set; }
	}
}
