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

namespace EngEval
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FrameNavigator("login");
        }

        public void FrameNavigator(string page)
        {
            frmMain.Navigate(new Uri("Pages/"+ page +".xaml", UriKind.Relative));
        }

        //退出系统
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确认退出系统?  (未完成测试会受到影响)", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Close();
            }
        }
    }
}
