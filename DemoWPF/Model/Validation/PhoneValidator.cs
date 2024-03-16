namespace DemoWPF.Model.Validation
{
    public static class PhoneValidator
    {
        public static bool IsFirstNumberValid(string value)
        {
            if (value.StartsWith("7") || value.StartsWith("8"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPhoneNumber(string value)
        {
            string phoneNumbers = value.Replace(" ", "");

            if (phoneNumbers.Length == 11)
            {
                return true;
            }

            return false;
        }

        public static string Convert(string value)
        {
            if (value == null)
            {
                return null;
            }

            string result = "";

            foreach (char c in value)
            {
                if (char.IsDigit(c))
                {
                    result += c;
                }
                else if (c == '_')
                {
                    result += ' ';
                }
            }

            return result;
        }
    }
}
