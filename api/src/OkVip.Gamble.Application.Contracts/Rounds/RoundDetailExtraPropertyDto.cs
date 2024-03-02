using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OkVip.Gamble.Rounds
{
	public class RoundDetailExtraPropertyDto
	{
		private List<BetItem> _bets;

		[JsonPropertyName("bet")]
		public List<BetItem> Bets
		{
			get => _bets ??= new List<BetItem>();
			set => _bets = value;
		}

		[JsonPropertyName("gameId")]
		public string GameId { get; set; }

		[JsonPropertyName("roundId")]
		public string RoundId { get; set; }

		[JsonPropertyName("totalBetAmount")]
		public decimal TotalBetAmount { get; set; }
	}

	public class BetItem
	{
		private List<int> _range;
		private List<int> _minMax;

		[JsonPropertyName("term")]
		public string Term { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("range")]
		public List<int> Range
		{
			get => _range ??= new List<int>();
			set => _range = value;
		}

		[JsonPropertyName("minMax")]
		public List<int> MinMax
		{
			get => _minMax ??= new List<int>();
			set => _minMax = value;
		}

		[JsonPropertyName("rate")]
		public decimal Rate { get; set; }

		[JsonPropertyName("block")]
		public Block Block { get; set; }

		[JsonPropertyName("chip")]
		public Chip Chip { get; set; }

		[JsonPropertyName("betAmount")]
		public decimal BetAmount { get; set; }
	}

	public class Block
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}

	public class Chip
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("amount")]
		public int Amount { get; set; }
	}
}