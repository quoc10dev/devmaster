

/*
select * from tblFunctions order by ParentCode, DisplayOrder
select * from tblRights
select * from tblRightsInRoles
*/

-- Thêm menu tần suất hoạt động
insert into tblFunctions values('Frequency Working', 'frequency_working', 'equipment_list_management', 
								N'Tần suất hoạt động của xe',12, 1, null, null, 1, 'FrequencyWorking.aspx', 'FM', null)

insert into tblRights values(2034, 'View frequency working', 'frequency_working_view', 'View frequency working', 1)
insert into tblRights values(2034, 'Add frequency working', 'frequency_working_add', 'Add frequency working', 1)
insert into tblRights values(2034, 'Edit frequency working', 'frequency_working_edit', 'Edit frequency working', 1)
insert into tblRights values(2034, 'Delete frequency working', 'frequency_working_delete', 'Delete frequency working', 1)

insert into tblRightsInRoles values(2087, 1)
insert into tblRightsInRoles values(2088, 1)
insert into tblRightsInRoles values(2089, 1)
insert into tblRightsInRoles values(2090, 1)

-- Thêm menu thẻ bảo dưỡng
insert into tblFunctions values('Maintenance request', 'maintenance_request', 'maintenance', 
								N'Thẻ bảo dưỡng',13, 1, null, null, 1, 'MaintenanceRequest.aspx', 'MR', null)
insert into tblRights values(2035, 'View maintenance request', 'maintenance_request_view', 'View maintenance reques', 1)
insert into tblRights values(2035, 'Add maintenance request', 'maintenance_request_add', 'Add maintenance reques', 1)
insert into tblRights values(2035, 'Edit maintenance request', 'maintenance_request_edit', 'Edit maintenance reques', 1)
insert into tblRights values(2035, 'Delete maintenance request', 'maintenance_request_delete', 'Delete maintenance reques', 1)

insert into tblRightsInRoles values(2091, 1)
insert into tblRightsInRoles values(2092, 1)
insert into tblRightsInRoles values(2093, 1)
insert into tblRightsInRoles values(2094, 1)

select * from tblRights
