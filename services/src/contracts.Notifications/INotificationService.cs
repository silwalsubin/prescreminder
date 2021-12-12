using System;
using System.Threading.Tasks;

namespace contracts.Notifications
{
    public interface INotificationService
    {
        Task AddOrUpdateEventNotification(EventNotification notification);
        Task DeleteById(Guid notificationId);
        void NotifyUserSessionForSystem(Guid userId, TimeZoneInfo timeZoneInfo);
        void AddDailyPushNotification(Guid userId, Guid prescriptionId, string name, int hour, int minute, TimeZoneInfo timeZoneInfo);
    }
}
