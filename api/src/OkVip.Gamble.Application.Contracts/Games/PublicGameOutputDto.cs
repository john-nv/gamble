using OkVip.Gamble.Rounds;
using System;

namespace OkVip.Gamble.Games
{
	public class PublicGameOutputDto : BaseEntityDto<Guid>
	{
		public string Name { get; set; }

		public string? Description { get; set; }

		public bool IsActive { get; set; }

		public Guid CategoryId { get; set; }

		public Guid? CurrentRoundId { get; set; }

		public PublicRoundOutputDto CurrentRound { get; set; }
	}
}