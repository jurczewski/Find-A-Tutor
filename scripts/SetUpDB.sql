create DATABASE FindATutor
GO
USE FindATutor

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolSubjects](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[SchoolSubjects] ADD  CONSTRAINT [PK_SchoolSubjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Role] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivateLessons](
	[Id] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[TutorId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[TakenAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[RelevantTo] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SchoolSubjectId] [uniqueidentifier] NULL  ,
	[IsPaid] [bit] NOT NULL,
	[IsDone] [bit] NOT NULL,
	[Time] float NOT NULL,
	[PricePerHour] float NULL,
	[TotalPrice] float NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrivateLessons] ADD  CONSTRAINT [PK_PrivateLessons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PrivateLessons_SchoolSubjectId] ON [dbo].[PrivateLessons]
(
	[SchoolSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrivateLessons]  WITH CHECK ADD  CONSTRAINT [FK_PrivateLessons_SchoolSubjects_SchoolSubjectId] FOREIGN KEY([SchoolSubjectId])
REFERENCES [dbo].[SchoolSubjects] ([Id])
GO
ALTER TABLE [dbo].[PrivateLessons] CHECK CONSTRAINT [FK_PrivateLessons_SchoolSubjects_SchoolSubjectId]
GO
