
ALTER PROCEDURE [dbo].[sp_Search_DinhMucNhienLieu]
   @IDCongTy int = null,
   @IDLoaiTrangThietBi int = null,
   @BienSo nvarchar(50) = null,
   @Ten nvarchar(300) = null,
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
				Count(1) over () AS TotalCount, a.ID, a.IDTrangThietBi, a.SoLuongDinhMuc, a.DonViTinh, a.NgayBatDauApDung,
								b.Ten as TenTrangThietBi, b.MaTaiSan, b.BienSo, c.Ten as LoaiTrangThietBi
		FROM tblDinhMucNhienLieu a left join tblTrangThietBi b on a.IDTrangThietBi = b.ID
									left join tblLoaiTrangThietBi c on b.IDLoaiTrangThietBi = c.ID
		WHERE (b.IDCongTy = @IDCongTy or @IDCongTy is null) And (b.ID = @IDLoaiTrangThietBi or @IDLoaiTrangThietBi is null) 
				And (b.Ten like '%' + @Ten + '%') And (b.BienSo = @BienSo or @BienSo = '' or @BienSo is null)
	)
	
	SELECT a.TotalCount, a.RowNum, a.ID, a.IDTrangThietBi, a.SoLuongDinhMuc, a.DonViTinh, a.NgayBatDauApDung, a.MaTaiSan, 
			a.TenTrangThietBi, a.LoaiTrangThietBi, a.BienSo
	INTO #tmp 
	FROM Results a 
	ORDER BY RowNum ASC

	Select Top 1 @TotalRecord = TotalCount from #tmp
    set @TotalRecord = ISNULL(@TotalRecord, 0)

	While (@pr_FirstRecord > @TotalRecord) 
		Set @pr_FirstRecord = @pr_FirstRecord - @PageSize
		
	Select * from #tmp where RowNum > @pr_FirstRecord AND RowNum < @pr_LastRecord   
    
END

