USE [GZJohnsonProcess]
GO

/****** Object:  Table [dbo].[wf_metadata]    Script Date: 08/02/2012 14:09:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[wf_metadata](
	[meta_id] [int] IDENTITY(1,1) NOT NULL,
	[meta_name] [nvarchar](50) NULL,
	[meta_value] [ntext] NULL,
 CONSTRAINT [PK_wf_metadata] PRIMARY KEY CLUSTERED 
(
	[meta_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_wf_metadata] UNIQUE NONCLUSTERED 
(
	[meta_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[wf_processMail]    Script Date: 08/02/2012 14:09:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[wf_processMail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](500) NULL,
	[subject] [nvarchar](500) NULL,
	[content] [ntext] NULL,
	[status] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[gz_johnson_process_form]    Script Date: 08/02/2012 14:09:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[gz_johnson_process_form](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[processName] [nvarchar](50) NOT NULL,
	[Incident] [int] NOT NULL,
	[processForm] [ntext] NULL,
	[processType] [nvarchar](50) NULL,
 CONSTRAINT [PK_gz_johnson_process] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


/****** Object:  Table [dbo].[wf_myToDoTaskMail]    Script Date: 08/02/2012 14:08:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[wf_myToDoTaskMail](
	[taskId] [nvarchar](50) NULL,
	[email] [nvarchar](500) NULL,
	[subject] [nvarchar](500) NULL,
	[content] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

alter table gz_johnson_process_form 
add status int
update gz_johnson_process_form set status = 2