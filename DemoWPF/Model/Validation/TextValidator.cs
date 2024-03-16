using System.Linq;
using System.Text.RegularExpressions;

namespace DemoWPF.Model.Validation
{
    public static class TextValidator
    {
        public static bool ContainsRussianLetter(string input)
        {
            Regex regex = new(@"[а-яА-Я]");
            return regex.IsMatch(input);
        }

        public static bool ContainsDigit(string input)
        {
            Regex regex = new(@"\d");
            return regex.IsMatch(input);
        }

        public static bool ContainsPunctuation(string input)
        {
            Regex regex = new(@"\p{P}");
            return regex.IsMatch(input);
        }

        public static bool ContainsUpperCase(string input)
        {
            return input.Any(char.IsUpper);
        }
    }
}
