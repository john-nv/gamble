using OkVip.Gamble.Categories;
using OkVip.Gamble.Rounds;
using System;
using System.Collections.Generic;

namespace OkVip.Gamble.Games
{
	public class Game : BaseEntity<Guid>
	{
		private ICollection<Round> _rounds;

		private ICollection<RoundDetail> _roundDetails;

		private readonly ICollection<GameLock> _gameLocks;

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool IsActive { get; set; }

		public Guid? CurrentRoundId { get; set; }

		public Guid CategoryId { get; set; }

		public virtual Category Category { get; set; }

		public virtual Round CurrentRound { get; set; }

		public virtual ICollection<Round> Rounds
		{
			get => _rounds ??= new List<Round>();
			set => _rounds = value;
		}

		public virtual ICollection<RoundDetail> RoundDetails
		{
			get => _roundDetails ??= new List<RoundDetail>();
			set => _roundDetails = value;
		}
	}
}