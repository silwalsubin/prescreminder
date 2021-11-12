using Dapper;
using Dapper.Contrib.Extensions;
using prescreminder.Database.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace prescreminder.Database.Tables.Users
{
    public class UsersRepository : PrecreminderBaseRepository
    {
        private readonly UsersTableSchema _usersTableSchema;

        public UsersRepository(UsersTableSchema usersTableSchema)
        {
            _usersTableSchema = usersTableSchema;
        }

        public async Task<IEnumerable<UsersTableSchema.UserRecord>> Get()
        {
            var sql = $"SELECT * FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]";
            var result = await DbConnection.QueryAsync<UsersTableSchema.UserRecord>(sql);
            return result;
        }

        public async Task Insert(UsersTableSchema.UserRecord record)
        {
            await DbConnection.InsertAsync(record);
        }
    }
}
