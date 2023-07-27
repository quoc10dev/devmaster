using System;
using System.Linq;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class UserDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.User_List;
        }
    }

    private int IdUser
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
                LoadUserDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Add) ||
                                    FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Edit);
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtUserName.MaxLength = 100;
        txtPassword.MaxLength = 200;
        txtFullname.MaxLength = 200;
        txtEmail.MaxLength = 200;
    }

    private void LoadUserDetail()
    {
        if (IdUser > 0)
        {
            lblAction.Text = "Edit";
            tblUser user = UserManager.GetUserByIdUser(IdUser);
            if (user != null)
            {
                txtUserName.Text = user.UserName;
                txtPassword.Text = user.Password;
                txtFullname.Text = user.FullName;
                txtEmail.Text = user.Email;
                chkActive.Checked = user.IsActive;
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IdUser > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Username</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Password</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtFullname.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Fullname</b>", MessageType.Message);
                return;
            }

            if (IdUser > 0)
            {
                tblUser user = new tblUser();
                user.ID = IdUser;
                user.UserName = txtUserName.Text.Trim();
                user.Password = SecurityHelper.Encrypt(txtPassword.Text.Trim());
                user.FullName = txtFullname.Text.Trim();
                user.Email = txtEmail.Text.Trim();
                user.IsActive = chkActive.Checked;
                UserManager.UpdateUser(user);
            }
            else
            {
                tblUser newUser = new tblUser();
                newUser.UserName = txtUserName.Text.Trim();
                newUser.Password = SecurityHelper.Encrypt(txtPassword.Text.Trim());
                newUser.FullName = txtFullname.Text.Trim();
                newUser.Email = txtEmail.Text.Trim();
                newUser.IsActive = chkActive.Checked;
                UserManager.InsertUser(newUser);
            }

            Response.Redirect("UserList.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}