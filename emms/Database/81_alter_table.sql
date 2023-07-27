

-- update table
alter table tblNhomTrangThietBi add TenNhomEng nvarchar(200) default ''
alter table tblLoaiTrangThietBi add TenEng nvarchar(300) default ''

insert into tblSystemSetting(Name, Value, SettingGroup)
values('maintenance_management_SoNgayNhapGanNhatTinhTanSuat', 10, 'maintenance_management')