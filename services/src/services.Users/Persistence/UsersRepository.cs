using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contracts.Persistence;
using Dapper;
using Dapper.Contrib.Extensions;

namespace services.Users.Persistence
{
    public class UsersRepository : BaseRepository
    {
        private readonly UsersTableSchema _usersTableSchema;

        public UsersRepository(UsersTableSchema usersTableSchema)
        {
            _usersTableSchema = usersTableSchema;
        }

        public async Task<UsersTableSchema.UserRecord> GetByUserName(string userName)
        {
            var sql = $@"
                SELECT * FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]
                WHERE UserName  = @userName";
            var result = await DbConnection.QueryAsync<UsersTableSchema.UserRecord>(sql, new { userName });
            return result.SingleOrDefault();
        }

        public async Task<UsersTableSchema.UserRecord> GetByEmailAddress(string emailAddress)
        {
            var sql = $@"
                SELECT * FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]
                WHERE EmailAddress  = @emailAddress";
            var result = await DbConnection.QueryAsync<UsersTableSchema.UserRecord>(sql, new { emailAddress });
            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<UsersTableSchema.UserRecord>> GetAll()
        {
            var sql = $@"
                SELECT * FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]
                WHERE EmailAddress  = @emailAddress";
            var result = await DbConnection.QueryAsync<UsersTableSchema.UserRecord>(sql);
            return result;
        }

        public async Task Insert(UsersTableSchema.UserRecord record)
        {
            await DbConnection.InsertAsync(record);
        }
    }
}
