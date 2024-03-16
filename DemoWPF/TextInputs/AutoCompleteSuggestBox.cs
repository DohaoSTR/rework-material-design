using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DemoWPF.View.Styles.TextInputs
{
    public class AutoCompleteSuggestBox : AutoSuggestBox
    {
        public new string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);

                var sortedItems = Sort((IEnumerable<string>)ItemsSource, value);
                Suggestions = new ObservableCollection<string>(sortedItems);
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AutoCompleteSuggestBox));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty HintProperty =
        DependencyProperty.Register("Hint", typeof(string), typeof(AutoCompleteSuggestBox));

        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public AutoCompleteSuggestBox()
        {
            Initialize();

            TextChanged += AutoSuggestBox_TextChanged;
            Loaded += AutoCompleteSuggestBox_Loaded;
        }

        public void Initialize()
        {
            BorderThickness = new Thickness(2);
        }

        private void AutoCompleteSuggestBox_Loaded(object sender, RoutedEventArgs e)
        {
            Suggestions = ItemsSource;

            BindingExpression bindingExpression = GetBindingExpression(TextProperty);
            bindingExpression?.UpdateSource();
        }

        private void AutoSuggestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AutoSuggestBox? box = sender as AutoSuggestBox;

            if (box is not null)
            {
                Text = box.Text;
            }
        }

        public static IEnumerable<string> Sort(IEnumerable<string> items, string filter)
        {
            return items.Where(x => x.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        static AutoCompleteSuggestBox() => DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteSuggestBox), new FrameworkPropertyMetadata(typeof(AutoCompleteSuggestBox)));
    }
}
