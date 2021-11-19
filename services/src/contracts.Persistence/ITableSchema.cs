using System;

namespace contracts.Persistence
{
    public interface ITableSchema
    {
        string Schema { get; }
        string TableName { get; }
        string CreateScript { get; }
        Type Dto { get; }
    }
}