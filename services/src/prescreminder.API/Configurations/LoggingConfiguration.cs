using System;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace prescreminder.API.Configurations
{
    public class LoggingConfiguration
    {
        public static void Configure(HostBuilderContext context, IServiceProvider provider,
            LoggerConfiguration loggerConfiguration)
        {
            var logsDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}/logs";
            loggerConfiguration
                .Enrich.FromLogContext()
                .WriteTo.File(
                    $"{logsDirectory}/log.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day
                );
        }
    }
}