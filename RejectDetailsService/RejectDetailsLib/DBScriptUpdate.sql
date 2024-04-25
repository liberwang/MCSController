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

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spGetShiftCount'))
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

	SELECT ISNULL(serial_number, '') AS serial_number, tag_cont 
	INTO #tag_temp 
	FROM dbo.tblTagContent WITH(NOLOCK)
	WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd
	AND ( @prodName != 'BOXSTEP' OR tag_name LIKE 'Station60_Data.Header.State.OK') ;

	DECLARE @tag_pass INT;
	DECLARE	@tag_reject INT;

	SET @tag_pass = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE serial_number != '' AND LEFT(serial_number,7) != 'Reject:' );
	IF @prodName = 'Honda-BulkHead' 
		SET @tag_reject = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE serial_number = '' OR LEFT(serial_number, 7) = 'Reject:' );
	ELSE IF @prodName = 'BOXSTEP'
		SET @tag_reject = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE tag_cont = 'False'  );
	ELSE
		SET @tag_reject = (SELECT COUNT(*) FROM #tag_temp WHERE serial_number = '' );

	SELECT @prodName AS prod_name,  
		@tag_pass + @tag_reject AS tag_cnt,
		@tag_pass AS pass_cnt,
		@tag_reject AS reject_cnt

END
GO

/****** Object:  Table [dbo].[tblOutput]    Script Date: 6/9/2023 11:35:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS( SELECT 1 FROM sys.tables WHERE name = 'tblOutput' )
BEGIN
	CREATE TABLE [dbo].[tblOutput](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[tagId] [int] NOT NULL,
		[byOrder] [int] NOT NULL,
	 CONSTRAINT [PK_tblOutput] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END 

GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spSetSelectedOutputs'))
	DROP PROCEDURE [dbo].[spSetSelectedOutputs]
GO

CREATE PROCEDURE dbo.spSetSelectedOutputs
	@controllerId int,
	@tagIdList VARCHAR(MAX)
AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM tblOutput 
	WHERE tagId IN ( SELECT tagId FROM tblFullTag WHERE controllerId = @controllerId );


	DECLARE @temp_tag TABLE (orderId int identity(1,1), tagId int);

	INSERT INTO @temp_tag (tagId )
	SELECT value FROM STRING_SPLIT(@tagIdList, ',')
	WHERE value != '';

	INSERT INTO tblOutput (tagId, byOrder)
	SELECT tt.tagId, tt.orderId 
	FROM @temp_tag tt
	JOIN tblFullTag ft ON tt.tagId = ft.tagId
	ORDER BY tt.orderId;

	COMMIT;
END
GO

IF NOT EXISTS( SELECT 1 FROM sys.columns WHERE name = 'tagTitle' AND object_name( object_id ) = 'tblFullTag' ) 
BEGIN
	ALTER TABLE tblFullTag ADD tagTitle VARCHAR(1024)
END

GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spGetTagQuery'))
	DROP PROCEDURE [dbo].[spGetTagQuery]
GO

CREATE PROCEDURE [dbo].[spGetTagQuery] 
	-- Add the parameters for the stored procedure here
	@pStartTime DATETIME,
	@pEndTime DATETIME,
	@pipAddress VARCHAR(15) = NULL,
	@ptagName VARCHAR(512) = NULL,
	@ptagValue VARCHAR(512) = NULL,
	@pserialNumber VARCHAR(256) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT tagName, ISNULL(tagTitle, tagName) AS tagTitle, byOrder
	INTO #tmp_title
	FROM tblFullTag ft WITH(NOLOCK)
	JOIN tblOutput ot WITH(NOLOCK) ON ft.tagId = ot.tagId
	WHERE RIGHT(ft.tagName,13) != '.SerialNumber'
	AND ( @ptagName IS NULL OR ( ISNULL(tagTitle, tagName)  LIKE '%' + @ptagName +'%' ) )
	ORDER BY ot.byOrder;

	CREATE INDEX idx_tmp_title ON #tmp_title (tagName, tagTitle);

	IF OBJECT_ID( 'tempdb..##tmp_result') IS NOT NULL
		DROP TABLE ##tmp_result;

	DECLARE @sqlString VARCHAR(MAX) = 'create table ##tmp_result ( [SerialNumber] varchar(256), [startTime] datetime, [endTime] datetime,';

	SELECT @sqlString = @sqlString + '[' + tagTitle + '] varchar(512),'
	FROM #tmp_title
	ORDER BY byOrder;

	SET @sqlString = LEFT( @sqlString, LEN(@sqlString) - 1) + '); create index idx_tmp_result on ##tmp_result (SerialNumber);';

	EXEC (@sqlString);

	SELECT tag_cont, controller_ip, tag_name, serial_number, tag_add_dt
	INTO #tmp_content
	FROM tblTagContent WITH(NOLOCK)
	WHERE tag_add_dt BETWEEN @pstartTime AND @pendTime 
	AND ( @pipAddress IS NULL OR controller_ip = @pipAddress ) 
	--AND ( @ptagName IS NULL OR tag_name like '%' + @ptagName +'%' )
	AND ( @ptagValue IS NULL OR tag_cont like '%' + @ptagValue + '%')
	AND ( @pserialNumber IS NULL OR serial_number like '%' + @pserialNumber + '%' );

	INSERT INTO ##tmp_result ([SerialNumber], [startTime], [endTime]) 
	SELECT serial_number, MIN(tag_add_dt), MAX(tag_add_dt)
	FROM #tmp_content
	WHERE serial_number != ''
	GROUP BY serial_number
	ORDER BY serial_number;

	DECLARE @tag_Name VARCHAR(512);
	DECLARE @tag_title VARCHAR(512);

	DECLARE cursor_content CURSOR FOR
	SELECT tagName, tagTitle 
	FROM #tmp_title

	OPEN cursor_content 
	FETCH NEXT FROM cursor_content INTO @tag_name, @tag_title 
	WHILE @@FETCH_STATUS = 0 
	BEGIN
		SET @sqlString = 'update tr set [' + @tag_title + '] = tc.tag_cont 
			from ##tmp_result tr 
			join #tmp_content tc on tr.SerialNumber COLLATE DATABASE_DEFAULT = tc.serial_number COLLATE DATABASE_DEFAULT 
			where tc.tag_name = ''' + @tag_Name + ''';'; 

		EXEC (@sqlString);

		FETCH NEXT FROM cursor_content INTO @tag_name, @tag_title 
	END 
	CLOSE cursor_content;
	DEALLOCATE cursor_content;
	
	SELECT * FROM ##tmp_result ORDER BY [SerialNumber];

	DROP TABLE #tmp_title;
	DROP TABLE ##tmp_result;
	DROP TABLE #tmp_content;
END

GO


IF EXISTS(SELECT 1 FROM SYS.INDEXES WHERE NAME = 'uind_tagName_tblFullTag' )
	DROP INDEX uind_tagName_tblFullTag ON tblFullTag;
GO

/****** Object:  Index [uind_tagName_tblFullTag]    Script Date: 8/7/2023 8:55:46 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uind_tagName_tblFullTag] ON [dbo].[tblFullTag]
(
	[tagName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

IF EXISTS(SELECT 1 FROM SYS.INDEXES WHERE NAME = 'uind_tagTitle_tblFullTag' )
	DROP INDEX uind_tagTitle_tblFullTag ON tblFullTag;
GO

/****** Object:  Index [uind_tagName_tblFullTag]    Script Date: 8/7/2023 8:55:46 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uind_tagTitle_tblFullTag] ON [dbo].[tblFullTag]
(
	[tagTitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS( SELECT 1 FROM sys.objects where name = 'DF__tblFullTag_tagOutput' )
	ALTER TABLE [dbo].[tblFullTag] ADD CONSTRAINT [DF__tblFullTag_tagOutput] DEFAULT ((0)) FOR [tagOutput]
GO

IF NOT EXISTS( SELECT 1 FROM dbo.tblTagType WHERE typeName = 'DINT' ) 
	INSERT INTO dbo.tblTagType (typeName) VALUES ('DINT' );
GO 

IF NOT EXISTS( SELECT 1 FROM dbo.tblTagType WHERE typeName = 'SINT' ) 
	INSERT INTO dbo.tblTagType (typeName) VALUES ('SINT' );
GO 

IF NOT EXISTS( SELECT 1 FROM dbo.tblTagType WHERE typeName = 'LINT' ) 
	INSERT INTO dbo.tblTagType (typeName) VALUES ('LINT' );
GO 

IF NOT EXISTS( SELECT 1 FROM dbo.tblTagType WHERE typeName = 'FLOAT64' ) 
	INSERT INTO dbo.tblTagType (typeName) VALUES ('FLOAT64' );
GO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spGetSumFaultTimeByStations'))
	DROP PROCEDURE [dbo].[spGetSumFaultTimeByStations]
GO

CREATE PROCEDURE [dbo].[spGetSumFaultTimeByStations]
@dtStart DATETIME,
@dtEnd DATETIME 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT tag_name, SUM( TRY_CAST(tag_cont AS INT) ) AS SumFaultTime 
	INTO #tmpFault
	FROM tblTagContent WITH(NOLOCK) 
	WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd
	AND RIGHT( tag_name, 10 ) = '.FaultTime'
	GROUP BY tag_name
	
	SELECT tag_name, SUM (SumFaultTime) AS SumFaultTime
	FROM ( 
		SELECT CASE RIGHT(LEFT(tag_name, 10 ), 1) 
			WHEN 'L' THEN LEFT(tag_name, 10)
			WHEN 'R' THEN LEFT(tag_name, 10)
			ELSE LEFT( tag_name, 9 ) END AS tag_name, SumFaultTime
		FROM #tmpFault ) A 
	GROUP BY A.tag_name 
	ORDER BY 1

	DROP TABLE #tmpFault
END
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.spGetAverageCycleTimeByStations'))
	DROP PROCEDURE [dbo].[spGetAverageCycleTimeByStations]
GO

CREATE PROCEDURE [dbo].[spGetAverageCycleTimeByStations]
@dtStart DATETIME,
@dtEnd DATETIME 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT tag_name, AVG( TRY_CAST(tag_cont AS INT) ) AS avgCycleTime 
	INTO #tmpCycle
	FROM tblTagContent WITH(NOLOCK) 
	WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd
	AND RIGHT( tag_name, 10 ) = '.TotalTime'
	GROUP BY tag_name
	
	SELECT tag_name, AVG (avgCycleTime) AS avgCycleTime
	FROM ( 
		SELECT CASE RIGHT(LEFT(tag_name, 10 ), 1) 
			WHEN 'L' THEN LEFT(tag_name, 10)
			WHEN 'R' THEN LEFT(tag_name, 10)
			ELSE LEFT( tag_name, 9 ) END AS tag_name, avgCycleTime
		FROM #tmpCycle ) A 
	GROUP BY A.tag_name 
	ORDER BY 1

	DROP TABLE #tmpCycle
END
GO