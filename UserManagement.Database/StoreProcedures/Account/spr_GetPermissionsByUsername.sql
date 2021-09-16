CREATE PROCEDURE [dbo].[spr_GetPermissionsByUsername]
	@EmailId VARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT nm.Id,
	   nm.[Name],
	   nm.ParentMenuId,
	   nm.Area,
	   nm.ControllerName,
	   nm.ActionName,
	   nm.IsExternal,
	   NM.IsExternal,
	   nm.DisplayOrder,
	   nm.Visible
	FROM dbo.NavigationMenu nm INNER JOIN dbo.GroupMenuPermission gmp 
	ON nm.Id = gmp.NavigationMenuId INNER JOIN dbo.UserGroup ug
	ON gmp.GroupId = ug.GroupId INNER JOIN dbo.Users u 
	ON ug.UserId = u.Id
	WHERE u.EmailId = @EmailId
END
GO
