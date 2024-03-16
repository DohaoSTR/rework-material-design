using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = AutoSuggestBoxListPart, Type = typeof(ListBox))]
public class AutoSuggestBox : TextBox
{
    private const string AutoSuggestBoxListPart = "PART_AutoSuggestBoxList";

    #region Properties

    protected ListBox? _autoSuggestBoxList;

    #endregion

    #region Dependency Properties

    public static readonly DependencyProperty HelpButtonsProperty =
    DependencyProperty.Register(
    "HelpButtons",
    typeof(ObservableCollection<HelpButton>),
    typeof(AutoSuggestBox),
    new PropertyMetadata(null));

    public ObservableCollection<HelpButton> HelpButtons
    {
        get => (ObservableCollection<HelpButton>)GetValue(HelpButtonsProperty);
        set => SetValue(HelpButtonsProperty, value);
    }

    public AutoSuggestBox() => HelpButtons = new ObservableCollection<HelpButton>();

    public IEnumerable Suggestions
    {
        get => (IEnumerable)GetValue(SuggestionsProperty);
        set => SetValue(SuggestionsProperty, value);
    }

    public static readonly DependencyProperty SuggestionsProperty =
        DependencyProperty.Register("Suggestions", typeof(IEnumerable), typeof(AutoSuggestBox), new PropertyMetadata(null));


    public string ValueMember
    {
        get => (string)GetValue(ValueMemberProperty);
        set => SetValue(ValueMemberProperty, value);
    }
    public static readonly DependencyProperty ValueMemberProperty =
        DependencyProperty.Register("ValueMember", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata(default(string)));


    public string DisplayMember
    {
        get => (string)GetValue(DisplayMemberProperty);
        set => SetValue(DisplayMemberProperty, value);
    }
    public static readonly DependencyProperty DisplayMemberProperty =
        DependencyProperty.Register("DisplayMember", typeof(string), typeof(AutoSuggestBox), new PropertyMetadata(default(string)));

