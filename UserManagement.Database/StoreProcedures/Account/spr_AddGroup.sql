CREATE PROCEDURE [dbo].[spr_AddGroup]
	@Name VARCHAR(100),
	@Description VARCHAR(MAX)
AS
DECLARE @result BIT = 0, @newId BIGINT, @message VARCHAR(100) = '';
BEGIN
SET NOCOUNT ON;
IF NOT EXISTS (SELECT TOP 1 Id FROM dbo.[Group] WHERE [Name] = @Name)
	BEGIN
		DECLARE @UserId BIGINT
		INSERT INTO dbo.[Group]([Name], [Description])
		VALUES(@Name, @Description)
		SET @newId = (SELECT SCOPE_IDENTITY());

		SET @result = 1;
		SET @message = 'Update Successfull';
	END
ELSE
	BEGIN
		SET @result = 0;
		SET @newId = (SELECT TOP 1 Id FROM dbo.[Group] WHERE [Name] = @Name);
		SET @message = 'Group Name alread exist';
	END
SELECT @result [Result], @message [Message], @newId [Id]
END
