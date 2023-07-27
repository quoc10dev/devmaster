
select ID from tblTrangThietBi where BienSo = N'120C-CXR-30701'

select * from tblNhatKyHoatDong where IDTrangThietBi = 1047 And NgayHoatDong >= '20190430' order by NgayHoatDong

select * from tblTanSuatHoatDongThucTe where IDTrangThietBi = 1047 And NgayHoatDong >= '20190430'

update tblTanSuatHoatDongThucTe Set SoLuong = 2.32 where IDTrangThietBi = 1025 And NgayHoatDong = '20190501'

insert into tblTanSuatHoatDongThucTe values(1025, '20190430', 5.68)

delete tblNhatKyHoatDong where NgayHoatDong >= '20190502'

delete tblTanSuatHoatDongThucTe where NgayHoatDong >= '20190501'

update tblTanSuatHoatDongThucTe Set SoLuong = 5.68 where IDTrangThietBi = 1025 And NgayHoatDong = '20190430'



