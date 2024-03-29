
ALTER PROCEDURE sp_SearchFrequency
   @IDLoaiTrangThietBi int = null,	
   @BienSo nvarchar(50) = '',
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
		SELECT ROW_NUMBER() OVER (ORDER BY a.ID DESC) AS RowNum,
				Count(1) over () AS TotalCount, a.ID, a.IDTrangThietBi, a.SoLuongTanSuat, a.NgayBatDauApDung, 
						b.Ten as TrangThietBi, b.BienSo, c.Ten as LoaiTrangThietBi
		FROM tblTanSuatHoatDong a left join tblTrangThietBi b on a.IDTrangThietBi = b.ID
								  left join tblLoaiTrangThietBi c on b.IDLoaiTrangThietBi = c.ID 
		WHERE (c.ID = @IDLoaiTrangThietBi or @IDLoaiTrangThietBi is null or @IDLoaiTrangThietBi = 0) And (b.BienSo like '%' + @BienSo + '%' or @BienSo = '')
	)
	
	SELECT TotalCount, RowNum, ID, LoaiTrangThietBi, TrangThietBi, BienSo, SoLuongTanSuat, NgayBatDauApDung
	INTO #tmp
	FROM Results as CPC
	ORDER BY RowNum ASC

	Select Top 1 @TotalRecord = TotalCount from #tmp
    set @TotalRecord = ISNULL(@TotalRecord, 0)

	While (@pr_FirstRecord > @TotalRecord) 
		Set @pr_FirstRecord = @pr_FirstRecord - @PageSize
		
	Select * from #tmp where RowNum > @pr_FirstRecord AND RowNum < @pr_LastRecord   
    
END

