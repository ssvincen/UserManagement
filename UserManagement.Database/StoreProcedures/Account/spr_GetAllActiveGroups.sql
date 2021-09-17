CREATE PROCEDURE [dbo].[spr_GetAllActiveGroups]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT Id
		  ,[Name]
		  ,[Description]
	FROM dbo.[Group]
	WHERE Active = 1
END
GO
