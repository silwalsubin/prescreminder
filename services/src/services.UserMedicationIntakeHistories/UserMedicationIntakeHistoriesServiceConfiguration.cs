using contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using services.UserMedicationIntakeHistories.Persistence;

namespace services.UserMedicationIntakeHistories
{
    public class UserMedicationIntakeHistoriesServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITableSchema, UserMedicationIntakeHistoriesTableSchema>();
            services.AddScoped<UserMedicationIntakeHistoriesTableSchema>();
            services.AddScoped<UserMedicationIntakeHistoriesRepository>();
        }
    }
}
