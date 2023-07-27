
GO
ALTER PROCEDURE [dbo].[sp_Search_NhomTrangThietBi]
   @TenNhom nvarchar(100)=null,
   @PageNo int = 1,
   @PageSize int = 10,
   @SortColumn nvarchar(100) = 'ID',
   @SortOrder nvarchar(4)='DESC',
   @TotalRecord int out
AS
BEGIN
	
	SET NOCOUNT ON
	
	DECLARE @pr_PageNbr int,
			@pr_PageSize int,
			@pr_SortColumn nvarchar(30),
			@pr_FirstRecord int,
			@pr_LastRecord int
    
    SET @pr_PageNbr = @PageNo 
    SET @pr_PageSize = @PageSize
    SET @pr_SortColumn = LTRIM(RTRIM(@SortColumn))

    SET @pr_FirstRecord = (@pr_PageNbr - 1) * @pr_PageSize
    SET @pr_LastRecord = @pr_PageNbr * @pr_PageSize + 1

    ;WITH Results
    AS (
		SELECT ROW_NUMBER() OVER (ORDER BY a.ID ASC) AS RowNum,
				Count(1) over () AS TotalCount, a.ID, a.TenNhom, a.GhiChu
		FROM tblNhomTrangThietBi a 
		WHERE a.TenNhom like '%' + @TenNhom + '%' 
	)
	
	SELECT ROW_NUMBER() OVER (ORDER BY RowNum DESC) AS STT, TotalCount, RowNum, ID, TenNhom, GhiChu
	INTO #tmp
	FROM Results as CPC
	ORDER BY RowNum DESC

	Select Top 1 @TotalRecord = TotalCount from #tmp
    set @TotalRecord = ISNULL(@TotalRecord, 0)

	While (@pr_FirstRecord > @TotalRecord) 
		Set @pr_FirstRecord = @pr_FirstRecord - @PageSize
		
	Select STT, TotalCount, RowNum, ID, TenNhom, GhiChu 
	from #tmp 
	where STT > @pr_FirstRecord AND STT < @pr_LastRecord 
    
END

