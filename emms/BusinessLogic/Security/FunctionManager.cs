using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Security
{
    public class FunctionCode
    {
        #region Equipment categories

        public const string Company_List = "company_list";
        public const string Equipment_Group_List = "equipment_group";
        public const string Equipment_Type_List = "equipment_type";
        public const string Fuel_Quota_Of_Equipment = "fuel_quota_of_equipment"; //danh mục định mức nhiên liệu trang thiết bị
        public const string Frequency_Working = "frequency_working";

        #endregion

        #region Maintenance categories

        public const string Failure_List = "failure_type";
        public const string Maintenance_Group_List = "maintenance_group_list";
        public const string Maintenance_List = "maintenance_list";
        public const string Maintenance_Level = "maintenance_level";
        public const string Maintenance_Request_Form = "maintenance_request_form";

        #endregion 

        #region Equipment management

        public const string Equipment_List = "equipment_list";
        public const string Equipment_Operation_Processing = "equipment_operation_processing"; //quá trình hoạt động trang thiết bị
        public const string Fule_Processing = "fule_processing"; //quá trình nạp nhiên liệu
        public const string Child_Maintenance_Type = "child_maintenance_type";
        public const string Transfer_Data_From_GPS = "transfer_data_from_gps";

        #endregion

        #region Maintenance management

        public const string Maintenance_Request = "maintenance_request";
        public const string Repair_Request = "repair_request";

        #endregion

        #region Report

        public const string Warning_Maintenance = "warning_maintenance";
        public const string Warning_Registration = "warning_registration";
        public const string Fuel_Consumption_Report = "fuel_consumption_report"; //báo cáo định mức tiêu hao nhiên liệu
        public const string Report_Operation_Processing = "report_operation_processing";
        public const string Report_Fule_Processing = "report_fule_processing";
        public const string Report_Maintenance_Plan = "report_maintenance_plan";
        public const string Report_Activity_History = "report_activity_history";
        public const string Report_Maintenance_Request = "maintenance_request_report";
        public const string Report_Repair_Request = "repair_request_report";

        #endregion

        #region System management

        public const string Default = "default";
        public const string Current_User_Info = "current_user_information";
        public const string Profile = "profile";
        public const string Change_Password = "change_password";
        public const string User_List = "user_list";
        public const string Role_List = "role_list";
        public const string System_Configuration = "system_configuration";

        #endregion
    }

    public class RightCode
    {
        #region Equipment categories

        //CompanyList.aspx
        public const string Company_View = "company_view";
        public const string Company_Add = "company_add";
        public const string Company_Edit = "company_edit";
        public const string Company_Delete = "company_delete";

        //EquipmentGroupList.aspx
        public const string EquipmentGroup_View = "equipment_group_view";
        public const string EquipmentGroup_Add = "equipment_group_add";
        public const string EquipmentGroup_Edit = "equipment_group_edit";
        public const string EquipmentGroup_Delete = "equipment_group_delete";

        //EquipmentTypeList.aspx
        public const string EquipmentType_View = "equipment_type_view";
        public const string EquipmentType_Add = "equipment_type_add";
        public const string EquipmentType_Edit = "equipment_type_edit";
        public const string EquipmentType_Delete = "equipment_type_delete";

        //FuelQuota.aspx
        public const string FuelQuota_View = "fuel_quota_view";
        public const string FuelQuota_Add = "fuel_quota_add";
        public const string FuelQuota_Edit = "fuel_quota_edit";
        public const string FuelQuota_Delete = "fuel_quota_delete";

        //FrequencyWorking.aspx
        public const string FrequencyWorking_View = "frequency_working_view";
        public const string FrequencyWorking_Add = "frequency_working_add";
        public const string FrequencyWorking_Edit = "frequency_working_edit";
        public const string FrequencyWorking_Delete = "frequency_working_delete";

        #endregion

        #region Maintenance categories

        //FailureType.aspx
        public const string Failure_Type_View = "failure_type_view";
        public const string Failure_Type_Add = "failure_type_add";
        public const string Failure_Type_Edit = "failure_type_edit";
        public const string Failure_Type_Delete = "failure_type_delete";

        //MaintenanceGroup.aspx
        public const string Maintenance_Group_View = "maintenance_group_view";
        public const string Maintenance_Group_Add = "maintenance_group_add";
        public const string Maintenance_Group_Edit = "maintenance_group_edit";
        public const string Maintenance_Group_Delete = "maintenance_group_delete";

        //MaintenanceList.aspx
        public const string Maintenance_List_View = "maintenance_list_view";
        public const string Maintenance_List_Add = "maintenance_list_add";
        public const string Maintenance_List_Edit = "maintenance_list_edit";
        public const string Maintenance_List_Delete = "maintenance_list_delete";

        //MaintenanceChildType.aspx
        public const string ChildMaintenanceType_View = "child_maintenance_type_view";
        public const string ChildMaintenanceType_Add = "child_maintenance_type_add";
        public const string ChildMaintenanceType_Edit = "child_maintenance_type_edit";
        public const string ChildMaintenanceType_Delete = "child_maintenance_type_delete";

        //MaintenanceLevel.aspx
        public const string Maintenance_Level_View = "maintenance_level_view";
        public const string Maintenance_Level_Add = "maintenance_level_add";
        public const string Maintenance_Level_Edit = "maintenance_level_edit";
        public const string Maintenance_Level_Delete = "maintenance_level_delete";

        //MaintenanceRequestForm.aspx
        public const string Maintenance_Request_Form_View = "maintenance_request_form_view";
        public const string Maintenance_Request_Form_Add = "maintenance_request_form_add";
        public const string Maintenance_Request_Form_Edit = "maintenance_request_form_edit";
        public const string Maintenance_Request_Form_Delete = "maintenance_request_form_delete";

        #endregion

        #region Equipment management

        //EquipmentList.aspx
        public const string Equipment_View = "equipment_view";
        public const string Equipment_Add = "equipment_add";
        public const string Equipment_Edit = "equipment_edit";
        public const string Equipment_Delete = "equipment_delete";

        //OperationProcessing.aspx
        public const string OperationProcessing_View = "operation_processing_view";
        public const string OperationProcessing_Add = "operation_processing_add";
        public const string OperationProcessing_Edit = "operation_processing_edit";
        public const string OperationProcessing_Delete = "operation_processing_delete";

        //FuleProcessing.aspx
        public const string FuleProcessing_View = "fule_processing_view";
        public const string FuleProcessing_Add = "fule_processing_add";
        public const string FuleProcessing_Edit = "fule_processing_edit";
        public const string FuleProcessing_Delete = "fule_processing_delete";

        //TransferData.aspx
        public const string TransferDataGPS_View = "transfer_data_GPS_view_data";
        public const string TransferDataGPS_Transfer_Data = "transfer_data_GPS_transfer_data";

        #endregion

        #region Maintenance management

        //MaintenanceRequest.aspx
        public const string Maintenance_Request_View = "maintenance_request_view";
        public const string Maintenance_Request_Add = "maintenance_request_add";
        public const string Maintenance_Request_Edit = "maintenance_request_edit";
        public const string Maintenance_Request_Delete = "maintenance_request_delete";

        //RepairRequest.aspx
        public const string Repair_Request_View = "repair_request_view";
        public const string Repair_Request_Add = "repair_request_add";
        public const string Repair_Request_Edit = "repair_request_edit";
        public const string Repair_Request_Delete = "repair_request_delete";

        #endregion

        #region Warehouse management

        //ProviderList.aspx
        public const string Provider_List_View = "provider_list_view";
        public const string Provider_List_Add = "provider_list_add";
        public const string Provider_List_Edit = "provider_list_edit";
        public const string Provider_List_Delete = "provider_list_delete";

        //DepartmentList.aspx
        public const string Department_List_View = "department_list_view";
        public const string Department_List_Add = "department_list_add";
        public const string Department_List_Edit = "department_list_edit";
        public const string Department_List_Delete = "department_list_delete";

        //MaterialTypeList.aspx
        public const string Material_Type_View = "material_type_view";
        public const string Material_Type_Add = "material_type_add";
        public const string Material_Type_Edit = "material_type_edit";
        public const string Material_Type_Delete = "material_type_delete";

        //MaterialList.aspx
        public const string Material_View = "material_list_view";
        public const string Material_Add = "material_add";
        public const string Material_Edit = "material_edit";
        public const string Material_Delete = "material_delete";

        #endregion

        #region Report

        //WarningRegistration.aspx
        public const string WarningRegistration_View = "view_warning_registration_report";

        //WarningMaintenance.aspx
        public const string Warning_Maintenance_View = "warning_maintenance_view";

        //ReportOperationProcessing.aspx
        public const string Operation_Processing_Report_View = "view_report_operation_processing";

        //ReportFuleProcessing.aspx
        public const string Fule_Processing_Report_View = "view_report_fule_processing";

        //FuelConsumptionReport.aspx
        public const string Fuel_Consumption_Report = "fuel_consumption_report";

        public const string Report_Maintenance_Plan_View = "view_report_maintenance_plan";

        public const string Report_Activity_History_View = "view_report_activity_history";

        public const string Report_Maintenance_Request_View = "maintenance_request_report_view_data";

        public const string Report_Repair_Request_View = "report_repair_request_view";

        #endregion

        #region System management

        //Profile
        public const string View_Profile = "view_profile";
        public const string Change_Password = "change_password";

        //UserList.aspx
        public const string User_View = "user_view";
        public const string User_Add = "user_add";
        public const string User_Edit = "user_edit";
        public const string User_Delete = "user_delete";

        //RoleList.aspx
        public const string Role_View = "role_list_view";
        public const string Role_Add = "role_add";
        public const string Role_Edit = "role_edit";
        public const string Role_Delete = "role_delete";

        //PermissionRole.aspx
        public const string Permission_View = "view_right_list";
        public const string Permission_Update = "update_right_for_role";

        //SystemConfiguration.aspx
        public const string Setting_Equipment_Management = "update_setting_equipment_management";
        public const string Setting_Maintenance_Management = "update_setting_maintenance_management";

        #endregion
    }

    public class RightInFunction
    {
        public string FunctionCode { get; set; }
        public string RightCode { get; set; }
    }

    public class FunctionManager
    {
        public static List<RightInFunction> GetRightsInRolesByIdUser(int idUser)
        {
            using (var db = new EMMEntities())
            {
                var EmpCol = db.spUser_GetRightsInRolesByIdUser(idUser);
                var EmpItems = from e in EmpCol
                               select new RightInFunction
                               {
                                   RightCode = e.RightCode,
                                   FunctionCode = e.FunctionCode
                               };
                List<RightInFunction> ls = new List<RightInFunction>(EmpItems.ToList());
                return ls;
            }
        }

        public static List<tblFunction> GetAllByIdUser(int idUser)
        {
            
            //Lấy tất cả các chức năng của người dùng để bind menu
            EMMEntities data = new EMMEntities();
            var EmpCol = data.Function_GetAllByIdUser(idUser);
            var EmpItems = from e in EmpCol
                           select new tblFunction
                           {
                               ID = e.ID,
                               Name = e.Name,
                               Code = e.Code,
                               ParentCode = e.ParentCode,
                               Description = e.Description,
                               DisplayOrder = e.DisplayOrder,
                               ShowInMenu = e.ShowInMenu,
                               GroupMenu = e.GroupMenu,
                               ImageMenu = e.ImageMenu,
                               ShortName = e.ShortName,
                               Url = e.Url,
                               Icon_css = e.Icon_css
                           };
            List<tblFunction> ls = new List<tblFunction>(EmpItems.ToList());
            return ls;
            
            //return new List<tblFunction>();
        }

        public static List<tblFunction> GetFunctionByParentCode(string parentCode)
        {
            //Lấy các chức năng con từ chức năng cha truyền vào
            //sắp xếp tăng dần theo GroupMenu, sau đó tăng dần theo DisplayOrder
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblFunctions
                         where p.ParentCode == parentCode
                         select p).OrderBy(p => p.GroupMenu).ThenBy(p => p.DisplayOrder).ToList();
            return query;
        }

        public static bool CheckUserHasRight(List<RightInFunction> rightsInFunctionsOfUser, string rightCode)
        {
            RightInFunction rightCodeResult = rightsInFunctionsOfUser.Find(
                                                       delegate (RightInFunction p)
                                                       {
                                                           return p.RightCode.Equals(rightCode);
                                                       });
            return (rightCodeResult != null) ? true : false;
        }

        public static List<tblRightsInRole> GetRightsInRoles(int idRole)
        {
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblRightsInRoles
                         where p.IdRole == idRole
                         select p).ToList();
            return query;
        }

        public static tblRightsInRole InsertRightsInRole(tblRightsInRole rightsToRole)
        {
            tblRightsInRole result = null;
            using (var db = new EMMEntities())
            {
                var obj = new tblRightsInRole
                {
                    IdRight = rightsToRole.IdRight,
                    IdRole = rightsToRole.IdRole
                };
                result = db.tblRightsInRoles.Add(rightsToRole);
                db.SaveChanges();

                return result;
            };
        }

        public static int UpdateRight(tblRightsInRole rightInRole)
        {
            int records = 0;
            using (var db = new EMMEntities())
            {
                var result = db.tblRightsInRoles.Find(rightInRole.ID); //tìm dòng trong bảng tblRightsInRole tương ứng với khóa
                if (result != null)
                {
                    result.IdRight = rightInRole.IdRight;
                    result.IdRole = rightInRole.IdRole;

                    db.tblRightsInRoles.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    records = db.SaveChanges(); //Trả về số dòng đã update
                }
            };
            return records;
        }

        public static tblRight GetRightByName(string rightCode)
        {
            //Lấy tài khoản đầu tiên nếu có trong danh sách, nếu không có trả về null
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblRights
                         where p.RightCode == rightCode
                         select p).FirstOrDefault();
            return query;
        }

        public static tblRightsInRole GetRightsInRoleByIdRightAndIdRole(int idRight, int idRole)
        {
            //Lấy tài khoản đầu tiên nếu có trong danh sách, nếu không có trả về null
            EMMEntities db = new EMMEntities();
            var query = (from p in db.tblRightsInRoles
                         where p.IdRight == idRight && p.IdRole == idRole
                         select p).FirstOrDefault();
            return query;
        }

        public static int DeleteRightInRoleByIdRight(int idRight, int idRole)
        {
            int record = 0;
            using (var db = new EMMEntities())
            {
                var result = (from p in db.tblRightsInRoles
                              where p.IdRight == idRight && p.IdRole == idRole
                              select p).FirstOrDefault();
                if (result != null)
                {
                    db.tblRightsInRoles.Remove(result);
                    record = db.SaveChanges();
                }
            }
            return record;
        }
    }
}

