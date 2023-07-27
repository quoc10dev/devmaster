
/*
	+ Vận chuyển dữ liệu từ GPS qua xử lý
*/
GO
CREATE PROCEDURE [dbo].[sp_TransferDataFromGPS] 
(
	@StartDate smalldatetime,
	@EndDate smalldatetime
)
AS
BEGIN
	Set Nocount On 

	-- Lấy dữ liệu từ ngày - đến ngày
	Select a.ID, b.ID as IDTrangThietBi, a.BienSo, a.NgayHoatDong, Round(a.SoPhut/60, 2) as SoGio, round(a.SoKm, 2) as SoKm, e.Ma 
	Into #tmp
	From tblSkySoft	a left join tblTrangThietBi b on a.BienSo = b.BienSo
					  left join tblLoaiTrangThietBi c on b.IDLoaiTrangThietBi = c.ID
					  left join tblLoaiBaoDuong d on c.IDLoaiBaoDuong = d.IDLoaiBaoDuong
					  left join tblKieuBaoDuong e on d.IDKieuBaoDuong = e.ID	
	Where (a.IsTransfered = 0 or a.IsTransfered is null) and  b.ID is not null And a.NgayHoatDong >= @StartDate And a.NgayHoatDong <= @EndDate
	Order by a.NgayHoatDong
	
	--Select * from #tmp Order by NgayHoatDong return
	
	-- Duyệt qua các dòng
	Declare @pr_ID bigint, @pr_IDTrangThietBi int
	Declare @pr_NgayHoatDong smalldatetime
	Declare @pr_SoGio float, @pr_SoKm float
	Declare @pr_KieuBaoDuong varchar(20)

	Declare @pr_SoGioTrenDongHo float
	Declare @pr_SoKmTrenDongHo float

	While exists(Select 1 from #tmp)
	Begin
		Select Top 1 @pr_ID = ID, @pr_IDTrangThietBi = IDTrangThietBi, @pr_NgayHoatDong = NgayHoatDong, 
					@pr_SoGio = SoGio, @pr_SoKm = SoKm, @pr_KieuBaoDuong = Ma 
		From #tmp Order by NgayHoatDong
				
		If @pr_KieuBaoDuong = 'gio'
		Begin
			-- Kiểm tra trong bảng tblNhatKyHoatDong: số giờ ngày hôm sau = số giờ đồng hồ ngày đang xét + số giờ đi được hôm nay
			If exists(Select 1 From tblNhatKyHoatDong where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = @pr_NgayHoatDong)
			Begin
				If not exists(Select 1 from tblNhatKyHoatDong where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = dateAdd(day , 1, @pr_NgayHoatDong))
				Begin
					Set @pr_SoGioTrenDongHo = null
					Select Top 1 @pr_SoGioTrenDongHo = isnull(SoGio, 0) From tblNhatKyHoatDong where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = @pr_NgayHoatDong

					Insert into tblNhatKyHoatDong(IDTrangThietBi, NgayHoatDong, SoGio) values(@pr_IDTrangThietBi, dateAdd(day , 1, @pr_NgayHoatDong), round((@pr_SoGioTrenDongHo + @pr_SoGio), 2))

					Delete tblTanSuatHoatDongThucTe Where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = @pr_NgayHoatDong
					Insert into tblTanSuatHoatDongThucTe(IDTrangThietBi, NgayHoatDong, SoLuong) values(@pr_IDTrangThietBi, @pr_NgayHoatDong, @pr_SoGio)

					Update tblSkySoft Set IsTransfered = 1 where ID = @pr_ID 
				End
			End
		End
		Else If @pr_KieuBaoDuong = 'km' or @pr_KieuBaoDuong = 'thang' 
		Begin
			-- Kiểm tra trong bảng tblNhatKyHoatDong: số km ngày hôm sau = số km đồng hồ ngày đang xét + số km đi được hôm nay
			If exists(Select 1 From tblNhatKyHoatDong where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = @pr_NgayHoatDong)
			Begin
				If not exists(Select 1 from tblNhatKyHoatDong where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = dateAdd(day , 1, @pr_NgayHoatDong))
				Begin
					Set @pr_SoKmTrenDongHo = null
					Select Top 1 @pr_SoKmTrenDongHo = isnull(SoKm, 0) From tblNhatKyHoatDong where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = @pr_NgayHoatDong

					Insert into tblNhatKyHoatDong(IDTrangThietBi, NgayHoatDong, SoKm) values(@pr_IDTrangThietBi, dateAdd(day , 1, @pr_NgayHoatDong), round((@pr_SoKmTrenDongHo + @pr_SoKm), 2))

					Delete tblTanSuatHoatDongThucTe Where IDTrangThietBi = @pr_IDTrangThietBi And NgayHoatDong = @pr_NgayHoatDong
					Insert into tblTanSuatHoatDongThucTe(IDTrangThietBi, NgayHoatDong, SoLuong) values(@pr_IDTrangThietBi, @pr_NgayHoatDong, @pr_SoKm)

					Update tblSkySoft Set IsTransfered = 1 where ID = @pr_ID 
				End
			End
		End

		Delete #tmp where ID = @pr_ID
	End
	
END
GO
-- Exec sp_TransferDataFromGPS '20190501', '20190505'



