using OkVip.Gamble.Games;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OkVip.Gamble.Rounds
{
	public class Round : BaseEntity<Guid>
	{
		private ICollection<RoundDetail> _roundDetails;

		public string Code { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public string Status { get; set; }

		public Guid GameId { get; set; }

		public virtual Game Game { get; set; }

		public virtual ICollection<RoundDetail> RoundDetails
		{
			get => _roundDetails ??= new List<RoundDetail>();
			set => _roundDetails = value;
		}

		public string GetResult()
		{
			return this.ExtraProperties["result"]?.ToString();
		}

		public void SetResult(string result)
		{
			this.ExtraProperties["blockResults"] = result.ToString().Chunk(2).Select(e => string.Join(string.Empty, e));
			this.ExtraProperties["result"] = result;
		}

		public void GenerateResult()
		{
			SetResult(this.CreateRandom(20));
		}

		public bool IsLive(DateTime currentNow)
		{
			return currentNow >= this.StartTime && currentNow <= this.EndTime;
		}
	}
}