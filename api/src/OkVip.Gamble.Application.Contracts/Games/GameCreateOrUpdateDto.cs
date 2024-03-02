using System;
using Volo.Abp.Data;

namespace OkVip.Gamble.Games
{
    public class GameCreateOrUpdateDto : IHasExtraProperties
    {
        private ExtraPropertyDictionary extraProperties;

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? CategoryId { get; set; }

        public bool IsActive { get; set; }

        public ExtraPropertyDictionary ExtraProperties
        {
            get => extraProperties ??= new ExtraPropertyDictionary();
            set => extraProperties = value;
        }
    }
}