using System.Data;
using Microsoft.Data.SqlClient;
using prescreminder.Utilities;

namespace prescreminder.Database.Domain
{
    public class PrecreminderBaseRepository
    {
        protected readonly IDbConnection DbConnection;

        public PrecreminderBaseRepository()
        {
            DbConnection = new SqlConnection(AppSettingsUtility.GetSettings<PersistenceSettings>().DbConnectionString);
        }
    }
}