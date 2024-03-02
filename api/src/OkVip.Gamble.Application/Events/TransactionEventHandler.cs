using Microsoft.Extensions.Logging;
using OkVip.Gamble.IdentityUsers;
using OkVip.Gamble.Transactions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Identity;

namespace OkVip.Gamble.Events
{
	public class TransactionEventHandler : ILocalEventHandler<EntityChangedEventData<Transaction>>, ITransientDependency
	{
		public IRepository<IdentityUser, Guid> IdentityUserRepository { get; set; }

		public IRepository<Transaction, Guid> TransactionRepository { get; set; }

		public ILogger<TransactionEventHandler> Logger { get; set; }

		public async Task HandleEventAsync(EntityChangedEventData<Transaction> eventData)
		{
			Logger.LogInformation("OkVip.Gamble.Events.TransactionEventHandler BEGIN");

			var entity = eventData.Entity;

			var queryable = await TransactionRepository.GetQueryableAsync();
			var identityUser = await IdentityUserRepository.GetAsync(entity.UserId);

			if (identityUser != null)
			{
				identityUser.SetAccountBalance(queryable.Where(e => e.UserId == entity.UserId).Sum(e => e.Amount));
				await IdentityUserRepository.UpdateAsync(identityUser, true);
			}

			Logger.LogInformation("OkVip.Gamble.Events.TransactionEventHandler END");
		}
	}
}
