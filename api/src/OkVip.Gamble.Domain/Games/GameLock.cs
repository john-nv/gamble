using System;

namespace OkVip.Gamble.Games
{
	public class GameLock : BaseEntity<Guid>
	{
		public Guid GameId { get; set; }

		public DateTime From { get; set; }

		public DateTime To { get; set; }

		public virtual Game Game { get; set; }
	}
}