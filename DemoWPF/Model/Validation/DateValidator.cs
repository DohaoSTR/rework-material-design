using System;
using System.Globalization;

namespace DemoWPF.Model.Validation
{
    public static class DateValidator
    {
        public static bool IsDayValid(string value)
        {
            string dayString = value.Substring(0, 2).Replace("_", "");

            if (string.IsNullOrEmpty(dayString) == false)
            {
                int day = int.Parse(dayString);

                if (day > 0 && day <= 31)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsMonthValid(string value)
        {
            string monthString = value.Substring(3, 2).Replace("_", "");

            if (string.IsNullOrEmpty(monthString) == false)
            {
                int month = int.Parse(monthString);

                if (month > 0 && month <= 12)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsYearValid(string value)
        {
            string yearString = value.Substring(6, 4).Replace("_", "");

            if (string.IsNullOrEmpty(yearString) == false)
            {
                int year = int.Parse(yearString);

                if (year >= 2000 && year <= 2100)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsShortDateValid(string value)
        {
            if (IsDayValid(value) && IsMonthValid(value))
            {
                return true;
            }

            return false;
        }

        public static bool IsLongDateValid(string value)
        {
            if (IsDayValid(value) && IsMonthValid(value) && IsYearValid(value))
            {
                return true;
            }

            return false;
        }

        public static bool IsShortDate(string value)
        {
            string pattern = "__/__";

            if (DateTime.TryParseExact(value, pattern, null, DateTimeStyles.None, out DateTime result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsLongDate(string value)
        {
            string pattern = "__/__/____";

            if (DateTime.TryParseExact(value, pattern, null, DateTimeStyles.None, out DateTime result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
