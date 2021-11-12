using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using prescreminder.API.Configurations;
using Serilog;

namespace prescreminder.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(c => c.Configure(ApplicationBuilderConfiguration.Configure))
                .ConfigureServices(ServicesConfigurations.Configure)
                .UseSerilog(LoggingConfiguration.Configure)
                .Build()
                .Run();
        }
    }
}
