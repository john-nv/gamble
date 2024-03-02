using System;
using System.Collections.Generic;

namespace OkVip.Gamble.Tickets
{
    public class TicketOutputDto : BaseEntityDto<Guid>
    {
        private IList<TicketLogOutputDto> ticketLogs;

        public string Title { get; set; } = string.Empty;

        public string? Note { get; set; }

        public string Status { get; set; } = string.Empty;

        public string TicketType { get; set; } = string.Empty;

        public string CreatorName { get; set; } = string.Empty;

        public string CreatorUsername { get; set; } = string.Empty;

        public string CreatorEmail { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;

        public string TicketTypeName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public decimal AmountInVnd { get; set; }

        public string AmountInVndText { get; set; }

        public Guid CreatorId { get; set; }

        public IList<TicketLogOutputDto> TicketLogs
        {
            get => ticketLogs ??= new List<TicketLogOutputDto>();
            set => ticketLogs = value;
        }
    }
}