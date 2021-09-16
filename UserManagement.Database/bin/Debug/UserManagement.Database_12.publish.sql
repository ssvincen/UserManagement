﻿/*
Deployment script for UserManagementDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "UserManagementDB"
:setvar DefaultFilePrefix "UserManagementDB"
:setvar DefaultDataPath "C:\Users\Sifiso Sikhakhane\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\"
:setvar DefaultLogPath "C:\Users\Sifiso Sikhakhane\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
CREATE TABLE #userTabale
(
	Id BIGINT,
	FirstName VARCHAR(50),
	Surname VARCHAR(50),
	EmailId VARCHAR(200),
	[Password] VARCHAR(250),
	EmailVerifyLink VARCHAR(250),
	IsEmailVerified BIT NOT NULL Default(0),
	LastLogin DateTime NOT NULL Default(GETDATE()),
	DateCreated DateTime NOT NULL Default(GETDATE()),
	LastModified DateTime NOT NULL Default(GETDATE()),
	Active BIT NOT NULL Default(1)	
)


INSERT INTO #userTabale(Id, FirstName, Surname, EmailId, [Password] )
VALUES (1, 'Sifiso', 'Sikhakhane', 'ss.vincen@gmail.com', '588+9PF8OZmpTyxvYS6KiI5bECaHjk4ZOYsjvTjsIho=')

INSERT INTO [dbo].[Users] (Id, FirstName, Surname, EmailId, [Password], EmailVerifyLink, IsEmailVerified, LastLogin, DateCreated, LastModified, Active)
SELECT Id, FirstName, Surname, EmailId, [Password], EmailVerifyLink, IsEmailVerified, LastLogin, DateCreated, LastModified, Active FROM #userTabale
WHERE EmailId NOT IN (SELECT EmailId FROM [dbo].[Users] WITH (NOLOCK))
DROP TABLE #userTabale

CREATE TABLE #groupTable 
( 
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(MAX),
	[Active] BIT NOT NULL DEFAULT(1)
)

INSERT INTO #groupTable([Name])
VALUES ('Administrator'),
	   ('User')

INSERT INTO [dbo].[Group] ([Name])
SELECT [Name] FROM #groupTable
WHERE [Name] NOT IN (SELECT [Name] FROM [dbo].[Group] WITH (NOLOCK))
DROP TABLE #groupTable

CREATE TABLE #navigationMenuTable
(
	[Id] BIGINT ,
	[Name] NVARCHAR(MAX),
	[ParentMenuId] BIGINT,
	[Area] NVARCHAR(MAX),
	[ControllerName] NVARCHAR(MAX) ,
	[ActionName] NVARCHAR(MAX) ,
	[IsExternal] BIT ,
	[ExternalUrl] NVARCHAR(MAX),
	[DisplayOrder] INT ,
	[Visible] BIT 
)
INSERT INTO #navigationMenuTable([Id], [Name], [ParentMenuId], [Area], [ControllerName], [ActionName], [IsExternal], [ExternalUrl], [DisplayOrder], [Visible])
VALUES (1,'Operator', NULL, NULL, '', '', 0, NULL,1,1),
	   (2,'Maintain', NULL, NULL, '', '', 0, NULL,1,1),
	   (3,'Report', NULL, NULL, '', '', 0, NULL,1,1),
	   (4,'Create User', 1, NULL, 'Account', 'CreateUser', 0, NULL,1,1),
	   (5,'Create Group', 1, NULL, 'Account', 'CreateGroup', 0, NULL,2,1),
	   (6,'Link Group', 1, NULL, 'Account', 'LinkGroup', 0, NULL,3,1),
	   (7,'Link User', 1, NULL, 'Account', 'LinkUser', 0, NULL,4,1),
	   (8,'Users', 2, NULL, 'Account', 'Users', 0, NULL,1,1),
	   (9,'Edit User', 2, NULL, 'Account', 'EditUser', 0, NULL,4,0),
	   (10,'List Users', 3, NULL, 'Account', 'Users', 0, NULL,1,1)

INSERT INTO dbo.NavigationMenu([Name], [ParentMenuId], [Area], [ControllerName], [ActionName], [IsExternal], [ExternalUrl], [DisplayOrder], [Visible])
SELECT [Name], [ParentMenuId], [Area], [ControllerName], [ActionName], [IsExternal], [ExternalUrl], [DisplayOrder], [Visible] 
FROM #navigationMenuTable
WHERE [Name] NOT IN (SELECT [Name] FROM dbo.NavigationMenu)
DROP TABLE #navigationMenuTable

CREATE TABLE #groupMenuPermission
(
	[GroupId] INT NOT NULL,
	[NavigationMenuId] BIGINT NOT NULL,	
)

INSERT INTO #groupMenuPermission
VALUES (1, 1),
	   (1, 2),	   
	   (1, 3),
	   (1, 4),	
	   (1, 5),	   
	   (1, 6),	   
	   (1, 7),	  
	   (1, 8),	   
	   (1, 9),	   
	   (1, 10),	   
	   (2, 2),	   
	   (2, 3)
INSERT INTO dbo.GroupMenuPermission([GroupId], [NavigationMenuId])
SELECT [GroupId], [NavigationMenuId]
FROM #groupMenuPermission
WHERE [GroupId] NOT IN (SELECT [GroupId] FROM dbo.GroupMenuPermission)
AND [NavigationMenuId] NOT IN (SELECT [NavigationMenuId] FROM dbo.GroupMenuPermission)
DROP TABLE #groupMenuPermission
GO

GO
PRINT N'Update complete.';


GO
