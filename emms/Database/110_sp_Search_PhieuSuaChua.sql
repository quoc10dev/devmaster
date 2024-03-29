
GO
ALTER PROCEDURE [dbo].[sp_Search_PhieuSuaChua]
   @NgayNhapTuNgay smalldatetime = null,
   @NgayNhapDenNgay smalldatetime = null,
   @NgaySuaChuaTuNgay smalldatetime = null,
   @NgaySuaChuaDenNgay smalldatetime = null,
   @IDCongTy int = null,
   @IDKieuBaoDuong int = null,
   @IDNhomTrangThietBi int = null,
   @IDLoaiTrangThietBi int = null,
   @MaPhieu varchar(50),
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
		SELECT ROW_NUMBER() OVER (ORDER BY a.ID ASC) AS RowNum,
				Count(1) over () AS TotalCount, a.ID, a.MaPhieu, a.NgaySuaChua, a.NguoiSuaChua, b.Ten as TenTrangThietBi, b.BienSo,
							c.Ten as LoaiTrangThietBi, e.Ten as KieuBaoDuong, f.TenNhom
		FROM tblPhieuSuaChua a left join tblTrangThietBi b on a.IDTrangThietBi = b.ID
							  left join tblLoaiTrangThietBi c on b.IDLoaiTrangThietBi = c.ID
							  left join tblLoaiBaoDuong d on c.IDLoaiBaoDuong = d.IDLoaiBaoDuong
							  left join tblKieuBaoDuong e on d.IDKieuBaoDuong = e.ID
							  left join tblNhomTrangThietBi f on c.IDNhomTrangThietBi = f.ID
		WHERE (a.NgayNhap >= @NgayNhapTuNgay And a.NgayNhap <= @NgayNhapDenNgay) And
				(a.NgaySuaChua >= @NgaySuaChuaTuNgay And a.NgaySuaChua <= @NgaySuaChuaDenNgay) And
				(c.ID = @IDLoaiTrangThietBi or @IDLoaiTrangThietBi is null or @IDLoaiTrangThietBi = 0) And
				(e.ID = @IDKieuBaoDuong or @IDKieuBaoDuong is null) And
				(a.MaPhieu like N'%' + @MaPhieu + '%' or @MaPhieu = '') And
				(b.BienSo like N'%' + @BienSo + '%' or @BienSo = '') And
				(b.IDCongTy = @IDCongTy or @IDCongTy is null) And
				(f.ID = @IDNhomTrangThietBi or @IDNhomTrangThietBi is null or @IDNhomTrangThietBi = 0)
	)
	
	SELECT ROW_NUMBER() OVER (ORDER BY RowNum DESC) AS STT, a.TotalCount, a.RowNum, a.ID,  a.MaPhieu, a.NgaySuaChua, a.NguoiSuaChua, a.KieuBaoDuong, 
			a.LoaiTrangThietBi, a.TenTrangThietBi, a.BienSo, a.TenNhom
	INTO #tmp 
	FROM Results a 
	ORDER BY RowNum DESC

	Select Top 1 @TotalRecord = TotalCount from #tmp
    set @TotalRecord = ISNULL(@TotalRecord, 0)

	While (@pr_FirstRecord > @TotalRecord) 
		Set @pr_FirstRecord = @pr_FirstRecord - @PageSize
		
	Select * from #tmp where STT > @pr_FirstRecord AND STT < @pr_LastRecord 
    
END

