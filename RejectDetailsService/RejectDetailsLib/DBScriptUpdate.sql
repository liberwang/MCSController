USE [MCS]
GO

IF NOT EXISTS ( SELECT 1 FROM SYS.COLUMNS WHERE name = 'tagRead' AND object_name( object_id ) = 'tblFullTag' ) 
BEGIN
	ALTER TABLE tblFullTag ADD tagRead SMALLINT NULL 
END

IF NOT EXISTS ( SELECT 1 FROM SYS.COLUMNS WHERE name = 'tagWrite' AND object_name( object_id ) = 'tblFullTag' ) 
BEGIN
	ALTER TABLE tblFullTag ADD tagWrite SMALLINT NULL 
END

IF NOT EXISTS ( SELECT 1 FROM SYS.COLUMNS WHERE name = 'tagOutput' AND object_name( object_id ) = 'tblFullTag' ) 
BEGIN
	ALTER TABLE tblFullTag ADD tagOutput SMALLINT NULL 
END

IF NOT EXISTS( SELECT 1 FROM sys.columns WHERE name = 'serial_number' AND object_name( object_id ) = 'tblTagContent' ) 
BEGIN
	ALTER TABLE tblTagContent ADD serial_number VARCHAR(256)
END

/****** Object:  StoredProcedure [dbo].[spGetShiftCount]    Script Date: 5/31/2023 10:50:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spGetShiftCount'))
	DROP PROCEDURE [dbo].[spGetShiftCount]
GO

CREATE PROCEDURE [dbo].[spGetShiftCount]
@prodName VARCHAR(256),
@dtStart DateTime,
@dtEnd DateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ISNULL(serial_number, '') AS serial_number 
	INTO #tag_temp 
	FROM dbo.tblTagContent WITH(NOLOCK)
	WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd;

	DECLARE @tag_pass INT;
	DECLARE	@tag_reject INT;

	SET @tag_pass = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE serial_number != '');
	SET @tag_reject = (SELECT COUNT(*) FROM #tag_temp WHERE serial_number = '' );

	--SELECT @prodName AS prod_name,  
	--(SELECT COUNT(DISTINCT serial_number) FROM dbo.tblTagContent WITH(NOLOCK)
	--WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd AND ISNULL(serial_number, '') != '' ) AS tag_cnt, 
	--(SELECT COUNT(*) FROM dbo.tblTagContent WITH(NOLOCK)
	--WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd AND ISNULL(serial_number, '') = '' ) AS reject_cnt 

	SELECT @prodName AS prod_name,  
		@tag_pass + @tag_reject AS tag_cnt,
		@tag_pass AS pass_cnt,
		@tag_reject AS reject_cnt

END
GO
