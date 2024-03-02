using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace OkVip.Gamble.Transactions
{
    public class TransactionGetListInputDto : PagedAndSortedResultRequestDto
    {
        private List<string> _types;

        public List<string> Types
        {
            get => _types ??= new List<string>();
            set => _types = value;
        }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
