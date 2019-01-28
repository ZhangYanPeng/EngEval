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

namespace EngEval.Pages.Test
{
    /// <summary>
    /// Prepare.xaml 的交互逻辑
    /// </summary>
    public partial class Prepare : Page
    {
        public Prepare()
        {
            InitializeComponent();
        }

        //开始测试
        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("Test/Dotest");
        }

        //返回首页
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("funclist");
        }
    }
}
