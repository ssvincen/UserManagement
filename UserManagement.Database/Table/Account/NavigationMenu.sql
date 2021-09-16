CREATE TABLE [dbo].[NavigationMenu]
(
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(MAX) NULL,
	[ParentMenuId] BIGINT NULL,
	[Area] NVARCHAR(MAX) NULL,
	[ControllerName] NVARCHAR(MAX) NULL,
	[ActionName] NVARCHAR(MAX) NULL,
	[IsExternal] BIT NOT NULL,
	[ExternalUrl] NVARCHAR(MAX) NULL,
	[DisplayOrder] INT NOT NULL,
	[Visible] BIT NOT NULL, 
    CONSTRAINT [FK_NavigationMenu_NavigationMenu_ParentMenuId] FOREIGN KEY ([ParentMenuId]) REFERENCES [dbo].[NavigationMenu] ([Id])
)

