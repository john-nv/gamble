using OkVip.Gamble.Audits;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Users;

namespace OkVip.Gamble.Events
{
    public class CreatorAuditNamingEventHandler : ILocalEventHandler<EntityCreatedEventData<BasicAggregateRoot<Guid>>>, ITransientDependency
    {
        public ICurrentUser CurrentUser { get; set; }
        public Task HandleEventAsync(EntityCreatedEventData<BasicAggregateRoot<Guid>> eventData)
        {
            var entity = eventData.Entity;

            if (entity is ICreatorNamingAudit)
            {
                var audit = (ICreatorNamingAudit)entity;

                audit.CreatorName = string.IsNullOrEmpty(CurrentUser?.Name) ? CurrentUser?.UserName : CurrentUser?.Name;
                audit.CreatorUsername = CurrentUser?.UserName;
            }

            return Task.CompletedTask;
        }
    }
}
