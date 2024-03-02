using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Permissions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace OkVip.Gamble.Transactions
{
    [Authorize]
    public class TransactionAppService : ReadOnlyAppService<Transaction, TransactionOutputDto, Guid, TransactionGetListInputDto>, ITransactionAppService
    {
        public IRepository<Transaction, Guid> TransactionRepository { get; set; }

        public TransactionAppService(IReadOnlyRepository<Transaction, Guid> repository) : base(repository)
        {
        }

        [Authorize(GamblePermissions.Transaction.Create)]
        public virtual async Task CreateDepositAsync(TransactionDepositCreateOrUpdateDto input)
        {
            using (var uow = UnitOfWorkManager.Begin(requiresNew: true))
            {
                await TransactionRepository.InsertAsync(new Transaction
                {
                    Amount = input.IsRevoke ? input.Amount * -1.0m : input.Amount,
                    Type = input.IsRevoke ? TransactionTypeConsts.Revoke : TransactionTypeConsts.Deposit,
                    UserId = input.UserId.Value,
                    Status = TransactionStatusConsts.Success
                }, true);

                await uow.CompleteAsync();
            }
        }

        public override Task<PagedResultDto<TransactionOutputDto>> GetListAsync(TransactionGetListInputDto input)
        {
            return base.GetListAsync(input);
        }

        protected override async Task<IQueryable<Transaction>> CreateFilteredQueryAsync(TransactionGetListInputDto input)
        {
            var queryable = await base.CreateFilteredQueryAsync(input);
            return queryable.Where(e => e.UserId == CurrentUser.Id)
                            .WhereIf(input.Types.Any(), e => input.Types.Contains(e.Type))
                            .WhereIf(input.From.HasValue, e => e.CreationTime.Date >= input.From.Value.Date)
                            .WhereIf(input.To.HasValue, e => e.CreationTime.Date < input.To.Value.AddDays(1).Date);
        }

        protected override IQueryable<Transaction> ApplySorting(IQueryable<Transaction> query, TransactionGetListInputDto input)
        {
            return query.OrderByDescending(e => e.CreationTime);
        }
    }
}