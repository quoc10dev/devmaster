

-- menu báo cáo kế hoạch bảo dưỡng
insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Maintenance plan', 'report_maintenance_plan', 'report_management', N'Báo cáo kế hoạch bảo dưỡng theo tháng', 6, 1, 1, 'ReportMaintenancePlan.aspx', 'MP')

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status])
values(2043, 'Maintenance plan report', 'view_report_maintenance_plan', 'View maintenance plan report', 1)

insert into tblRightsInRoles(IdRight, IdRole) values(2106, 1)

-- menu báo cáo activity history
GO
insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Activity history', 'report_activity_history', 'report_management', N'Báo cáo lịch sử hoạt động', 7, 1, 1, 'ReportActivityHistory.aspx', 'AH')

GO
insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status])
values(2044, 'Activity history report', 'view_report_activity_history', 'View activity history report', 1)

GO
insert into tblRightsInRoles(IdRight, IdRole) values(2107, 1)

/*
select * from tblRights where IdFunction = 2044

select * from tblRightsInRoles where IdRight = 2106

select * from tblFunctions
*/

