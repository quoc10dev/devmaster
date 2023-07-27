using System;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public static string FunctionPageCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)  //&& System.Web.HttpContext.Current.Session["FirstLoadMenu"] == null
        {
            this.ucMenu.LoadMenu(FunctionPageCode);
            //System.Web.HttpContext.Current.Session["FirstLoadMenu"] = true;
        }
        //else
        //upButton.Update();

        //if (!LoginHelper.IsLogin)
        //{
        //    this.ucMenu.LoadMenu();
        //}
    }
}
