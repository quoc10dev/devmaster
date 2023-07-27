

GO
ALTER PROCEDURE [dbo].[sp_TinhDinhMucTheoNgay]
(
	@Ngay smalldatetime,
	@IDTrangThietBi int
)
AS
BEGIN
	Declare @DonViGhiNhanHoatDong varchar(20)

	Select Top 1 @DonViGhiNhanHoatDong = b.DonViGhiNhanHoatDong
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
	where a.ID = @IDTrangThietBi
	
	IF (@DonViGhiNhanHoatDong <> '')
	BEGIN
		Declare @NgayTiepTheo smalldatetime
		Set @NgayTiepTheo = DateAdd(day, 1, @Ngay)

		Declare @SoGioKm_NgayCanTinh float, @SoLit_NgayCanTinh float
		Declare @SoGioKm_NgayTiepTheo float, @SoLit_NgayTiepTheo float

		If (exists(Select 1 from tblNhatKyHoatDong where NgayHoatDong >= @Ngay And NgayHoatDong <= @Ngay and IDTrangThietBi = @IDTrangThietBi) And
			exists(Select 1 from tblNapNhienLieu where NgayNap >= @Ngay And NgayNap <= @Ngay and IDTrangThietBi = @IDTrangThietBi))
		Begin
			If (@DonViGhiNhanHoatDong = 'gio')
				Select Top 1 @SoGioKm_NgayCanTinh = SoGio from tblNhatKyHoatDong where NgayHoatDong >= @Ngay And NgayHoatDong <= @Ngay and IDTrangThietBi = @IDTrangThietBi
			Else if (@DonViGhiNhanHoatDong = 'km')
				Select Top 1 @SoGioKm_NgayCanTinh = SoKm from tblNhatKyHoatDong where NgayHoatDong >= @Ngay And NgayHoatDong <= @Ngay and IDTrangThietBi = @IDTrangThietBi

			Select Top 1 @SoLit_NgayCanTinh = SoLuong from tblNapNhienLieu where NgayNap >= @Ngay And NgayNap <= @Ngay and IDTrangThietBi = @IDTrangThietBi
		End

		If (exists(Select 1 from tblNhatKyHoatDong where NgayHoatDong >= @NgayTiepTheo And NgayHoatDong <= @NgayTiepTheo and IDTrangThietBi = @IDTrangThietBi) And
			exists(Select 1 from tblNapNhienLieu where NgayNap >= @NgayTiepTheo And NgayNap <= @NgayTiepTheo and IDTrangThietBi = @IDTrangThietBi))
		Begin
			If (@DonViGhiNhanHoatDong = 'gio')
				Select Top 1 @SoGioKm_NgayTiepTheo = SoGio from tblNhatKyHoatDong where NgayHoatDong >= @NgayTiepTheo And NgayHoatDong <= @NgayTiepTheo and IDTrangThietBi = @IDTrangThietBi
			else if (@DonViGhiNhanHoatDong = 'km')
				Select Top 1 @SoGioKm_NgayTiepTheo = SoKm from tblNhatKyHoatDong where NgayHoatDong >= @NgayTiepTheo And NgayHoatDong <= @NgayTiepTheo and IDTrangThietBi = @IDTrangThietBi

			Select Top 1 @SoLit_NgayTiepTheo = SoLuong from tblNapNhienLieu where NgayNap >= @NgayTiepTheo And NgayNap <= @NgayTiepTheo and IDTrangThietBi = @IDTrangThietBi
		End
	
		If (@SoGioKm_NgayCanTinh is not null And @SoGioKm_NgayTiepTheo is not null And @SoLit_NgayCanTinh is not null And @SoLit_NgayTiepTheo is not null)
		Begin
			
			Declare @SoGioHoatDong float
			Set @SoGioHoatDong = isnull(@SoGioKm_NgayTiepTheo, 0) - isnull(@SoGioKm_NgayCanTinh,0)

			Declare @SoLitTieuThu float
			Set @SoLitTieuThu = isnull(@SoLit_NgayTiepTheo, 0)

			Declare @DinhMuc float 
			if (@SoLitTieuThu > 0 and @SoGioHoatDong > 0)
				Set @DinhMuc = Round((@SoLitTieuThu / @SoGioHoatDong), 2)
			else
				Set @DinhMuc = 0
		
			If exists(Select 1 From tblDinhMucTheoNgay where IDTrangThietBi = @IDTrangThietBi And Ngay >= @Ngay And Ngay <= @Ngay)
				Update tblDinhMucTheoNgay Set DinhMuc = @DinhMuc Where IDTrangThietBi = @IDTrangThietBi And Ngay >= @Ngay And Ngay <= @Ngay
			Else
				Insert into tblDinhMucTheoNgay(IDTrangThietBi, Ngay, DinhMuc) values(@IDTrangThietBi, @Ngay, @DinhMuc)
		End
	END
END


