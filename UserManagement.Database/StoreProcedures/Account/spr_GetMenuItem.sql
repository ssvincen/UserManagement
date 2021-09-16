CREATE PROCEDURE [dbo].[spr_GetMenuItem]
	@RoleId INT,
	@ControllerName NVARCHAR(MAX),
	@ActionName NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	SET NOCOUNT ON;

	DECLARE @tempNavigationMenu TABLE 
	(
		[Id] BIGINT,
		[Name] NVARCHAR (MAX),
		[ParentMenuId] BIGINT,
		[Area] NVARCHAR(MAX),
		[ControllerName] NVARCHAR(MAX),
		[ActionName] NVARCHAR(MAX),
		[IsExternal] BIT,
		[ExternalUrl] NVARCHAR(MAX),
		[DisplayOrder] INT,
		[Visible] BIT
	)
	INSERT INTO @tempNavigationMenu
	EXEC spr_GetPermissionsByRoleId @RoleId

	SELECT *
	FROM @tempNavigationMenu
	WHERE ControllerName = @ControllerName 
	AND ActionName = @ActionName
END
