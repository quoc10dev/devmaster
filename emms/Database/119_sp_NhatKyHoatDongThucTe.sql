

GO
ALTER PROCEDURE [dbo].[sp_NhatKyHoatDongThucTe] (
	@StartDate smalldatetime,
	@EndDate smalldatetime,
	@IDCongTy int = null,
	@IDNhomTrangThietBi int = null,
	@IDLoaiTrangThietBi int = null,
	@BienSo nvarchar(50) = ''
)
AS
BEGIN
	SET NOCOUNT ON 

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
	IF OBJECT_ID('tempdb..#tmpNhatKyHoatDongThucTe') IS NOT NULL DROP TABLE #tmpNhatKyHoatDongThucTe

	select  IDTrangThietBi, NgayHoatDong as Ngay, Isnull(Sum(SoLuong), 0) as SoKm_SoGio
	INTO #tmpNhatKyHoatDongThucTe
	from tblTanSuatHoatDongThucTe 
	where NgayHoatDong between @StartDate and @EndDate
	group by IDTrangThietBi, NgayHoatDong
	order by NgayHoatDong

	-- Cập nhật số giờ - km
	UPDATE #tmpResult
	SET #tmpResult.SoKm_SoGio = Isnull(b.SoKm_SoGio, 0)
	FROM #tmpResult a LEFT JOIN #tmpNhatKyHoatDongThucTe b ON a.IDTrangThietBi = b.IDTrangThietBi and a.Ngay = b.Ngay
	
	select IDTrangThietBi, Isnull(Sum(SoKm_SoGio), 0) as Total into #tmpTotal from #tmpResult group by IDTrangThietBi 

	-- Biến dòng thành cột
	Select distinct Ngay into #tmpCol from #tmpResult order by Ngay

	DECLARE @sql nvarchar(max), @col nvarchar(max)
	SELECT @col = (
						SELECT DISTINCT ','+  QUOTENAME(convert(varchar(30), cast([Ngay] as date), 112))
						FROM #tmpCol 
						FOR XML PATH ('')
					)

	SELECT @sql = N'
					SELECT * 
					FROM #tmpResult
					PIVOT (
							MAX([SoKm_SoGio]) FOR [Ngay] IN ('+STUFF(@col,1,1,'')+')
					) as pvt '  

	SELECT @sql = N'Select * into #tmpFinal From (' + @sql + ') A; 

					Alter table #tmpFinal add Total float null default 0;
					
					UPDATE #tmpFinal SET #tmpFinal.Total = #tmpTotal.Total 
					FROM #tmpTotal, #tmpFinal 
					WHERE #tmpTotal.IDTrangThietBi = #tmpFinal.IDTrangThietBi

					Select ROW_NUMBER() OVER (Order by IDNhomTrangThietBi, IDLoaiTrangThietBi, IDTrangThietBi) AS STT, * from #tmpFinal';

	EXEC sp_executesql @sql 
END
GO

exec sp_NhatKyHoatDongThucTe @StartDate='2019-10-01 00:00:00',@EndDate='2019-10-31 00:00:00',@IDCongTy=1,@BienSo=N''


