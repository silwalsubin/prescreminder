using Microsoft.Data.SqlClient;
using prescreminder.Utilities;
using System.Data;

namespace contracts.Persistence
{
    public class BaseRepository
    {
        protected readonly IDbConnection DbConnection;

        public BaseRepository()
        {
            DbConnection = new SqlConnection(AppSettingsUtility.GetSettings<PersistenceSettings>().DbConnectionString);
        }
    }
}