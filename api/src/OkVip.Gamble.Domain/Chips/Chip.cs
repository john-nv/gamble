using System;

namespace OkVip.Gamble.Chips
{
    public class Chip : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}