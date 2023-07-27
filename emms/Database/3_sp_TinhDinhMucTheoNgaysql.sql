
CREATE PROCEDURE sp_TinhDinhMucTheoNgay
(
	@Ngay smalldatetime,
	@IDTrangThietBi int
)
AS
BEGIN
	Declare @NgayTiepTheo smalldatetime
	Set @NgayTiepTheo = DateAdd(day, 1, @Ngay)

	Declare @SoGio_NgayCanTinh float, @SoLit_NgayCanTinh float
	Declare @SoGio_NgayTiepTheo float, @SoLit_NgayTiepTheo float

	If (exists(Select 1 from tblNhatKyHoatDong where NgayHoatDong >= @Ngay And NgayHoatDong <= @Ngay) And
		exists(Select 1 from tblNapNhienLieu where NgayNap >= @Ngay And NgayNap <= @Ngay))
	Begin
		Select Top 1 @SoGio_NgayCanTinh = SoGio from tblNhatKyHoatDong where NgayHoatDong >= @Ngay And NgayHoatDong <= @Ngay
		Select Top 1 @SoLit_NgayCanTinh = SoLuong from tblNapNhienLieu where NgayNap >= @Ngay And NgayNap <= @Ngay
	End

	If (exists(Select 1 from tblNhatKyHoatDong where NgayHoatDong >= @NgayTiepTheo And NgayHoatDong <= @NgayTiepTheo) And
		exists(Select 1 from tblNapNhienLieu where NgayNap >= @NgayTiepTheo And NgayNap <= @NgayTiepTheo))
	Begin
		Select Top 1 @SoGio_NgayTiepTheo = SoGio from tblNhatKyHoatDong where NgayHoatDong >= @NgayTiepTheo And NgayHoatDong <= @NgayTiepTheo
		Select Top 1 @SoLit_NgayTiepTheo = SoLuong from tblNapNhienLieu where NgayNap >= @NgayTiepTheo And NgayNap <= @NgayTiepTheo
	End
	
	If (@SoGio_NgayCanTinh is not null And @SoGio_NgayTiepTheo is not null And @SoLit_NgayCanTinh is not null And @SoLit_NgayTiepTheo is not null)
	Begin
		Declare @SoGioHoatDong float
		Set @SoGioHoatDong = @SoGio_NgayTiepTheo - @SoGio_NgayCanTinh

		Declare @SoLitTieuThu float
		Set @SoLitTieuThu = @SoLit_NgayTiepTheo - @SoLit_NgayCanTinh

		Declare @DinhMuc float 
		Set @DinhMuc = @SoLitTieuThu / @SoGioHoatDong
		
		If exists(Select 1 From tblDinhMucTheoNgay where IDTrangThietBi = @IDTrangThietBi And Ngay >= @Ngay And Ngay <= @Ngay)
			Update tblDinhMucTheoNgay Set DinhMuc = @DinhMuc Where IDTrangThietBi = @IDTrangThietBi And Ngay >= @Ngay And Ngay <= @Ngay
		Else
			Insert into tblDinhMucTheoNgay(IDTrangThietBi, Ngay, DinhMuc) values(@IDTrangThietBi, @Ngay, @DinhMuc)
	End

END
GO
Exec sp_TinhDinhMucTheoNgay '20181014', 2


--select * from tblDinhMucTheoNgay

