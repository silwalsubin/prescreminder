using contracts.Persistence;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using prescreminder.Utilities;
using System;

namespace infrastructures.BackgroundJobs
{
    public static class BackgroundJobsInfrastructureServiceCollection
    {
        public static void Configure(IServiceCollection services)
        {
            var connectionString = AppSettingsUtility.GetSettings<PersistenceSettings>().DbConnectionString;
            // Add HangFire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                    SchemaName = "HangFire"
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddScoped<IBackgroundJobsInfrastructure, BackgroundJobsInfrastructure>();
        }

        public static void ConfigureBackgroundJobsInfrastructure(this IApplicationBuilder builder)
        {
            builder.UseHangfireDashboard();
        }

        public static void ConfigureBackgroundJobsInfrastructure(this IEndpointRouteBuilder builder)
        {
            builder.MapHangfireDashboard();
        }
    }
}
