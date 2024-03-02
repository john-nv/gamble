using System;

namespace OkVip.Gamble.Tickets
{
    public class TicketLogOutputDto : BaseEntityDto<Guid>
    {
        public string Status { get; set; } = string.Empty;

        public string CreatorName { get; set; } = string.Empty;

        public string CreatorUsername { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

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
    }
}
