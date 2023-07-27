

GO
ALTER PROCEDURE sp_LayHangMucBaoDuongTheoLoaiTrangThietBi
(
	@IDLoaiTrangThietBi int
)
AS
BEGIN
	SET NOCOUNT ON

	Select ID as IDHangMucLoaiTTB, IDHangMuc as Item into #tmpHangMucDaChon from tblHangMucLoaiTrangThietBi where IDLoaiTrangThietBi = @IDLoaiTrangThietBi

	-- Lấy danh sách nhóm các hạng mục
	Select a.IDHangMucLoaiTTB, b.ID as IDHangMuc, b.Ten as TenHangMuc, b.SoThuTuHienThi as SoThuTuHienThi_HangMuc,
			c.ID as ID, c.Ten, c.SoThuTuHienThi, c.IDParent
	into #tmpHangMucNhomHangmuc
	from #tmpHangMucDaChon a left join tblHangMucBaoDuong b on a.Item = b.ID
							 left join tblNhomHangMucBaoDuong c on b.IDNhomHangMucBaoDuong = c.ID
						
	Select distinct b.* into #tmp1
	from #tmpHangMucNhomHangmuc a left join tblNhomHangMucBaoDuong b on a.IDParent = b.ID
	where b.IDParent = '' or b.IDParent is null
	Order by b.SoThuTuHienThi

	CREATE TABLE #tmpHangMucBaoDuong
	(
		IDHangMucLoaiTTB int NULL,
		IDNhomHangMuc int NULL,
		IDNhomHangMucCha int NULL,
		IDHangMuc int null,
		STT varchar(10) null,
		Ten	nvarchar(200) NULL,
		IsBold bit null,
		TaskNameList nvarchar(max) default '',
		TaskIdList nvarchar(max) default ''
	)

	Declare @charIndex int
	Set @charIndex = 65

	Declare @IdHangMucNhomHangmuc int
	Declare @IdNhomHangMuc int
	Declare @IdNhomHangMucCha int
	Declare @IdHangMuc int
	Declare @Ten nvarchar(200)
	Declare @STT int
	While exists(Select 1 from #tmp1)
	Begin
		-- Chèn nhóm hạng mục cha
		Select Top 1 @IdNhomHangMuc = ID, @IdNhomHangMucCha = IDParent, @Ten = Upper(Ten) From #tmp1
		Insert into #tmpHangMucBaoDuong(IDNhomHangMuc, IDNhomHangMucCha, STT, Ten, IsBold) values(@IdNhomHangMuc, @IdNhomHangMucCha, Char(@charIndex), @Ten, 1)
		
		-- Chèn nhóm hạng mục con
		Declare @IDNhomHangMucCon int
		Select ID, Ten, SoThuTuHienThi, IDParent into #tmp2 from #tmpHangMucNhomHangmuc where IDParent = @IdNhomHangMuc order by SoThuTuHienThi
		While exists(Select 1 From #tmp2)
		Begin
			
			Select Top 1 @IDNhomHangMucCon = ID, @Ten = Ten From #tmp2 order by SoThuTuHienThi
			Insert into #tmpHangMucBaoDuong(IDNhomHangMuc, IDNhomHangMucCha, Ten, IsBold) values(@IDNhomHangMucCon, @IdNhomHangMuc, @Ten, 1)
			
			-- Chèn loại hạng mục con
			Set @STT = 1
			Select IDHangMucLoaiTTB, IDHangMuc, TenHangMuc, SoThuTuHienThi_HangMuc into #tmp3 From #tmpHangMucNhomHangmuc where ID = @IDNhomHangMucCon Order by SoThuTuHienThi_HangMuc
			While exists(Select 1 From #tmp3)
			Begin
				Select Top 1 @IdHangMucNhomHangmuc = IDHangMucLoaiTTB, @IdHangMuc = IDHangMuc, @Ten = TenHangMuc From #tmp3 order by SoThuTuHienThi_HangMuc

				Insert into #tmpHangMucBaoDuong(IDHangMucLoaiTTB, IDNhomHangMuc, IDNhomHangMucCha, IDHangMuc, STT, Ten, IsBold)
				values(@IDHangMucNhomHangmuc, @IDNhomHangMucCon, @IdNhomHangMucCha, @IdHangMuc, @STT, @Ten, 0)

				Set @STT = @STT + 1
				Delete #tmp3 where IDHangMuc = @IdHangMuc
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

