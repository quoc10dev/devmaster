using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic.Helper
{
    public class LoginHelper
    {
        public static bool IsLogin
        {
            get
            {
                //add reference thư viện System.Web để sử dụng session
                bool isLogin = false;
                if (HttpContext.Current.Session["IsLogin"] != null)
                    isLogin = (bool)HttpContext.Current.Session["IsLogin"];

                return isLogin;
            }
            set
            {
                HttpContext.Current.Session["IsLogin"] = value;
            }
        }

        public static tblUser User
        {
            get
            {
                //add reference thư viện System.Web để sử dụng session
                tblUser currentUser = null;
                if (HttpContext.Current.Session["CurrentUser"] != null)
                    currentUser = (tblUser)HttpContext.Current.Session["CurrentUser"];

                return currentUser;
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }

        public static List<tblFunction> FunctionOfCurrentUser
        {
            get
            {
                //add reference thư viện System.Web để sử dụng session
                List<tblFunction> parentFunctions = null;
                if (HttpContext.Current.Session["FunctionOfCurrentUser"] != null)
                    parentFunctions = (List<tblFunction>)HttpContext.Current.Session["FunctionOfCurrentUser"];

                return parentFunctions;
            }
            set
            {
                HttpContext.Current.Session["FunctionOfCurrentUser"] = value;
            }
        }
    }
}
