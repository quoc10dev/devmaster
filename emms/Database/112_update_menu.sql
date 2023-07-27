

-- menu Phiếu sửa chữa
insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Repair request', 'repair_request', 'maintenance', N'Phiếu sửa chữa', 2, 1, 1, 'RepairRequest.aspx', 'RR')

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2047, 'View repair request', 'repair_request_view', 'Repair repair request', 1)
insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2047, 'Add repair request', 'repair_request_add', 'Add repair request', 1)
insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2047, 'Edit repair request', 'repair_request_edit', 'Edit repair request', 1)
insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2047, 'Delete repair request', 'repair_request_delete', 'Delete repair request', 1)

insert into tblRightsInRoles values(2111, 1)  
insert into tblRightsInRoles values(2112, 1)  
insert into tblRightsInRoles values(2113, 1)  
insert into tblRightsInRoles values(2114, 1)  

-- Sửa lại menu 
--update tblFunctions Set Name = 'Maintenance & Repair' where ID = 2022

-- select * from tblFunctions order by ParentCode desc, DisplayOrder
--select * from tblRights where IdFunction = 2048


-- menu Báo cáo lịch sử sửa chữa
insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Repair request', 'repair_request_report', 'report_management', N'Lịch sử sửa chữa', 8, 1, 1, 'ReportRepairRequest.aspx', 'RR')

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) 
values(2048, 'View repair request report', 'report_repair_request_view', 'Repair request report', 1)

insert into tblRightsInRoles values(2119, 1) 

--update tblFunctions Set Name = 'Repair request' where Id = 2048
