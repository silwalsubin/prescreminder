using System;
using System.Collections.Generic;

namespace prescreminder.Database.Domain
{
    public interface ITableSchema
    {
        IEnumerable<ITableSchema> DependsOn { get; }
        string Schema { get; }
        string TableName { get; }
        string CreateScript { get; }
        Type Dto { get; }
    }
}