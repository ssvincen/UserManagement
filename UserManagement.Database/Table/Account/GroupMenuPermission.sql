CREATE TABLE [dbo].[GroupMenuPermission]
(
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[GroupId] INT NOT NULL,
	[NavigationMenuId] BIGINT NOT NULL,
	CONSTRAINT [FK_RoleMenuPermission_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]),
	CONSTRAINT [FK_RoleMenuPermission_NavigationMenu] FOREIGN KEY ([NavigationMenuId]) REFERENCES [dbo].[NavigationMenu] ([Id])
)
