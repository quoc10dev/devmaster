
/*
select * from tblNhomHangMucBaoDuong order by IDParent; 

select * from tblHangMucBaoDuong

delete tblNhomHangMucBaoDuong where ID <= 24
update tblNhomHangMucBaoDuong Set Ten = N'Động cơ Toyota' where ID = 25
*/

-- Thân động cơ
insert into tblHangMucBaoDuong values(N'Tiếng kêu lạ, độ rung lúc khởi động, tăng giảm ga', 1, 26)
insert into tblHangMucBaoDuong values(N'Đường hút, xả', 2, 26)
insert into tblHangMucBaoDuong values(N'Lọc không khí , xả', 3, 26)
insert into tblHangMucBaoDuong values(N'Áp suất buồng đốt', 4, 26)

-- Hệ thống nhiên liệu
insert into tblHangMucBaoDuong values(N'Chất lượng tia phun và áp suất phun', 1, 27)
insert into tblHangMucBaoDuong values(N'Vòi phun và kim phun', 2, 27)
insert into tblHangMucBaoDuong values(N'Bầu lọc nhiên liệu', 3, 27)
insert into tblHangMucBaoDuong values(N'Tinh trạng của bơm cao áp', 4, 27)

-- Hệ thống bôi trơn
insert into tblHangMucBaoDuong values(N'Dầu bôi trơn', 1, 28)
insert into tblHangMucBaoDuong values(N'Lọc dầu bôi trơn', 2, 28)
insert into tblHangMucBaoDuong values(N'Rò rỉ hư hỏng trong hệ thống', 3, 28)
insert into tblHangMucBaoDuong values(N'Đồng hồ (đèn) báo áp suất dầu', 4, 28)

-- Hệ thống làm mát
insert into tblHangMucBaoDuong values(N'Rò rỉ trong hệ thống', 1, 29)
insert into tblHangMucBaoDuong values(N'Bề mặt toả nhiệt của két nước', 2, 29)
insert into tblHangMucBaoDuong values(N'Nước làm mát', 3, 29)
insert into tblHangMucBaoDuong values(N'Bình nước phụ', 4, 29)
