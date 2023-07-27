

select * from tblFunctions order by ParentCode, DisplayOrder

-- Ẩn các menu quản lý nạp nhiên liệu
update tblFunctions Set ShowInMenu = 0 where ID = 11
update tblFunctions Set ShowInMenu = 0 where ID = 2034
update tblFunctions Set ShowInMenu = 0 where ID = 13
update tblFunctions Set ShowInMenu = 0 where ID = 2042
update tblFunctions Set ShowInMenu = 0 where ID = 2042
update tblFunctions Set ShowInMenu = 0 where ID = 16