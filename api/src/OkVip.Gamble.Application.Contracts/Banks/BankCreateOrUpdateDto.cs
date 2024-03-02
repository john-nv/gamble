namespace OkVip.Gamble.Banks
{
	public class BankCreateOrUpdateDto
	{
		public string Name { get; set; }

		public string Code { get; set; }

		public string ShortName { get; set; }

		public string Bin { get; set; }

		public string Logo { get; set; }

		public bool TransferSupported { get; set; }

		public bool LookupSupported { get; set; }

		public string SwiftCode { get; set; }
	}
}
