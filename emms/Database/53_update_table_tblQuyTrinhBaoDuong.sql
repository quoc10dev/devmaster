
-- cập nhật bảng quy trình bảo dưỡng

delete tblQuyTrinhBaoDuong where id = 22

GO
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'K4 (20000 <= km < 25000)' where ID = 17
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'K5 (25000 <= km < 30000)' where ID = 18
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'K6 (30000 <= km < 35000)' where ID = 19
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'K7 (35000 <= km < 40000)' where ID = 20
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'K8 (40000 <= km < 45000)' where ID = 21

GO
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'M12 (18 <= month < 19.5)' where ID = 46
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'M13 (19.5 <= month < 21)' where ID = 47
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'M14 (21 <= month < 22.5)' where ID = 48
update tblQuyTrinhBaoDuong set MaQuyTrinh = N'M15 (22.5 <= month < 24)' where ID = 49

insert tblQuyTrinhBaoDuong values(N'M16 (24 <= month < 25.5)', 16, 24, 4)
delete tblQuyTrinhBaoDuong where id = 50

/*
select a.*, b.IDLoaiBaoDuong, b.Ten, b.SoLuongBaoDuongDinhKy, c.ID, c.Ten, c.SoLuongMoc, d.* 
from tblKieuBaoDuong a left join tblLoaiBaoDuong b on a.ID = b.IDKieuBaoDuong
						left join tblCapBaoDuong c on b.IDLoaiBaoDuong = c.IDLoaiBaoDuong
						left join tblQuyTrinhBaoDuong d on c.ID = d.IDCapBaoDuong
where a.ID = 3 -- and b.IDLoaiBaoDuong = 3
order by a.ID, b.IDLoaiBaoDuong, d.ThuTu

-- select * from tblQuyTrinhBaoDuong
*/