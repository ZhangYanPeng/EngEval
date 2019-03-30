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

namespace EngEval.Pages.Questionarie
{
    /// <summary>
    /// TestQuestionaire.xaml 的交互逻辑
    /// </summary>
    public partial class TestQuestionaire : Window
    {
        string testid { get; set; }
        public TestQuestionaire(string tid)
        {
            InitializeComponent();
            testid = tid;
        }

        private bool ValidateInfo()
        {
            for(int i = 1; i <= 20; i++)
            {
                if (getQues(i).Content.ToString() == "请选择")
                    return false;
            }
            return true;
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
                parameters.Add("userId", mainwin.User.id.ToString());
                parameters.Add("testId", testid);
                parameters.Add("ques", FormatQuestionaire());
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/RateSubmit", parameters, ref isSuccess);
                if (isSuccess)
                {
                    Close();
                }
            }
        }

        private string FormatQuestionaire()
        {
            string questionaire = "";
            for (int i = 1; i <= 20; i++)
            {
                questionaire += getQues(i).Content.ToString() + " | ";
            }
            return questionaire;
        }

        private ComboBoxItem getQues(int i)
        {
            switch (i)
            {
                case 1: return Q1.SelectedItem as ComboBoxItem;
                case 2: return Q2.SelectedItem as ComboBoxItem;
                case 3: return Q3.SelectedItem as ComboBoxItem;
                case 4: return Q4.SelectedItem as ComboBoxItem;
                case 5: return Q5.SelectedItem as ComboBoxItem;
                case 6: return Q6.SelectedItem as ComboBoxItem;
                case 7: return Q7.SelectedItem as ComboBoxItem;
                case 8: return Q8.SelectedItem as ComboBoxItem;
                case 9: return Q9.SelectedItem as ComboBoxItem;
                case 10: return Q10.SelectedItem as ComboBoxItem;
                case 11: return Q11.SelectedItem as ComboBoxItem;
                case 12: return Q12.SelectedItem as ComboBoxItem;
                case 13: return Q13.SelectedItem as ComboBoxItem;
                case 14: return Q14.SelectedItem as ComboBoxItem;
                case 15: return Q15.SelectedItem as ComboBoxItem;
                case 16: return Q16.SelectedItem as ComboBoxItem;
                case 17: return Q17.SelectedItem as ComboBoxItem;
                case 18: return Q18.SelectedItem as ComboBoxItem;
                case 19: return Q19.SelectedItem as ComboBoxItem;
                case 20: return Q20.SelectedItem as ComboBoxItem;
            }
            return null;
        }
    }
}
