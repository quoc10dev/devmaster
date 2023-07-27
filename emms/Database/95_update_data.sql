
/*
select * from tblSkySoft where NgayHoatDong = '20190501' 

select * from tblNhatKyHoatDong where NgayHoatDong >= '20190502'

select * from tblTanSuatHoatDongThucTe where NgayHoatDong >= '20190502'

select * from tblNhatKyHoatDong where IDTrangThietBi = 1032 order by NgayHoatDong desc

select * from tblTanSuatHoatDongThucTe where IDTrangThietBi = 1032 order by NgayHoatDong desc

select * from tblNhatKyHoatDong order by NgayHoatDong desc
*/

-- xóa dữ liệu hoạt động sau ngày 01/05/2019
update tblSkySoft set IsTransfered = 0
update tblSkySoft Set IsTransfered = 1 where NgayHoatDong < '20190501'

delete tblTanSuatHoatDongThucTe where NgayHoatDong >= '20190502'
delete tblNhatKyHoatDong where NgayHoatDong >= '20190502'

