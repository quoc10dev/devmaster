
/*
	+ Báo cáo số km/ số giờ đi được từng ngày
*/
GO
ALTER PROCEDURE sp_ReportSkySoftData (
	@StartDate smalldatetime,
	@EndDate smalldatetime
)
AS
BEGIN
	
	CREATE TABLE #tmpResult 
	(
		BienSo nvarchar(50) null,
		Ngay smalldatetime null,
		SoPhut float null,
		SoKm float null,
		SoPhu_SoKm varchar(100)
	)

	select distinct BienSo into #tmpTTB from tblSkySoft 

	-- Lấy danh sách các ngày
	IF OBJECT_ID('tempdb..#tmpDateList') IS NOT NULL DROP TABLE #tmpDateList
	CREATE TABLE #tmpDateList ( Ngay smalldatetime NOT NULL) 

	;WITH
	   n AS (SELECT num = ROW_NUMBER() OVER (ORDER BY (SELECT 1)) FROM sys.all_columns a1 CROSS JOIN sys.all_columns a2),
	   dt AS (SELECT [date] = DATEADD(day, n.num - 1, @StartDate) FROM n WHERE n.num BETWEEN 1 AND DATEDIFF(day, @StartDate, @EndDate) + 1)
	INSERT INTO #tmpDateList Select * from dt;

	-- Danh sách TTB	
	Declare @pr_BienSo nvarchar(50)
	While exists(Select BienSo from #tmpTTB)
	BEGIN
		Select Top 1 @pr_BienSo = BienSo 
		From #tmpTTB

		Insert into #tmpResult(BienSo, Ngay)
		select @pr_BienSo, Ngay from #tmpDateList

		Delete #tmpTTB where BienSo = @pr_BienSo
	END 

	--Select * from #tmpTTB
	--Select * from #tmpDateList
	--Select * from #tmpResult

	-- Dữ liệu nhật ký hoạt động
	IF OBJECT_ID('tempdb..#tmpNhatKyHoatDongSkySoft') IS NOT NULL DROP TABLE #tmpNhatKyHoatDongSkySoft

	select  BienSo, NgayHoatDong as Ngay, SoPhut, SoKm
	INTO #tmpNhatKyHoatDongSkySoft
	from tblSkySoft 
	where NgayHoatDong between @StartDate and @EndDate
	order by NgayHoatDong

	-- Cập nhật số giờ - km
	UPDATE #tmpResult
	SET #tmpResult.SoPhut = b.SoPhut, #tmpResult.SoKm = b.SoKm,  
				#tmpResult.SoPhu_SoKm = (cast(b.SoPhut as varchar(50)) + ' || ' + cast(round(b.SoKm, 2) as varchar(50)))
	FROM #tmpResult a INNER JOIN #tmpNhatKyHoatDongSkySoft b ON a.BienSo = b.BienSo and a.Ngay = b.Ngay
	
	alter table #tmpResult drop column SoPhut
	alter table #tmpResult drop column SoKm
	-- select * from #tmpResult where SoPhut = 0

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
						MAX([SoPhu_SoKm]) FOR [Ngay] IN ('+STUFF(@col,1,1,'')+')
					) as pvt order by BienSo'

	EXEC sp_executesql @sql 
END
GO

EXEC sp_ReportSkySoftData '20190301', '20190310'