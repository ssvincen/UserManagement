CREATE PROCEDURE [dbo].[spr_AddUser]
	@FirstName NVARCHAR(150) , 
	@Surname NVARCHAR(150) ,
	@EmailAddress NVARCHAR(200) , 
	@PasswordHash NVARCHAR(MAX),
	@EmailOTP VARCHAR(250),
	@GroupId INT
	
AS
DECLARE @result BIT = 0, @newId BIGINT, @message VARCHAR(100) = '';
BEGIN
SET NOCOUNT ON;
IF NOT EXISTS (SELECT TOP 1 Id FROM dbo.[Users] WHERE EmailId = @EmailAddress)
	BEGIN
		DECLARE @UserId BIGINT
		INSERT INTO dbo.[Users](FirstName, Surname, EmailId, [Password], EmailVerifyLink)
		VALUES(@FirstName, @Surname, @EmailAddress, @PasswordHash, @EmailOTP)
		SET @newId = (SELECT SCOPE_IDENTITY());

		INSERT INTO dbo.[UserGroup](UserId, GroupId)
		VALUES(@newId, @GroupId)

		SET @result = 1;
		SET @message = 'Update Successfull';

	END
ELSE
	BEGIN
		SET @result = 0;
		SET @newId = (SELECT TOP 1 Id FROM dbo.[Users] WHERE EmailId = @EmailAddress);
		SET @message = 'Email Address already Exist';
	END
SELECT @result [Result], @message [Message], @newId [Id]
END
