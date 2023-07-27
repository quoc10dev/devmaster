using BusinessLogic.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public class MessageType
    {
        //Danh sách các title khi hiển thị thông báo popup modal
        public static string Message = "Message";
        public static string Error = "Error";
    }

    //public BasePage()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    public virtual string FunctionPageCode
    {
        //Mã từng trang, tương ứng với cột Code trong bảng tblFunctions
        //Dùng làm căn cứ để active menu
        get
        {
            return string.Empty;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!BusinessLogic.Helper.LoginHelper.IsLogin)
        {
            //Nếu chưa đăng nhập thì bắt buộc phải đăng nhập mới nhảy tới trang
            var returnUrl = Server.UrlEncode(Request.Url.PathAndQuery);

            string path = Request.Url.GetLeftPart(UriPartial.Path); //get url without querystring
            string fileName = System.IO.Path.GetFileName(path);
            if (!fileName.Equals("Login.aspx"))
                Response.Redirect("Login.aspx?ReturnURL=" + returnUrl);
        }
        else
        {
            //Nếu đã đăng nhập thành công, kiểm tra quyền được phép truy cập trang
            CheckFunctionPermission();
        }
    }

    public void CheckFunctionPermission()
    {
        if (CurrentUserRight != null && CurrentUserRight.Count > 0 && !string.IsNullOrEmpty(FunctionPageCode))
        {
            if (!FunctionPageCode.Equals(FunctionCode.Default))
            {
                IEnumerable<RightInFunction> result = CurrentUserRight.Where(x => x.FunctionCode.Equals(FunctionPageCode));
                if (result.Count() == 0)
                    Response.Redirect("AccessDenied.aspx");
            }
        }
    }

    public List<RightInFunction> CurrentUserRight
    {
        get
        {
            if (System.Web.HttpContext.Current.Session["CurrentUserRight"] != null)
                return (List<RightInFunction>)System.Web.HttpContext.Current.Session["CurrentUserRight"];

            return null;
        }
        set
        {
            System.Web.HttpContext.Current.Session["CurrentUserRight"] = value;
        }
    }


    public static void ShowMessage(Page page, string content, string messageType)
    {
        //Popup thông báo chung: bật modal ShowMessage.ascx (ID = ShowMessage trong Master Page)
        Label lblMessageType = (Label)page.Master.FindControl("ShowMessage").FindControl("lblMessageType");
        if (lblMessageType != null)
            lblMessageType.Text = messageType;

        Literal lblContent = (Literal)page.Master.FindControl("ShowMessage").FindControl("ltrContentMessage");
        if (lblContent != null)
            lblContent.Text = content;

        ScriptManager.RegisterStartupScript(page, page.GetType(), "OpenMessageDialog", "OpenMessage();", true);
    }

    public static void SetFormatControl(Control control, string element)
    {
        //Chỉnh lại format các control kiểu datetime, number
        ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "SetFormatControls", "Control.Build(" + element + ")", true);
    }

    public int GetValueOfQueryStringTypeInt(string nameQueryString)
    {
        if (ViewState[nameQueryString] != null)
            return Convert.ToInt32(ViewState[nameQueryString]);
        else if (Request.QueryString.AllKeys.Contains(nameQueryString))
        {
            if (!string.IsNullOrEmpty(Request.QueryString[nameQueryString]))
            {
                int result = 0;
                int.TryParse(Request.QueryString[nameQueryString], out result);
                ViewState[nameQueryString] = result;
                return result;
            }
            else
                return 0;
        }
        else
            return 0;
    }

    public string GetValueOfQueryStringTypeString(string nameQueryString)
    {
        if (ViewState[nameQueryString] != null)
            return ViewState[nameQueryString].ToString();
        else if (Request.QueryString.AllKeys.Contains(nameQueryString))
        {
            if (!string.IsNullOrEmpty(Request.QueryString[nameQueryString]))
            {
                string result = Request.QueryString[nameQueryString];
                ViewState[nameQueryString] = result;
                return result;
            }
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }
}