
-- menu Báo cáo lịch sử bảo dưỡng
insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Maintenance request', 'maintenance_request_report', 'report_management', N'Báo cáo lịch sử bảo dưỡng', 8, 1, 1, 'ReportMaintenanceRequest.aspx', 'MR')

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2046, 'View data', 'maintenance_request_report_view_data', 'View data', 1)

insert into tblRightsInRoles values(2110, 1)  


-- sửa lại mã thẻ bảo dưỡng
update tblTheBaoDuong Set MaThe = Concat('M', MaThe)  



