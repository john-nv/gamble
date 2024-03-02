using System;

namespace OkVip.Gamble.Chips
{
    public class ChipOutputDto : BaseEntityDto<Guid>
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
