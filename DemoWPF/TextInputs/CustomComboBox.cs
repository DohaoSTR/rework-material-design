using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DemoWPF.View.Styles.TextInputs
{
    public class CustomComboBox : ComboBox
    {
        public ObservableCollection<HelpButton> HelpButtons
        {
            get { return (ObservableCollection<HelpButton>)GetValue(HelpButtonsProperty); }
            set { SetValue(HelpButtonsProperty, value); }
        }

        public static readonly DependencyProperty HelpButtonsProperty =
            DependencyProperty.Register("HelpButtons", typeof(ObservableCollection<HelpButton>), typeof(CustomComboBox), new PropertyMetadata(null));

        public static readonly DependencyProperty HintProperty =
        DependencyProperty.Register("Hint", typeof(string), typeof(CustomComboBox));

        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public CustomComboBox()
        {
            Initialize();

            Loaded += CustomComboBox_Loaded;

            HelpButtons = new ObservableCollection<HelpButton>();
        }

        private void CustomComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            BindingExpression bindingExpression = GetBindingExpression(TextProperty);
            bindingExpression?.UpdateSource();
        }

        public void Initialize()
        {
            BorderThickness = new Thickness(2);
        }

        static CustomComboBox() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomComboBox), new FrameworkPropertyMetadata(typeof(CustomComboBox)));
    }
}
