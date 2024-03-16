using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace DemoWPF.View.Styles.TextInputs
{
    public class CustomMaskedTextBox : MaskedTextBox
    {
        public ObservableCollection<HelpButton> HelpButtons
        {
            get { return (ObservableCollection<HelpButton>)GetValue(HelpButtonsProperty); }
            set { SetValue(HelpButtonsProperty, value); }
        }

        public static readonly DependencyProperty HelpButtonsProperty =
            DependencyProperty.Register("HelpButtons", typeof(ObservableCollection<HelpButton>), typeof(CustomMaskedTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty HintProperty =
        DependencyProperty.Register("Hint", typeof(string), typeof(CustomMaskedTextBox), new PropertyMetadata(""));

        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public CustomMaskedTextBox()
        {
            Initialize();

            Loaded += CustomMaskedTextBox_Loaded;

            HelpButtons = new ObservableCollection<HelpButton>();
        }

        private void CustomMaskedTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            BindingExpression bindingExpression = GetBindingExpression(TextProperty);
            bindingExpression?.UpdateSource();
        }

        public void Initialize()
        {
            BorderThickness = new Thickness(2);
        }

        static CustomMaskedTextBox() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomMaskedTextBox), new FrameworkPropertyMetadata(typeof(CustomMaskedTextBox)));
    }
}
