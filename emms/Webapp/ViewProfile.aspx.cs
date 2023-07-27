using System;
using BusinessLogic.Helper;
using BusinessLogic.Security;

public partial class ViewProfile : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Profile;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MasterPage.FunctionPageCode = FunctionPageCode;
                LoadCurrentUser();
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadCurrentUser()
    {
        if (LoginHelper.User != null)
        {
            lblUserName.Text = LoginHelper.User.UserName;
            lblFullname.Text = LoginHelper.User.FullName;
            lblEmail.Text = LoginHelper.User.Email;
        }
    }
}