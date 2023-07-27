
-- thêm mới cột cho biết có hiển thị cảnh báo đăng kiểm hay không
alter table tblTrangThietBi add CanhBaoDangKiem bit null
update tblTrangThietBi Set CanhBaoDangKiem = 1

-- thêm cột số giờ/km vào thẻ bảo dưỡng, ngày vào xưởng, ngày xuất xưởng
alter table tblTheBaoDuong add SoGioHoacKm float null
alter table tblTheBaoDuong add NgayVaoXuong smalldatetime null
alter table tblTheBaoDuong add NgayXuatXuong smalldatetime null