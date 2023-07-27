


GO
ALTER PROCEDURE sp_GetMaintenanceDetail
(
	@idMaintenance int
)
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tmpHangMucBaoDuong
	(
		IDNhomHangMuc int NULL,
		IDNhomHangMucCha int NULL,
		IDHangMuc int null,
		STT varchar(10) null,
		Ten	nvarchar(200) NULL,
		IsBold bit null,
		TaskNameList nvarchar(max) default '',
		TaskIdList nvarchar(max) default ''
	)
	
	Select a.ID as IDChiTietBaoDuong, a.IDTheBaoDuong, a.IDHangMucBaoDuong as ID, 
			b.Ten, b.SoThuTuHienThi, b.IDNhomHangMucBaoDuong, 
			c.Ten as TenNhomHangMuc, c.SoThuTuHienThi as STT_NhomHangMuc, c.IDParent
	into #tmpChiTietBaoDuong 
	from tblChiTietBaoDuong a left join tblHangMucBaoDuong b on a.IDHangMucBaoDuong = b.ID
							  left join tblNhomHangMucBaoDuong c on b.IDNhomHangMucBaoDuong = c.ID
	where IDTheBaoDuong = @idMaintenance
	Order by c.IDParent, c.SoThuTuHienThi, b.SoThuTuHienThi

	-- Nhóm hạng mục bảo dưỡng cha
	Select ID, Ten, SoThuTuHienThi, IDParent into #tmp1
	from tblNhomHangMucBaoDuong 
	where ID in (Select IDParent from #tmpChiTietBaoDuong IDParent where IDParent is not null group by IDParent)
	order by SoThuTuHienThi

	-- Nhóm hạng mục bảo dưỡng con
	Select ID, Ten, SoThuTuHienThi, IDParent into #tmp2
	from tblNhomHangMucBaoDuong 
	where ID in (Select IDNhomHangMucBaoDuong from #tmpChiTietBaoDuong group by IDNhomHangMucBaoDuong)
	order by SoThuTuHienThi

	Declare @charIndex int
	Set @charIndex = 65

	Declare @IdNhomHangMuc int
	Declare @IdNhomHangMucCha int
	Declare @IdHangMuc int
	Declare @Ten nvarchar(200)
	Declare @STT int
	Declare @IdChiTietBaoDuong int
	While exists(Select 1 from #tmp1)
	Begin
		-- Chèn nhóm hạng mục cha
		Select Top 1 @IdNhomHangMuc = ID, @IdNhomHangMucCha = IDParent, @Ten = Upper(Ten) From #tmp1
		Insert into #tmpHangMucBaoDuong(IDNhomHangMuc, IDNhomHangMucCha, STT, Ten, IsBold) values(@IdNhomHangMuc, @IdNhomHangMucCha, Char(@charIndex), @Ten, 1)

		-- Chèn nhóm hạng mục con
		Declare @IDNhomHangMucCon int
		While exists(Select 1 From #tmp2)
		Begin
			
			Select Top 1 @IDNhomHangMucCon = ID, @Ten = Ten From #tmp2 order by SoThuTuHienThi
			Insert into #tmpHangMucBaoDuong(IDNhomHangMuc, IDNhomHangMucCha, Ten, IsBold) values(@IDNhomHangMucCon, @IdNhomHangMuc, @Ten, 1)
			
			-- Chèn danh sách hạng mục con
			Set @STT = 1
			Select ID, Ten, SoThuTuHienThi, IDChiTietBaoDuong into #tmp3 From #tmpChiTietBaoDuong where IDNhomHangMucBaoDuong = @IDNhomHangMucCon Order by SoThuTuHienThi
			While exists(Select 1 From #tmp3)
			Begin
				Select Top 1 @IdHangMuc = ID, @Ten = Ten, @IdChiTietBaoDuong = IDChiTietBaoDuong From #tmp3 order by SoThuTuHienThi

				Declare @taskNameList nvarchar(max)
				Declare @taskIdList nvarchar(max)
				Set @taskNameList = ''
				Set @taskIdList = ''

				-- Lấy danh sách công việc ứng với từng hạng mục
				Select a.ID, b.ID as IDCongViec, b.MaCongViec, b.TenCongViec
				into #tmpDanhSachCongViec 
				from tblChiTietBaoDuongCongViec a left join tblCongViec b on a.IDCongViec = b.ID
				where IDChiTietBaoDuong = @IdChiTietBaoDuong

				Declare @IdChiTietBaoDuongCongViec int
				Declare @IDCongViec int
				Declare @MaCongViec varchar(10)
				While exists(Select 1 From #tmpDanhSachCongViec)
				Begin
					Select Top 1 @IdChiTietBaoDuongCongViec = ID, @IDCongViec = IDCongViec, @MaCongViec= MaCongViec  From #tmpDanhSachCongViec

					If (@taskNameList = '')
					Begin
						Set @taskNameList = @MaCongViec
						Set @taskIdList = cast(@IDCongViec as varchar(20))
					End
					Else
					Begin
						Set @taskNameList = @taskNameList+ ',' + @MaCongViec
						Set @taskIdList = @taskIdList + ',' + cast(@IDCongViec as varchar(20))
					End

					Delete #tmpDanhSachCongViec where ID = @IdChiTietBaoDuongCongViec
				End
				Drop table #tmpDanhSachCongViec

				Insert into #tmpHangMucBaoDuong(IDNhomHangMuc, IDNhomHangMucCha, IDHangMuc, STT, Ten, IsBold, TaskNameList, TaskIdList)
				values(@IDNhomHangMucCon, @IdNhomHangMucCha, @IdHangMuc, @STT, @Ten, 0, @taskNameList, @taskIdList)

				Set @STT = @STT + 1
				Delete #tmp3 where ID = @IdHangMuc
			End
			Drop table #tmp3
			
			Set @charIndex = @charIndex + 1
			Delete #tmp2 where ID = @IDNhomHangMucCon
		End
		Drop table #tmp2
		
		Delete #tmp1 where ID = @IdNhomHangMuc
	End
	Drop table #tmp1
	
	Select * from #tmpHangMucBaoDuong
END

GO
exec dbo.sp_GetMaintenanceDetail 28;

