using System;
using System.Collections.Generic;
using System.Linq;
using contracts.Persistence;
using Dapper;

namespace infrastructure.Database.Domain
{
    public class TablesInstaller : BaseRepository
    {
        private readonly IEnumerable<ITableSchema> _tableSchemas;

        public TablesInstaller(IEnumerable<ITableSchema> tableSchemas)
        {
            _tableSchemas = tableSchemas;
        }

        public void CreateTables()
        {
            var completedTableSchemas = new List<ITableSchema>();
            var count = completedTableSchemas.Count;
            while (completedTableSchemas.Count != _tableSchemas.Count())
            {
                foreach (var tableSchema in _tableSchemas)
                {
                    if (!tableSchema.DependsOn.Any() || tableSchema.DependsOn.Intersect(completedTableSchemas).Count() == tableSchema.DependsOn.Count())
                    {
                        var sql = $@"
                        IF (NOT EXISTS (SELECT * 
                         FROM INFORMATION_SCHEMA.TABLES 
                         WHERE TABLE_SCHEMA = '{tableSchema.Schema}' 
                         AND  TABLE_NAME = '{tableSchema.TableName}'))
                        BEGIN
                            {tableSchema.CreateScript}
                        END
                        ";
                        DbConnection.Execute(sql);
                        completedTableSchemas.Add(tableSchema);
                    }
                }

                if (count == completedTableSchemas.Count)
                {
                    throw new Exception("The table dependencies are not set right.");
                }
            }
        }
    }
}