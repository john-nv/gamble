namespace OkVip.Gamble.Banks
{
	public class Bank : BaseEntity<long>
	{
		public string Name { get; set; } = string.Empty;

		public string Code { get; set; } = string.Empty;

		public string ShortName { get; set; } = string.Empty;

		public string Bin { get; set; } = string.Empty;

		public string Logo { get; set; } = string.Empty;

		public bool TransferSupported { get; set; }

		public bool LookupSupported { get; set; }

		public string SwiftCode { get; set; } = string.Empty;
	}
}