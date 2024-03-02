using Volo.Abp.Data;

namespace OkVip.Gamble.Rounds
{
	public class RoundDetailCreateDto
	{
		private ExtraPropertyDictionary extraProperties;

		public decimal TotalBetAmount { get; set; }

		public ExtraPropertyDictionary ExtraProperties
		{
			get => extraProperties ??= new ExtraPropertyDictionary();
			set => extraProperties = value;
		}
	}
}
