using infrastructure.Database.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.Database
{
    public class DatabaseInfrastructureConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<DatabaseInstaller>();
            services.AddSingleton<TablesInstaller>();
        }
    }
}
