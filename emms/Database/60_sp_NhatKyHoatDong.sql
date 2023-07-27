

ALTER PROCEDURE sp_NhatKyHoatDong (
	@StartDate smalldatetime,
	@EndDate smalldatetime,
	@IDCongTy int = null,
	@IDNhomTrangThietBi int = null,
	@IDLoaiTrangThietBi int = null,
	@BienSo nvarchar(50) = ''
)
AS
BEGIN
	
	CREATE TABLE #tmpResult 
	(
		IDNhomTrangThietBi int not null,
		TenNhom nvarchar(200) not null,
		IDLoaiTrangThietBi int not null,
		TenLoai nvarchar(300) not null,
		IDTrangThietBi int not null,
		BienSo nvarchar(50) not null,
		Ngay smalldatetime not null,
		SoKm_SoGio float null
	)

	select a.ID, a.BienSo, b.ID as IDLoaiTrangThietBi, b.Ten as TenLoai, c.ID as IDNhomTrangThietBi, c.TenNhom as TenNhom
	into #tmpTTB
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
						   left join tblNhomTrangThietBi c on b.IDNhomTrangThietBi = c.ID
	where (a.IDCongTy = @IDCongTy or @IDCongTy is null) and 
		  (b.ID = @IDLoaiTrangThietBi or @IDLoaiTrangThietBi is null) and 
		  (c.ID = @IDNhomTrangThietBi or @IDNhomTrangThietBi is null) and
		  (a.BienSo = @BienSo or @BienSo = '')

	-- Lấy danh sách các ngày
	IF OBJECT_ID('tempdb..#tmpDateList') IS NOT NULL DROP TABLE #tmpDateList
	CREATE TABLE #tmpDateList ( Ngay smalldatetime NOT NULL) 

	;WITH
	   n AS (SELECT num = ROW_NUMBER() OVER (ORDER BY (SELECT 1)) FROM sys.all_columns a1 CROSS JOIN sys.all_columns a2),
	   dt AS (SELECT [date] = DATEADD(day, n.num - 1, @StartDate) FROM n WHERE n.num BETWEEN 1 AND DATEDIFF(day, @StartDate, @EndDate) + 1)
	INSERT INTO #tmpDateList Select * from dt;

	-- Danh sách TTB
	Declare @pr_IdNhomTrangThietBi int	
	Declare @pr_TenNhom nvarchar(200)	
	Declare @pr_IdLoaiTrangThietBi int
	Declare @pr_TenLoai nvarchar(300)	
	Declare @IdTrangThietBi int	
	Declare @pr_BienSo nvarchar(50)
	While exists(Select ID, BienSo from #tmpTTB)
	BEGIN
		Select Top 1 @pr_IdNhomTrangThietBi = IDNhomTrangThietBi, @pr_TenNhom = TenNhom, 
				@pr_IdLoaiTrangThietBi = IDLoaiTrangThietBi, @pr_TenLoai = TenLoai,
				@IdTrangThietBi = ID, @pr_BienSo = BienSo 
		From #tmpTTB

		Insert into #tmpResult(IDNhomTrangThietBi, TenNhom, IDLoaiTrangThietBi, IDTrangThietBi, TenLoai, BienSo, Ngay)
		select @pr_IdNhomTrangThietBi, @pr_TenNhom, @pr_IdLoaiTrangThietBi, @IdTrangThietBi, @pr_TenLoai, @pr_BienSo, Ngay from #tmpDateList

		Delete #tmpTTB where ID = @IdTrangThietBi
	END 

	-- Dữ liệu nhật ký hoạt động
	IF OBJECT_ID('tempdb..#tmpNhatKyHoatDong') IS NOT NULL DROP TABLE #tmpNhatKyHoatDong

	select  IDTrangThietBi, NgayHoatDong as Ngay, Sum((case when SoKm is not null then SoKm else SoGio end)) as SoKm_SoGio
	INTO #tmpNhatKyHoatDong
	from tblNhatKyHoatDong 
	where NgayHoatDong between @StartDate and @EndDate
	group by IDTrangThietBi, NgayHoatDong
	order by NgayHoatDong

	-- Cập nhật số giờ - km
	UPDATE #tmpResult
	SET #tmpResult.SoKm_SoGio = b.SoKm_SoGio
	FROM #tmpResult a INNER JOIN #tmpNhatKyHoatDong b ON a.IDTrangThietBi = b.IDTrangThietBi and a.Ngay = b.Ngay
	
	-- Biến dòng thành cột
	Select distinct Ngay into #tmpCol from #tmpResult order by Ngay

	DECLARE @sql nvarchar(max), @col nvarchar(max)
	SELECT @col = (
						SELECT DISTINCT ','+  QUOTENAME(convert(varchar(30), cast([Ngay] as date), 112))
						FROM #tmpCol 
						FOR XML PATH ('')
					)
	
	SELECT @sql = N'SELECT *
					FROM #tmpResult
					PIVOT (
						MAX([SoKm_SoGio]) FOR [Ngay] IN ('+STUFF(@col,1,1,'')+')
					) as pvt order by IDNhomTrangThietBi, IDLoaiTrangThietBi, IDTrangThietBi'

	EXEC sp_executesql @sql 
END
GO
EXEC sp_NhatKyHoatDong '20190301', '20190331', null, null, null, ''