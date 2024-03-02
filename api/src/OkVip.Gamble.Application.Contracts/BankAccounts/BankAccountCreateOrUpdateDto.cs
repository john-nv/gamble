namespace OkVip.Gamble.BankAccounts
{
	public class BankAccountCreateOrUpdateDto
	{
		public string No { get; set; }

		public string Name { get; set; }

		public string Branch { get; set; }

		public long BankId { get; set; }
	}
}