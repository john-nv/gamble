using System;

namespace OkVip.Gamble.Announcements
{
    public class AnnouncementCreateOrUpdateDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsCarousel { get; set; }

        public int DisplayOrder { get; set; }
    }
}