
GO
/*
	+ Tính ngày bảo dưỡng tiếp theo
	+ Số ngày tới hạn bão dưỡng tiếp theo
	+ Tần suất được tính từ định mức thực tế theo N ngày nhập gần nhất
*/
ALTER PROCEDURE sp_TinhHanBaoDuongTheoKmTungXe
	@ID int,
	@tanSuatHoatDong float,
	@ngayNhapGanNhat smalldatetime,
	@soKmNhapGanNhat float,
	@ngayBaoDuongTiepTheo smalldatetime out,
	@soNgayConLaiChoLanBaoDuongTiepTheo int out,
	@soKmMocBaoDuongTiepTheo float out
AS
BEGIN
	SET NOCOUNT ON
	
	-- Lấy số km bảo dưỡng định kỳ: 500, 1000...
	Declare @soKmBaoDuongDinhKy float
	Select  Top 1 @soKmBaoDuongDinhKy = c.SoLuongBaoDuongDinhKy
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
							left join tblLoaiBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
	where a.ID = @ID

	-- Tính số km còn lại tới mốc bão dưỡng tiếp theo
	Declare @soKmBaoDuongTiepTheo float
	Set @soKmBaoDuongTiepTheo = cast(round(@soKmNhapGanNhat, 0) as int) % cast(round(@soKmBaoDuongDinhKy, 0) as int) 
	Set @soKmBaoDuongTiepTheo = @soKmBaoDuongDinhKy - @soKmBaoDuongTiepTheo

	-- Tính số ngày còn lại tới mốc bão dưỡng tiếp theo
	Set @soNgayConLaiChoLanBaoDuongTiepTheo = round((@soKmBaoDuongTiepTheo / @tanSuatHoatDong), 0)

	-- Tính số giờ mốc bão dưỡng tiếp
	Set @soKmMocBaoDuongTiepTheo = ((cast(round(@soKmNhapGanNhat, 0) as int) / cast(round(@soKmBaoDuongDinhKy, 0) as int))  * @soKmBaoDuongDinhKy) + @soKmBaoDuongDinhKy
	
	-- Tính ngày bảo dưỡng tiếp theo, kể từ ngày nhập số giờ gần nhất
	Set @ngayBaoDuongTiepTheo = DATEADD(dd, @soNgayConLaiChoLanBaoDuongTiepTheo, @ngayNhapGanNhat)
END

