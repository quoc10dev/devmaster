
GO
ALTER PROCEDURE [dbo].[sp_CanhBaoBaoDuongTheoThangCacXe]
(
	@IDCongTy int = null,
	@IDNhomTrangThietBi int = null,
	@IDLoaiTrangThietBi int = null,
	@BienSo nvarchar(50) = null,
	@SoNgayPhamViCanhBao int = 30
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
			(a.BienSo like '%' + @BienSo + '%' or @BienSo is null or @BienSo = '') And
	--And e.Ma like 'km'
			(b.IDNhomTrangThietBi = @IDNhomTrangThietBi or @IDNhomTrangThietBi is null or @IDNhomTrangThietBi = 0)
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
	declare @pr_SoNgayConLaiChoLanBaoDuongTiepTheo int
	declare @pr_soGioMocBaoDuongTiepTheo float 
	While exists(Select 1 from #tmp) 
	Begin
		Select Top 1 @pr_IdTrangThietBi = IDTrangThietBi, @pr_MaKieuBaoDuong = MaKieuBaoDuong From #tmp 
		
		Set @pr_NgayBaoDuongTiepTheo = null
		Set @pr_SoNgayConLaiChoLanBaoDuongTiepTheo = null
		Set @pr_soGioMocBaoDuongTiepTheo = null

		If (@pr_MaKieuBaoDuong = 'thang')
		Begin
			--Select @pr_IdTrangThietBi
			exec sp_CanhBaoBaoDuongTheoThangTungXe @pr_IdTrangThietBi, @pr_NgayBaoDuongTiepTheo out, @pr_SoNgayConLaiChoLanBaoDuongTiepTheo out
		End
		Else If (@pr_MaKieuBaoDuong = 'km')
		Begin
			--Select @pr_IdTrangThietBi 
			exec sp_CanhBaoBaoDuongTheoKmTungXe @pr_IdTrangThietBi, @pr_NgayBaoDuongTiepTheo out, @pr_SoNgayConLaiChoLanBaoDuongTiepTheo out
		End
		Else If (@pr_MaKieuBaoDuong = 'gio')
		Begin	
			--Select @pr_IdTrangThietBi
			exec sp_CanhBaoBaoDuongTheoGioTungXe @pr_IdTrangThietBi, @pr_NgayBaoDuongTiepTheo out, @pr_SoNgayConLaiChoLanBaoDuongTiepTheo out, @pr_soGioMocBaoDuongTiepTheo out
		End
		Update #tmpResult 
		Set NgayBaoDuongTiepTheo = @pr_ngayBaoDuongTiepTheo, SoNgayConLaiChoLanBaoDuongTiepTheo = @pr_soNgayConLaiChoLanBaoDuongTiepTheo,
			MocBaoDuongTiepTheo = @pr_soGioMocBaoDuongTiepTheo
		Where IDTrangThietBi = @pr_IdTrangThietBi

		Delete #tmp where IDTrangThietBi = @pr_IdTrangThietBi
	End

	--select * from #tmpResult return

	Select IDTrangThietBi, NhomXe, LoaiTrangThietBi, TenXe, BienSo, MaKieuBaoDuong, LoaiBaoDuong, NgayBaoDuongGanNhat, 
			(case when MaKieuBaoDuong = 'km' then SoKmDaChay when MaKieuBaoDuong = 'gio' then SoGioDaChay End)  as SoKmGioDaChay,
			TanSuatHoatDong, NgayBaoDuongTiepTheo, MocBaoDuongTiepTheo, SoNgayConLaiChoLanBaoDuongTiepTheo
	From #tmpResult
	Where SoNgayConLaiChoLanBaoDuongTiepTheo <= @SoNgayPhamViCanhBao
	Order by MaKieuBaoDuong, SoNgayConLaiChoLanBaoDuongTiepTheo

	Drop table #tmp
	Drop table #tmpResult
END
