using Microsoft.Extensions.Configuration;
using System;

namespace prescreminder.Utilities
{
    public static class AppSettingsUtility
    {
        public static T GetSettings<T>()
        {
            var aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (!string.IsNullOrWhiteSpace(aspNetCoreEnvironment))
                configurationBuilder.AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", optional: true);

            var settings = configurationBuilder.Build().GetSection(typeof(T).Name).Get<T>();
            return settings;
        }
    }
}