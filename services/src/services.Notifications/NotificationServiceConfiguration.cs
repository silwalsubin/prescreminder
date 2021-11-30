using contracts.Notifications;
using contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using services.Notifications.Persistence;

namespace services.Notifications
{
    public class NotificationServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<ITableSchema, UserEventNotificationsTableSchema>();
            services.AddScoped<UserEventNotificationRepository>();
            services.AddScoped<UserEventNotificationsTableSchema>();
        }
    }
}
