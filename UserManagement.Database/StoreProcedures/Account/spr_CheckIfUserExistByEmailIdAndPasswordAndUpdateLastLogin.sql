CREATE PROCEDURE [dbo].[spr_CheckIfUserExistByEmailIdAndPasswordAndUpdateLastLogin]
	@EmailId VARCHAR(200),
	@Password VARCHAR(250)
AS
DECLARE @result BIT = 0, @newId BIGINT, @message VARCHAR(100) = '';
IF EXISTS (SELECT TOP 1 Id FROM dbo.Users WHERE EmailId = @EmailId AND [Password] = @Password)
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Users
	SET LastLogin = GETDATE()
	WHERE EmailId = @EmailId AND [Password] = @Password
 
	SET @result = 1;
	SET @newId = (SELECT TOP 1 Id FROM dbo.Users WHERE EmailId = @EmailId AND [Password] = @Password);
	SET @message = 'Update Successfull';
	END
ELSE
	BEGIN
		SET @result = 0; 
		SET @newId = 0;
		SET @message = 'Invalid Credentials';
	END 
SELECT @result [Result], @message [Message], @newId [Id]
