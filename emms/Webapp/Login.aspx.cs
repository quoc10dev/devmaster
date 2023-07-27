using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Web;

public partial class Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Iam hêrre");
        if (!IsPostBack)
        {
            txtUsername.MaxLength = 100;
            txtPassword.MaxLength = 200;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            //string str = string.Empty;
            //str = SecurityHelper.Encrypt(System.Configuration.ConfigurationManager.ConnectionStrings["EMMEntities"].ConnectionString);
            //str = SecurityHelper.Encrypt(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

            if (!string.IsNullOrEmpty(txtUsername.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                string userName = txtUsername.Text.Trim();
                string passWord = txtPassword.Text.Trim();
                tblUser user = UserManager.GetUserByUserName(userName);
                if (user != null && SecurityHelper.Decrypt(user.Password).Equals(passWord))
                {
                    LoginHelper.IsLogin = true;
                    LoginHelper.User = user;
                    CurrentUserRight = FunctionManager.GetRightsInRolesByIdUser(user.ID); //lấy các RightCode ứng với quyền của user

                    //Kiểm tra nhảy đến trang đã copy dán lên trình duyệt
                    if (Request.QueryString["ReturnUrl"] == null)
                    {
                        //Redirect bình thường sẽ lỗi Thread was being aborted
                        Response.Redirect("Default.aspx", false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        string returnUrl = Request.QueryString["ReturnUrl"];
                        Response.Redirect(returnUrl, false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "The username or password is incorrect";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please enter username";
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please enter password";
                    txtPassword.Focus();
                    return;
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

}