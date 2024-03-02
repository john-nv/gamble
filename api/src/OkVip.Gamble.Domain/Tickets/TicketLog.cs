using OkVip.Gamble.Audits;
using System;
using Volo.Abp.Identity;

namespace OkVip.Gamble.Tickets
{
    public class TicketLog : BaseEntity<Guid>, ICreatorNamingAudit
    {
        public string Status { get; set; } = string.Empty;

        public string? CreatorName { get; set; }

        public string? CreatorUsername { get; set; }

        public string Note { get; set; } = string.Empty;

        public Guid TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual IdentityUser Creator { get; set; }
    }
}