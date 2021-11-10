using Microsoft.Extensions.Configuration;
using System;

namespace prescreminder.API.Domain
{
    public static class AppSettingsUtility
    {
        public static T GetSettings<T>()
        {
            var environment = Environment.GetEnvironmentVariable("EnvironmentName");
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (!string.IsNullOrWhiteSpace(environment))
            {
                builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("EnvironmentName")}.json",
                    optional: true);
            }

            var configuration = builder.Build();
            var settings = configuration.GetSection(typeof(T).Name).Get<T>();
            return settings;
        }
    }
}