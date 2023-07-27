using System;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Security;
using DataAccess;

public partial class EquipmentGroupDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Equipment_Group_List;
        }
    }

    private int IDEquipmentGroup
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
                
                LoadDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentGroup_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentGroup_Edit);

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + formatControl.ClientID + "')";
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
        txtName.MaxLength = 200;
        txtNameEng.MaxLength = 200; 
    }

    private void LoadDetail()
    {
        if (IDEquipmentGroup > 0)
        {
            lblAction.Text = "Edit";
            
            tblNhomTrangThietBi item = EquipmentGroupManager.GetEquipmentGroupById(IDEquipmentGroup);
            if (item != null)
            {
                txtName.Text = item.TenNhom;
                txtNameEng.Text = item.TenNhomEng;
                txtNote.Text = item.GhiChu;
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDEquipmentGroup > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentGroup_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentGroup_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Equment group name</b>", MessageType.Message);
                return;
            }

            if (IDEquipmentGroup > 0)
            {
                tblNhomTrangThietBi item = new tblNhomTrangThietBi();
                item.ID = IDEquipmentGroup;
                item.TenNhom = txtName.Text.Trim();
                item.TenNhomEng = txtNameEng.Text.Trim();
                item.GhiChu = txtNote.Text.Trim();
                EquipmentGroupManager.UpdateEquipmentGroup(item);
            }
            else
            {
                tblNhomTrangThietBi newItem = new tblNhomTrangThietBi();
                newItem.TenNhom = txtName.Text.Trim();
                newItem.TenNhomEng = txtNameEng.Text.Trim();
                newItem.GhiChu = txtNote.Text.Trim();
                EquipmentGroupManager.InsertEquipmentGroup(newItem);
            }

            Response.Redirect("EquipmentGroupList.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}