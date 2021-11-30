using contracts.Notifications;
using services.Notifications.Persistence;
using System;
using System.Threading.Tasks;

namespace services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly UserEventNotificationRepository _userEventNotificationRepository;

        public NotificationService(UserEventNotificationRepository userEventNotificationRepository)
        {
            _userEventNotificationRepository = userEventNotificationRepository;
        }

        public async Task AddOrUpdateEventNotification(EventNotification notification)
        {
            await DeleteById(notification.NotificationId);
            var record = new UserEventNotificationsTableSchema.UserEventNotificationRecord
            {
                UserId = notification.UserId,
                Event = notification.Event,
                NotificationId = notification.NotificationId,
                ClearedDateUtc = null,
                EventDateUtc = notification.EventDateUtc,
            };

            await _userEventNotificationRepository.InsertAsync(record);
        }

        public async Task DeleteById(Guid notificationId)
        {
            await _userEventNotificationRepository.DeleteByNotificationId(notificationId);
        }
    }
}
