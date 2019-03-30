using Http;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

        private void OverallReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            //ReportQRCode page = new ReportQRCode(mainwin.User.id.ToString(),1);
            //mainwin.frmMain.Content = page;
            string strCode = Setting.BASE_URL + "pdf/";
            Boolean isSuccess = false;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("uid", mainwin.User.id.ToString());
            string url = Setting.BASE_URL + "test/getOverallReport";
            string rtext = HttpRequestHelper.HttpGet(url, parameters, ref isSuccess);
            strCode += mainwin.User.id + "overall.pdf";

            url = strCode;
            System.Diagnostics.Process.Start(strCode);
        }
    }
}
