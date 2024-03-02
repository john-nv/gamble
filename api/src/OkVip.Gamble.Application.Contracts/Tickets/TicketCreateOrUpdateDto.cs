using Volo.Abp.Data;

namespace OkVip.Gamble.Tickets
{
    public class TicketCreateOrUpdateDto : IHasExtraProperties
    {
        public string Title { get; set; } = string.Empty;

        public string? Note { get; set; } = string.Empty;

        public string TicketType { get; set; } = string.Empty;

        public ExtraPropertyDictionary ExtraProperties { get; set; } = new ExtraPropertyDictionary();
    }
}