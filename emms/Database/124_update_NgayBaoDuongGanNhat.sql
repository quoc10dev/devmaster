



/* Cập nhật ngày bảo dưỡng mới nhất, số km/ số giờ bảo dưỡng mới nhất */
Begin
	SET NOCOUNT ON

	Declare @Id int
	Declare @NgayBaoDuong smalldatetime, @NgayBD smalldatetime
	Declare @Ma varchar(10)
	declare @SoGioHoacKm float

	Select a.Id, a.NgayBaoDuongGanNhat, d.Ma into #tmp 
	
	from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
						left join tblLoaiBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
						left join tblKieuBaoDuong d on c.IDKieuBaoDuong = d.ID

	While exists(select 1 from #tmp)
	Begin
		select Top 1 @Id = ID, @NgayBD = NgayBaoDuongGanNhat, @Ma = Ma from #tmp

		select Top 1 @NgayBaoDuong = NgayBaoDuong, @SoGioHoacKm = SoGioHoacKm
		from tblTheBaoDuong where IDTrangThietBi = @Id order by NgayBaoDuong desc

		/*
		--Select @ID
		print '---------'
		print Cast(@ID as varchar(10))
		print Cast(@NgayBaoDuong as varchar(20))
		print Cast(@NgayBD as varchar(20))
		*/

		update tblTrangThietBi Set NgayBaoDuongGanNhat = @NgayBaoDuong where id = @ID

		if (@SoGioHoacKm is not null)
		Begin
			if @Ma = 'gio'
				update tblTrangThietBi Set SoGioBaoDuongGanNhat = @SoGioHoacKm where id = @ID
			else if @Ma = 'km'
				update tblTrangThietBi Set SoKmBaoDuongGanNhat = @SoGioHoacKm where id = @ID
		End

		delete #tmp where id = @Id
	End
	drop table #tmp
End