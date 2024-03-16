using GalaSoft.MvvmLight.Command;
using DemoWPF.Model.Validation;
using DemoWPF.Model.Validation.Rules;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security;
using MvvmCross.ViewModels;
using DemoWPF.Model;

namespace DemoWPF.ViewModel
{
    public class InputPanelViewModel : MvxNotifyPropertyChanged, INotifyDataErrorInfo
    {
        public InputPanelViewModel() => Models = CarsProvider.GetStrings();

        #region Properties Implementation

        public string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string _shortDate;
        public string ShortDate
        {
            get
            {
                return _shortDate;
            }
            set
            {
                SetProperty(ref _shortDate, value);
                ValidateProperty(nameof(ShortDate));
            }
        }

        public string _longDate;
        public string LongDate
        {
            get
            {
                return _longDate;
            }
            set
            {
                SetProperty(ref _longDate, value);
                ValidateProperty(nameof(LongDate));
            }
        }

        public string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                SetProperty(ref _phoneNumber, value);
                ValidateProperty(nameof(PhoneNumber));
            }
        }

        private string _vin;
        public string VIN
        {
            get
            {
                return _vin;
            }
            set
            {
                SetProperty(ref _vin, value);
                ValidateProperty(nameof(VIN));
            }
        }

        private string _firstSelectedModel;
        public string FirstSelectedModel
        {
            get
            {
                return _firstSelectedModel;
            }
            set
            {
                SetProperty(ref _firstSelectedModel, value);
                ValidateProperty(nameof(FirstSelectedModel));
            }
        }

        private string _secondSelectedModel;
        public string SecondSelectedModel
        {
            get
            {
                return _secondSelectedModel;
            }
            set
            {
                SetProperty(ref _secondSelectedModel, value);
                ValidateProperty(nameof(SecondSelectedModel));
            }
        }

        private IEnumerable _models;
        public IEnumerable Models
        {
            get { return _models; }
            set
            {
                if (_models != value)
                {
                    SetProperty(ref _models, value);
                }
            }
        }

        private SecureString _securePassword;
        public SecureString SecurePassword
        {
            private get { return _securePassword; }
            set
            {
                SetProperty(ref _securePassword, value);
                ValidateProperty(nameof(SecurePassword));
            }
        }

        #endregion

        #region Commands Implementation

        public ICommand PasteNameCommand => new RelayCommand<string>(PasteName);

        private void PasteName(string isWrap)
        {
            string text = Clipboard.GetText();

            if (isWrap == "false")
            {
                text = text.Replace("\n", "").Replace("\r", "");
            }

            Name = text;
        }

        public ICommand CopyNameCommand => new RelayCommand(CopyName, () =>
        string.IsNullOrEmpty(Name) == false &&
        string.IsNullOrWhiteSpace(Name) == false);

        private void CopyName()
        {
            Clipboard.SetText(Name);
        }

        //

        public ICommand PastePhoneNumberCommand => new RelayCommand<string>(PastePhoneNumber);

        private void PastePhoneNumber(string isWrap)
        {
            string text = Clipboard.GetText();

            if (isWrap == "false")
            {
                text = text.Replace("\n", "").Replace("\r", "");
            }

            PhoneNumber = text;
        }

        public ICommand CopyPhoneNumberCommand => new RelayCommand(CopyPhoneNumber, () =>
        string.IsNullOrEmpty(PhoneValidator.Convert(PhoneNumber)) == false &&
        string.IsNullOrWhiteSpace(PhoneValidator.Convert(PhoneNumber)) == false);

        private void CopyPhoneNumber()
        {
            string convertedPhone = PhoneValidator.Convert(PhoneNumber);
            Clipboard.SetText(convertedPhone);
        }

        //

        public ICommand PasteVINCommand => new RelayCommand<string>(PasteVIN);

        private void PasteVIN(string isWrap)
        {
            string text = Clipboard.GetText();

            if (isWrap == "false")
            {
                text = text.Replace("\n", "").Replace("\r", "");
            }

            VIN = text;
        }

        public ICommand CopyVINCommand => new RelayCommand(CopyVIN, () =>
        string.IsNullOrEmpty(VIN) == false &&
        string.IsNullOrWhiteSpace(VIN) == false);

        private void CopyVIN()
        {
            Clipboard.SetText(VIN);
        }

        //

        public ICommand PasteCurrentShortDateCommand => new RelayCommand(PasteCurrentShortDate);

        private void PasteCurrentShortDate()
        {
            DateTime dateTime = DateTime.Now;
            ShortDate = dateTime.ToString("dd/MM");
        }

        public ICommand PasteCurrentLongDateCommand => new RelayCommand(PasteCurrentLongDate);

        private void PasteCurrentLongDate()
        {
            DateTime dateTime = DateTime.Now;
            LongDate = dateTime.ToString("dd/MM/yyyy");
        }

        //

        public ICommand PasteFirstSelectedModelCommand => new RelayCommand(PasteFirstSelectedModel);

        private void PasteFirstSelectedModel()
        {
            string text = Clipboard.GetText();
            text = text.Replace("\n", "").Replace("\r", "");
            FirstSelectedModel = text;
        }

        public ICommand CopyFirstSelectedModelCommand => new RelayCommand(CopyFirstSelectedModel, () =>
        string.IsNullOrEmpty(FirstSelectedModel) == false &&
        string.IsNullOrWhiteSpace(FirstSelectedModel) == false);

        private void CopyFirstSelectedModel()
        {
            Clipboard.SetText(FirstSelectedModel);
        }

        //

        public ICommand PasteSecondSelectedModelCommand => new RelayCommand(PasteSecondSelectedModel);

        private void PasteSecondSelectedModel()
        {
            string text = Clipboard.GetText();
            text = text.Replace("\n", "").Replace("\r", "");
            SecondSelectedModel = text;
        }

        public ICommand CopySecondSelectedModelCommand => new RelayCommand(CopySecondSelectedModel, () =>
        string.IsNullOrEmpty(SecondSelectedModel) == false &&
        string.IsNullOrWhiteSpace(SecondSelectedModel) == false);

        private void CopySecondSelectedModel()
        {
            Clipboard.SetText(SecondSelectedModel);
        }

        #endregion

        #region INotifyDataErrorInfo Implementation

        private readonly Dictionary<string, List<string>> _errors = new();

        public bool HasErrors => _errors.Values.Any(list => list != null && list.Count > 0);

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public string this[string columnName] => ValidateProperty(columnName);

        public IEnumerable? GetErrors(string propertyName)
        {
            if (propertyName == null)
            {
                return null;
            }

            if (_errors.TryGetValue(propertyName, out var errors))
            {
                return (List<string>?)errors;
            }
            else
            {
                return null;
            }
        }

        private string? ValidateProperty(string propertyName)
        {
            var validationResult = Validate(propertyName);
            _errors[propertyName] = validationResult.Errors;

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

            return validationResult.IsValid ? null : validationResult.Errors.FirstOrDefault();
        }

        private (bool IsValid, List<string> Errors) Validate(string propertyName)
        {
            ValidationRule validator = propertyName switch
            {
                "VIN" => new VINValidationRule(),
                "ShortDate" => new ShortDateValidationRule(),
                "LongDate" => new LongDateValidationRule(),
                "PhoneNumber" => new PhoneValidationRule(),
                "FirstSelectedModel" => new ComboBoxValidationRule() { Items = (IEnumerable<string>)Models },
                "SecondSelectedModel" => new ComboBoxValidationRule() { Items = (IEnumerable<string>)Models },
                "SecurePassword" => new PasswordValidationRule(),
                _ => throw new ArgumentException($"Неизвестное свойство: {propertyName}"),
            };

            var validationResult = validator.Validate(GetPropertyValue(propertyName), CultureInfo.CurrentCulture);

            return (validationResult.IsValid, validationResult.IsValid ? new List<string>() : new List<string> { validationResult.ErrorContent.ToString() });
        }

        private object? GetPropertyValue(string propertyName) =>
        GetType().GetProperty(propertyName)?.GetValue(this);

        #endregion
    }
}
