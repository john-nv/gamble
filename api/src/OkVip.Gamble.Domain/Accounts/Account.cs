using OkVip.Gamble.Banks;
using System;

namespace OkVip.Gamble.Accounts
{
	public class Account : BaseEntity<Guid>
	{
		public string No { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public string Branch { get; set; } = string.Empty;

		public long BankId { get; set; }

		public virtual Bank Bank { get; set; }
	}
}