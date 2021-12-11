using contracts.Notifications;
using services.Notifications.Persistence;
using System;

namespace services.Notifications
{
    public static class NotificationVerbiageGenerator
    {
        public static string GetVerbiage(this UserEventNotificationsTableSchema.UserEventNotificationRecord record)
        {
            return record.Event switch
            {
                NotificationType.PrescriptionExpiration => record.EventDateUtc > DateTime.UtcNow
                    ? $"{record.Entity} is running out soon. Please refill it as soon as possible"
                    : $"You've ran out of {record.Entity}. Please refill it immediately.",
                _ => throw new Exception("Notification Event is not supported")
            };
        }
    }
}
