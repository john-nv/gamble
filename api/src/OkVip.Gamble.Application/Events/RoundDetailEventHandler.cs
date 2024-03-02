using OkVip.Gamble.Rounds;
using OkVip.Gamble.Transactions;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;

namespace OkVip.Gamble.Events
{
	public class RoundDetailEventHandler :
		ILocalEventHandler<EntityCreatedEventData<RoundDetail>>,
		ITransientDependency
	{
		public IRepository<Transaction, Guid> TransactionRepository { get; set; }

		public IGuidGenerator GuidGenerator { get; set; }

		public async Task HandleEventAsync(EntityCreatedEventData<RoundDetail> eventData)
		{
			var entity = eventData.Entity;

			// Tiền đặt cược 
			await TransactionRepository.InsertAsync(new Transaction
			{
				Amount = entity.TotalBetAmount * -1m,
				Status = TransactionStatusConsts.Success,
				Type = TransactionTypeConsts.Betting,
				UserId = entity.UserId,
			});
		}
	}
}
