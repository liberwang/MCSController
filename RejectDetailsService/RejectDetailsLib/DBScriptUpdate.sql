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

IF NOT EXISTS ( SELECT 1 FROM sys.columns WHERE NAME = 'isStatistics' AND OBJECT_NAME(object_id) = 'tblController') 
BEGIN 
	ALTER TABLE tblController ADD isStatistics BIT NOT NULL DEFAULT 0;
END 
GO

IF NOT EXISTS( SELECT 1 FROM sys.tables WHERE NAME = 'tblStatisticsContent' ) 
BEGIN 
	CREATE TABLE [dbo].[tblStatisticsContent](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[tag_cont] [varchar](max) NULL,
		[tag_add_dt] [datetime] NULL DEFAULT getdate(),
		[controller_ip] [varchar](15) NULL,
		[tag_name] [varchar](512) NULL
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END 
GO

CREATE OR ALTER PROCEDURE [dbo].[spGetTagQuery] 
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
	JOIN tblController con WITH(NOLOCK) ON ft.controllerId = con.id AND con.isStatistics = 0
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

CREATE OR ALTER PROCEDURE [dbo].[GetStationStatusBySerial]
    @StartTime DATETIME,
    @EndTime DATETIME
AS
BEGIN
    -- Declare variables to hold dynamically constructed SQL and column names
    DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)
    DECLARE @ColumnName AS NVARCHAR(MAX)
    DECLARE @PivotColumns AS NVARCHAR(MAX)

    -- Create a temporary table for station mappings
    IF OBJECT_ID('tempdb..#StationMap') IS NOT NULL
        DROP TABLE #StationMap
    CREATE TABLE #StationMap (StationName NVARCHAR(255), StationStatus NVARCHAR(255))

    -- Populate the temporary table with distinct stations and their status tag names
    INSERT INTO #StationMap (StationName, StationStatus)
    SELECT DISTINCT
        SUBSTRING(tag_name, 1, CHARINDEX('.Header.Status', tag_name) - 1) AS StationName,
        tag_name AS StationStatus
    FROM tblTagContent
    WHERE tag_name LIKE 'station%.Header.Status'

    -- Initialize the column variables to empty string to avoid null issues in concatenation
    SET @ColumnName = ''
    SET @PivotColumns = ''

    -- Select distinct values of the station name pattern which needs to be transformed into columns
    SELECT
        @ColumnName = @ColumnName + QUOTENAME(StationName) + ',',
        @PivotColumns = @PivotColumns + 'MAX(CASE WHEN serial_number IS NOT NULL AND serial_number <> '''' THEN ' + QUOTENAME(StationName) + ' END) AS ' + QUOTENAME(StationName) + ','
    FROM #StationMap
    ORDER BY StationName; -- Reverse the order by using DESC

    -- Remove trailing comma
    SET @ColumnName = LEFT(@ColumnName, LEN(@ColumnName) - 1)
    SET @PivotColumns = LEFT(@PivotColumns, LEN(@PivotColumns) - 1)

    -- Construct the dynamic SQL to perform the pivot table creation and include formatted time columns
    SET @DynamicPivotQuery =
    --N'SELECT serial_number, FORMAT(MIN(tag_add_dt), ''yyyy-MM-dd HH:mm'') AS Start_Time, FORMAT(MAX(tag_add_dt), ''yyyy-MM-dd HH:mm'') AS End_Time, ' + @PivotColumns + '
      N'SELECT serial_number,  ' + @PivotColumns + '

	 FROM
      (
          SELECT
              serial_number,
              tag_add_dt,
              sm.StationName,
              tag_cont
          FROM tblTagContent tc
          INNER JOIN #StationMap sm ON tc.tag_name = sm.StationStatus
          WHERE tag_add_dt BETWEEN @StartTime AND @EndTime
      ) AS SourceTable
      PIVOT
      (
          MAX(tag_cont)
          FOR StationName IN (' + @ColumnName + ')
      ) AS PivotTable
      GROUP BY serial_number
      ORDER BY serial_number DESC;'; -- Reverse the order by using DESC

    -- Print the dynamic SQL for debugging
    PRINT @DynamicPivotQuery;

    -- Execute the dynamic SQL
    EXEC sp_executesql @DynamicPivotQuery, N'@StartTime DATETIME, @EndTime DATETIME', @StartTime, @EndTime;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[spGetAllFullTagNames]
AS
BEGIN
    SET NOCOUNT ON;  -- Stops the message indicating the number of rows affected

    -- Selecting the full tag names from the tblFullTag table
    -- where tagID exists in the tblOutput table and ordering by the tblOutput order
    SELECT ft.tagName
    FROM tblFullTag ft WITH(NOLOCK)
	JOIN tblController con WITH(NOLOCK) ON ft.controllerId = con.id AND con.isStatistics = 0
    INNER JOIN tblOutput o ON ft.tagID = o.tagID
    ORDER BY o.byOrder  -- Adjust this line if a specific order column is available in tblOutput

    -- Index to improve performance on operations involving tagName and tagTitle

END
GO

CREATE OR ALTER PROCEDURE [dbo].[spGetShiftCount]
@prodName VARCHAR(256),
@dtStart DateTime,
@dtEnd DateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ISNULL(serial_number, '') AS serial_number, tag_cont , tag_name
	INTO #tag_temp 
	FROM dbo.tblTagContent WITH(NOLOCK)
	WHERE [tag_add_dt] BETWEEN @dtStart AND @dtEnd
	AND ( @prodName != 'BOXSTEP' OR (tag_name LIKE 'Station60_Data.Header.Status' and tag_cont IN ('1', '2'))) ;

	DECLARE @tag_pass INT;
	DECLARE	@tag_reject INT;

	IF @prodName = 'BOXSTEP'
	SET @tag_pass = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE serial_number != ''  AND  tag_name LIKE 'Station60_Data.Header.Status' and tag_cont = '2' );
    ELSE
	SET @tag_pass = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE serial_number != '' AND LEFT(serial_number,7) != 'Reject:' );


	IF @prodName = 'Honda-BulkHead' 
		SET @tag_reject = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE serial_number = '' OR LEFT(serial_number, 7) = 'Reject:' );
	ELSE IF @prodName = 'BOXSTEP'
		SET @tag_reject = (SELECT COUNT(DISTINCT serial_number) FROM #tag_temp WHERE tag_name LIKE 'Station60_Data.Header.Status' and tag_cont = '1'  );
	ELSE
		SET @tag_reject = (SELECT COUNT(*) FROM #tag_temp WHERE serial_number = '' );

	SELECT @prodName AS prod_name,  
		@tag_pass + @tag_reject AS tag_cnt,
		@tag_pass AS pass_cnt,
		@tag_reject AS reject_cnt

END
GO


CREATE OR ALTER PROCEDURE [dbo].[spGetTagContentByTagName]
    @tagName VARCHAR(255), -- Changed from @stationNamePart to @tagName
	@startTime DATETIME,
    @endTime DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT tag_name, tag_cont, tag_add_dt
    FROM [dbo].[tblTagContent]  -- Replace "YourTableName" with the actual name of your table
    WHERE tag_name LIKE '%' + @tagName + '%' AND
          tag_add_dt BETWEEN @startTime AND @endTime
    ORDER BY tag_add_dt ASC;  -- Orders results from earliest to latest by date
END
GO


CREATE OR ALTER PROCEDURE [dbo].[spGetTagOnePartForOther]
    @pSerialNumber VARCHAR(256),  -- Focused on specific serial number
    @pipAddress VARCHAR(15) = NULL,
    @ptagName VARCHAR(512) = NULL,
    @ptagValue VARCHAR(512) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Temporary table for tag titles
    SELECT tagName, ISNULL(tagTitle, tagName) AS tagTitle, byOrder
    INTO #tmp_title
    FROM tblFullTag ft WITH(NOLOCK)
	JOIN tblController con WITH(NOLOCK) ON ft.controllerId = con.id AND con.isStatistics = 0
    JOIN tblOutput ot WITH(NOLOCK) ON ft.tagId = ot.tagId
    WHERE RIGHT(ft.tagName, 13) != '.SerialNumber'
    AND ( @ptagName IS NULL OR ( ISNULL(tagTitle, tagName) LIKE '%' + @ptagName + '%' ) )
    ORDER BY ot.byOrder;

    -- Creating index on the temporary table
    CREATE INDEX idx_tmp_title ON #tmp_title (tagName, tagTitle);

    -- Checking and dropping a global temporary table if exists
    IF OBJECT_ID('tempdb..##tmp_result_other') IS NOT NULL
        DROP TABLE ##tmp_result_other;

    -- Building SQL string for creating global temporary table
    DECLARE @sqlString VARCHAR(MAX) = 'CREATE TABLE ##tmp_result_other ([SerialNumber] varchar(256),';

    SELECT @sqlString = @sqlString + '[' + tagTitle + '] varchar(512),'
    FROM #tmp_title
    ORDER BY byOrder;

    SET @sqlString = LEFT(@sqlString, LEN(@sqlString) - 1) + '); CREATE INDEX idx_tmp_result_other ON ##tmp_result_other (SerialNumber);';

    EXEC (@sqlString);

    -- Inserting data into global temporary table
    SELECT tag_cont, controller_ip, tag_name, serial_number
    INTO #tmp_content_other
    FROM tblTagContent WITH(NOLOCK)
    WHERE serial_number = @pSerialNumber
    AND (@pipAddress IS NULL OR controller_ip = @pipAddress)
    AND (@ptagValue IS NULL OR tag_cont LIKE '%' + @ptagValue + '%');

    INSERT INTO ##tmp_result_other ([SerialNumber])
    SELECT DISTINCT serial_number
    FROM #tmp_content_other;

    -- Cursor for updating global temporary table
    DECLARE @tag_Name VARCHAR(512);
    DECLARE @tag_title VARCHAR(512);

    DECLARE cursor_content_other CURSOR FOR
    SELECT tagName, tagTitle
    FROM #tmp_title

    OPEN cursor_content_other
    FETCH NEXT FROM cursor_content_other INTO @tag_name, @tag_title
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @sqlString = 'UPDATE tr SET [' + @tag_title + '] = tc.tag_cont
        FROM ##tmp_result_other tr
        JOIN #tmp_content_other tc ON tr.SerialNumber COLLATE DATABASE_DEFAULT = tc.serial_number COLLATE DATABASE_DEFAULT
        WHERE tc.tag_name = ''' + @tag_Name + ''';';

        EXEC (@sqlString);

        FETCH NEXT FROM cursor_content_other INTO @tag_name, @tag_title
    END
    CLOSE cursor_content_other;
    DEALLOCATE cursor_content_other;

    -- Selecting results and cleaning up
    SELECT * FROM ##tmp_result_other WHERE SerialNumber = @pSerialNumber;

    DROP TABLE #tmp_title;
    DROP TABLE ##tmp_result_other;
    DROP TABLE #tmp_content_other;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[spGetTotalOperationalTimeByStationAndSerial]
    @stationIdentifier VARCHAR(20), -- Input parameter for alphanumeric station identifier
    @startTime DATETIME, -- Start date and time for the filtering period
    @endTime DATETIME -- End date and time for the filtering period
AS
BEGIN
    SET NOCOUNT ON;

    -- Select various operational time metrics, the latest update time for each serial number, and order the results by this time
    SELECT
        serial_number,
        MAX(tag_add_dt) AS LatestUpdateTime, -- Add a column to display the latest update time for each serial number
        SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.TotalTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS TotalOperationalTime,
        SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.InputTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS InputTime,
		SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.OperatorTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS OperatorTime,
        SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.MachineTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS MachineTime,
		SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.OutputTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS OutputTime,
        SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.TransferTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS TransferTime,
        SUM(CASE WHEN tag_name =  @stationIdentifier + 'Data.FaultTime' THEN CAST(tag_cont AS INT) ELSE 0 END) AS FaultTime
    FROM [dbo].[tblTagContent]
    WHERE
        (tag_name LIKE  @stationIdentifier + 'Data.%Time') AND
        tag_add_dt BETWEEN @startTime AND @endTime
    GROUP BY
        serial_number
    ORDER BY
        MAX(tag_add_dt) ASC -- Order the results by the latest update time in descending order
END
GO

ALTER TABLE tblController ALTER COLUMN [description] VARCHAR(256) NOT NULL;
GO

/****** Object:  Index [unique_ipaddress_description_tblController]    Script Date: 5/11/2024 6:48:46 PM ******/
IF NOT EXISTS( SELECT 1 FROM sys.indexes WHERE name = 'unique_ipaddress_description_tblController')
	CREATE UNIQUE NONCLUSTERED INDEX [unique_ipaddress_description_tblController] ON [dbo].[tblController]
	(
		[ip_address] ASC,
		[description] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE [name] = 'controller_ip' AND OBJECT_NAME( object_id ) = 'tblStatisticsContent' AND max_length = 256) 
	ALTER TABLE [dbo].[tblStatisticsContent] ALTER COLUMN controller_ip VARCHAR(256) NULL

GO

IF NOT EXISTS( SELECT 1 FROM sys.columns WHERE NAME = 'isAlarm' AND OBJECT_NAME ( OBJECT_ID ) = 'tblController' )
	ALTER TABLE tblController ADD isAlarm BIT NOT NULL DEFAULT (0);
GO

IF NOT EXISTS( SELECT 1 FROM sys.columns WHERE NAME = 'parentTagId' AND OBJECT_NAME ( OBJECT_ID ) = 'tblFullTag' )
	ALTER TABLE tblFullTag ADD parentTagId INT NULL;
GO

IF NOT EXISTS( SELECT 1 FROM sys.tables WHERE NAME = 'tblAlarmContent' ) 
BEGIN 
	CREATE TABLE [dbo].[tblAlarmContent](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[tag_cont] [varchar](max) NULL,
		[tag_add_dt] [datetime] NULL DEFAULT getdate(),
		[controller_ip] [varchar](15) NULL,
		[tag_name] [varchar](512) NULL
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END 
GO