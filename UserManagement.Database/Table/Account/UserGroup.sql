CREATE TABLE [dbo].[UserGroup]
(
	Id BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId BIGINT NOT NULL,
	GroupId INT NOT NULL,
	CONSTRAINT [FK_UserGroup_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
	CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])
)
