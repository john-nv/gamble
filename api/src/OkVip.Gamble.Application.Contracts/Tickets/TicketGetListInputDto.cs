using System;
using Volo.Abp.Application.Dtos;

namespace OkVip.Gamble.Tickets
{
    public class TicketGetListInputDto : PagedAndSortedResultRequestDto
    {
        public string? Status { get; set; }

        public string? Username { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}