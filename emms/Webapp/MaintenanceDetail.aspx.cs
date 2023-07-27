using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class MaintenanceDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_List;
        }
    }

    private int IDMaintenance
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
                MasterPage.FunctionPageCode = FunctionPageCode;
                SetMaxLength();
                LoadMaintenanceParentGroup();
                LoadDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Edit);
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + formatControl.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtMaintenanceName.MaxLength = 200;
        txtEnglishName.MaxLength = 200;
        txtNumberDisplay.MaxLength = 5;
    }

    private void LoadMaintenanceParentGroup()
    {
        DataTable dt = MaintenanceGroupManager.GetMaintenanceParentGroupForAddMaintenance();
        DataRow newRow = dt.NewRow();
        newRow["ID"] = 0;
        newRow["Ten"] = "--- Chọn nhóm hạng mục ---";
        dt.Rows.InsertAt(newRow, 0);

        dlMaintenanceGroup.DataSource = dt;
        dlMaintenanceGroup.DataTextField = "Ten";
        dlMaintenanceGroup.DataValueField = "ID";
        dlMaintenanceGroup.DataBind();
    }

    private void LoadDetail()
    {
        if (IDMaintenance > 0)
        {
            lblAction.Text = "Edit";

            tblHangMucBaoDuong item = MaintenanceManager.GetMaintenanceById(IDMaintenance);
            if (item != null)
            {
                txtMaintenanceName.Text = item.Ten;
                txtEnglishName.Text = item.TenEng;
                txtNumberDisplay.Text = item.SoThuTuHienThi.ToString();
                txtNote.Text = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu : string.Empty;
                dlMaintenanceGroup.SelectedValue = item.IDNhomHangMucBaoDuong.ToString();
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDMaintenance > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(dlMaintenanceGroup.SelectedValue) || dlMaintenanceGroup.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please enter <b>Maintenance group</b>", MessageType.Message);
                return;
            }

            //Nếu có nhóm cha - con --> Kiểm tra ko cho phép chọn nhóm cha, bắt buộc phải chọn nhóm con
            int idMaintenanceGroup = int.Parse(dlMaintenanceGroup.SelectedValue);
            tblNhomHangMucBaoDuong maintenanceGroup = MaintenanceGroupManager.GetMaintenanceGroupByIdParent(idMaintenanceGroup);
            if (maintenanceGroup != null)
            {
                ShowMessage(this, "Không thể chọn nhóm cha, vui lòng chọn nhóm con.", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtMaintenanceName.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Maintenance name</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(dlMaintenanceGroup.SelectedValue))
            {
                ShowMessage(this, "Please select <b>Maintenance group</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtNumberDisplay.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Number display in report</b>", MessageType.Message);
                return;
            }

            short numberDisplay = 0;
            if (!NumberHelper.ConvertToShort(txtNumberDisplay.Text.Trim(), out numberDisplay))
            {
                ShowMessage(this, "Please enter <b>Number display in report</b> is a number", MessageType.Message);
                return;
            }

            if (numberDisplay <= 0)
            {
                ShowMessage(this, "Please enter <b>Number display in report</b> is greater than 0", MessageType.Message);
                return;
            }

            if (IDMaintenance > 0)
            {
                tblHangMucBaoDuong item = new tblHangMucBaoDuong();
                item.ID = IDMaintenance;
                item.Ten = txtMaintenanceName.Text.Trim();
                item.TenEng = txtEnglishName.Text.Trim();
                item.SoThuTuHienThi = numberDisplay;
                item.IDNhomHangMucBaoDuong = idMaintenanceGroup;
                item.GhiChu = txtNote.Text.Trim();
                MaintenanceManager.UpdateMaintenance(item);
            }
            else
            {
                tblHangMucBaoDuong newItem = new tblHangMucBaoDuong();
                newItem.Ten = txtMaintenanceName.Text.Trim();
                newItem.TenEng = txtEnglishName.Text.Trim();
                newItem.SoThuTuHienThi = numberDisplay;
                newItem.IDNhomHangMucBaoDuong = idMaintenanceGroup;
                newItem.GhiChu = txtNote.Text.Trim();
                MaintenanceManager.InsertMaintenance(newItem);
            }

            Response.Redirect("MaintenanceList.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}