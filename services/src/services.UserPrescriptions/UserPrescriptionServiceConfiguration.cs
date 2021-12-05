using contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using services.UserPrescriptions.Persistence;

namespace services.UserPrescriptions
{
    public class UserPrescriptionServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITableSchema, UserPrescriptionsTableSchema>();
            services.AddScoped<UserPrescriptionsTableSchema>();
            services.AddScoped<UserPrescriptionsRepository>();
            services.AddTransient<ITableSchema, PrescriptionTimesTableSchema>();
            services.AddScoped<PrescriptionTimesTableSchema>();
            services.AddScoped<PrescriptionTimesRepository>();
            services.AddScoped<PrescriptionsPdfGenerator>();
        }
    }
}
