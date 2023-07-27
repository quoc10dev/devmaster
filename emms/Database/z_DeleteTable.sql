

/*
delete tblCapBaoDuongCongViec
delete tblHangMucLoaiTrangThietBi
delete tblChiTietBaoDuongCongViec
delete tblChiTietBaoDuong
delete tblTheBaoDuong
--delete tblCongViec
delete tblHangMucBaoDuong
delete tblNhomHangMucBaoDuong
delete tblLoaiHuHong
delete tblNapNhienLieu
delete tblNhatKyHoatDong
delete tblDinhMucNhienLieu
delete tblDinhMucTheoNgay
delete tblTanSuatHoatDong
delete tblTrangThietBi
--delete tblQuyTrinhBaoDuong
--delete tblCapBaoDuong
delete tblLoaiTrangThietBi
--delete tblLoaiBaoDuong
delete tblNhomTrangThietBi
--delete tblKieuBaoDuong
delete tblPhongBan
--delete tblCongTy
delete tblCapBaoDuongVatTu
delete tblVatTu
delete tblLoaiVatTu
delete tblNhaCungCap
*/

select * from tblQuyTrinhBaoDuong

SELECT o.NAME, i.rowcnt 
FROM sysindexes AS i INNER JOIN sysobjects AS o ON i.id = o.id 
WHERE i.indid < 2  AND OBJECTPROPERTY(o.id, 'IsMSShipped') = 0
ORDER BY i.rowcnt


select a.Ma, b.IDLoaiBaoDuong, b.Ten, b.SoLuongBaoDuongDinhKy, c.Ten, c.SoLuongMoc, c.ID 
from tblKieuBaoDuong a left join tblLoaiBaoDuong b on a.ID = b.IDKieuBaoDuong
						left join tblCapBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
where b.IDLoaiBaoDuong = 4
order by a.Ma, b.IDLoaiBaoDuong, c.SoLuongMoc

select a.* 
from tblQuyTrinhBaoDuong a left join tblCapBaoDuong b on a.IDCapBaoDuong = b.ID
where b.IDLoaiBaoDuong = 4