using Volo.Abp.Application.Dtos;

namespace OkVip.Gamble.Banks
{
	public class BankGetListInput : PagedAndSortedResultRequestDto
	{
		public bool TransferSupported { get; set; } = true;
	}
}