    public Brush DropDownBackground
    {
        get => (Brush)GetValue(DropDownBackgroundProperty);
        set => SetValue(DropDownBackgroundProperty, value);
    }
    public static readonly DependencyProperty DropDownBackgroundProperty =
        DependencyProperty.Register("DropDownBackground", typeof(Brush), typeof(AutoSuggestBox), new PropertyMetadata(default(Brush)));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(AutoSuggestBox), new PropertyMetadata(default(DataTemplate)));

    public Style ItemContainerStyle
    {
        get => (Style)GetValue(ItemContainerStyleProperty);
        set => SetValue(ItemContainerStyleProperty, value);
    }
    public static readonly DependencyProperty ItemContainerStyleProperty =
        DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(AutoSuggestBox), new PropertyMetadata(default(Style)));

    public Elevation DropDownElevation
    {
        get => (Elevation)GetValue(DropDownElevationProperty);
        set => SetValue(DropDownElevationProperty, value);
    }
    public static readonly DependencyProperty DropDownElevationProperty =
        DependencyProperty.Register("DropDownElevation", typeof(Elevation), typeof(AutoSuggestBox), new PropertyMetadata(default(Elevation)));

    public double DropDownMaxHeight
    {
        get => (double)GetValue(DropDownMaxHeightProperty);
        set => SetValue(DropDownMaxHeightProperty, value);
    }
    public static readonly DependencyProperty DropDownMaxHeightProperty =
        DependencyProperty.Register("DropDownMaxHeight", typeof(double), typeof(AutoSuggestBox), new PropertyMetadata(200.0));

    public bool IsSuggestionOpen
    {
        get => (bool)GetValue(IsSuggestionOpenProperty);
        set
        {
            SetValue(IsSuggestionOpenProperty, value);
        }
    }
    public static readonly DependencyProperty IsSuggestionOpenProperty =
        DependencyProperty.Register("IsSuggestionOpen", typeof(bool), typeof(AutoSuggestBox), new PropertyMetadata(default(bool)));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(AutoSuggestBox), new PropertyMetadata(default(object)));


    public object SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }
    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register("SelectedValue", typeof(object), typeof(AutoSuggestBox), new PropertyMetadata(default(object)));





    public static readonly RoutedEvent SuggestionChosenEvent =
        EventManager.RegisterRoutedEvent(
            "SuggestionChosen",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<object>),
            typeof(AutoSuggestBox));

    public event RoutedPropertyChangedEventHandler<object> SuggestionChosen
    {
        add => AddHandler(SuggestionChosenEvent, value);
        remove => RemoveHandler(SuggestionChosenEvent, value);
    }

    #endregion

    static AutoSuggestBox() => DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoSuggestBox), new FrameworkPropertyMetadata(typeof(AutoSuggestBox)));

    #region Override methods

    public override void OnApplyTemplate()
    {
        if (GetTemplateChild(AutoSuggestBoxListPart) is ListBox listBox)
        {
            _autoSuggestBoxList = listBox;

            listBox.PreviewMouseDown -= AutoSuggestionListBox_PreviewMouseDown;

            base.OnApplyTemplate();

            listBox.PreviewMouseDown += AutoSuggestionListBox_PreviewMouseDown;
        }
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);
        if (_autoSuggestBoxList is null) return;
        switch (e.Key)
        {
            case Key.Down:
                IncrementSelection();
                break;
            case Key.Up:
                DecrementSelection();
                break;
            case Key.Enter:
                CommitValueSelection();
                break;
            case Key.Escape:
                CloseAutoSuggestionPopUp();
                break;
            case Key.Tab:
                CommitValueSelection();
                break;
            default:
                return;
        }
        e.Handled = true;
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        CloseAutoSuggestionPopUp();
    }

    protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseUp(e);

        if (e.OriginalSource is Thumb || e.OriginalSource is RepeatButton || e.OriginalSource is Rectangle ||
            (e.OriginalSource is Border && (e.OriginalSource as Border).TemplatedParent is Thumb) || e.OriginalSource is TextBlock)
        {
            //Debug.WriteLine($"Type OnPreviewMouseUp  - {e.OriginalSource.GetType()}");
            //Debug.WriteLine($"Open OnPreviewMouseUp OriginalSource - {IsSuggestionOpen}");
            return;
        }

        if (_autoSuggestBoxList is null)
            return;
        if (IsFocused && _autoSuggestBoxList.Items.Count > 0 && IsSuggestionOpen == false)
        {
            //Debug.WriteLine($"Open OnPreviewMouseUp if - {IsSuggestionOpen}");
            IsSuggestionOpen = true;
        }
        else if (IsFocused && IsSuggestionOpen == true)
        {
            //Debug.WriteLine($"Open OnPreviewMouseUp else - {IsSuggestionOpen}");
            IsSuggestionOpen = false;
        }
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);

        if (_autoSuggestBoxList is null)
            return;
        if (Text.Length >= 0 && IsFocused && _autoSuggestBoxList.Items.Count > 0)
        {
            IsSuggestionOpen = true;
        }    
        else
        {
            IsSuggestionOpen = false;
        }
    }

    #endregion

    #region Callback handlers

    private void AutoSuggestionListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (_autoSuggestBoxList is not null && e.OriginalSource is FrameworkElement element)
        {
            if (!_autoSuggestBoxList.Items.Contains(element.DataContext))
                return;
            _autoSuggestBoxList.SelectedItem = element.DataContext;
            CommitValueSelection();
        }
    }

    #endregion

    #region Methods    

    private void CloseAutoSuggestionPopUp()
    {
        IsSuggestionOpen = false;
    }

    private void CommitValueSelection()
    {
        if (_autoSuggestBoxList?.SelectedValue is { } selectedValue)
        {
            var oldValue = Text;
            Text = selectedValue.ToString();
            if (Text != null)
            {
                CaretIndex = Text.Length;
            }
            CloseAutoSuggestionPopUp();
            var args = new RoutedPropertyChangedEventArgs<object?>(oldValue, Text)
            {
                RoutedEvent = SuggestionChosenEvent
            };
            RaiseEvent(args);
        }
    }

    private void DecrementSelection()
    {
        if (_autoSuggestBoxList is null)
            return;
        ICollectionView collectionView = CollectionViewSource.GetDefaultView(Suggestions);
        if (collectionView.IsCurrentBeforeFirst)
            collectionView.MoveCurrentToLast();
        else
            collectionView.MoveCurrentToPrevious();
        _autoSuggestBoxList.ScrollIntoView(_autoSuggestBoxList.SelectedItem);
    }

    private void IncrementSelection()
    {
        if (_autoSuggestBoxList is null)
            return;
        ICollectionView collectionView = CollectionViewSource.GetDefaultView(Suggestions);
        if (collectionView.IsCurrentAfterLast)
            collectionView.MoveCurrentToFirst();
        else
            collectionView.MoveCurrentToNext();
        _autoSuggestBoxList.ScrollIntoView(_autoSuggestBoxList.SelectedItem);
    }

    #endregion
}
