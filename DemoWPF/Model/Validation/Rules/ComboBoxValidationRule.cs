using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace DemoWPF.Model.Validation.Rules
{
    public class ComboBoxValidationRule : ValidationRule
    {
        public IEnumerable<string>? Items { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(false, "Введите название модели.");
            }

            if (Items == null || Items?.Contains(value.ToString()) == false)
            {
                return new ValidationResult(false, "Такое название модели не добавлено в БД.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
