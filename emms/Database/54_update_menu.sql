

-- cập nhật menu danh mục, nghiệp vụ, báo cáo

insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, GroupMenu, ImageMenu, IndexShowInPermission, Url, ShortName, Icon_css)
values('Equipment categories', 'equipment_categories', '', N'Danh mục trang thiết bị', 1, 1, null, null, 1, null, 'EC', 'fas fa-truck-moving')

insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, GroupMenu, ImageMenu, IndexShowInPermission, Url, ShortName, Icon_css)
values('Maintenance categories', 'maintenance_categories', '', N'Danh mục bảo dưỡng - sửa chữa', 2, 1, null, null, 1, null, 'NC', 'fas fa-wrench')

insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, GroupMenu, ImageMenu, IndexShowInPermission, Url, ShortName, Icon_css)
values('Report management', 'report_management', '', N'Quản lý báo cáo', 5, 1, null, null, 1, null, 'RM', 'fas fa-wrench')


update tblFunctions Set ParentCode = 'equipment_categories', Name = 'Company' where ID = 7
update tblFunctions Set ParentCode = 'equipment_categories', Name = 'Equipment group', IndexShowInPermission = 1 where ID = 2037
update tblFunctions Set ParentCode = 'equipment_categories', Name = 'Equipment type' where ID = 8
update tblFunctions Set ParentCode = 'equipment_categories', Name = 'Fuel quota' where ID = 11
update tblFunctions Set ParentCode = 'equipment_categories', Name = 'Frequency working' where ID = 2034

update tblFunctions set ShortName = 'CO' where ID = 7
update tblFunctions set ShortName = 'FW' where ID = 2034

update tblFunctions Set ParentCode = 'maintenance_categories' where ID = 2023
update tblFunctions Set ParentCode = 'maintenance_categories', ShortName = 'MT' where ID = 2032
update tblFunctions Set ParentCode = 'maintenance_categories' where ID = 2026
update tblFunctions Set ParentCode = 'maintenance_categories' where ID = 2024
update tblFunctions Set ParentCode = 'maintenance_categories' where ID = 2025
update tblFunctions Set ParentCode = 'maintenance_categories' where ID = 2036

update tblFunctions Set ParentCode = 'report_management' where ID = 15
update tblFunctions Set ParentCode = 'report_management' where ID = 16
update tblFunctions Set ParentCode = 'report_management' where ID = 2033

update tblFunctions set DisplayOrder = 3 where ID = 5
update tblFunctions set DisplayOrder = 4 where ID = 2022
update tblFunctions set DisplayOrder = 5 where ID = 2027
update tblFunctions set DisplayOrder = 6 where ID = 2040
update tblFunctions set DisplayOrder = 7 where ID = 3
update tblFunctions set DisplayOrder = 0 where ID = 1018

update tblFunctions set DisplayOrder = 4 where ID = 11
update tblFunctions set DisplayOrder = 5 where ID = 2034
update tblFunctions set DisplayOrder = 1 where ID = 10
update tblFunctions set DisplayOrder = 2 where ID = 12
update tblFunctions set DisplayOrder = 3 where ID = 13
update tblFunctions set DisplayOrder = 1 where ID = 2035
update tblFunctions set DisplayOrder = 6 where ID = 2036

update tblFunctions set DisplayOrder = 2 where ID = 2033
update tblFunctions set DisplayOrder = 1 where ID = 15
update tblFunctions set DisplayOrder = 3, Name = 'Fuel consumption' where ID = 16

update tblFunctions set Icon_css = 'fas fa-chart-bar' where ID = 2040
update tblFunctions set Icon_css = 'fas fa-folder-open' where ID = 2038
update tblFunctions set Icon_css = 'fas fa-folder-open' where ID = 2039

update tblFunctions Set Description = N'Quản lý danh sách trang thiết bị' where ID = 5
update tblFunctions Set Description = N'Quản lý bảo dưỡng - sửa chữa trang thiết bị' where ID = 2022
update tblFunctions Set Description = N'Quản lý kho vật tư' where ID = 2027

update tblFunctions Set [Description] = N'Định mức tiêu hao nhiên liệu' where ID = 16

