USE [catalog]
GO

/****** Object:  Table [dbo].[Track]    Script Date: 04/10/2016 13:48:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Track](
	[id] [bigint] NOT NULL,
	[title] [varchar](128) NOT NULL,
	[duration] [int] NOT NULL,
	[genreid] [bigint] NULL,
	[artistid] [bigint] NULL,
	[recordingid] [bigint] NULL,
 CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

