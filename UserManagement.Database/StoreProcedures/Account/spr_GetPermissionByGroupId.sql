CREATE PROCEDURE [dbo].[spr_GetPermissionByGroupId]
	@GroupId INT
AS
BEGIN

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT nm.Id,
		   nm.[Name],
		   nm.ParentMenuId,
		   nm.Area,
		   nm.ControllerName,
		   nm.ActionName,
		   nm.IsExternal,
		   nm.IsExternal,
		   nm.DisplayOrder,
		   nm.Visible,
		   CASE WHEN gmp.GroupId IS NULL THEN 0 ELSE 1 END 'Permitted'
	FROM dbo.NavigationMenu nm LEFT JOIN dbo.GroupMenuPermission gmp 
	ON nm.Id = gmp.NavigationMenuId 
	AND gmp.GroupId = @GroupId
END
