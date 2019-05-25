using Http;
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
using System.Windows.Shapes;

namespace EngEval.Pages.Report
{
    /// <summary>
    /// SystemFeedback.xaml 的交互逻辑
    /// </summary>
    public partial class SystemFeedback : Window
    {
        public SystemFeedback()
        {
            InitializeComponent();
            Loaded += SystemFeedback_Loaded;
        }

        private void SystemFeedback_Loaded(object sender, RoutedEventArgs e)
        {
            Q2.Labels = new List<string>() {"非常不流畅","不流畅","一般","流畅","非常流畅"};
            for(int i=3; i<=18; i++)
            {
                EvaluationControl ec = QContent.FindName("Q" + i.ToString()) as EvaluationControl;
                ec.Labels = new List<string>() { "非常弱", "比较弱", "一般", "比较强", "非常强" };
            }
            Q19.Labels = new List<string>() { "非常不准确", "不准确", "一般", "准确", "非常准确" };
            for (int i = 21; i <= 35; i+=2)
            {
                TextBlock tb = QContent.FindName("R" + i.ToString()) as TextBlock;
                tb.Visibility = Visibility.Hidden;
                TextBox tbox = QContent.FindName("Q" + i.ToString()) as TextBox;
                tbox.Visibility = Visibility.Hidden;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInfo())
            {
                MessageBox.Show("请回答所有问题。", "提示");
                return;
            }

            Boolean isSuccess = false;
            while (!isSuccess)
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("uid", mainwin.User.id.ToString());
                parameters.Add("ques", FormatQuestionaire());
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/submitSystemFeedback", parameters, ref isSuccess);
                if (isSuccess)
                {
                    mainwin.User.systemFeedback = "1";
                    Close();
                }
            }
        }

        private string FormatQuestionaire()
        {
            String ques = "";
            ques += Q1.Text + @"|";
            for (int i = 3; i <= 19; i++)
            {
                EvaluationControl ec = QContent.FindName("Q" + i.ToString()) as EvaluationControl;
                ques += ec.GetSelectionValue() + @"|";
            }
            for (int i = 20; i <= 34; i+=2)
            {
                CheckBox cb = QContent.FindName("Q" + i.ToString()) as CheckBox;
                TextBox tbox = QContent.FindName("Q" + (i+1).ToString()) as TextBox;
                if (cb.IsChecked.HasValue && cb.IsChecked.Value)
                {
                    ques += cb.Tag + tbox.Text + @"|";
                }
            }
            ques += Q36.Text + @"|";
            ques += Q37.Text + @"|";
            return ques;
        }

        private bool ValidateInfo()
        {
            if (Q1.Text == "" || Q1.Text == null)
                return false;
            if (Q36.Text == "" || Q36.Text == null)
                return false;
            if (Q37.Text == "" || Q37.Text == null)
                return false;
            return true;
        }

        private void CheckEvent(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            int no = int.Parse(checkBox.Name.Substring(1)) +1;

            TextBlock tb = QContent.FindName("R" + no.ToString()) as TextBlock;
            tb.Visibility = Visibility.Visible;
            TextBox tbox = QContent.FindName("Q" + no.ToString()) as TextBox;
            tbox.Visibility = Visibility.Visible;
        }

        private void UnCheckEvent(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            int no = int.Parse(checkBox.Name.Substring(1)) + 1;

            TextBlock tb = QContent.FindName("R" + no.ToString()) as TextBlock;
            tb.Visibility = Visibility.Hidden;
            TextBox tbox = QContent.FindName("Q" + no.ToString()) as TextBox;
            tbox.Visibility = Visibility.Hidden;
        }
    }
}
