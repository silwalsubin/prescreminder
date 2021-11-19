using contracts.Persistence;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace services.Users.Persistence
{
    public class UsersTableSchema : ITableSchema
    {
        public string Schema => "dbo";
        public string TableName => "Users";
        public string CreateScript => $@"
            CREATE TABLE [{Schema}].[{TableName}] (
	            [UserId] [UniqueIdentifier] NOT NULL PRIMARY KEY,
	            [UserName] [nvarchar](max) NOT NULL,
	            [EmailAddress] [nvarchar](max) NOT NULL,
	            [Password] [nvarchar](max) NOT NULL,
	            [FirstName] [nvarchar](max) NOT NULL,
	            [MiddleName] [nvarchar](max) NULL,       
	            [LastName] [nvarchar](max) NOT NULL,
				[DateOfBirthUtc] [datetime] NOT NULL
			)
        ";
        public Type Dto => typeof(UserRecord);

        [Table("Users")]
        public class UserRecord
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string EmailAddress { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirthUtc { get; set; }
        }
    }
}