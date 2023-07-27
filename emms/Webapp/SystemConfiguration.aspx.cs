using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class SystemConfiguration : BaseAdminGridPage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.System_Configuration;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MasterPage.FunctionPageCode = FunctionPageCode;
                Permission();
                BindAllSetting();

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnUpdateEquipmentManagement.OnClientClick = "Control.Destroy('#" + settingEquipmentBody.ClientID + "')";
                btnUpdateMaintenanceManagement.OnClientClick = "Control.Destroy('#" + settingMaintenanceBody.ClientID + "')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + upUserList.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnUpdateEquipmentManagement.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Setting_Equipment_Management);
        btnUpdateMaintenanceManagement.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Setting_Maintenance_Management);
    }

    private void BindAllSetting()
    {
        //Equipment management
        if (FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Setting_Equipment_Management))
        {
            settingEquipmentTitle.Visible = true;
            settingEquipmentBody.Visible = true;
            BindEquipmentManagement();
        }
        else
        {
            settingEquipmentTitle.Visible = false;
            settingEquipmentBody.Visible = false;
        }

        //Maintenance management
        if (FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Setting_Maintenance_Management))
        {
            settingMaintenanceTitle.Visible = true;
            settingMaintenanceBody.Visible = true;
            BindMaintenanceManagement();
        }
        else
        {
            settingMaintenanceTitle.Visible = false;
            settingMaintenanceBody.Visible = false;
        }
    }

    #region Equipment management

    private void BindEquipmentManagement()
    {
        List<tblSystemSetting> settingList = SystemSettingManager.GetItemByGroup(SystemSettingGroup.Equipment_Management);
        if (settingList.Count > 0)
        {
            foreach (tblSystemSetting item in settingList)
            {
                switch (item.Name)
                {
                    case SystemSettingParameter.Equipment_Management_SoNgayCanhBaoDangKiem:
                        {
                            txtSoNgayCanhBaoDangKiem.Text = item.Value;
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }

    protected void btnUpdateEquipmentManagement_Click(object sender, EventArgs e)
    {
        try
        {
            //Số ngày cảnh báo đăng kiểm
            int soNgayCanhBaoDangKiem = 0;
            if (string.IsNullOrEmpty(txtSoNgayCanhBaoDangKiem.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Phạm vi hiện cảnh báo đăng kiểm trang thiết bị (số ngày)</b>", MessageType.Message);
                return;
            }
            else if (!NumberHelper.ConvertToInt(txtSoNgayCanhBaoDangKiem.Text.Trim(), out soNgayCanhBaoDangKiem))
            {
                ShowMessage(this, "Please enter <b>Phạm vi hiện cảnh báo đăng kiểm trang thiết bị (số ngày)</b> is a number", MessageType.Message);
                return;
            }

            tblSystemSetting item = SystemSettingManager.GetItemByName(SystemSettingParameter.Equipment_Management_SoNgayCanhBaoDangKiem);
            if (item != null)
            {
                item.Value = soNgayCanhBaoDangKiem.ToString();
                SystemSettingManager.UpdateItem(item);
            }
            else
            {
                tblSystemSetting newItem = new tblSystemSetting();
                newItem.Name = SystemSettingParameter.Equipment_Management_SoNgayCanhBaoDangKiem;
                newItem.Value = soNgayCanhBaoDangKiem.ToString();
                newItem.SettingGroup = SystemSettingGroup.Equipment_Management;
                SystemSettingManager.InsertItem(newItem);
            }

            BindEquipmentManagement();
            Permission();
            ShowMessage(this, "Update successfully <b>Equipment management</b>.", MessageType.Message);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion

    #region Maintenance management

    private void BindMaintenanceManagement()
    {
        List<tblSystemSetting> settingList = SystemSettingManager.GetItemByGroup(SystemSettingGroup.Maintenance_Management);
        if (settingList.Count > 0)
        {
            foreach (tblSystemSetting item in settingList)
            {
                switch (item.Name)
                {
                    case SystemSettingParameter.Maintenance_Management_SoNgayCanhBaoBaoDuong:
                        {
                            txtSoNgayCanhBaoBaoDuong.Text = item.Value;
                            break;
                        }
                    case SystemSettingParameter.Maintenance_Management_SoNgayTinhTanSuat:
                        {
                            txtSoNgayTinhTanSuat.Text = item.Value;
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }

    protected void btnUpdateMaintenanceManagement_Click(object sender, EventArgs e)
    {
        try
        {
            //Số ngày cảnh báo bảo dưỡng
            int soNgayCanhBaoBaoDuong = 0;
            if (string.IsNullOrEmpty(txtSoNgayCanhBaoBaoDuong.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Phạm vi hiện cảnh báo bảo dưỡng trang thiết bị (số ngày)</b>", MessageType.Message);
                return;
            }
            else if (!NumberHelper.ConvertToInt(txtSoNgayCanhBaoBaoDuong.Text.Trim(), out soNgayCanhBaoBaoDuong))
            {
                ShowMessage(this, "Please enter <b>Phạm vi hiện cảnh báo bảo dưỡng trang thiết bị (số ngày)</b> is a number", MessageType.Message);
                return;
            }

            //Số ngày gần nhất tính tần suất
            int soNgayGanNhatTinhTanSuat = 0;
            if (string.IsNullOrEmpty(txtSoNgayTinhTanSuat.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Số ngày gần nhất nhập liệu để tính tần suất</b>", MessageType.Message);
                return;
            }
            else if (!NumberHelper.ConvertToInt(txtSoNgayTinhTanSuat.Text.Trim(), out soNgayGanNhatTinhTanSuat))
            {
                ShowMessage(this, "Please enter <b>Số ngày gần nhất nhập liệu để tính tần suất</b> is a number", MessageType.Message);
                return;
            }

            //Số ngày cảnh báo bảo dưỡng
            tblSystemSetting item = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayCanhBaoBaoDuong);
            if (item != null)
            {
                item.Value = soNgayCanhBaoBaoDuong.ToString();
                SystemSettingManager.UpdateItem(item);
            }
            else
            {
                tblSystemSetting newItem = new tblSystemSetting();
                newItem.Name = SystemSettingParameter.Maintenance_Management_SoNgayCanhBaoBaoDuong;
                newItem.Value = soNgayCanhBaoBaoDuong.ToString();
                newItem.SettingGroup = SystemSettingGroup.Maintenance_Management;
                SystemSettingManager.InsertItem(newItem);
            }

            //Số ngày gần nhất tính tần suất
            item = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayTinhTanSuat);
            if (item != null)
            {
                item.Value = soNgayGanNhatTinhTanSuat.ToString();
                SystemSettingManager.UpdateItem(item);
            }
            else
            {
                tblSystemSetting newItem = new tblSystemSetting();
                newItem.Name = SystemSettingParameter.Maintenance_Management_SoNgayTinhTanSuat;
                newItem.Value = soNgayGanNhatTinhTanSuat.ToString();
                newItem.SettingGroup = SystemSettingGroup.Maintenance_Management;
                SystemSettingManager.InsertItem(newItem);
            }

            BindMaintenanceManagement();
            Permission();
            ShowMessage(this, "Update successfully <b>Maintenance management</b>.", MessageType.Message);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}