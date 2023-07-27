using System;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class ChangePassword : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Change_Password;
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
                LoadCurrentUser();
            }

            //Khi postback không bị mất giá trị
            txtCurrentPassword.Attributes.Add("value", txtCurrentPassword.Text);
            txtNewPassword.Attributes.Add("value", txtNewPassword.Text);
            txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtCurrentPassword.MaxLength = 200;
        txtNewPassword.MaxLength = 200;
        txtConfirmPassword.MaxLength = 200;
    }

    private void LoadCurrentUser()
    {
        if (LoginHelper.User != null)
        {
            lblUserName.Text = LoginHelper.User.UserName;
            lblFullname.Text = LoginHelper.User.FullName;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Current password</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>New password</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Confirm new password</b>", MessageType.Message);
                return;
            }

            if (!txtNewPassword.Text.Trim().Equals(txtConfirmPassword.Text.Trim()))
            {
                ShowMessage(this, "Please enter value <b>Confirm new password</b> have to same <b>New password</b>", MessageType.Message);
                return;
            }

            if (LoginHelper.User != null)
            {
                tblUser item = UserManager.GetUserByIdUser(LoginHelper.User.ID);
                if (item != null)
                {
                    //Kiểm tra nhập đúng password hiện tại
                    string decryptedPassword = SecurityHelper.Decrypt(item.Password);
                    if (!decryptedPassword.Equals(txtCurrentPassword.Text.Trim()))
                    {
                        ShowMessage(this, "Current password is not correct.", MessageType.Message);
                        return;
                    }

                    //Kiểm tra password mới phải khác với password hiện tại
                    if (txtCurrentPassword.Text.Trim().Equals(txtNewPassword.Text.Trim()))
                    {
                        ShowMessage(this, "New password have to difference current password", MessageType.Message);
                        return;
                    }

                    item.Password = SecurityHelper.Encrypt(txtNewPassword.Text.Trim());
                    UserManager.UpdateUser(item);

                    //Đặt lại về trống
                    txtCurrentPassword.Attributes.Add("value", string.Empty);
                    txtNewPassword.Attributes.Add("value", string.Empty);
                    txtConfirmPassword.Attributes.Add("value", string.Empty);

                    ShowMessage(this, "Change password successfully", MessageType.Message);
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}