
-- Hạng mục bảo dưỡng
Begin

Select a.ID, a.Ten as NhomBaoDuong, a.SoThuTuHienThi as STT_Nhom, b.Ten as TenHangMucBaoDuong,b.SoThuTuHienThi as STT_HangMuc 
from tblNhomHangMucBaoDuong a left join tblHangMucBaoDuong b on a.ID = b.IDNhomHangMucBaoDuong
order by a.SoThuTuHienThi, b.SoThuTuHienThi

End

-- Bảo dưỡng
select a.ID, a.Ten, b.IDLoaiBaoDuong, b.Ten, b.SoLuongBaoDuongDinhKy, c.Ten, c.SoLuongMoc 
from tblKieuBaoDuong a left join tblLoaiBaoDuong b on a.ID = b.IDKieuBaoDuong
						left join tblCapBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong

select * from tblTrangThietBi where id = 11

select * from tblKieuBaoDuong
select * from tblLoaiBaoDuong
select * from tblCapBaoDuong

select * from tblFunctions order by ParentCode


update tblTrangThietBi set SoGioBatDauTinhBaoDuong = 270 where ID = 11
update tblTrangThietBi set IDQuyTrinhBaoDuong = 1 where ID = 11


update tblTrangThietBi set TanSuatHoatDong = 1 where ID = 11

select * from tblTanSuatHoatDong


  select * from tblQuyTrinhBaoDuong

  insert into tblQuyTrinhBaoDuong values('T1', 1, '250', 9, 7)
  insert into tblQuyTrinhBaoDuong values('T2', 2, '500', 10, 7)

  insert into tblQuyTrinhBaoDuong values('T3', 3, '750', 9, 7)
  insert into tblQuyTrinhBaoDuong values('T4', 4, '1000', 11, 7)
  insert into tblQuyTrinhBaoDuong values('T5', 5, '1250', 9, 7)
  insert into tblQuyTrinhBaoDuong values('T6', 6, '1500', 10, 7)
  insert into tblQuyTrinhBaoDuong values('T7', 7, '1750', 9, 7)
  insert into tblQuyTrinhBaoDuong values('T8', 8, '2000', 21, 7)


 select * 
from tblTrangThietBi a left join tblLoaiTrangThietBi b on a.IDLoaiTrangThietBi = b.ID
						left join tblLoaiBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
						--left join tblKieuBaoDuong d on c.IDKieuBaoDuong = d.ID
where DonViGhiNhanHoatDong = 'gio'



update tblTrangThietBi set SoGioBatDauTinhBaoDuong = 0 where id in (11, 12, 13, 15)
update tblTrangThietBi set NgayBatDauTinhBaoDuong = '20180101' where id in (11, 12, 13, 15)

select * from tblTrangThietBi

insert into tblTanSuatHoatDong values(11, 1, '20180101')
insert into tblTanSuatHoatDong values(12, 2, '20180101')
insert into tblTanSuatHoatDong values(13, 5, '20180101')
insert into tblTanSuatHoatDong values(15, 10, '20180101')


update tblTrangThietBi set SoGioBatDauTinhBaoDuong = 0 where id  = 11

select a.*, b.* 
from tblLoaiBaoDuong a left join tblCapBaoDuong b on a.IDLoaiBaoDuong = b.IDLoaiBaoDuong
order by b.idLoaiBaoDuong, b.SoLuongMoc