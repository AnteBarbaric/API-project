USE [TEST]
GO

/****** Object:  Table [dbo].[Language]    Script Date: 13.6.2021. 22:38:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Language](
	[LanguageId] [nvarchar](5) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[State]    Script Date: 13.6.2021. 23:00:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[State](
	[StateId] [nvarchar](1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserRole]    Script Date: 13.6.2021. 23:03:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[UserLogin]    Script Date: 13.6.2021. 22:58:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserLogin](
	[UserLoginId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[Email] [nvarchar](30) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[AreaId] [int] NULL,
	[LanguageId] [nvarchar](5) NULL,
	[UserRoleId] [int] NOT NULL,
	[Username] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[LastLogin] [datetime] NULL,
	[StateId] [nvarchar](1) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Changed] [datetime2](7) NULL,
	[Deleted] [datetime2](7) NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserLoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserLogin] ADD  CONSTRAINT [DF_UserLogin_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO

ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_Language_LanguageId]
GO

ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_State_StateId]
GO

ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_UserRole_UserRoleId] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([UserRoleId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_UserRole_UserRoleId]
GO


insert into [Language](LanguageId, Name)values('hr', 'Croatian')
insert into [Language](LanguageId, Name)values('en', 'English')

insert into UserRole(UserRoleId, Name)values(1, 'Administrator')
insert into UserRole(UserRoleId, Name)values(2, 'User')

insert into [State](StateId, Name)values('A', 'Active')
insert into [State](StateId, Name)values('I', 'Inactive')

insert into UserLogin(username, password, email, userRoleId, StateId)values('user1', '123', 'user1@gmail.com', 1, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user2', '123', 'user2@gmail.com', 2, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user3', '123', 'user3@gmail.com', 2, 'I')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user4', '123', 'user4@gmail.com', 2, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user5', '123', 'user5@gmail.com', 2, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user6', '123', 'user6@gmail.com', 2, 'I')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user7', '123', 'user7@gmail.com', 2, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user8', '123', 'user8@gmail.com', 2, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user9', '123', 'user9@gmail.com', 2, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user10', '123', 'user10@gmail.com', 1, 'A')
insert into UserLogin(username, password, email, userRoleId, StateId)values('user11', '123', 'user11@gmail.com', 2, 'A')

SET ANSI_PADDING ON
GO

/****** Object:  Index [Unique_username_ix]    Script Date: 13.6.2021. 23:12:36 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_username_ix] ON [dbo].[UserLogin]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

