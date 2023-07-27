
GO
/*
	+ Tính số giờ/ số km đi được trong một ngày
*/
CREATE PROCEDURE sp_TinhTanSuatHoatDongThucTeTungNgay
(
	@IDTrangThietBi int,
	@NgayHoatDong smalldatetime 
)
AS
BEGIN
	SET NOCOUNT ON

	Declare @KieuBaoDuong varchar(10)
	Select Top 1 @KieuBaoDuong = e.Ma
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
						   left join tblLoaiBaoDuong d on b.IDLoaiBaoDuong = d.IDLoaiBaoDuong
						   left join tblKieuBaoDuong e on d.IDKieuBaoDuong = e.ID
	Where a.ID = @IDTrangThietBi
	
	-- Số giờ/km đi được hôm nay = Số giờ/km ghi nhận ngày hôm sau - Số giờ/km ghi nhận ngày hôm nay
	Declare @NgayTiepTheo smalldatetime
	Declare @SoLuong float

	If @KieuBaoDuong = 'gio'
	Begin
		Declare @SoGio float	
		Select @SoGio = SoGio from tblNhatKyHoatDong where IDTrangThietBi = @IDTrangThietBi and NgayHoatDong = @NgayHoatDong
		If @SoGio is not null
		Begin
			Set @NgayTiepTheo = DateAdd(day, 1, @NgayHoatDong)
			Declare @SoGioHomSau float
			Select Top 1 @SoGioHomSau = SoGio from tblNhatKyHoatDong where IDTrangThietBi = @IDTrangThietBi and NgayHoatDong = @NgayTiepTheo
			If @SoGioHomSau is not null
			Begin
				Set @SoLuong = Round((@SoGioHomSau - @SoGio), 2)
				If exists(Select 1 From tblTanSuatHoatDongThucTe where IDTrangThietBi = @IDTrangThietBi And NgayHoatDong = @NgayHoatDong)
				Begin
					Update tblTanSuatHoatDongThucTe Set SoLuong = @SoLuong where IDTrangThietBi = @IDTrangThietBi And NgayHoatDong = @NgayHoatDong
				End
				Else
				Begin
					Insert into tblTanSuatHoatDongThucTe(IDTrangThietBi, NgayHoatDong, SoLuong) values(@IDTrangThietBi, @NgayHoatDong, @SoLuong)
				End	
			End
			Else
				Delete tblTanSuatHoatDongThucTe where IDTrangThietBi = @IDTrangThietBi And NgayHoatDong = @NgayHoatDong
		End
	End
	Else If @KieuBaoDuong = 'km'
	Begin
		Declare @SoKm float	
		Select @SoKm = SoKm from tblNhatKyHoatDong where IDTrangThietBi = @IDTrangThietBi and NgayHoatDong = @NgayHoatDong
		If @SoKm is not null
		Begin
			Set @NgayTiepTheo = DateAdd(day, 1, @NgayHoatDong)
			Declare @SoKmHomSau float
			Select Top 1 @SoKmHomSau = SoKm from tblNhatKyHoatDong where IDTrangThietBi = @IDTrangThietBi and NgayHoatDong = @NgayTiepTheo
			If @SoKmHomSau is not null
			Begin
				Set @SoLuong = Round((@SoKmHomSau - @SoKm), 2)
				If exists(Select 1 From tblTanSuatHoatDongThucTe where IDTrangThietBi = @IDTrangThietBi And NgayHoatDong = @NgayHoatDong)
				Begin
					Update tblTanSuatHoatDongThucTe Set SoLuong = @SoLuong where IDTrangThietBi = @IDTrangThietBi And NgayHoatDong = @NgayHoatDong
				End
				Else
				Begin
					Insert into tblTanSuatHoatDongThucTe(IDTrangThietBi, NgayHoatDong, SoLuong) values(@IDTrangThietBi, @NgayHoatDong, @SoLuong)
				End	
			End
			Else
				Delete tblTanSuatHoatDongThucTe where IDTrangThietBi = @IDTrangThietBi And NgayHoatDong = @NgayHoatDong
		End
	End
END

GO
-- exec sp_TinhTanSuatHoatDongThucTeTungNgay 1040, '20190306'   -- 875

--select * from tblNhatKyHoatDong where IDTrangThietBi = 1040

--select * from tblTanSuatHoatDongThucTe
/*
update tblNhatKyHoatDong set SoGio = 875 where ID = 10280

Set IDENTITY_INSERT tblNhatKyHoatDong ON

insert into tblNhatKyHoatDong(ID, IDTrangThietBi, NgayHoatDong, SoGio, SoKm, GhiChu)
values(10280, 1040, '20190307', 874, null, null)

Set IDENTITY_INSERT tblNhatKyHoatDong OFF

delete tblNhatKyHoatDong where ID = 10280

*/
