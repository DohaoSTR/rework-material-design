using System.Globalization;
using System.Windows.Controls;

namespace DemoWPF.Model.Validation.Rules
{
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string phone == false)
            {
                return new ValidationResult(false, "Введите номер телефона.");
            }

            string convertedPhone = PhoneValidator.Convert(phone);

            if (PhoneValidator.IsFirstNumberValid(convertedPhone) == false)
            {
                return new ValidationResult(false, "Номер телефона должен начинаться с 7,8.");
            }

            if (PhoneValidator.IsPhoneNumber(convertedPhone) == false)
            {
                return new ValidationResult(false, "Введите номер телефона полностью.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
