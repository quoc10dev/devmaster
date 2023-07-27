


-- select * from tblFunctions order by ParentCode, DisplayOrder

-- đặt lại tên các trang 
Update tblFunctions Set Url = 'ReportWarningRegistration.aspx' Where ID = 15
Update tblFunctions Set Url = 'ReportFuelConsumption.aspx' Where ID = 16
Update tblFunctions Set Url = 'ReportWarningMaintenance.aspx' Where ID = 2033