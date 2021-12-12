using contracts.Persistence;
using Dapper;
using infrastructure.Database;
using infrastructure.Database.Domain;
using Microsoft.Extensions.DependencyInjection;
using middleware.Authentication;
using services.Notifications;
using services.UserMedicationIntakeHistories;
using services.UserPrescriptions;
using services.Users;
using System;
using infrastructures.BackgroundJobs;

namespace prescreminder.API.Configurations
{
    public class ServicesConfigurations
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddHostedService<DatabaseInstaller>();
            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\dist";
            });

            NotificationServiceConfiguration.Configure(services);
            UserServiceConfiguration.Configure(services);
            UserPrescriptionServiceConfiguration.Configure(services);
            AuthenticationMiddleWareConfiguration.Configure(services);
            UserMedicationIntakeHistoriesServiceConfiguration.Configure(services);
            DatabaseInfrastructureConfiguration.Configure(services);
            BackgroundJobsInfrastructureServiceCollection.Configure(services);
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }
    }
}
