using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public partial class UserControl_LeftMenu : System.Web.UI.UserControl
{
    public void LoadMenu(string currentFunctionPageCode)
    {
        if (LoginHelper.User != null)
        {
            if (currentFunctionPageCode.Equals(FunctionCode.Default))
            {
                //Active menu Dashboard khi vào trang Default.aspx
                liDashboard.Attributes["class"] = "active";
            }
            else if (currentFunctionPageCode.Equals(FunctionCode.Profile) || currentFunctionPageCode.Equals(FunctionCode.Change_Password))
            {
                liCurrentUserInfo.Attributes.Remove("class");
                liCurrentUserInfo.Attributes["class"] = "active";

                fullName.Attributes.Remove("aria-expanded");
                fullName.Attributes["aria-expanded"] = "true";

                ulCurrentUser.Attributes.Remove("aria-expanded");
                ulCurrentUser.Attributes["aria-expanded"] = "true";

                ulCurrentUser.Attributes.Remove("class");
                ulCurrentUser.Attributes["class"] = "collapse in";

                if (currentFunctionPageCode.Equals(FunctionCode.Profile))
                    liProfile.Attributes["class"] = "active";
                else if (currentFunctionPageCode.Equals(FunctionCode.Change_Password))
                    liChangePassword.Attributes["class"] = "active";
            }

            tblUser currentUser = LoginHelper.User;
            lblFullName.Text = currentUser.FullName;

            //Lấy tất cả các chức năng được phân quyền cho user
            //Bind chức năng cha
            //Bind chức năng con của chức năng cha theo đệ quy
            StringBuilder sbResult = new StringBuilder();
            StringBuilder sbParent = new StringBuilder();

            //Kiểm tra trong session nếu có thì lấy, tránh lấy trong database nhiều lần
            List<tblFunction> funcAll = null;
            if (LoginHelper.FunctionOfCurrentUser != null)
                funcAll = LoginHelper.FunctionOfCurrentUser;
            else
            {
                funcAll = FunctionManager.GetAllByIdUser(currentUser.ID);
                LoginHelper.FunctionOfCurrentUser = funcAll;
            }

            //Kiểm tra quyền truy cập trang ViewProfile.aspx và ChangePassword.aspx để ẩn menu
            if (HttpContext.Current.Session["CurrentUserRight"] != null)
            {
                List<RightInFunction> CurrentUserRight = (List<RightInFunction>)HttpContext.Current.Session["CurrentUserRight"];
                IEnumerable<RightInFunction> result = CurrentUserRight.Where(x => x.FunctionCode.Equals(FunctionCode.Profile));
                if (result.Count() == 0)
                    liProfile.Visible = false;
                result = CurrentUserRight.Where(x => x.FunctionCode.Equals(FunctionCode.Change_Password));
                if (result.Count() == 0)
                    liChangePassword.Visible = false;
            }

            //chỉ lấy các function cha cho phép show lên menu
            List<tblFunction> parent = funcAll.FindAll(x => ((x.ParentCode == string.Empty || x.ParentCode == null) && x.ShowInMenu == true));
            List<tblFunction> child = null;
            foreach (tblFunction function in parent)
            {
                bool isActive = false;
                sbParent = new StringBuilder();

                child = funcAll.FindAll(x => x.ParentCode == function.Code && x.ShowInMenu == true);

                sbParent.Append("<span class=\"has-icon\">");
                if (!string.IsNullOrEmpty(function.Icon_css))
                    sbParent.AppendFormat("<i class=\"{0}\"></i>", function.Icon_css.Trim());
                sbParent.Append("</span>");
                sbParent.Append("<span class=\"nav-title\">");
                sbParent.Append(function.Name);
                sbParent.Append("</span>");
                sbParent.Append("</a>");
                sbParent.AppendFormat("{0}", LoadChildrenMenu(child, currentFunctionPageCode, ref isActive));
                sbParent.Append("</li>");

                if (isActive)
                {
                    sbResult.AppendFormat("<li class=\"active\"><a href='#' class=\"has-arrow\" aria-expanded=\"true\" title='{0}'>{1}",
                        function.Description, sbParent.ToString());
                }
                else
                {
                    sbResult.AppendFormat("<li><a href='#' class=\"has-arrow\" aria-expanded=\"false\" title='{0}'>{1}",
                        function.Description, sbParent.ToString());
                }
            }
            ltrMenu.Text = sbResult.ToString();
        }
    }

    private string LoadChildrenMenu(List<tblFunction> child, string currentFunctionPageCode, ref bool isActive)
    {
        //Chỉ dùng menu 2 cấp
        //Lấy các chức năng con của chức năng cha gốc
        //Với mỗi chức năng con
        StringBuilder sbResult = new StringBuilder();
        StringBuilder sbSubItem = new StringBuilder();
        if (child != null && child.Count > 0)
        {
            foreach (tblFunction item in child)
            {
                if (item.Code.Equals(currentFunctionPageCode))
                {
                    sbSubItem.Append("<li class='active'>");
                    isActive = true;
                }
                else
                    sbSubItem.Append("<li>");

                sbSubItem.AppendFormat("<a href=\"{0}\" title=\"{1}\">", item.Url, item.Description);
                sbSubItem.Append("<span class=\"has-icon\">");
                sbSubItem.AppendFormat("<i class=\"text-icons\">{0}</i>", item.ShortName);
                sbSubItem.Append("</span>");
                sbSubItem.AppendFormat("<span class=\"nav-title\">{0}</span>", item.Name);
                sbSubItem.Append("</a>");
                sbSubItem.Append("</li>");
            }
            sbSubItem.Append("</ul>");

            if (isActive)
                sbResult.AppendFormat("<ul aria-expanded='true' class='collapse in'> {0}", sbSubItem.ToString());
            else
                sbResult.AppendFormat("<ul aria-expanded='false' class='collapse'> {0}", sbSubItem.ToString());
        }
        return sbResult.ToString();
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        LoginHelper.IsLogin = false;
        LoginHelper.User = null;
        LoginHelper.FunctionOfCurrentUser = null;
        HttpContext.Current.Session["CurrentUserRight"] = null;  //gán session chứa các RightCode của user về null (BasePage)

        System.Web.HttpContext.Current.Session["FirstLoadMenu"] = null;

        Response.Redirect("Login.aspx");
    }
}