
GO
/*
	+ Tính ngày bảo dưỡng tiếp theo
	+ Số ngày tới hạn bão dưỡng tiếp theo
*/
ALTER PROCEDURE [dbo].[sp_CanhBaoBaoDuongTheoGioTungXe]
	@ID int,
	@ngayBaoDuongTiepTheo smalldatetime out,
	@soNgayConLaiChoLanBaoDuongTiepTheo int out
AS
BEGIN
	SET NOCOUNT ON
	
	-- Ngày hiện tại
	Declare @ngayHienTai smalldatetime
	Set @ngayHienTai = getdate() 

	-- Ngày bắt đầu tính bảo dưỡng
	Declare @ngayBatDauTinhBaoDuong smalldatetime
	
	-- Số km bảo dưỡng định kỳ
	Declare @soGioBaoDuongDinhKy float
	Declare @soGioBatDauTinhBaoDuong float
	Declare @tanSuatHoatDong float			
	Select  Top 1 @soGioBaoDuongDinhKy = b.SoLuongBaoDuongDinhKy, @ngayBatDauTinhBaoDuong = a.NgayBatDauTinhBaoDuong, 
			@soGioBatDauTinhBaoDuong = a.SoGioBatDauTinhBaoDuong
	from tblTrangThietBi a left join tblLoaiBaoDuong b on a.IDLoaiBaoDuong = b.IDLoaiBaoDuong
	where a.ID = @ID

	Select Top 1 @tanSuatHoatDong = Isnull(SoLuongTanSuat, 0) from tblTanSuatHoatDong where IDTrangThietBi = @ID order by NgayBatDauApDung desc

	If (@soGioBaoDuongDinhKy is not null and @ngayBatDauTinhBaoDuong is not null and @tanSuatHoatDong > 0)
	Begin
		--Tính số giờ bảo dưỡng tiếp theo
		-- @soGioBatDauTinhBaoDuong thay bằng số giờ đã chạy được mới nhất/ lớn nhất lấy từ bảng quá trình hoạt động
		Declare @soGioBaoDuongTiepTheo float
		Set @soGioBaoDuongTiepTheo = 0
		Declare @i int
		Set @i = 1
		While @soGioBaoDuongTiepTheo <= @soGioBatDauTinhBaoDuong
		Begin
			Set @soGioBaoDuongTiepTheo = @soGioBaoDuongDinhKy * @i
			Set @i = @i + 1
		End
		--select @soGioBaoDuongTiepTheo
		Set @ngayBaoDuongTiepTheo = @ngayBatDauTinhBaoDuong
		
		While @soGioBatDauTinhBaoDuong <  @soGioBaoDuongTiepTheo
		Begin
			Set @soGioBatDauTinhBaoDuong = @soGioBatDauTinhBaoDuong + @tanSuatHoatDong
			Set @ngayBaoDuongTiepTheo = DATEADD(dd, 1, @ngayBaoDuongTiepTheo) 
		End
		--select @soGioBatDauTinhBaoDuong, @soGioBaoDuongTiepTheo, @ngayBaoDuongTiepTheo
		Set @soNgayConLaiChoLanBaoDuongTiepTheo = DATEDIFF(dd, @ngayHienTai, DATEADD(dd, -1, @ngayBaoDuongTiepTheo)) 
	End
END

GO  
declare @ngayBaoDuongTiepTheo smalldatetime
declare @soNgayConLaiChoLanBaoDuongTiepTheo int
exec sp_CanhBaoBaoDuongTheoGioTungXe 11, @ngayBaoDuongTiepTheo out, @soNgayConLaiChoLanBaoDuongTiepTheo out
select @ngayBaoDuongTiepTheo, @soNgayConLaiChoLanBaoDuongTiepTheo

--select DATEADD(dd, 250, '20180301')