using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace prescreminder.Database.Domain
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
