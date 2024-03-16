using System.Globalization;
using System.Windows.Controls;

namespace DemoWPF.Model.Validation.Rules
{
    public class LongDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string date == false)
            {
                return new ValidationResult(false, "Введите дату.");
            }

            if (DateValidator.IsDayValid(date) == false)
            {
                return new ValidationResult(false, "День не может быть меньше 1 или больше 31.");
            }

            if (DateValidator.IsMonthValid(date) == false)
            {
                return new ValidationResult(false, "Месяц не может быть меньше 1 или больше 12.");
            }

            if (DateValidator.IsYearValid(date) == false)
            {
                return new ValidationResult(false, "Год не может быть меньше 2000 или больше 2100.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
