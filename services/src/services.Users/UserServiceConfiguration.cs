using contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using services.Users.Persistence;

namespace services.Users
{
    public class UserServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITableSchema, UsersTableSchema>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<UsersTableSchema>();
        }
    }
}
