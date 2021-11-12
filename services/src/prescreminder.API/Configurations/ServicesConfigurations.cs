using Microsoft.Extensions.DependencyInjection;
using prescreminder.Database;
using prescreminder.Database.Domain;
using System;

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
            PrescreminderDatabaseServiceCollection.Configure(services);
        }
    }
}
