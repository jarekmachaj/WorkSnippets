DECLARE @ColumnName nvarchar(256) = 'Column to search'
DECLARE @StringSearch nvarchar(256) = 'search val'

DECLARE @TableCount int 
DECLARE @SQL nvarchar(500)
DECLARE @ParamDefinition nvarchar(500)
DECLARE @TableName nvarchar(256)
DECLARE @Counter int = 0
DECLARE @TableList table (TableName	varchar(256))
DECLARE @TablesMatching table (TableName varchar(256), RecordsMatching int)

-- all tables filtered by column
INSERT INTO @TableList 
SELECT TABLE_NAME From INFORMATION_SCHEMA.COLUMNS Where column_name = @ColumnName

PRINT '------------- PROCESSING -------------'
SELECT @TableCount = COUNT(1) from @TableList
PRINT CONCAT('Tables with ' , @ColumnName, ' column count: ', @TableCount)
PRINT '---------------------------------------'

WHILE @TableCount > 0 
BEGIN
	SET @Counter = 0
	SELECT Top 1 @TableName = TableName from @TableList ORDER BY TableName	
	PRINT CONCAT('PROCESSING TABLE: ', @TableName)
	
	--sp_executesql
	SET @ParamDefinition = N'@FoundCountOUT int OUTPUT';
	SET @SQL = N'Select @FoundCountOUT = count(1) from ' + @TableName +' WHERE CreatedBy Like ''' + @StringSearch +''' having count(1) > 0';	
	EXECUTE  sp_executesql @SQL, @ParamDefinition, @FoundCountOUT = @Counter OUTPUT;
	--sp_executesql

	PRINT CONCAT('Found ', @Counter, ' records in ',  @TableName, ' table');
	
	DELETE 	@TableList WHERE TableName = @TableName
	SELECT @TableCount = COUNT(1) from @TableList

	IF @Counter > 0
	BEGIN
		INSERT INTO @TablesMatching (TableName, RecordsMatching) VALUES (@TableName, @Counter)
	END	
END

SELECT TableName, RecordsMatching FROM @TablesMatching
 