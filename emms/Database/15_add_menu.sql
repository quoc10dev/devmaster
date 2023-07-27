USE [EMM]
GO
SET IDENTITY_INSERT [dbo].[tblFunctions] ON 
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2022, N'Maintenance management', N'maintenance', NULL, N'Bão dưỡng - sửa chữa trang thiết bị', 3, 1, NULL, NULL, 1, NULL, N'MM', N'fas fa-wrench')
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2023, N'Failure type', N'failure_type', N'maintenance', N'Loại hư hỏng thiết bị', 1, 1, NULL, NULL, 1, N'FailureType.aspx', N'FT', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2024, N'Maintenance group', N'maintenance_group_list', N'maintenance', N'Nhóm hạng mục bão dưỡng', 2, 1, NULL, NULL, 1, N'MaintenanceGroup.aspx', N'MG', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2025, N'Maintenance list', N'maintenance_list', N'maintenance', N'Danh sách các hạng mục bão dưỡng', 3, 1, NULL, NULL, 1, N'MaintenanceList.aspx', N'ML', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2026, N'Maintenance levels', N'maintenance_level', N'maintenance', N'Cấp bão dưỡng', 4, 1, NULL, NULL, 1, N'MaintenanceLevel.aspx', N'ML', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2027, N'Warehouse management', N'warehouse', NULL, N'Kho vật tư', 4, 1, NULL, NULL, 1, NULL, N'WM', N'fas fa-warehouse')
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2028, N'Provider list', N'provider_list', N'warehouse', N'Danh sách nhà cung cấp', 1, 1, NULL, NULL, 1, N'ProviderList.aspx', N'PL', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2029, N'Department list', N'department_list', N'warehouse', N'Danh sách phòng ban sử dụng vật tư', 2, 1, NULL, NULL, 1, N'DepartmentList.aspx', N'DL', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2030, N'Material type list', N'material_type_list', N'warehouse', N'Danh sách loại vật tư - phụ tùng', 3, 1, NULL, NULL, 1, N'MaterialTypeList.aspx', N'MT', NULL)
INSERT [dbo].[tblFunctions] ([ID], [Name], [Code], [ParentCode], [Description], [DisplayOrder], [ShowInMenu], [GroupMenu], [ImageMenu], [IndexShowInPermission], [Url], [ShortName], [Icon_css]) VALUES (2031, N'Material list', N'material_list', N'warehouse', N'Danh sách vật tư - phụ tùng', 4, 1, NULL, NULL, 1, N'MaterialList.aspx', N'MT', NULL)
SET IDENTITY_INSERT [dbo].[tblFunctions] OFF


SET IDENTITY_INSERT [dbo].[tblRights] ON 

INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2050, 2023, N'View failure type', N'failure_type_view', N'View failure type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2051, 2023, N'Add failure type', N'failure_type_add', N'Add failure type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2052, 2023, N'Edit failure type', N'failure_type_edit', N'Edit failure type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2053, 2023, N'Delete failure type', N'failure_type_delete', N'Delete failure type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2054, 2024, N'View maintenance group', N'maintenance_group_view', N'View maintenance group', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2055, 2024, N'Add maintenance group', N'maintenance_group_add', N'Add maintenance group', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2056, 2024, N'Edit maintenance group', N'maintenance_group_edit', N'Edit maintenance group', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2057, 2024, N'Delete maintenance group', N'maintenance_group_delete', N'Delete maintenance group', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2058, 2025, N'View maintenance list', N'maintenance_list_view', N'View maintenance list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2059, 2025, N'Add maintenance list', N'maintenance_list_add', N'Add maintenance list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2060, 2025, N'Edit maintenance list', N'maintenance_list_edit', N'Edit maintenance list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2061, 2025, N'Delete maintenance list', N'maintenance_list_delete', N'Delete maintenance list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2062, 2026, N'View maintenance level', N'maintenance_level_view', N'View maintenance level', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2063, 2026, N'Add maintenance level', N'maintenance_level_add', N'Add maintenance level', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2064, 2026, N'Edit maintenance level', N'maintenance_level_edit', N'Edit maintenance level', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2065, 2026, N'Delete maintenance level', N'maintenance_level_delete', N'Delete maintenance level', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2066, 2028, N'View provider list', N'provider_list_view', N'View provider list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2067, 2028, N'Add provider list', N'provider_list_add', N'Add provider list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2068, 2028, N'Edit provider list', N'provider_list_edit', N'Edit provider list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2069, 2028, N'Delete provider list', N'provider_list_delete', N'Delete provider list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2070, 2029, N'View department list', N'department_list_view', N'View department list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2071, 2029, N'Add department list', N'department_list_add', N'Add department list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2072, 2029, N'Edit department list', N'department_list_edit', N'Edit department list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2073, 2029, N'Delete department list', N'department_list_delete', N'Delete department list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2074, 2030, N'View material type', N'material_type_view', N'View material type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2075, 2030, N'Add material type', N'material_type_add', N'Add material type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2076, 2030, N'Edit material type', N'material_type_edit', N'Edit material type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2077, 2030, N'Delete material type', N'material_type_delete', N'Delete material type', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2078, 2031, N'View material list', N'material_list_view', N'View material list', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2079, 2031, N'Add material', N'material_add', N'Add material', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2080, 2031, N'Edit material', N'material_edit', N'Edit material', 1)
INSERT [dbo].[tblRights] ([ID], [IdFunction], [RightName], [RightCode], [Description], [Status]) VALUES (2081, 2031, N'Delete material', N'material_delete', N'Delete material', 1)
SET IDENTITY_INSERT [dbo].[tblRights] OFF
SET IDENTITY_INSERT [dbo].[tblRightsInRoles] ON 

INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2052, 2050, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2053, 2051, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2054, 2052, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2055, 2053, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2056, 2054, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2057, 2055, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2058, 2056, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2059, 2057, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2060, 2058, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2061, 2059, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2062, 2060, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2063, 2061, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2064, 2062, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2065, 2063, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2066, 2064, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2067, 2065, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2068, 2066, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2069, 2067, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2070, 2068, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2071, 2069, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2072, 2070, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2073, 2071, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2074, 2072, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2075, 2073, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2076, 2074, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2077, 2075, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2078, 2076, 1)

INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2079, 2077, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2080, 2078, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2081, 2079, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2082, 2080, 1)
INSERT [dbo].[tblRightsInRoles] ([ID], [IdRight], [IdRole]) VALUES (2083, 2081, 1)
SET IDENTITY_INSERT [dbo].[tblRightsInRoles] OFF
SET IDENTITY_INSERT [dbo].[tblUserRole] ON 

INSERT [dbo].[tblUserRole] ([ID], [IDUser], [IDRole]) VALUES (2, 16, 1)
INSERT [dbo].[tblUserRole] ([ID], [IDUser], [IDRole]) VALUES (1003, 828, 1)
SET IDENTITY_INSERT [dbo].[tblUserRole] OFF

GO
update tblFunctions Set DisplayOrder = 5 where ID = 3
