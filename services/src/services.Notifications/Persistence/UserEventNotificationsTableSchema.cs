using contracts.Notifications;
using contracts.Persistence;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace services.Notifications.Persistence
{
    public class UserEventNotificationsTableSchema : ITableSchema
    {
        public string Schema => "dbo";
        public string TableName => "UserEventNotifications";
        public string CreateScript => $@"
            CREATE TABLE [{Schema}].[{TableName}] (
	            [NotificationId] [UniqueIdentifier] NOT NULL PRIMARY KEY,
	            [UserId] [UniqueIdentifier] NOT NULL,
	            [Event] [int] NOT NULL,
	            [Entity] [nvarchar](max) NOT NULL,
	            [EventDateUtc] [datetime] NOT NULL,
				[ClearedDateUtc] [datetime] NULL
			)
        ";

        public Type Dto => typeof(UserEventNotificationRecord);

        [Table("UserEventNotifications")]
        public class UserEventNotificationRecord
        {
            public Guid NotificationId { get; set; }
            public Guid UserId { get; set; }
            public NotificationType Event { get; set; }
            public string Entity { get; set; }
            public DateTime EventDateUtc { get; set; }
            public DateTime? ClearedDateUtc { get; set; }
        }
    }
}
