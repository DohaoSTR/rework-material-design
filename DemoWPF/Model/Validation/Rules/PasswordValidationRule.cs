using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Controls;

namespace DemoWPF.Model.Validation.Rules
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = DecryptSecureString((SecureString)value);

            if (password == null)
            {
                return new ValidationResult(false, "Введите пароль.");
            }

            if (password.Contains(" "))
            {
                return new ValidationResult(false, "В пароле не должно быть пробелов.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult(false, "Введите пароль.");
            }

            if (TextValidator.ContainsRussianLetter(password) == true)
            {
                return new ValidationResult(false, "В пароле не должно быть русских букв.");
            }

            if (TextValidator.ContainsDigit(password) == false)
            {
                return new ValidationResult(false, "В пароле должна быть хотя бы одна цифра.");
            }

            if (TextValidator.ContainsPunctuation(password) == false)
            {
                return new ValidationResult(false, "В пароле должен быть хотя бы один символ пунктуации.");
            }

            if (TextValidator.ContainsUpperCase(password) == false)
            {
                return new ValidationResult(false, "В пароле должна быть хотя бы одна заглавная буква.");
            }

            if (password.Length < 10)
            {
                return new ValidationResult(false, "Пароль должен состоять минимум из 10 символов.");
            }

            return ValidationResult.ValidResult;
        }

        public static string DecryptSecureString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToBSTR(secureString);
                return Marshal.PtrToStringBSTR(unmanagedString);
            }
            finally
            {
                if (unmanagedString != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(unmanagedString);
                }
            }
        }
    }
}
