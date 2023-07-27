using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helper
{
    public class DateTimeHelper
    {
        public static DateTime FirstDayOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime LastDayOfMonth(DateTime value)
        {
            return FirstDayOfMonth(value).AddMonths(1).AddMinutes(-1);
        }

        public static DateTime FirstDayOfCurrentYear()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        public static DateTime LastDayOfCurrentYear()
        {
            return new DateTime(DateTime.Now.Year, 12, 31);
        }

        public static bool ConvertStringToDateTime(string value, out DateTime result)
        {
            bool isValid = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            return isValid;
        }
    }
}
