using System;

namespace OkVip.Gamble.Rounds
{
	public class RoundCalculation : BaseEntity<Guid>
	{
		public Guid GameId { get; set; }

		public Guid RoundId { get; set; }
	}
}
