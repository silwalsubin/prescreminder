using System;
using System.Linq.Expressions;

namespace infrastructures.BackgroundJobs
{
    public interface IBackgroundJobsInfrastructure
    {
        void AddOrUpdateRecurringJob(string jobName, Expression<Action> expression, string cronExpression, TimeZoneInfo timeZoneInfo);
    }
}