using System.Windows;
using Prism.Commands;
using LaserFeederHelper.VM;

namespace LaserFeederHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DelegateCommand MyCommand { get; set; }


        public MainWindow(MainWindowVM windowVM)
        {
            InitializeComponent();
            DataContext = windowVM;
        }


    }
}
