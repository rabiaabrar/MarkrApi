/****** Object:  Database [MarkrApi]    Script Date: 19/09/2018 2:48:19 AM ******/
CREATE DATABASE [MarkrApi]
GO

USE MarkrApi
GO

/****** Object:  Table [dbo].[TestResults]    Script Date: 19/09/2018 2:48:19 AM ******/
CREATE TABLE [dbo].[TestResults](
	[TestId] [int] NOT NULL,
	[StudentNumber] [int] NOT NULL,
	[FirstName] [varchar](500) NOT NULL,
	[LastName] [varchar](500) NOT NULL,
	[MarksAvailable] [int] NOT NULL,
	[MarksObtained] [int] NOT NULL,
	[ScannedOn] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_MCQTestResults] PRIMARY KEY CLUSTERED
(
	[TestId] ASC,
	[StudentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TestResults] ADD  CONSTRAINT [DF_MCQTestResults_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO


/****** Object:  Login [MarkrApi]    Script Date: 19/09/2018 2:55:48 AM ******/
CREATE LOGIN [MarkrApi] WITH PASSWORD=N'Markr123', DEFAULT_DATABASE=[MarkrApi], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

/****** Object:  User [MarkrApi]    Script Date: 19/09/2018 3:01:49 AM ******/
CREATE USER [MarkrApi] FOR LOGIN [MarkrApi] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [MarkrApi]
GO

