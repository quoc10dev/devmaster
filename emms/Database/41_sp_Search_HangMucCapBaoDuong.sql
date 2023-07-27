

GO
ALTER PROCEDURE sp_Search_MauInTheBaoDuong
   @IDNhomTrangThietBi int = null,
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
		SELECT ROW_NUMBER() OVER (ORDER BY B.ID DESC) AS RowNum,
				Count(1) over () AS TotalCount, B.ID, B.TenNhom 
		FROM (
				Select IDNhomTrangThietBi 
				from tblHangMucNhomTrangThietBi 
				group by IDNhomTrangThietBi
				) A left join tblNhomTrangThietBi B on A.IDNhomTrangThietBi = B.ID
		WHERE B.ID = @IDNhomTrangThietBi or @IDNhomTrangThietBi is null
	)
	
	SELECT a.TotalCount, a.RowNum, a.ID, a.TenNhom
	INTO #tmp 
	FROM Results a 

	Select Top 1 @TotalRecord = TotalCount from #tmp
    set @TotalRecord = ISNULL(@TotalRecord, 0)

	While (@pr_FirstRecord > @TotalRecord) 
		Set @pr_FirstRecord = @pr_FirstRecord - @PageSize
		
	Select * from #tmp where RowNum > @pr_FirstRecord AND RowNum < @pr_LastRecord   
    
END

