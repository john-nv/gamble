using OkVip.Gamble.Audits;
using OkVip.Gamble.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Identity;

namespace OkVip.Gamble.Tickets
{
    public class Ticket : BaseEntity<Guid>, ICreatorNamingAudit
    {
        private ICollection<TicketLog> _ticketLogs;

        public string Title { get; set; } = string.Empty;

        public string? Note { get; set; }

        public string Status { get; set; } = string.Empty;

        public string TicketType { get; set; } = string.Empty;

        public string? CreatorName { get; set; }

        public string? CreatorUsername { get; set; }

        public DateTime? ApprovedTime { get; set; }

        public decimal Amount { get; set; }

        public Guid? TransactionId { get; set; }

        public string StatusName
        {
            get
            {
                if (string.IsNullOrEmpty(Status))
                {
                    return string.Empty;
                }

                return TicketStatusConsts.Translate[Status];
            }
        }

        public string TicketTypeName
        {
            get
            {
                if (string.IsNullOrEmpty(TicketType))
                {
                    return string.Empty;
                }

                return TicketTypeConsts.Translate[TicketType];
            }
        }

        public virtual ICollection<TicketLog> TicketLogs
        {
            get => _ticketLogs ??= new List<TicketLog>();
            set => _ticketLogs = value;
        }

        public virtual Transaction Transaction { get; set; }

        public virtual IdentityUser Creator { get; set; }

        public virtual IdentityUser LastModifier { get; set; }

        public bool IsApproved()
        {
            return TicketLogs.Any(e => e.Status == TicketStatusConsts.Approved || e.Status == TicketStatusConsts.Rejected);
        }

        public Guid GetTransactionId()
        {
            if (!ExtraProperties.ContainsKey("transactionId"))
            {
                throw new UserFriendlyException("transactionId not found");
            }

            return Guid.Parse(ExtraProperties.ContainsKey("transactionId").ToString());
        }
    }
}