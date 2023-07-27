

select * from tblTrangThietBi
select * from tblTanSuatHoatDong
select * from tblQuyTrinhBaoDuong

update tblTrangThietBi set IDQuyTrinhBaoDuong = null
update tblTrangThietBi set SoGioBaoDuongGanNhat = 0
update tblTrangThietBi set NgayBaoDuongGanNhat = '20181127'

exec sp_CanhBaoBaoDuongTheoThangCacXe @IDCongTy=1,@IDLoaiTrangThietBi=0,@BienSo=N''

insert into tblTanSuatHoatDong(IDTrangThietBi, SoLuongTanSuat, NgayBatDauApDung) values(14, 50, '20180101')
insert into tblTanSuatHoatDong(IDTrangThietBi, SoLuongTanSuat, NgayBatDauApDung) values(16, 50, '20180101')
insert into tblTanSuatHoatDong(IDTrangThietBi, SoLuongTanSuat, NgayBatDauApDung) values(17, 50, '20180101')
insert into tblTanSuatHoatDong(IDTrangThietBi, SoLuongTanSuat, NgayBatDauApDung) values(18, 50, '20180101')
insert into tblTanSuatHoatDong(IDTrangThietBi, SoLuongTanSuat, NgayBatDauApDung) values(19, 100, '20180101')



