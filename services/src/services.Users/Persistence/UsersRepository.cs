using contracts.Persistence;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<string> GetUserFullName(Guid userId)
        {
            var sql = $@"
                SELECT * FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]
                WHERE UserId  = @userId";
            var result = (await DbConnection.QueryAsync<UsersTableSchema.UserRecord>(sql, new { userId })).Single();
            return result.MiddleName != null
                ? $"{result.FirstName} {result.MiddleName} {result.LastName}"
                : $"{result.FirstName} {result.LastName}";
        }

        public async Task<UsersTableSchema.UserRecord> GetByEmailAddress(string emailAddress)
        {
            var sql = $@"
                SELECT * FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]
                WHERE EmailAddress  = @emailAddress";
            var result = await DbConnection.QueryAsync<UsersTableSchema.UserRecord>(sql, new { emailAddress });
            return result.SingleOrDefault();
        }

        public async Task DeleteByUserId(Guid userId)
        {
            var sql = $@"
                DELETE FROM [{_usersTableSchema.Schema}].[{_usersTableSchema.TableName}]
                WHERE UserId = @userId
            ";
            await DbConnection.ExecuteAsync(sql, new { userId });
        }

        public async Task InsertAsync(UsersTableSchema.UserRecord record)
        {
            await DbConnection.InsertAsync(record);
        }
    }
}
