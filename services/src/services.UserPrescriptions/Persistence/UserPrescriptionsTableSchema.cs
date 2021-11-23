using contracts.Persistence;
using Dapper.Contrib.Extensions;
using System;

namespace services.UserPrescriptions.Persistence
{
    public class UserPrescriptionsTableSchema : ITableSchema
    {
        public string Schema => "dbo";
        public string TableName => "UserPrescriptions";
        public string CreateScript => $@"
            CREATE TABLE [{Schema}].[{TableName}] (
	            [PrescriptionId] [UniqueIdentifier] NOT NULL PRIMARY KEY,
	            [UserId] [UniqueIdentifier] NOT NULL,
	            [Name] [nvarchar](max) NOT NULL,
	            [Quantity] [nvarchar](max) NOT NULL,
				[StartDateUtc] [datetime] NOT NULL,
				[CompleteDateUtc] [datetime] NULL,
				[ExpirationDateUtc] [datetime] NULL,
				[CreatedDateUtc] [datetime] NOT NULL,
				[ModifiedDateUtc] [datetime] NOT NULL
			)
        ";
        public Type Dto => typeof(UserPrescriptionRecord);

        [System.ComponentModel.DataAnnotations.Schema.Table("UserPrescriptions")]
        public class UserPrescriptionRecord : IGenericRecord
        {
            [ExplicitKey]
            public Guid PrescriptionId { get; set; }
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string Quantity { get; set; }
            public DateTime StartDateUtc { get; set; }
            public DateTime? CompleteDateUtc { get; set; }
            public DateTime? ExpirationDateUtc { get; set; }
            public DateTime CreatedDateUtc { get; set; }
            public DateTime ModifiedDateUtc { get; set; }
        }
    }
}
