CREATE TABLE [dbo].[Users]
(
	Id BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName VARCHAR(50) NOT NULL,
	Surname VARCHAR(50) NOT NULL,
	EmailId VARCHAR(200) UNIQUE NOT NULL,
	[Password] VARCHAR(250) NOT NULL,
	EmailVerifyLink VARCHAR(250),
	IsEmailVerified BIT NOT NULL Default(0),
	LastLogin DateTime NOT NULL Default(GETDATE()),
	DateCreated DateTime NOT NULL Default(GETDATE()),
	LastModified DateTime NOT NULL Default(GETDATE()),
	Active BIT NOT NULL Default(1)	
)
