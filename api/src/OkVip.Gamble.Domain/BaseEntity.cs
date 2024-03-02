using System;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace OkVip.Gamble
{
    public class BaseEntity<TKey> : BasicAggregateRoot<TKey>, IFullAuditedObject, IHasExtraProperties
    {
        private ExtraPropertyDictionary _extraProperties;

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public Guid? DeleterId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public bool IsDeleted { get; set; }

        public ExtraPropertyDictionary ExtraProperties
        {
            get => _extraProperties ??= new ExtraPropertyDictionary();
            set => _extraProperties = value;
        }

        public BaseEntity()
        {
            ExtraProperties = new ExtraPropertyDictionary();
        }

        public void SetId(TKey id)
        {
            Id = id;
        }
    }
}