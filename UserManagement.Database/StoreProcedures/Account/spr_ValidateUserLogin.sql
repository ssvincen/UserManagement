CREATE PROCEDURE [dbo].[spr_ValidateUserLogin]
	@EmailId VARCHAR(200),
	@Password VARCHAR(250)
AS
DECLARE @result BIT = 0;
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT TOP 1 Id FROM dbo.[Users] WHERE EmailId = @EmailId AND [Password] = @Password)
		BEGIN
			SET @result = 1;
		END
	ELSE
		BEGIN
			SET @result = 0;
		END
	SELECT @result [Result]
END