using System;
using System.Linq;
using BusinessLogic.Security;
using DataAccess;

public partial class RoleDetail : BasePage
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
                MasterPage.FunctionPageCode = FunctionPageCode;
                SetMaxLength();
                LoadRoleDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Edit);
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtRoleName.MaxLength = 200;
        txtDescription.MaxLength = 1000;
    }

    private void LoadRoleDetail()
    {
        if (IdRole > 0)
        {
            lblAction.Text = "Edit";
            tblRole role = RoleManager.GetRoleByIdRole(IdRole);
            if (role != null)
            {
                txtRoleName.Text = role.RoleName;
                txtDescription.Text = role.Description;
                chkActive.Checked = role.Status;
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IdRole > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtRoleName.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Role name</b>", MessageType.Message);
                return;
            }

            if (IdRole > 0)
            {
                tblRole newRole = new tblRole();
                newRole.ID = IdRole;
                newRole.RoleName = txtRoleName.Text.Trim();
                newRole.Description = txtDescription.Text.Trim();
                newRole.Status = chkActive.Checked;
                RoleManager.UpdateRole(newRole);
            }
            else
            {
                //Kiểm tra nhập trùng RoleName
                tblRole role = RoleManager.GetRoleByRoleName(txtRoleName.Text.Trim());
                if (role != null)
                {
                    ShowMessage(this, string.Format("The role name <b>{0}</b> is exists. Please enter other role name.", txtRoleName.Text.Trim()), MessageType.Message);
                    return;
                }

                tblRole newRole = new tblRole();
                newRole.RoleName = txtRoleName.Text.Trim();
                newRole.Description = txtDescription.Text.Trim();
                newRole.Status = chkActive.Checked;
                RoleManager.InsertRole(newRole);
            }

            Response.Redirect("RoleList.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}