using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Transactions
{
    public interface ITransactionAppService : IReadOnlyAppService<TransactionOutputDto, Guid, TransactionGetListInputDto>
    {
        Task CreateDepositAsync(TransactionDepositCreateOrUpdateDto input);
    }
}
