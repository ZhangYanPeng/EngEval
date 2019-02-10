using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EngEval.Pages
{
    /// <summary>
    /// funclist.xaml 的交互逻辑
    /// </summary>
    public partial class funclist : Page
    {
        public funclist()
        {
            InitializeComponent();
        }

        private void BeginTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("Test/Prepare");
        }

        private void SingleReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("Report/ReportList");
        }
    }
}
