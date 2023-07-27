
GO
/*
	+ Tính ngày bảo dưỡng tiếp theo
	+ Số ngày tới hạn bão dưỡng tiếp theo
	+ Tần suất được tính từ định mức thực tế theo N ngày nhập gần nhất
*/
ALTER PROCEDURE sp_TinhHanBaoDuongTheoGioTungXe
	@ID int,
	@tanSuatHoatDong float,
	@ngayNhapGanNhat smalldatetime,
	@soGioNhapGanNhat float,
	@ngayBaoDuongTiepTheo smalldatetime out,
	@soNgayConLaiChoLanBaoDuongTiepTheo int out,
	@soGioMocBaoDuongTiepTheo float out
AS
BEGIN
	SET NOCOUNT ON
	
	-- Lấy số giờ bảo dưỡng định kỳ: 250, 500...
	Declare @soGioBaoDuongDinhKy float
	Select  Top 1 @soGioBaoDuongDinhKy = c.SoLuongBaoDuongDinhKy
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
							left join tblLoaiBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
	where a.ID = @ID

	-- Tính số giờ còn lại tới mốc bão dưỡng tiếp theo
	Declare @soGioBaoDuongTiepTheo float
	Set @soGioBaoDuongTiepTheo = cast(round(@soGioNhapGanNhat, 0) as int) % cast(round(@soGioBaoDuongDinhKy, 0) as int) 
	Set @soGioBaoDuongTiepTheo = @soGioBaoDuongDinhKy - @soGioBaoDuongTiepTheo

	-- Tính số ngày còn lại tới mốc bão dưỡng tiếp theo
	Set @soNgayConLaiChoLanBaoDuongTiepTheo = round((@soGioBaoDuongTiepTheo / @tanSuatHoatDong), 0)

	-- Tính số giờ mốc bão dưỡng tiếp
	Set @soGioMocBaoDuongTiepTheo = ((cast(round(@soGioNhapGanNhat, 0) as int) / cast(round(@soGioBaoDuongDinhKy, 0) as int))  * @soGioBaoDuongDinhKy) + @soGioBaoDuongDinhKy
	
	-- Tính ngày bảo dưỡng tiếp theo, kể từ ngày nhập số giờ gần nhất
	Set @ngayBaoDuongTiepTheo = DATEADD(dd, @soNgayConLaiChoLanBaoDuongTiepTheo, @ngayNhapGanNhat)
	
END

GO
declare @ngayBaoDuongTiepTheo smalldatetime 
declare	@soNgayConLaiChoLanBaoDuongTiepTheo int 
declare @soGioMocBaoDuongTiepTheo float
exec sp_TinhHanBaoDuongTheoGioTungXe 1025, 3.5, '20190407', 2228,
												 @ngayBaoDuongTiepTheo out, 
												 @soNgayConLaiChoLanBaoDuongTiepTheo out, 
												 @soGioMocBaoDuongTiepTheo out
select @ngayBaoDuongTiepTheo, @soNgayConLaiChoLanBaoDuongTiepTheo, @soGioMocBaoDuongTiepTheo
