using System;

namespace contracts.Notifications
{
    public class EventNotification
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }
        public string Event { get; set; }
        public DateTime EventDateUtc { get; set; }
    }
}