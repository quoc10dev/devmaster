

/*
	+ Tính ngày bảo dưỡng tiếp theo
	+ Số ngày tới hạn bão dưỡng tiếp theo
	+ Mốc bảo dưỡng tiếp theo
*/
ALTER PROCEDURE [dbo].[sp_TinhHanBaoDuongTheoThangTungXe]
	@ID int,
	@ngayBaoDuongGanNhat smalldatetime out,
	@ngayBaoDuongTiepTheo smalldatetime out,
	@soNgayConLaiChoLanBaoDuongTiepTheo int out,
	@mocBaoDuongTiepTheo int out
AS
BEGIN
	SET NOCOUNT ON
	
	Set @ngayBaoDuongTiepTheo = null
	Set @soNgayConLaiChoLanBaoDuongTiepTheo = null

	-- Ngày hiện tại
	Declare @ngayHienTai smalldatetime
	Set @ngayHienTai = getdate()

	-- Ngày bắt đầu tính bảo dưỡng
	--Declare @ngayBatDauTinhBaoDuong smalldatetime
	--Set @ngayBatDauTinhBaoDuong = null

	-- Số tháng bảo dưỡng định kỳ
	Declare @soThangBaoDuongDinhKy float			
	Select  Top 1 @soThangBaoDuongDinhKy = c.SoLuongBaoDuongDinhKy, @ngayBaoDuongGanNhat = a.NgayBaoDuongGanNhat, @mocBaoDuongTiepTheo = cast(d.GiaTri as int)
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
							left join tblLoaiBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
							left join tblQuyTrinhBaoDuong d on a.IDQuyTrinhBaoDuong = d.ID
	where a.ID = @ID

	-- Tính mốc bảo dưỡng tiếp theo
	Set @mocBaoDuongTiepTheo = @mocBaoDuongTiepTheo + @soThangBaoDuongDinhKy
	
	If (@ngayBaoDuongGanNhat is not null)
	Begin
		Declare @soLanBaoDuong float 
		Set @soLanBaoDuong = 0
	
		-- Tính phần tháng lẻ của một lần bảo dưỡng
		Declare @PhanThangLe float
		Set @PhanThangLe = round(@soThangBaoDuongDinhKy, 0) - @soThangBaoDuongDinhKy
		Set @ngayBaoDuongTiepTheo = @ngayBaoDuongGanNhat
		Set @soNgayConLaiChoLanBaoDuongTiepTheo = DATEDIFF(dd, @ngayHienTai, @ngayBaoDuongTiepTheo) 
	
		-- Tính ngày bảo dưỡng tiếp theo (giá trị lớn hơn gần nhất so với ngày hiện tại)
		Declare @i float 
		Set @i = 1
		While @soNgayConLaiChoLanBaoDuongTiepTheo <= 0 
		Begin
			 Set @i = 1
			 While @i <= @soThangBaoDuongDinhKy 
			 Begin
				Set @ngayBaoDuongTiepTheo = DATEADD(MM, 1, @ngayBaoDuongTiepTheo) 
				Set @i = @i + 1
			 End
			 Set @ngayBaoDuongTiepTheo = DATEADD(dd, @PhanThangLe * 30, @ngayBaoDuongTiepTheo) 
		  
			Set @soNgayConLaiChoLanBaoDuongTiepTheo = DATEDIFF(dd, @ngayHienTai, @ngayBaoDuongTiepTheo) 
			Set @soLanBaoDuong = @soLanBaoDuong + 1
		End
	End
END

