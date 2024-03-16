using System.Globalization;
using System.Windows.Controls;

namespace DemoWPF.Model.Validation.Rules
{
    public class VINValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string vin == false)
            {
                return new ValidationResult(false, "Введите VIN.");
            }

            if (vin.Contains(" "))
            {
                return new ValidationResult(false, "В VIN не должно быть пробелов.");
            }

            if (string.IsNullOrWhiteSpace(vin))
            {
                return new ValidationResult(false, "Введите VIN.");
            }

            if (VINValidator.IsRussianLetters(vin))
            {
                return new ValidationResult(false, "В VIN не должно быть русских букв.");
            }

            if (VINValidator.IsDigitsAndSymbols(vin))
            {
                return new ValidationResult(false, "В VIN должны быть только буквы и цифры.");
            }

            if (VINValidator.IsSimilarLetters(vin))
            {
                return new ValidationResult(false, "В VIN не должно быть букв - I, O, Q.");
            }

            if (VINValidator.IsCapitalLetters(vin))
            {
                return new ValidationResult(false, "В VIN все буквы должны быть заглавными.");
            }

            if (vin.Length != 17)
            {
                return new ValidationResult(false, "VIN должен состоять из 17 символов");
            }

            return ValidationResult.ValidResult;
        }
    }
}
