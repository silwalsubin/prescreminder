using contracts.Persistence;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace services.UserMedicationIntakeHistories.Persistence
{
    public class UserMedicationIntakeHistoriesRepository : BaseRepository
    {
        private readonly UserMedicationIntakeHistoriesTableSchema _userMedicationIntakeHistoriesTableSchema;

        public UserMedicationIntakeHistoriesRepository(UserMedicationIntakeHistoriesTableSchema userMedicationIntakeHistoriesTableSchema)
        {
            _userMedicationIntakeHistoriesTableSchema = userMedicationIntakeHistoriesTableSchema;
        }

        public async Task InsertAsync(UserMedicationIntakeHistoriesTableSchema.UserMedicationIntakeHistoryRecord record)
        {
            await DbConnection.InsertAsync(record);
        }

        public async Task<IEnumerable<UserMedicationIntakeHistoriesTableSchema.UserMedicationIntakeHistoryRecord>> GetByEventDateRangeAsync(Guid userId, DateTime fromUtc, DateTime toUtc)
        {
            var sql = @$"
                SELECT * FROM [{_userMedicationIntakeHistoriesTableSchema.Schema}].[{_userMedicationIntakeHistoriesTableSchema.TableName}]
                WHERE EventDateUtc BETWEEN @fromUtc AND @toUtc 
                AND UserId = @userId
            ";
            var result =
                await DbConnection.QueryAsync<UserMedicationIntakeHistoriesTableSchema.UserMedicationIntakeHistoryRecord>(sql, new { fromUtc, toUtc, userId });
            return result;
        }

        public async Task<IEnumerable<UserMedicationIntakeHistoriesTableSchema.UserMedicationIntakeHistoryRecord>> GetByUserIdAsync(Guid userId)
        {
            var sql = @$"
                SELECT * FROM [{_userMedicationIntakeHistoriesTableSchema.Schema}].[{_userMedicationIntakeHistoriesTableSchema.TableName}]
                WHERE UserId = @userId
            ";
            var result =
                await DbConnection.QueryAsync<UserMedicationIntakeHistoriesTableSchema.UserMedicationIntakeHistoryRecord>(sql, new { userId });
            return result;
        }

        public async Task DeleteAsync(Guid historyId)
        {
            var sql = @$"
                DELETE FROM [{_userMedicationIntakeHistoriesTableSchema.Schema}].[{_userMedicationIntakeHistoriesTableSchema.TableName}]
                WHERE HistoryId = @historyId
            ";
            await DbConnection.ExecuteAsync(sql, new { historyId });
        }
    }
}
