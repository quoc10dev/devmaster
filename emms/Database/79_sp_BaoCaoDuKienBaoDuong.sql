

ALTER PROCEDURE [dbo].[sp_BaoCaoDuKienBaoDuong]
(
	@IDCongTy int = null,
	@IDNhomTrangThietBi int = null,
	@IDLoaiTrangThietBi int = null,
	@BienSo nvarchar(50) = null,
	@SoNgayPhamViCanhBao int = 30,
	@SoNgayGanNhatTinhTanSuat int = 5,
	@BaoDuongTuNgay smalldatetime = null,
	@BaoDuongDenNgay smalldatetime = null
)
AS
BEGIN
	SET NOCOUNT ON

	-- Lọc danh sách TTB
	Select c.TenVietTat as TenCongTy, a.ID as IDTrangThietBi, a.BienSo, a.Ten as TenXe, f.TenNhom,
			d.Ten as LoaiBaoDuong, d.SoLuongBaoDuongDinhKy,
			b.Ten as LoaiTrangThietBi, a.NgayBaoDuongGanNhat, a.SoGioBaoDuongGanNhat, a.SoKmBaoDuongGanNhat, 
			e.Ma as MaKieuBaoDuong,
			(Select Top 1 SoLuongTanSuat From tblTanSuatHoatDong Where IDTrangThietBi = a.ID Order By NgayBatDauApDung Desc) as TanSuatHoatDong
	Into #tmp
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
						   left join tblCongTy c on a.IDCongTy = c.ID
						   left join tblLoaiBaoDuong d on b.IDLoaiBaoDuong = d.IDLoaiBaoDuong
						   left join tblKieuBaoDuong e on d.IDKieuBaoDuong = e.ID
						   left join tblNhomTrangThietBi f on b.IDNhomTrangThietBi = f.ID
	Where (a.IDCongTy = @IDCongTy or @IDCongTy is null or @IDCongTy = 0) And 
			(b.ID = @IDLoaiTrangThietBi or @IDLoaiTrangThietBi is null or @IDLoaiTrangThietBi = 0) And
			(a.BienSo like '%' + @BienSo + '%' or @BienSo is null or @BienSo = '') 
			-- And e.Ma like 'km' 
			And (b.IDNhomTrangThietBi = @IDNhomTrangThietBi or @IDNhomTrangThietBi is null or @IDNhomTrangThietBi = 0)
	Order by c.TenVietTat, b.SoThuTuHienThi
	
	CREATE TABLE #tmpResult (
		IDTrangThietBi int null,
		NhomXe nvarchar(300) null,
		LoaiTrangThietBi nvarchar(300) null,
		TenCongTy nvarchar(100),
		BienSo nvarchar(50) null,
		TenXe nvarchar(300) null,
		MaKieuBaoDuong nvarchar(10) null,
		LoaiBaoDuong nvarchar(200) null,
		NgayBaoDuongGanNhat smalldatetime null,
		SoKmDaChay float null,
		SoGioDaChay float null,
		NgayNhapGanNhat smalldatetime null,
		SoKmGanNhat float null,
		SoGioGanNhat float null,
		TanSuatHoatDong float null,
		NgayBaoDuongTiepTheo smalldatetime null,
		SoNgayConLaiChoLanBaoDuongTiepTheo int null,
		MocBaoDuongTiepTheo float null
	);

	Insert into #tmpResult(IDTrangThietBi, NhomXe, LoaiTrangThietBi, TenCongTy, BienSo, TenXe, MaKieuBaoDuong,
			LoaiBaoDuong, NgayBaoDuongGanNhat, SoKmDaChay, SoGioDaChay, TanSuatHoatDong) 
	Select IDTrangThietBi, TenNhom, LoaiTrangThietBi, TenCongTy, BienSo, TenXe, MaKieuBaoDuong,
			LoaiBaoDuong, NgayBaoDuongGanNhat, SoKmBaoDuongGanNhat, SoGioBaoDuongGanNhat, TanSuatHoatDong
	From #tmp
	
	-- Duyệt qua từng TTB
	Declare @pr_IdTrangThietBi int
	Declare @pr_MaKieuBaoDuong varchar(20)
	declare @pr_NgayBaoDuongTiepTheo smalldatetime
	declare @pr_SoNgayConLaiToiMocBaoDuongTiepTheo int
	declare @pr_MocBaoDuongTiepTheo float  -- mốc số giờ/km lần bảo dưỡng tiếp theo
	declare @pr_TanSuatHoatDong float   -- ngày nhập số giờ/km (nhật ký hoạt động) gần nhất
	declare @pr_NgayNhapLieuGanNhat smalldatetime
	declare @pr_SoLuongNhapGanNhat float  -- số giờ/km của ngày nhập mới nhất
	While exists(Select 1 from #tmp) 
	Begin
		Select Top 1 @pr_IdTrangThietBi = IDTrangThietBi, @pr_MaKieuBaoDuong = MaKieuBaoDuong From #tmp 
		
		Set @pr_NgayBaoDuongTiepTheo = null
		Set @pr_SoNgayConLaiToiMocBaoDuongTiepTheo = null
		Set @pr_MocBaoDuongTiepTheo = null
		Set @pr_TanSuatHoatDong = 0
	
		Declare @TongSoLuong float
		Declare @TongSoNgay float
		Declare @TanSuatTrungBinh float

		select Top (@SoNgayGanNhatTinhTanSuat) NgayHoatDong, SoLuong into #tmpLichSuHoatDong
		from tblTanSuatHoatDongThucTe where IDTrangThietBi = @pr_IdTrangThietBi 
		order by NgayHoatDong desc

		Select Top 1 @pr_NgayNhapLieuGanNhat = NgayHoatDong from #tmpLichSuHoatDong

		Select @TongSoLuong = Sum(SoLuong) From #tmpLichSuHoatDong
		Select @TongSoNgay = Count(1) From #tmpLichSuHoatDong where SoLuong > 0
		Set @pr_TanSuatHoatDong = Round(@TongSoLuong / @TongSoNgay, 2)

		drop table #tmpLichSuHoatDong
		
		If (@pr_MaKieuBaoDuong = 'thang')
		Begin
			exec sp_TinhHanBaoDuongTheoThangTungXe @pr_IdTrangThietBi, @pr_NgayNhapLieuGanNhat out, @pr_NgayBaoDuongTiepTheo out, 
					@pr_SoNgayConLaiToiMocBaoDuongTiepTheo out, @pr_MocBaoDuongTiepTheo out
			
		End
		Else If (@pr_MaKieuBaoDuong = 'km')
		Begin
			-- Lấy số km chạy đã được nhập mới nhất
			Select Top 1 @pr_SoLuongNhapGanNhat = SoKm from tblNhatKyHoatDong 
			where IDTrangThietBi = @pr_IdTrangThietBi and SoKm is not null
			order by NgayHoatDong desc

			Set @pr_NgayNhapLieuGanNhat = DATEADD(dd, 1, @pr_NgayNhapLieuGanNhat)

			exec sp_TinhHanBaoDuongTheoKmTungXe @pr_IdTrangThietBi, 
												@pr_TanSuatHoatDong, 
												@pr_NgayNhapLieuGanNhat,
												 @pr_SoLuongNhapGanNhat, 
												@pr_NgayBaoDuongTiepTheo out, 
												@pr_SoNgayConLaiToiMocBaoDuongTiepTheo out,
												 @pr_MocBaoDuongTiepTheo out

			--select @pr_IdTrangThietBi, @pr_TanSuatHoatDong, @pr_NgayNhapLieuGanNhat, @pr_SoLuongNhapGanNhat, @pr_NgayBaoDuongTiepTheo, 
			-- @pr_SoNgayConLaiToiMocBaoDuongTiepTheo, @pr_MocBaoDuongTiepTheo
		End
		Else If (@pr_MaKieuBaoDuong = 'gio')
		Begin	
			-- Lấy số giờ chạy đã được nhập mới nhất
			Select Top 1 @pr_SoLuongNhapGanNhat = SoGio from tblNhatKyHoatDong 
			where IDTrangThietBi = @pr_IdTrangThietBi and SoGio is not null
			order by NgayHoatDong desc

			Set @pr_NgayNhapLieuGanNhat = DATEADD(dd, 1, @pr_NgayNhapLieuGanNhat)

			exec sp_TinhHanBaoDuongTheoGioTungXe @pr_IdTrangThietBi, 
												 @pr_TanSuatHoatDong, 
												 @pr_NgayNhapLieuGanNhat, 
												 @pr_SoLuongNhapGanNhat,
												 @pr_NgayBaoDuongTiepTheo out, 
												 @pr_SoNgayConLaiToiMocBaoDuongTiepTheo out, 
												 @pr_MocBaoDuongTiepTheo out

			-- select @pr_IdTrangThietBi, @pr_TanSuatHoatDong, @pr_NgayNhapLieuGanNhat, @pr_SoLuongNhapGanNhat, @pr_NgayBaoDuongTiepTheo, 
			-- @pr_SoNgayConLaiToiMocBaoDuongTiepTheo, @pr_soGioMocBaoDuongTiepTheo
		End

		Update #tmpResult 
		Set NgayBaoDuongTiepTheo = @pr_ngayBaoDuongTiepTheo, SoNgayConLaiChoLanBaoDuongTiepTheo = @pr_SoNgayConLaiToiMocBaoDuongTiepTheo,
			MocBaoDuongTiepTheo = @pr_MocBaoDuongTiepTheo, TanSuatHoatDong = @pr_TanSuatHoatDong,
			NgayNhapGanNhat = @pr_NgayNhapLieuGanNhat, SoKmGanNhat = @pr_SoLuongNhapGanNhat, SoGioGanNhat = @pr_SoLuongNhapGanNhat
		Where IDTrangThietBi = @pr_IdTrangThietBi

		Delete #tmp where IDTrangThietBi = @pr_IdTrangThietBi
	End

	Select ROW_NUMBER() OVER (ORDER BY DATEDIFF(dd, getdate(), NgayBaoDuongTiepTheo) ASC, SoNgayConLaiChoLanBaoDuongTiepTheo ASC, MaKieuBaoDuong ASC) AS STT,
			IDTrangThietBi, NhomXe, LoaiTrangThietBi, TenXe, BienSo, MaKieuBaoDuong, LoaiBaoDuong, 
			NgayBaoDuongGanNhat, 
			(case when MaKieuBaoDuong = 'km' then SoKmDaChay when MaKieuBaoDuong = 'gio' then SoGioDaChay End)  as SoKmGioDaChay,
			NgayNhapGanNhat,
			(case when MaKieuBaoDuong = 'km' then SoKmGanNhat when MaKieuBaoDuong = 'gio' then SoGioGanNhat End)  as SoKmGioNhapGanNhat,
			TanSuatHoatDong, NgayBaoDuongTiepTheo, MocBaoDuongTiepTheo, SoNgayConLaiChoLanBaoDuongTiepTheo,
			DATEDIFF(dd, getdate(), NgayBaoDuongTiepTheo) as SoNgayQuaHan
	From #tmpResult
	Where SoNgayConLaiChoLanBaoDuongTiepTheo is not null 
		And (NgayBaoDuongTiepTheo >= @BaoDuongTuNgay or @BaoDuongTuNgay is null)
		And (NgayBaoDuongTiepTheo <= @BaoDuongDenNgay or @BaoDuongDenNgay is null)

	Drop table #tmp
	Drop table #tmpResult
END

GO
exec sp_BaoCaoDuKienBaoDuong @IDCongTy=1,@BienSo=N'',@SoNgayPhamViCanhBao=30, @SoNgayGanNhatTinhTanSuat = 10, 
@BaoDuongTuNgay = '20190101', @BaoDuongDenNgay = '20191230'
