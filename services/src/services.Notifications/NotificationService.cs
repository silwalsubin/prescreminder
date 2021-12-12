using contracts.Notifications;
using infrastructures.BackgroundJobs;
using Microsoft.Extensions.Hosting;
using services.Notifications.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly UserEventNotificationRepository _userEventNotificationRepository;
        private readonly IBackgroundJobsInfrastructure _backgroundJobsInfrastructure;
        private readonly DailyPushNotificationsRepository _dailyPushNotificationsRepository;

        public NotificationService(
            UserEventNotificationRepository userEventNotificationRepository,
            IBackgroundJobsInfrastructure backgroundJobsInfrastructure,
            DailyPushNotificationsRepository dailyPushNotificationsRepository)
        {
            _userEventNotificationRepository = userEventNotificationRepository;
            _backgroundJobsInfrastructure = backgroundJobsInfrastructure;
            _dailyPushNotificationsRepository = dailyPushNotificationsRepository;
        }

        public async Task AddOrUpdateEventNotification(EventNotification notification)
        {
            await DeleteById(notification.NotificationId);
            var record = new UserEventNotificationsTableSchema.UserEventNotificationRecord
            {
                UserId = notification.UserId,
                Event = notification.NotificationType,
                Entity = notification.Entity,
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

        public void NotifyUserSessionForSystem(Guid userId, TimeZoneInfo timeZoneInfo)
        {
            _backgroundJobsInfrastructure.AddOrUpdateRecurringJob(
                $"reset-push-notification-{userId}",
                () => _dailyPushNotificationsRepository.TruncateTable(),
                "59 23 * * *",
                timeZoneInfo
            );
        }

        public void AddDailyPushNotification(Guid userId, Guid prescriptionId, string message, int hour, int minute, TimeZoneInfo timeZoneInfo)
        {
            _backgroundJobsInfrastructure.AddOrUpdateRecurringJob(
                $"{userId}_{prescriptionId}_{hour}_{minute}",
                () => _dailyPushNotificationsRepository.Add(userId, message),
                $"{minute} {hour} * * *",
                timeZoneInfo
            );
        }
    }

    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IBackgroundJobsInfrastructure _backgroundJobsInfrastructure;
        private readonly DailyPushNotificationsService _dailyPushNotificationsService;

        public NotificationBackgroundService(
            IBackgroundJobsInfrastructure backgroundJobsInfrastructure,
            DailyPushNotificationsService dailyPushNotificationsService
        )
        {
            _backgroundJobsInfrastructure = backgroundJobsInfrastructure;
            _dailyPushNotificationsService = dailyPushNotificationsService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _backgroundJobsInfrastructure.AddOrUpdateRecurringJob(
                $"push-notifications-recurring-service",
                () => _dailyPushNotificationsService.PushNotifications(),
                "* * * * *",
                TimeZoneInfo.Local
            );
            return Task.CompletedTask;
        }
    }

    public class DailyPushNotificationsService
    {
        private readonly DailyPushNotificationsRepository _dailyPushNotificationsRepository;

        public DailyPushNotificationsService(DailyPushNotificationsRepository dailyPushNotificationsRepository)
        {
            _dailyPushNotificationsRepository = dailyPushNotificationsRepository;
        }

        public void PushNotifications()
        {
            var asdfasdffasd = "asdfsadfsadf";
        }
    }
}
