using OkVip.Gamble.Games;
using System;

namespace OkVip.Gamble.Rounds
{
	public class RoundDetail : BaseEntity<Guid>
	{
		public Guid RoundId { get; set; }

		public Guid UserId { get; set; }

		public Guid GameId { get; set; }

		public string Status { get; set; }

		public decimal AmountEarned { get; set; }

		public decimal Rate { get; set; } = RateEarnConsts.Default;

		public string? Note { get; set; }

		public decimal TotalBetAmount { get; set; }

		public virtual Round Round { get; set; }

		public virtual Game Game { get; set; }
	}
}