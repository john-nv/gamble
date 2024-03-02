using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.BankAccounts
{
    [Authorize]
    public class BankAccountAppService : CrudAppService<Account, BankAccountOutputDto, Guid, BankAccountGetListInputDto, BankAccountCreateOrUpdateDto>, IBankAccountAppService
    {
        public BankAccountAppService(IRepository<Account, Guid> repository) : base(repository)
        {
        }

        public virtual async Task<IList<BankAccountOutputDto>> GetMyAccountsAsync()
        {
            var queryable = await Repository.WithDetailsAsync(e => e.Bank);

            var accounts = queryable.Where(e => e.CreatorId == CurrentUser.Id);
            return await MapToGetListOutputDtosAsync(accounts.ToList());
        }

        protected override IQueryable<Account> ApplyPaging(IQueryable<Account> query, BankAccountGetListInputDto input)
        {
            return query;
        }

        protected override async Task<IQueryable<Account>> CreateFilteredQueryAsync(BankAccountGetListInputDto input)
        {
            var queryable = await base.CreateFilteredQueryAsync(input);
            return queryable.WhereIf(input.CreatorId.HasValue, e => e.CreatorId == input.CreatorId);
        }
    }
}