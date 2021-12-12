using contracts.Persistence;
using Hangfire;
using Hangfire.SqlServer;
using prescreminder.Utilities;
using System;
using System.Linq.Expressions;

namespace infrastructures.BackgroundJobs
{
    public class BackgroundJobsInfrastructure : IBackgroundJobsInfrastructure
    {
        private readonly RecurringJobManager _recurringJobManager;

        public BackgroundJobsInfrastructure()
        {
            var connectionString = AppSettingsUtility.GetSettings<PersistenceSettings>().DbConnectionString;
            var sqlServerStorage = new SqlServerStorage(connectionString);
            _recurringJobManager = new RecurringJobManager(sqlServerStorage);
        }

        public void AddOrUpdateRecurringJob(string jobName, Expression<Action> expression, string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _recurringJobManager.AddOrUpdate(jobName, expression, cronExpression, timeZoneInfo);
        }
    }
}