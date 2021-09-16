CREATE PROCEDURE [dbo].[spr_GetAllUsers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT FirstName
		  ,Surname
		  ,EmailId
	FROM dbo.Users
END
GO
