

select * from tblFunctions order by ParentCode, DisplayOrder

-- Thêm menu mẫu in thẻ bảo dưỡng
insert into tblFunctions values('Maintenance request form', 'maintenance_request_form', 'maintenance', 
								N'Mẫu in thẻ bảo dưỡng',6, 1, null, null, 1, 'MaintenanceRequestForm.aspx', 'MF', null)

insert into tblRights values(2036, 'View maintenance request form', 'maintenance_request_form_view', 'View maintenance reques form', 1)
insert into tblRights values(2036, 'Add maintenance request form', 'maintenance_request_form_add', 'Add maintenance reques form', 1)
insert into tblRights values(2036, 'Edit maintenance request form', 'maintenance_request_form_edit', 'Edit maintenance reques form', 1)
insert into tblRights values(2036, 'Delete maintenance request form', 'maintenance_request_form_delete', 'Delete maintenance reques form', 1)

insert into tblRightsInRoles values(2095, 1)
insert into tblRightsInRoles values(2096, 1)
insert into tblRightsInRoles values(2097, 1)
insert into tblRightsInRoles values(2098, 1)