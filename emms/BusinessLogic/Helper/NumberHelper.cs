using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helper
{
    public class NumberHelper
    {
        public static CultureInfo VnCulture
        {
            get
            {
                return new CultureInfo("vi-VN");
            }
        }

        private static NumberFormatInfo NumberFormat()
        {
            NumberFormatInfo numberFormat = new NumberFormatInfo();
            numberFormat.NumberDecimalSeparator = "."; //dấu phân cách thập phân
            numberFormat.NumberGroupSeparator = ","; //dấu phân cách hàng ngàn

            return numberFormat;
        }

        public static bool ConvertToDecimal(string numberString, out decimal value)
        {
            if (decimal.TryParse(numberString, NumberStyles.Number, NumberFormat(), out value))
                return true;
            else
                return false;
        }

        public static bool ConvertToDouble(string numberString, out double value)
        {
            if (double.TryParse(numberString, NumberStyles.Number, NumberFormat(), out value))
                return true;
            else
                return false;
        }

        public static bool ConvertToInt(string numberString, out int value)
        {
            if (int.TryParse(numberString, NumberStyles.Number, NumberFormat(), out value))
                return true;
            else
                return false;
        }

        public static bool ConvertToShort(string numberString, out short value)
        {
            if (short.TryParse(numberString, NumberStyles.Number, NumberFormat(), out value))
                return true;
            else
                return false;
        }

        public static string ToStringNumber(object value)
        {
            string result = String.Format(VnCulture, "{0:N}", value);
            return result.Contains(",") ? result.TrimEnd('0').TrimEnd(',') : result;
        }
    }
}
