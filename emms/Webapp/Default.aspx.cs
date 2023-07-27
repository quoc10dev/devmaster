using BusinessLogic.Helper;
using BusinessLogic.Security;
using System;

public partial class _Default : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Default;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MasterPage.FunctionPageCode = FunctionPageCode;
            if (LoginHelper.User != null)
            {
                ltTitle.Text = string.Format("Welcome <b>{0}<b/>!", LoginHelper.User.FullName);
            }
        }
    }
}