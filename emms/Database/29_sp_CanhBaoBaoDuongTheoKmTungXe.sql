
GO
/*
	+ Tính ngày bảo dưỡng tiếp theo
	+ Số ngày tới hạn bão dưỡng tiếp theo
*/
ALTER PROCEDURE [dbo].[sp_CanhBaoBaoDuongTheoKmTungXe]
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
	Declare @soKmBaoDuongDinhKy float
	Declare @soKmBatDauTinhBaoDuong float
	Declare @tanSuatHoatDong float			
	Select  Top 1 @soKmBaoDuongDinhKy = b.SoLuongBaoDuongDinhKy, @ngayBatDauTinhBaoDuong = a.NgayBatDauTinhBaoDuong, 
			@soKmBatDauTinhBaoDuong = a.SoKmBatDauTinhBaoDuong
	from tblTrangThietBi a left join tblLoaiBaoDuong b on a.IDLoaiBaoDuong = b.IDLoaiBaoDuong
	where a.ID = @ID

	Select Top 1 @tanSuatHoatDong = Isnull(SoLuongTanSuat, 0) from tblTanSuatHoatDong where IDTrangThietBi = @ID order by NgayBatDauApDung desc

	If (@soKmBaoDuongDinhKy is not null and @ngayBatDauTinhBaoDuong is not null and @tanSuatHoatDong > 0)
	Begin
		--Tính số km bảo dưỡng tiếp theo
		-- @soKmBatDauTinhBaoDuong thay bằng số km đã chạy được lấy từ bảng quá trình hoạt động
		Declare @soKmBaoDuongTiepTheo float
		Set @soKmBaoDuongTiepTheo = 0
		Declare @i int
		Set @i = 1
		While @soKmBaoDuongTiepTheo <= @soKmBatDauTinhBaoDuong
		Begin
			Set @soKmBaoDuongTiepTheo = @soKmBaoDuongDinhKy * @i
			Set @i = @i + 1
		End

		Set @ngayBaoDuongTiepTheo = @ngayBatDauTinhBaoDuong

		While @soKmBatDauTinhBaoDuong <  @soKmBaoDuongTiepTheo
		Begin
			Set @soKmBatDauTinhBaoDuong = @soKmBatDauTinhBaoDuong + @TanSuatHoatDong
			Set @ngayBaoDuongTiepTheo = DATEADD(dd, 1, @ngayBaoDuongTiepTheo) 
		End
		Set @soNgayConLaiChoLanBaoDuongTiepTheo = DATEDIFF(dd, @ngayHienTai, DATEADD(dd, -1, @ngayBaoDuongTiepTheo)) 
	End
END

GO  
declare @ngayBaoDuongTiepTheo smalldatetime
declare @soNgayConLaiChoLanBaoDuongTiepTheo int
exec sp_CanhBaoBaoDuongTheoKmTungXe 16, @ngayBaoDuongTiepTheo out, @soNgayConLaiChoLanBaoDuongTiepTheo out
select @ngayBaoDuongTiepTheo, @soNgayConLaiChoLanBaoDuongTiepTheo
