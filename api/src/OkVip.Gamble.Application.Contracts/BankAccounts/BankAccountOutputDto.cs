using OkVip.Gamble.Banks;
using System;

namespace OkVip.Gamble.BankAccounts
{
	public class BankAccountOutputDto : BaseEntityDto<Guid>
	{
		public string No { get; set; }

		public string Name { get; set; }

		public string Branch { get; set; }

		public long BankId { get; set; }

		public BankOutputDto Bank { get; set; }
	}
}
