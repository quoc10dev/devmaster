
-- bảng tblNapNhienLieu cho phép nhập null cột SoLuong
alter table tblNapNhienLieu alter column SoLuong float null

-- update tblFunctions
update tblFunctions Set Url = 'OperationProcessingEveryWeek.aspx' where ID = 12
update tblFunctions Set Url = 'FuleProcessingEveryWeek.aspx' where ID = 13



--select * from tblFunctions order by ParentCode, DisplayOrder

-- thêm 2 menu báo cáo
insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Operation processing', 'report_operation_processing', 'report_management', N'Báo cáo quá trình hoạt động TTB', 4, 1, 1, 'ReportOperationProcessing.aspx', 'OP')

insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Fule processing', 'report_fule_processing', 'report_management', N'Báo cáo quá trình nạp nhiên liệu', 5, 1, 1, 'ReportFuleProcessing.aspx', 'FP')


-- select * from tblFunctions
-- select * from tblRights
--select * from tblRightsInRoles

-- 2041 2042

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status])
values(2041, 'View operation processing report', 'view_report_operation_processing', 'View operation processing report', 1)

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status])
values(2042, 'View fule processing report', 'view_report_fule_processing', 'View fule processing report', 1)

insert into tblRightsInRoles(IdRight, IdRole) values(2104, 1)
insert into tblRightsInRoles(IdRight, IdRole) values(2105, 1)


update tblFunctions Set DisplayOrder = 3 where ID = 2041
update tblFunctions Set DisplayOrder = 4 where ID = 2042
update tblFunctions Set DisplayOrder = 5 where ID = 16