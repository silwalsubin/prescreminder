using contracts.Persistence;
using Dapper.Contrib.Extensions;
using System;

namespace services.UserMedicationIntakeHistories.Persistence
{
    public class UserMedicationIntakeHistoriesTableSchema : ITableSchema
    {
        public string Schema => "dbo";
        public string TableName => "UserMedicationIntakeHistories";
        public string CreateScript => $@"
            CREATE TABLE [{Schema}].[{TableName}] (
	            [HistoryId] [UniqueIdentifier] NOT NULL PRIMARY KEY,
	            [UserId] [UniqueIdentifier] NOT NULL,
	            [PrescriptionName] [nvarchar](max) NOT NULL,
	            [Quantity] [nvarchar](max) NOT NULL,
				[Hour] [int] NOT NULL,
				[Minute] [int] NOT NULL,
				[EventDateUtc] [datetime] NOT NULL
			)
        ";
        public Type Dto => typeof(UserMedicationIntakeHistoryRecord);

        [System.ComponentModel.DataAnnotations.Schema.Table("UserMedicationIntakeHistories")]
        public class UserMedicationIntakeHistoryRecord
        {
            [ExplicitKey]
            public Guid HistoryId { get; set; }
            public Guid UserId { get; set; }
            public string PrescriptionName { get; set; }
            public string Quantity { get; set; }
            public int Hour { get; set; }
            public int Minute { get; set; }
            public DateTime EventDateUtc { get; set; }
        }
    }
}
