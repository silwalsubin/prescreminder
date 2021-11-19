using contracts.Persistence;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace services.UserPrescriptions.Persistence
{
    public class PrescriptionTimesTableSchema : ITableSchema
    {
        public string Schema => "dbo";
        public string TableName => "PrescriptionTimes";
        public string CreateScript => $@"
            CREATE TABLE [{Schema}].[{TableName}] (
	            [PrescriptionTimeId] [UniqueIdentifier] NOT NULL PRIMARY KEY,
	            [PrescriptionId] [UniqueIdentifier] NOT NULL,
	            [Hour] [int] NOT NULL,
	            [Minute] [int] NOT NULL,
				[Second] [int] NOT NULL
			)
        ";

        public Type Dto => typeof(PrescriptionTimeRecord);

        [Table("PrescriptionTimes")]
        public class PrescriptionTimeRecord
        {
            public Guid PrescriptionTimeId { get; set; }
            public Guid PrescriptionId { get; set; }
            public int Hour { get; set; }
            public int Minute { get; set; }
            public int Second { get; set; }
        }
    }
}
