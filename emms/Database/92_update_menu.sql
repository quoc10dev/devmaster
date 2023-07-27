
/*
select * from tblFunctions where ParentCode = 'equipment_list_management' order by ParentCode, DisplayOrder
select * from tblRights
select * from tblRightsInRoles   -- 2107
*/

insert into tblFunctions(Name, Code, ParentCode, [Description], DisplayOrder, ShowInMenu, IndexShowInPermission, Url, ShortName)
values('Get data from GPS', 'transfer_data_from_gps', 'equipment_list_management', N'Lấy dữ liệu từ GPS', 4, 1, 1, 'TransferData.aspx', 'GS')

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2045, 'View data', 'transfer_data_GPS_view_data', 'View data', 1)

insert into tblRights(IdFunction, RightName, RightCode, [Description], [Status]) values(2045, 'Transfer data', 'transfer_data_GPS_transfer_data', 'Transfer data', 1)

insert into tblRightsInRoles values(2108, 1)  
insert into tblRightsInRoles values(2109, 1)

-- chỉnh bảng tblSkySoft
alter table tblSkySoft add IsTransfered bit default 0
update tblSkySoft Set IsTransfered = 0

select * from tblRights



