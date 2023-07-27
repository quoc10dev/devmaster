using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helper
{
    public class Sort
    {
        //Ký hiệu sắp xếp theo cú pháp sql server
        public static string ASC = "ASC";
        public static string DESC = "DESC";
    }

    public class SystemSetting
    {
        public static int PageSize = 20;
        public static string SqlConnectStringName = "Con";
        public static string EntityConnectStringName = "EMMEntities";
    }
}
