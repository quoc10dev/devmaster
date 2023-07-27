

select * from tblQuyTrinhBaoDuong


update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K1 (250 <= km < 500)' where ID = 1
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K2 (500 <= km < 750)' where ID = 2
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K3 (750 <= km < 1000)' where ID = 3
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K4 (1000 <= km < 1250)' where ID = 4
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K5 (1250 <= km < 1500)' where ID = 5
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K6 (1500 <= km < 1750)' where ID = 6
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K7 (1750 <= km < 2000)' where ID = 7
update tblQuyTrinhBaoDuong set MaQuyTrinh = 'K8 (2000 <= km < 2250)' where ID = 8

/*
GO
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M1 (1.5 <= month < 3)', 1, '1.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M2 (3 <= month < 4.5)', 2, '3', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M3 (4.5 <= month < 6)', 3, '4.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M4 (6 <= month < 7.5)', 4, '6', 2)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M5 (7.5 <= month < 9)', 5, '7.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M6 (9 <= month < 10.5)', 6, '9', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M7 (10.5 <= month < 12)', 7, '10.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M8 (12 <= month < 13.5)', 8, '12', 3)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M9 (13.5 <= month < 15)', 9, '13.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M10 (15 <= month < 16.5)', 10, '15', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M11 (16.5 <= month < 18)', 11, '16.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M12 (18 <= month < 19.5', 12, '18', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M13 (19.5 <= month < 21', 13, '19.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M14 (21 <= month < 22.5', 14, '21', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M15 (22.5 <= month < 24', 15, '22.5', 23)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M16 (24 <= month < 25.5', 16, '24', 4)

GO
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M1 (3 <= month < 6)', 1, '3', 5)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M2 (6 <= month < 9)', 2, '6', 6)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M3 (9 <= month < 12)', 3, '9', 5)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M4 (12 <= month < 15)', 4, '12', 7)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M5 (15 <= month < 18)', 5, '15', 5)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M6 (18 <= month < 21)', 6, '18', 5)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M7 (21 <= month < 24)', 7, '21', 5)
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M8 (24 <= month < 27)', 8, '24', 24)

GO
insert into tblQuyTrinhBaoDuong(MaQuyTrinh, ThuTu, GiaTri, IDCapBaoDuong) values('M1 (6 <= month <= 6)', 1, '6', 4)
*/