using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class MaintenanceGroupDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Group_List;
        }
    }

    private int IDMaintenanceGroup
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
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Edit);
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
        txtMaintenanceGroupName.MaxLength = 200;
        txtEnglishName.MaxLength = 200;
        txtNumberDisplay.MaxLength = 5;
    }

    private void LoadMaintenanceParentGroup()
    {
        DataTable listItem = MaintenanceGroupManager.GetMaintenanceParentGroup(IDMaintenanceGroup);

        ////Thêm item trống - không chọn group cha
        //tblNhomHangMucBaoDuong emptyItem = new tblNhomHangMucBaoDuong();
        //emptyItem.ID = 0;
        //emptyItem.Ten = "----- Không có nhóm cha -----";
        //listItem.Insert(0, emptyItem);

        DataRow newRow = listItem.NewRow();
        newRow["ID"] = 0;
        newRow["Ten"] = "Không có nhóm cha";
        listItem.Rows.InsertAt(newRow, 0);

        dlParentGroup.DataSource = listItem;
        dlParentGroup.DataTextField = "Ten";
        dlParentGroup.DataValueField = "ID";
        dlParentGroup.DataBind();
    }

    private void LoadDetail()
    {
        if (IDMaintenanceGroup > 0)
        {
            lblAction.Text = "Edit";

            tblNhomHangMucBaoDuong item = MaintenanceGroupManager.GetMaintenanceGroupById(IDMaintenanceGroup);
            if (item != null)
            {
                txtMaintenanceGroupName.Text = item.Ten;
                txtEnglishName.Text = item.TenEng;
                txtNote.Text = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() : string.Empty;
                txtNumberDisplay.Text = item.SoThuTuHienThi.ToString();
                if (item.IDParent.HasValue)
                    dlParentGroup.SelectedValue = item.IDParent.Value.ToString();
                else
                    dlParentGroup.SelectedValue = "0";
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDMaintenanceGroup > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtMaintenanceGroupName.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Maintenance group name</b>", MessageType.Message);
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

            if (IDMaintenanceGroup > 0)
            {
                tblNhomHangMucBaoDuong item = new tblNhomHangMucBaoDuong();
                item.ID = IDMaintenanceGroup;
                item.Ten = txtMaintenanceGroupName.Text.Trim();
                item.TenEng = txtEnglishName.Text.Trim();
                item.SoThuTuHienThi = numberDisplay;
                item.GhiChu = txtNote.Text.Trim();
                if (dlParentGroup.SelectedValue.Equals("0"))
                    item.IDParent = null;
                else
                    item.IDParent = int.Parse(dlParentGroup.SelectedValue);

                MaintenanceGroupManager.UpdateMaintenanceGroup(item);
            }
            else
            {
                tblNhomHangMucBaoDuong newItem = new tblNhomHangMucBaoDuong();
                newItem.Ten = txtMaintenanceGroupName.Text.Trim();
                newItem.TenEng = txtEnglishName.Text.Trim();
                newItem.SoThuTuHienThi = numberDisplay;

                if (dlParentGroup.SelectedValue.Equals("0"))
                    newItem.IDParent = null;
                else
                    newItem.IDParent = int.Parse(dlParentGroup.SelectedValue);
                newItem.GhiChu = txtNote.Text.Trim();
                MaintenanceGroupManager.InsertMaintenanceGroup(newItem);
            }

            Response.Redirect("MaintenanceGroup.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}