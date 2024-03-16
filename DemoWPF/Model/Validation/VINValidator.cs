using System.Text.RegularExpressions;

namespace DemoWPF.Model.Validation
{
    public static class VINValidator
    {
        public static bool IsSimilarLetters(string value)
        {
            string pattern = "[IOQ]";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            if (value is null)
            {
                return false;
            }

            return regex.IsMatch(value);
        }

        public static bool IsRussianLetters(string value)
        {
            string pattern = @"[\p{IsCyrillic}]";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            if (value is null)
            {
                return false;
            }

            return regex.IsMatch(value);
        }

        public static bool IsDigitsAndSymbols(string value)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            if (value is null)
            {
                return false;
            }

            if (regex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsCapitalLetters(string value)
        {
            string pattern = @"^[A-Z0-9]+$";
            Regex regex = new(pattern);

            if (value is null)
            {
                return false;
            }

            if (regex.IsMatch(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
