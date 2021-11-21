using contracts.Persistence;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace services.UserPrescriptions.Persistence
{
    public class UserPrescriptionsRepository : BaseRepository
    {
        private readonly UserPrescriptionsTableSchema _userPrescriptionsTableSchema;

        public UserPrescriptionsRepository(UserPrescriptionsTableSchema userPrescriptionsTableSchema)
        {
            _userPrescriptionsTableSchema = userPrescriptionsTableSchema;
        }

        public async Task InsertAsync(UserPrescriptionsTableSchema.UserPrescriptionRecord record)
        {
            await DbConnection.InsertAsync(record);
        }

        public async Task<IEnumerable<UserPrescriptionsTableSchema.UserPrescriptionRecord>> GetByUserIdAsync(Guid userId)
        {
            var sql = @$"
                SELECT * FROM [{_userPrescriptionsTableSchema.Schema}].[{_userPrescriptionsTableSchema.TableName}]
                WHERE UserId = @userId
            ";
            var result =
                await DbConnection.QueryAsync<UserPrescriptionsTableSchema.UserPrescriptionRecord>(sql, new { userId });
            return result;
        }

        public async Task DeleteAsync(Guid prescriptionId)
        {
            var sql = @$"
                DELETE FROM [{_userPrescriptionsTableSchema.Schema}].[{_userPrescriptionsTableSchema.TableName}]
                WHERE PrescriptionId = @prescriptionId
            ";
            await DbConnection.ExecuteAsync(sql, new { prescriptionId });
        }
    }
}
