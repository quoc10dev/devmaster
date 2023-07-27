
CREATE PROCEDURE sp_BaoCaoDinhMucNhienLieu
(
	@IDCongTy int = null,
	@IDLoaiTrangThietBi int = null,
	@TuNgay smalldatetime = null,
	@DenNgay smalldatetime = null
)
AS
BEGIN
	SET NOCOUNT ON

	-- Lọc danh sách trang thiết bị
	Select b.ID as IDTrangThietBi, a.Ngay, a.DinhMuc 
	Into #tmp
	from tblDinhMucTheoNgay a left join tblTrangThietBi b on a.IDTrangThietBi = b.ID
								left join tblLoaiTrangThietBi c on b.IDLoaiTrangThietBi = c.ID
									left join tblCongTy d on b.IDCongTy = d.ID
	Where Ngay >= @TuNgay And Ngay <= @DenNgay And
			(b.IDCongTy = @IDCongTy or @IDCongTy is null) And
			(c.ID = @IDLoaiTrangThietBi or @IDLoaiTrangThietBi is null)
	Order by d.TenVietTat, c.SoThuTuHienThi, a.Ngay

	-- Tính tổng định mức và số ngày
	Select IDTrangThietBi, Sum(DinhMuc) as TongDinhMuc, Count(1) as SoNgay 
	Into #tmpResult
	From #tmp
	Group by IDTrangThietBi
	
	-- Trả về kết quả định mức trong khoảng thời gian
	Select ROW_NUMBER() OVER (ORDER BY c.SoThuTuHienThi DESC) AS STT, 
			c.Ten as LoaiXe, b.Ten as TenXe, b.BienSo, (TongDinhMuc/SoNgay) as DinhMuc,
			(Select Top 1 SoLuongDinhMuc From tblDinhMucNhienLieu where IDTrangThietBi = a.IDTrangThietBi order by NgayBatDauApDung desc) as DinhMucBanHanh
	from #tmpResult a left join tblTrangThietBi b on a.IDTrangThietBi = b.ID
						left join tblLoaiTrangThietBi c on a.IDTrangThietBi = c.ID
	Order by c.SoThuTuHienThi
END
GO
exec sp_BaoCaoDinhMucNhienLieu 1, null, '20181001', '20181029' 


--select * from tblNhatKyHoatDong