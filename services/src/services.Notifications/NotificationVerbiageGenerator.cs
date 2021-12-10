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
                    ? $"Prescription for {record.Entity} needs to be refilled."
                    : $"Prescription for {record.Entity} needed to be refilled.",
                _ => throw new Exception("Notification Event is not supported")
            };
        }
    }
}
