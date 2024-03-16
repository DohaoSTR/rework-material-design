using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace DemoWPF.Model.Validation.Rules
{
    public class LoginValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string login == false)
            {
                return new ValidationResult(false, "Введите имя пользователя.");
            }

            if (login.Contains(" "))
            {
                return new ValidationResult(false, "В имени не должно быть пробелов.");
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                return new ValidationResult(false, "Введите имя пользователя.");
            }

            if (TextValidator.ContainsRussianLetter(login) == true)
            {
                return new ValidationResult(false, "В имени пользователя не должно быть русских букв.");
            }

            if (login.All(char.IsDigit) == true)
            {
                return new ValidationResult(false, "Имя пользователя не должно полностью состоять из цифр.");
            }

            if (TextValidator.ContainsPunctuation(login) == true)
            {
                return new ValidationResult(false, "В имени пользователя не должно быть символов пунктуации.");
            }

            if (login.Length < 5)
            {
                return new ValidationResult(false, "Имя пользователя должен состоять минимум из 5 символов");
            }

            return ValidationResult.ValidResult;
        }
    }
}
