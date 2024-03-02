using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OkVip.Gamble.Games
{
	public class GameTermValue
	{
		private List<int> _range;

		private List<int> _minMax;

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
	}
}