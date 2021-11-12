using Microsoft.Extensions.DependencyInjection;
using prescreminder.Database.Domain;
using prescreminder.Database.Tables.Users;

namespace prescreminder.Database
{
    public class PrescreminderDatabaseServiceCollection
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<DatabaseInstaller>();
            services.AddSingleton<TablesInstaller>();
            services.AddTransient<ITableSchema, UsersTableSchema>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<UsersTableSchema>();
        }
    }
}
