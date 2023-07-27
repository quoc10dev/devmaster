using BusinessLogic.Security;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PermissionRole : BaseAdminGridPage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Role_List;
        }
    }

    private int IdRole
    {
        get
        {
            if (ViewState["ID"] != null)
                return Convert.ToInt32(ViewState["ID"]);
            else if (Request.QueryString.AllKeys.Contains("ID"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    int result = 0;
                    int.TryParse(Request.QueryString["ID"], out result);
                    ViewState["ID"] = result;
                    return result;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Permission();
                BindDataOfRole();
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnUpdate.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Permission_Update); //Phân quyền thêm
    }

    private void BindDataOfRole()
    {
        tblRole role = RoleManager.GetRoleByIdRole(IdRole);
        if (role != null)
            lblRoleName.Text = role.RoleName;

        List<tblRightsInRole> listRight = FunctionManager.GetRightsInRoles(IdRole);
        if (listRight.Count > 0)
        {
            foreach (tblRightsInRole item in listRight)
            {
                switch (item.tblRight.RightCode)
                {
                    //User list
                    case RightCode.User_View: 
                        {
                            chkUser_View.Checked = true;
                            break;
                        }
                    case RightCode.User_Add:
                        {
                            chkUser_Add.Checked = true;
                            break;
                        }
                    case RightCode.User_Edit:
                        {
                            chkUser_Edit.Checked = true;
                            break;
                        }
                    case RightCode.User_Delete:
                        {
                            chkUser_Delete.Checked = true;
                            break;
                        }
                    //Role list
                    case RightCode.Role_View: 
                        {
                            chkRole_View.Checked = true;
                            break;
                        }
                    case RightCode.Role_Add:
                        {
                            chkRole_Add.Checked = true;
                            break;
                        }
                    case RightCode.Role_Edit:
                        {
                            chkRole_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Role_Delete:
                        {
                            chkRole_Delete.Checked = true;
                            break;
                        }
                    //PermissionRole.aspx
                    case RightCode.Permission_View: 
                        {
                            chkPermission_View.Checked = true;
                            break;
                        }
                    case RightCode.Permission_Update:
                        {
                            chkPermission_Update.Checked = true;
                            break;
                        }
                    //CompanyList.aspx
                    case RightCode.Company_View: 
                        {
                            chkCompany_View.Checked = true;
                            break;
                        }
                    case RightCode.Company_Add:
                        {
                            chkCompany_Add.Checked = true;
                            break;
                        }
                    case RightCode.Company_Edit:
                        {
                            chkCompany_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Company_Delete:
                        {
                            chkCompany_Delete.Checked = true;
                            break;
                        }
                    //EquipmentGroupList.aspx
                    case RightCode.EquipmentGroup_View:
                        {
                            chkEquipmentGroup_View.Checked = true;
                            break;
                        }
                    case RightCode.EquipmentGroup_Add:
                        {
                            chkEquipmentGroup_Add.Checked = true;
                            break;
                        }
                    case RightCode.EquipmentGroup_Edit:
                        {
                            chkEquipmentGroup_Edit.Checked = true;
                            break;
                        }
                    case RightCode.EquipmentGroup_Delete:
                        {
                            chkEquipmentGroup_Delete.Checked = true;
                            break;
                        }
                    //EquipmentTypeList.aspx
                    case RightCode.EquipmentType_View: 
                        {
                            chkEquipmentType_View.Checked = true;
                            break;
                        }
                    case RightCode.EquipmentType_Add:
                        {
                            chkEquipmentType_Add.Checked = true;
                            break;
                        }
                    case RightCode.EquipmentType_Edit:
                        {
                            chkEquipmentType_Edit.Checked = true;
                            break;
                        }
                    case RightCode.EquipmentType_Delete:
                        {
                            chkEquipmentType_Delete.Checked = true;
                            break;
                        }
                    //EquipmentList.aspx
                    case RightCode.Equipment_View: 
                        {
                            chkEquipment_View.Checked = true;
                            break;
                        }
                    case RightCode.Equipment_Add:
                        {
                            chkEquipment_Add.Checked = true;
                            break;
                        }
                    case RightCode.Equipment_Edit:
                        {
                            chkEquipment_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Equipment_Delete:
                        {
                            chkEquipment_Delete.Checked = true;
                            break;
                        }
                    //FuelQuota.aspx
                    case RightCode.FuelQuota_View: 
                        {
                            chkFuelQuota_View.Checked = true;
                            break;
                        }
                    case RightCode.FuelQuota_Add:
                        {
                            chkFuelQuota_Add.Checked = true;
                            break;
                        }
                    case RightCode.FuelQuota_Edit:
                        {
                            chkFuelQuota_Edit.Checked = true;
                            break;
                        }
                    case RightCode.FuelQuota_Delete:
                        {
                            chkFuelQuota_Delete.Checked = true;
                            break;
                        }
                    //OperationProcessing.aspx
                    case RightCode.OperationProcessing_View: 
                        {
                            chkOperationProcessing_View.Checked = true; 
                            break;
                        }
                    case RightCode.OperationProcessing_Add:
                        {
                            chkOperationProcessing_Add.Checked = true;
                            break;
                        }
                    case RightCode.OperationProcessing_Edit:
                        {
                            chkOperationProcessing_Edit.Checked = true;
                            break;
                        }
                    case RightCode.OperationProcessing_Delete:
                        {
                            chkOperationProcessing_Delete.Checked = true;
                            break;
                        }
                    //FuleProcessing.aspx
                    case RightCode.FuleProcessing_View: 
                        {
                            chkFuleProcessing_View.Checked = true;
                            break;
                        }
                    case RightCode.FuleProcessing_Add:
                        {
                            chkFuleProcessing_Add.Checked = true;
                            break;
                        }
                    case RightCode.FuleProcessing_Edit:
                        {
                            chkFuleProcessing_Edit.Checked = true;
                            break;
                        }
                    case RightCode.FuleProcessing_Delete:
                        {
                            chkFuleProcessing_Delete.Checked = true;
                            break;
                        }
                    //TransferData.aspx
                    case RightCode.TransferDataGPS_View:
                        {
                            chkTransferData_View.Checked = true;
                            break;
                        }
                    case RightCode.TransferDataGPS_Transfer_Data:
                        {
                            chkTransferData_TransferData.Checked = true;
                            break;
                        }
                    //WarningRegistration.aspx
                    case RightCode.WarningRegistration_View: 
                        {
                            chkViewWarningRegistrationReport.Checked = true;
                            break;
                        }
                    //FuelConsumptionReport.aspx
                    case RightCode.Fuel_Consumption_Report: 
                        {
                            chkViewFuelConsumption.Checked = true;
                            break;
                        }
                    //MaintenanceLevel.aspx
                    case RightCode.Maintenance_Level_View:
                        {
                            chkMaintenaceLevel_View.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Level_Add:
                        {
                            chkMaintenaceLevel_Add.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Level_Edit:
                        {
                            chkMaintenaceLevel_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Level_Delete:
                        {
                            chkMaintenaceLevel_Delete.Checked = true;
                            break;
                        }
                    //MaintenanceChildType.aspx
                    case RightCode.ChildMaintenanceType_View:
                        {
                            chkChildMaintenanceType_View.Checked = true;
                            break;
                        }
                    case RightCode.ChildMaintenanceType_Add:
                        {
                            chkChildMaintenanceType_Add.Checked = true;
                            break;
                        }
                    case RightCode.ChildMaintenanceType_Edit:
                        {
                            chkChildMaintenanceType_Edit.Checked = true;
                            break;
                        }
                    case RightCode.ChildMaintenanceType_Delete:
                        {
                            chkChildMaintenanceType_Delete.Checked = true;
                            break;
                        }
                    //FailureType.aspx
                    case RightCode.Failure_Type_View: 
                        {
                            chkFailureType_View.Checked = true;
                            break;
                        }
                    case RightCode.Failure_Type_Add:
                        {
                            chkFailureType_Add.Checked = true;
                            break;
                        }
                    case RightCode.Failure_Type_Edit:
                        {
                            chkFailureType_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Failure_Type_Delete:
                        {
                            chkFailureType_Delete.Checked = true;
                            break;
                        }
                    //FrequencyWorking.aspx
                    case RightCode.FrequencyWorking_View:
                        {
                            chkFrequencyWorking_View.Checked = true;
                            break;
                        }
                    case RightCode.FrequencyWorking_Add:
                        {
                            chkFrequencyWorking_Add.Checked = true;
                            break;
                        }
                    case RightCode.FrequencyWorking_Edit:
                        {
                            chkFrequencyWorking_Edit.Checked = true;
                            break;
                        }
                    case RightCode.FrequencyWorking_Delete:
                        {
                            chkFrequencyWorking_Delete.Checked = true;
                            break;
                        }
                    //MaintenanceGroup.aspx
                    case RightCode.Maintenance_Group_View:
                        {
                            chkMaintenaceGroup_View.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Group_Add:
                        {
                            chkMaintenaceGroup_Add.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Group_Edit:
                        {
                            chkMaintenaceGroup_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Group_Delete:
                        {
                            chkMaintenaceGroup_Delete.Checked = true;
                            break;
                        }
                    //MaintenanceList.aspx
                    case RightCode.Maintenance_List_View:
                        {
                            chkMaintenaceList_View.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_List_Add:
                        {
                            chkMaintenaceList_Add.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_List_Edit:
                        {
                            chkMaintenaceList_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_List_Delete:
                        {
                            chkMaintenaceList_Delete.Checked = true;
                            break;
                        }
                    //WarningMaintenance.aspx
                    case RightCode.Warning_Maintenance_View:
                        {
                            chkWarningMaintenance_View.Checked = true;
                            break;
                        }
                    //MaintenanceRequestForm.aspx
                    case RightCode.Maintenance_Request_Form_View:
                        {
                            chkMaintenanceRequestForm_View.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Request_Form_Add:
                        {
                            chkMaintenanceRequestForm_Add.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Request_Form_Edit:
                        {
                            chkMaintenanceRequestForm_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Request_Form_Delete:
                        {
                            chkMaintenanceRequestForm_Delete.Checked = true;
                            break;
                        }
                    //MaintenanceRequest.aspx
                    case RightCode.Maintenance_Request_View:
                        {
                            chkMaintenanceRequest_View.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Request_Add:
                        {
                            chkMaintenanceRequest_Add.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Request_Edit:
                        {
                            chkMaintenanceRequest_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Maintenance_Request_Delete:
                        {
                            chkMaintenanceRequest_Delete.Checked = true;
                            break;
                        }
                    //RepairRequest.aspx
                    case RightCode.Repair_Request_View:
                        {
                            chkRepairRequest_View.Checked = true;
                            break;
                        }
                    case RightCode.Repair_Request_Add:
                        {
                            chkRepairRequest_Add.Checked = true;
                            break;
                        }
                    case RightCode.Repair_Request_Edit:
                        {
                            chkRepairRequest_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Repair_Request_Delete:
                        {
                            chkRepairRequest_Delete.Checked = true;
                            break;
                        }
                    //ReportOperationProcessing.aspx
                    case RightCode.Operation_Processing_Report_View:
                        {
                            chkViewOperationProcessingReport.Checked = true;
                            break;
                        }
                    //ReportFuleProcessing.aspx
                    case RightCode.Fule_Processing_Report_View:
                        {
                            chkViewFuleProcessingReport.Checked = true;
                            break;
                        }
                    //MaintenancePlan.aspx
                    case RightCode.Report_Maintenance_Plan_View:
                        {
                            chkMaintenancePlan.Checked = true;
                            break;
                        }
                    //ReportActivityHistory.aspx
                    case RightCode.Report_Activity_History_View:
                        {
                            chkActivityHistory.Checked = true;
                            break;
                        }
                    //ReportRepairRequest.aspx
                    case RightCode.Report_Repair_Request_View:
                        {
                            chkViewRepairRequestReport.Checked = true;
                            break;
                        }
                    //ProviderList.aspx
                    case RightCode.Provider_List_View:
                        {
                            chkProviderList_View.Checked = true;
                            break;
                        }
                    case RightCode.Provider_List_Add:
                        {
                            chkProviderList_Add.Checked = true;
                            break;
                        }
                    case RightCode.Provider_List_Edit:
                        {
                            chkProviderList_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Provider_List_Delete:
                        {
                            chkProviderList_Delete.Checked = true;
                            break;
                        }
                    //DepartmentList.aspx
                    case RightCode.Department_List_View:
                        {
                            chkDepartmentList_View.Checked = true;
                            break;
                        }
                    case RightCode.Department_List_Add:
                        {
                            chkDepartmentList_Add.Checked = true;
                            break;
                        }
                    case RightCode.Department_List_Edit:
                        {
                            chkDepartmentList_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Department_List_Delete:
                        {
                            chkDepartmentList_Delete.Checked = true;
                            break;
                        }
                    //MaterialTypeList.aspx
                    case RightCode.Material_Type_View:
                        {
                            chkMaterialType_View.Checked = true;
                            break;
                        }
                    case RightCode.Material_Type_Add:
                        {
                            chkMaterialType_Add.Checked = true;
                            break;
                        }
                    case RightCode.Material_Type_Edit:
                        {
                            chkMaterialType_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Material_Type_Delete:
                        {
                            chkMaterialType_Delete.Checked = true;
                            break;
                        }
                    //MaterialList.aspx
                    case RightCode.Material_View:
                        {
                            chkMaterial_View.Checked = true;
                            break;
                        }
                    case RightCode.Material_Add:
                        {
                            chkMaterial_Add.Checked = true;
                            break;
                        }
                    case RightCode.Material_Edit:
                        {
                            chkMaterial_Edit.Checked = true;
                            break;
                        }
                    case RightCode.Material_Delete:
                        {
                            chkMaterial_Delete.Checked = true;
                            break;
                        }
                    //MaterialList.aspx
                    case RightCode.Report_Maintenance_Request_View:
                        {
                            chkViewMaintenanceRequestReport.Checked = true;
                            break;
                        }
                    //Configuration.aspx
                    case RightCode.Setting_Equipment_Management: 
                        {
                            chkSetting_EquipmentManagement.Checked = true;
                            break;
                        }
                    case RightCode.Setting_Maintenance_Management:
                        {
                            chkSetting_MaintenanceManagement.Checked = true;
                            break;
                        }
                    case RightCode.View_Profile: //View profile
                        {
                            chkView_Profile.Checked = true;
                            break;
                        }
                    case RightCode.Change_Password: 
                        {
                            chkChange_Password.Checked = true;
                            break;
                        }
                    default:
                        break;
                }

            }
        }
    }

    private void UpdatePermission(bool isChecked, string rightCode, int idRole)
    {
        tblRight right = FunctionManager.GetRightByName(rightCode);
        if (right != null)
        {
            tblRightsInRole rightInRole = null;
            if (isChecked)
            {
                //Kiểm tra nếu chưa có dưới bảng tblRightsInRole thì thêm mới, nếu có rồi thì không cần thêm
                rightInRole = FunctionManager.GetRightsInRoleByIdRightAndIdRole(right.ID, idRole);
                if (rightInRole == null)
                {
                    tblRightsInRole newRightInRole = new tblRightsInRole();
                    newRightInRole.IdRight = right.ID;
                    newRightInRole.IdRole = idRole;
                    FunctionManager.InsertRightsInRole(newRightInRole);
                }
            }
            else
            {
                //Kiểm tra nếu đã có dưới bảng tblRightsInRole thì xóa, nếu chưa có thì không làm gì cả
                rightInRole = FunctionManager.GetRightsInRoleByIdRightAndIdRole(right.ID, idRole);
                if (rightInRole != null)
                    FunctionManager.DeleteRightInRoleByIdRight(rightInRole.IdRight, idRole);
            }
        }
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Permission_Update))
            {
                ShowMessage(this, "You don't have permission to Update this role", MessageType.Message);
                return;
            }

            #region Equipment management

            //Company
            UpdatePermission(chkCompany_View.Checked, RightCode.Company_View, IdRole);
            UpdatePermission(chkCompany_Add.Checked, RightCode.Company_Add, IdRole);
            UpdatePermission(chkCompany_Edit.Checked, RightCode.Company_Edit, IdRole);
            UpdatePermission(chkCompany_Delete.Checked, RightCode.Company_Delete, IdRole);

            //Equipment group
            UpdatePermission(chkEquipmentGroup_View.Checked, RightCode.EquipmentGroup_View, IdRole);
            UpdatePermission(chkEquipmentGroup_Add.Checked, RightCode.EquipmentGroup_Add, IdRole);
            UpdatePermission(chkEquipmentGroup_Edit.Checked, RightCode.EquipmentGroup_Edit, IdRole);
            UpdatePermission(chkEquipmentGroup_Delete.Checked, RightCode.EquipmentGroup_Delete, IdRole);

            //Equipment type
            UpdatePermission(chkEquipmentType_View.Checked, RightCode.EquipmentType_View, IdRole);
            UpdatePermission(chkEquipmentType_Add.Checked, RightCode.EquipmentType_Add, IdRole);
            UpdatePermission(chkEquipmentType_Edit.Checked, RightCode.EquipmentType_Edit, IdRole);
            UpdatePermission(chkEquipmentType_Delete.Checked, RightCode.EquipmentType_Delete, IdRole);

            //Equipment
            UpdatePermission(chkEquipment_View.Checked, RightCode.Equipment_View, IdRole);
            UpdatePermission(chkEquipment_Add.Checked, RightCode.Equipment_Add, IdRole);
            UpdatePermission(chkEquipment_Edit.Checked, RightCode.Equipment_Edit, IdRole);
            UpdatePermission(chkEquipment_Delete.Checked, RightCode.Equipment_Delete, IdRole);

            //Fule quota
            UpdatePermission(chkFuelQuota_View.Checked, RightCode.FuelQuota_View, IdRole);
            UpdatePermission(chkFuelQuota_Add.Checked, RightCode.FuelQuota_Add, IdRole);
            UpdatePermission(chkFuelQuota_Edit.Checked, RightCode.FuelQuota_Edit, IdRole);
            UpdatePermission(chkFuelQuota_Delete.Checked, RightCode.FuelQuota_Delete, IdRole);

            //Operation processing
            UpdatePermission(chkOperationProcessing_View.Checked, RightCode.OperationProcessing_View, IdRole);
            UpdatePermission(chkOperationProcessing_Add.Checked, RightCode.OperationProcessing_Add, IdRole);
            UpdatePermission(chkOperationProcessing_Edit.Checked, RightCode.OperationProcessing_Edit, IdRole);
            UpdatePermission(chkOperationProcessing_Delete.Checked, RightCode.OperationProcessing_Delete, IdRole);

            //Fule processing
            UpdatePermission(chkFuleProcessing_View.Checked, RightCode.FuleProcessing_View, IdRole);
            UpdatePermission(chkFuleProcessing_Add.Checked, RightCode.FuleProcessing_Add, IdRole);
            UpdatePermission(chkFuleProcessing_Edit.Checked, RightCode.FuleProcessing_Edit, IdRole);
            UpdatePermission(chkFuleProcessing_Delete.Checked, RightCode.FuleProcessing_Delete, IdRole);

            //Transfer data
            UpdatePermission(chkTransferData_View.Checked, RightCode.TransferDataGPS_View, IdRole);
            UpdatePermission(chkTransferData_TransferData.Checked, RightCode.TransferDataGPS_Transfer_Data, IdRole);

            //MaintenaceLevel.aspx
            UpdatePermission(chkMaintenaceLevel_View.Checked, RightCode.Maintenance_Level_View, IdRole);
            UpdatePermission(chkMaintenaceLevel_Add.Checked, RightCode.Maintenance_Level_Add, IdRole);
            UpdatePermission(chkMaintenaceLevel_Edit.Checked, RightCode.Maintenance_Level_Edit, IdRole);
            UpdatePermission(chkMaintenaceLevel_Delete.Checked, RightCode.Maintenance_Level_Delete, IdRole);

            //MaintenanceChildType.aspx
            UpdatePermission(chkChildMaintenanceType_View.Checked, RightCode.ChildMaintenanceType_View, IdRole);
            UpdatePermission(chkChildMaintenanceType_Add.Checked, RightCode.ChildMaintenanceType_Add, IdRole);
            UpdatePermission(chkChildMaintenanceType_Edit.Checked, RightCode.ChildMaintenanceType_Edit, IdRole);
            UpdatePermission(chkChildMaintenanceType_Delete.Checked, RightCode.ChildMaintenanceType_Delete, IdRole);

            //FrequencyWorking.aspx
            UpdatePermission(chkFrequencyWorking_View.Checked, RightCode.FrequencyWorking_View, IdRole);
            UpdatePermission(chkFrequencyWorking_Add.Checked, RightCode.FrequencyWorking_Add, IdRole);
            UpdatePermission(chkFrequencyWorking_Edit.Checked, RightCode.FrequencyWorking_Edit, IdRole);
            UpdatePermission(chkFrequencyWorking_Delete.Checked, RightCode.FrequencyWorking_Delete, IdRole);

            #endregion

            #region Maintenance management

            //FailureType.aspx
            UpdatePermission(chkFailureType_View.Checked, RightCode.Failure_Type_View, IdRole);
            UpdatePermission(chkFailureType_Add.Checked, RightCode.Failure_Type_Add, IdRole);
            UpdatePermission(chkFailureType_Edit.Checked, RightCode.Failure_Type_Edit, IdRole);
            UpdatePermission(chkFailureType_Delete.Checked, RightCode.Failure_Type_Delete, IdRole);

            //MaintenaceGroup.aspx
            UpdatePermission(chkMaintenaceGroup_View.Checked, RightCode.Maintenance_Group_View, IdRole);
            UpdatePermission(chkMaintenaceGroup_Add.Checked, RightCode.Maintenance_Group_Add, IdRole);
            UpdatePermission(chkMaintenaceGroup_Edit.Checked, RightCode.Maintenance_Group_Edit, IdRole);
            UpdatePermission(chkMaintenaceGroup_Delete.Checked, RightCode.Maintenance_Group_Delete, IdRole);

            //MaintenaceList.aspx
            UpdatePermission(chkMaintenaceList_View.Checked, RightCode.Maintenance_List_View, IdRole);
            UpdatePermission(chkMaintenaceList_Add.Checked, RightCode.Maintenance_List_Add, IdRole);
            UpdatePermission(chkMaintenaceList_Edit.Checked, RightCode.Maintenance_List_Edit, IdRole);
            UpdatePermission(chkMaintenaceList_Delete.Checked, RightCode.Maintenance_List_Delete, IdRole);

            //MaintenanceRequestForm.aspx
            UpdatePermission(chkMaintenanceRequestForm_View.Checked, RightCode.Maintenance_Request_Form_View, IdRole);
            UpdatePermission(chkMaintenanceRequestForm_Add.Checked, RightCode.Maintenance_Request_Form_Add, IdRole);
            UpdatePermission(chkMaintenanceRequestForm_Edit.Checked, RightCode.Maintenance_Request_Form_Edit, IdRole);
            UpdatePermission(chkMaintenanceRequestForm_Delete.Checked, RightCode.Maintenance_Request_Form_Delete, IdRole);

            //MaintenanceRequest.aspx
            UpdatePermission(chkMaintenanceRequest_View.Checked, RightCode.Maintenance_Request_View, IdRole);
            UpdatePermission(chkMaintenanceRequest_Add.Checked, RightCode.Maintenance_Request_Add, IdRole);
            UpdatePermission(chkMaintenanceRequest_Edit.Checked, RightCode.Maintenance_Request_Edit, IdRole);
            UpdatePermission(chkMaintenanceRequest_Delete.Checked, RightCode.Maintenance_Request_Delete, IdRole);

            //RepairRequest.aspx
            UpdatePermission(chkRepairRequest_View.Checked, RightCode.Repair_Request_View, IdRole);
            UpdatePermission(chkRepairRequest_Add.Checked, RightCode.Repair_Request_Add, IdRole);
            UpdatePermission(chkRepairRequest_Edit.Checked, RightCode.Repair_Request_Edit, IdRole);
            UpdatePermission(chkRepairRequest_Delete.Checked, RightCode.Repair_Request_Delete, IdRole);

            #endregion

            #region Warehouse management

            //ProviderList.aspx
            UpdatePermission(chkProviderList_View.Checked, RightCode.Provider_List_View, IdRole);
            UpdatePermission(chkProviderList_Add.Checked, RightCode.Provider_List_Add, IdRole);
            UpdatePermission(chkProviderList_Edit.Checked, RightCode.Provider_List_Edit, IdRole);
            UpdatePermission(chkProviderList_Delete.Checked, RightCode.Provider_List_Delete, IdRole);

            //DepartmentList.aspx
            UpdatePermission(chkDepartmentList_View.Checked, RightCode.Department_List_View, IdRole);
            UpdatePermission(chkDepartmentList_Add.Checked, RightCode.Department_List_Add, IdRole);
            UpdatePermission(chkDepartmentList_Edit.Checked, RightCode.Department_List_Edit, IdRole);
            UpdatePermission(chkDepartmentList_Delete.Checked, RightCode.Department_List_Delete, IdRole);

            //MaterialTypeList.aspx
            UpdatePermission(chkMaterialType_View.Checked, RightCode.Material_Type_View, IdRole);
            UpdatePermission(chkMaterialType_Add.Checked, RightCode.Material_Type_Add, IdRole);
            UpdatePermission(chkMaterialType_Edit.Checked, RightCode.Material_Type_Edit, IdRole);
            UpdatePermission(chkMaterialType_Delete.Checked, RightCode.Material_Type_Delete, IdRole);

            //MaterialList.aspx
            UpdatePermission(chkMaterial_View.Checked, RightCode.Material_View, IdRole);
            UpdatePermission(chkMaterial_Add.Checked, RightCode.Material_Add, IdRole);
            UpdatePermission(chkMaterial_Edit.Checked, RightCode.Material_Edit, IdRole);
            UpdatePermission(chkMaterial_Delete.Checked, RightCode.Material_Delete, IdRole);

            #endregion

            #region Report management 

            //WarningMaintenance.aspx
            UpdatePermission(chkWarningMaintenance_View.Checked, RightCode.Warning_Maintenance_View, IdRole);

            //Warning Registration
            UpdatePermission(chkViewWarningRegistrationReport.Checked, RightCode.WarningRegistration_View, IdRole);

            //Fuel consumption report
            UpdatePermission(chkViewWarningRegistrationReport.Checked, RightCode.Fuel_Consumption_Report, IdRole);

            //Operation Processing report
            UpdatePermission(chkViewOperationProcessingReport.Checked, RightCode.Operation_Processing_Report_View, IdRole);

            //Fule processing report
            UpdatePermission(chkViewFuleProcessingReport.Checked, RightCode.Fule_Processing_Report_View, IdRole);

            //Maintenance plan report
            UpdatePermission(chkMaintenancePlan.Checked, RightCode.Report_Maintenance_Plan_View, IdRole);

            //Activity history report
            UpdatePermission(chkViewMaintenanceRequestReport.Checked, RightCode.Report_Maintenance_Request_View, IdRole);

            //Maintenance request report
            UpdatePermission(chkActivityHistory.Checked, RightCode.Report_Activity_History_View, IdRole);

            //Repair request report
            UpdatePermission(chkViewRepairRequestReport.Checked, RightCode.Report_Repair_Request_View, IdRole);

            #endregion

            #region System management

            //Configuration.aspx
            UpdatePermission(chkSetting_EquipmentManagement.Checked, RightCode.Setting_Equipment_Management, IdRole);
            UpdatePermission(chkSetting_MaintenanceManagement.Checked, RightCode.Setting_Maintenance_Management, IdRole);
            
            //View profile - change password
            UpdatePermission(chkView_Profile.Checked, RightCode.View_Profile, IdRole);
            UpdatePermission(chkChange_Password.Checked, RightCode.Change_Password, IdRole);

            //User list
            UpdatePermission(chkUser_View.Checked, RightCode.User_View, IdRole);
            UpdatePermission(chkUser_Add.Checked, RightCode.User_Add, IdRole);
            UpdatePermission(chkUser_Edit.Checked, RightCode.User_Edit, IdRole);
            UpdatePermission(chkUser_Delete.Checked, RightCode.User_Delete, IdRole);

            //Role list
            UpdatePermission(chkRole_View.Checked, RightCode.Role_View, IdRole);
            UpdatePermission(chkRole_Add.Checked, RightCode.Role_Add, IdRole);
            UpdatePermission(chkRole_Edit.Checked, RightCode.Role_Edit, IdRole);
            UpdatePermission(chkRole_Delete.Checked, RightCode.Role_Delete, IdRole);

            //Permission
            UpdatePermission(chkPermission_View.Checked, RightCode.Permission_View, IdRole);
            UpdatePermission(chkPermission_Update.Checked, RightCode.Permission_Update, IdRole);

            #endregion

            ShowMessage(this, "Update successfully.", MessageType.Message);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}