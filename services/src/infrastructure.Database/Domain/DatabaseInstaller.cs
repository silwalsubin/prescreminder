using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace infrastructure.Database.Domain
{
    public class DatabaseInstaller : BackgroundService
    {
        private readonly TablesInstaller _tablesInstaller;

        public DatabaseInstaller(TablesInstaller tablesInstaller)
        {
            _tablesInstaller = tablesInstaller;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _tablesInstaller.CreateTables();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                Environment.FailFast(e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
