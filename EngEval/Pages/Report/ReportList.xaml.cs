using EngEval.Model;
using EngEval.Pages.Questionarie;
using Http;
using Newtonsoft.Json;
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

namespace EngEval.Pages.Report
{
    /// <summary>
    /// ReportList.xaml 的交互逻辑
    /// </summary>
    public partial class ReportList : Page
    {
        public ReportList()
        {
            InitializeComponent();
            Loaded += LoadAllReports;
        }

        private void LoadAllReports(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Boolean isSuccess = false;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("uid", mainwin.User.id.ToString());
            ListeningTest[] tests = null;
            while (!isSuccess)
            {
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/loadAnsweredTest", parameters, ref isSuccess);
                tests = JsonConvert.DeserializeObject<ListeningTest[]>(rtext);
            }
            TestList.ItemsSource = tests;
        }

        private void SingleReport_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ListeningTest test = btn.DataContext as ListeningTest;
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

            bool isSuccess = false; Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("uid", mainwin.User.id.ToString());
            parameters.Add("tid", test.id);
            string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/getAnswerStatus", parameters, ref isSuccess);
            if (!isSuccess)
            {
                MessageBox.Show("网络错误，请重试！", "抱歉");
            }
            else
            {
                //if(rtext=="" || rtext== null)
                //{
                //    TestQuestionaire testQuestionaire = new TestQuestionaire(test.id);
                //    testQuestionaire.ShowDialog();
                //}
                ReportQRCode page = new ReportQRCode(test.id, 0);
                mainwin.frmMain.Content = page;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("funclist");
        }
    }
}
