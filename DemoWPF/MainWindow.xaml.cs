using DemoWPF.ViewModel;

namespace DemoWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new InputPanelViewModel();
        }
    }
}
