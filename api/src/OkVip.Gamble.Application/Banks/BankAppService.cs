using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.Banks
{
	[Authorize]
	public class BankAppService : CrudAppService<Bank, BankOutputDto, long, BankGetListInput, BankCreateOrUpdateDto>
	{
		public BankAppService(IRepository<Bank, long> repository) : base(repository)
		{

		}

		protected override async Task<IQueryable<Bank>> CreateFilteredQueryAsync(BankGetListInput input)
		{
			var queryable = await base.CreateFilteredQueryAsync(input);
			return queryable.Where(e => e.TransferSupported == input.TransferSupported);
		}

		protected override IQueryable<Bank> ApplyDefaultSorting(IQueryable<Bank> query)
		{
			return query.OrderBy(e => e.Name);
		}

		protected override IQueryable<Bank> ApplyPaging(IQueryable<Bank> query, BankGetListInput input)
		{
			return query;
		}
	}
}
