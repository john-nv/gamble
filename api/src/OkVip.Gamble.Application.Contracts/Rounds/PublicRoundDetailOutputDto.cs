using System;

namespace OkVip.Gamble.Rounds
{
	public class PublicRoundDetailOutputDto : BaseEntityDto<Guid>
	{
		public string Status { get; set; }

		public decimal AmountEarned { get; set; }
	}
}
