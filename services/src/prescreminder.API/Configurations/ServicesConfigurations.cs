using contracts.Persistence;
using Dapper;
using infrastructure.Database;
using infrastructure.Database.Domain;
using Microsoft.Extensions.DependencyInjection;
using middleware.Authentication;
using services.UserPrescriptions;
using services.Users;
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

            UserServiceConfiguration.Configure(services);
            UserPrescriptionServiceConfiguration.Configure(services);
            AuthenticationMiddleWareConfiguration.Configure(services);
            DatabaseInfrastructureConfiguration.Configure(services);
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }
    }
}
