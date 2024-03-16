using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace MaterialDesignThemes.Wpf.Internal;

public static class ClearText
{
    public static readonly RoutedCommand ClearCommand = new();

    public static bool GetHandlesClearCommand(DependencyObject obj)
        => (bool)obj.GetValue(HandlesClearCommandProperty);

    public static void SetHandlesClearCommand(DependencyObject obj, bool value)
        => obj.SetValue(HandlesClearCommandProperty, value);

    public static readonly DependencyProperty HandlesClearCommandProperty =
        DependencyProperty.RegisterAttached("HandlesClearCommand", typeof(bool), typeof(ClearText), new PropertyMetadata(false, OnHandlesClearCommandChanged));

    private static void OnHandlesClearCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.CommandBindings.Add(new CommandBinding(ClearCommand, OnClearCommand));
            }
            else
            {
                for (int i = element.CommandBindings.Count - 1; i >= 0; i--)
                {
                    if (element.CommandBindings[i].Command == ClearCommand)
                    {
                        element.CommandBindings.RemoveAt(i);
                    }
                }
            }
        }

        static void OnClearCommand(object sender, ExecutedRoutedEventArgs e)
        {
            switch (sender)
            {
                case DatePicker datePicker:
                    datePicker.SetCurrentValue(DatePicker.SelectedDateProperty, null);
                    break;
                case TimePicker timePicker:
                    timePicker.SetCurrentValue(TimePicker.SelectedTimeProperty, null);
                    break;
                case TextBox textBox:
                    if (Clipboard.ContainsText())
                    {
                        string text = Clipboard.GetText();
                        //textBox.ClearValue(TextBox.TextProperty);
                        textBox.Text = "";
                        //textBox.SetCurrentValue(TextBox.TextProperty, text);
                        textBox.Text = text;
                    }
                    break;
                case ComboBox comboBox:
                    comboBox.SetCurrentValue(ComboBox.TextProperty, null);
                    comboBox.SetCurrentValue(Selector.SelectedItemProperty, null);
                    break;
                case PasswordBox passwordBox:
                    passwordBox.Password = null;
                    break;
            }
            e.Handled = true;
        }
    }
}
