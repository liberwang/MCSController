USE [MCS]
GO

/****** Object:  Table [dbo].[tblController]    Script Date: 5/19/2023 3:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblController]') AND type in (N'U'))
DROP TABLE [dbo].[tblController]
GO

/****** Object:  Table [dbo].[tblController]    Script Date: 5/19/2023 3:31:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblController](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ip_address] [varchar](15) NOT NULL,
	[description] [varchar](256) NULL,
	[cpuTypeId] [int] NULL,
	[isEnabled] [bit] NULL,
 CONSTRAINT [PK_tblController] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------- TAG TYPE 
/****** Object:  Table [dbo].[tblTagType]    Script Date: 5/19/2023 3:35:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTagType]') AND type in (N'U'))
DROP TABLE [dbo].[tblTagType]
GO

/****** Object:  Table [dbo].[tblTagType]    Script Date: 5/19/2023 3:35:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblTagType](
	[typeId] [int] IDENTITY(1,1) NOT NULL,
	[typeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblTagType] PRIMARY KEY CLUSTERED 
(
	[typeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

------------------ 
/****** Object:  Table [dbo].[tblFullTag]    Script Date: 5/19/2023 3:31:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFullTag]') AND type in (N'U'))
BEGIN
ALTER TABLE [dbo].[tblFullTag] DROP CONSTRAINT [FK_tblFullTag_tblTagType]
ALTER TABLE [dbo].[tblFullTag] DROP CONSTRAINT [FK_tblFullTag_tblController]

DROP TABLE [dbo].[tblFullTag]
END 
GO

/****** Object:  Table [dbo].[tblFullTag]    Script Date: 5/19/2023 3:31:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblFullTag](
	[tagId] [int] IDENTITY(1,1) NOT NULL,
	[controllerId] [int] NOT NULL,
	[tagName] [varchar](512) NOT NULL,
	[tagType] [int] NOT NULL,
	[tagDescription] [varchar](1024) NULL,
	[tagRead] [smallint] NULL,
	[tagWrite] [smallint] NULL,
 CONSTRAINT [PK_tblFullTag] PRIMARY KEY CLUSTERED 
(
	[tagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblFullTag]  WITH CHECK ADD  CONSTRAINT [FK_tblFullTag_tblController] FOREIGN KEY([controllerId])
REFERENCES [dbo].[tblController] ([id])
GO

ALTER TABLE [dbo].[tblFullTag] CHECK CONSTRAINT [FK_tblFullTag_tblController]
GO

ALTER TABLE [dbo].[tblFullTag]  WITH CHECK ADD  CONSTRAINT [FK_tblFullTag_tblTagType] FOREIGN KEY([tagType])
REFERENCES [dbo].[tblTagType] ([typeId])
GO

ALTER TABLE [dbo].[tblFullTag] CHECK CONSTRAINT [FK_tblFullTag_tblTagType]
GO



---------------------------------------------------------------- TAG CONTENT 
/****** Object:  Table [dbo].[tblTagContent]    Script Date: 5/19/2023 3:34:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTagContent]') AND type in (N'U'))
BEGIN
ALTER TABLE [dbo].[tblTagContent] DROP CONSTRAINT [DF_tblTagContent_tag_add_dt]
DROP TABLE [dbo].[tblTagContent]
END 
GO

/****** Object:  Table [dbo].[tblTagContent]    Script Date: 5/19/2023 3:34:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblTagContent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tag_cont] [varchar](max) NULL,
	[tag_add_dt] [datetime] NULL,
	[controller_ip] [varchar](15) NULL,
	[category_id] [int] NULL,
	[stationtag_id] [int] NULL,
	[tag_name] [varchar](512) NULL,
	[serial_number] [varchar](256) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblTagContent] ADD  CONSTRAINT [DF_tblTagContent_tag_add_dt]  DEFAULT (getdate()) FOR [tag_add_dt]
GO



------------------- SYSTEM KEYWORD
/****** Object:  Table [dbo].[tblSystemSettings]    Script Date: 5/19/2023 3:33:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSystemSettings]') AND type in (N'U'))
DROP TABLE [dbo].[tblSystemSettings]
GO

/****** Object:  Table [dbo].[tblSystemSettings]    Script Date: 5/19/2023 3:33:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblSystemSettings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[keyName] [varchar](128) NOT NULL,
	[keyValue] [varchar](512) NULL,
 CONSTRAINT [PK_tblSystemSettings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



----------------- INSERT DATA 
INSERT INTO dbo.tblTagType (typeName ) VALUES 
('Int'), ('Bool'), ('Real'), ('String');


INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('OutputFileFolder','c:\Users\MCS')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('OutputFileNamePrefix','tag-')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('OutputFileName','yyyy-MM-dd')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('CopyFolder','\\mytfs01\public\EOLT_DataSource\Honda_bulkhead')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('CopyFilePrefix','RejectDetails-tag-')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('CopyFileExt','csv')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('VisitInterval','1502')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('CopyInterval','31000')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('LogFileFolder','c:\temp\log')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('SaveToFile','TRUE')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('SaveToDB','TRUE')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('GetDataFromXML','FALSE')
INSERT INTO dbo.tblSystemSettings (keyname,keyvalue) values ('OutputFileNameExt','csv')



