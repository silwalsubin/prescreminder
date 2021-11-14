using System;
using System.Collections.Generic;

namespace contracts.Persistence
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