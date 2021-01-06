using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Mvvm;
using Prism;
using Prism.Commands;
using LaserFeederHelper.View;
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
