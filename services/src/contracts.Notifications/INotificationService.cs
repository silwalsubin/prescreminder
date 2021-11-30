using System;
using System.Threading.Tasks;

namespace contracts.Notifications
{
    public interface INotificationService
    {
        Task AddOrUpdateEventNotification(EventNotification notification);
        Task DeleteById(Guid notificationId);
    }
}
