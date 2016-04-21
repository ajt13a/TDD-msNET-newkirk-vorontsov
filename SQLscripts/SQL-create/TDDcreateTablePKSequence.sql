USE [catalog]
GO

/****** Object:  Table [dbo].[PKSequence]    Script Date: 04/10/2016 13:51:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PKSequence](
	[tablename] [varchar](128) NOT NULL,
	[nextid] [bigint] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

