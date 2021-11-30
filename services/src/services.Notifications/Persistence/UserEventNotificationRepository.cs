using contracts.Persistence;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace services.Notifications.Persistence
{
    public class UserEventNotificationRepository : BaseRepository
    {
        private readonly UserEventNotificationsTableSchema _userEventNotificationsTableSchema;

        public UserEventNotificationRepository(UserEventNotificationsTableSchema userEventNotificationsTableSchema)
        {
            _userEventNotificationsTableSchema = userEventNotificationsTableSchema;
        }

        public async Task InsertAsync(UserEventNotificationsTableSchema.UserEventNotificationRecord record)
        {
            await DbConnection.InsertAsync(record);
        }

        public async Task<IEnumerable<UserEventNotificationsTableSchema.UserEventNotificationRecord>> GetByUserIdAsync(Guid userId)
        {
            var sql = @$"
                SELECT * FROM [{_userEventNotificationsTableSchema.Schema}].[{_userEventNotificationsTableSchema.TableName}]
                WHERE UserId = @userId
            ";
            var result =
                await DbConnection.QueryAsync<UserEventNotificationsTableSchema.UserEventNotificationRecord>(sql, new { userId });
            return result;
        }

        public async Task DeleteByNotificationId(Guid notificationId)
        {
            var sql = @$"
                DELETE FROM [{_userEventNotificationsTableSchema.Schema}].[{_userEventNotificationsTableSchema.TableName}]
                WHERE NotificationId = @notificationId
            ";
            await DbConnection.ExecuteAsync(sql, new { notificationId });
        }

        public async Task ClearByNotificationId(Guid notificationId)
        {
            var clearedDateUtc = DateTime.UtcNow;
            var sql = @$"
                UPDATE [{_userEventNotificationsTableSchema.Schema}].[{_userEventNotificationsTableSchema.TableName}]
                SET ClearedDateUtc = @clearedDateUtc
                WHERE NotificationId = @notificationId
            ";
            await DbConnection.ExecuteAsync(sql, new { clearedDateUtc, notificationId });
        }
    }
}
