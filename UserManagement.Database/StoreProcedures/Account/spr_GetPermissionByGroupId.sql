CREATE PROCEDURE [dbo].[spr_GetPermissionByGroupId]
	@GroupId INT
AS
BEGIN

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	  SELECT nm.Id
			,nm.[Name]
			,nm.Area
			,nm.ActionName
			,nm.ControllerName
			,nm.IsExternal
			,nm.ExternalUrl
			,nm.DisplayOrder
			,nm.ParentMenuId
			,nm.Visible
	  FROM dbo.NavigationMenu nm LEFT JOIN dbo.GroupMenuPermission gmp
	  ON nm.Id = gmp.NavigationMenuId
	  WHERE gmp.GroupId = @GroupId
END
