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
    /// Information.xaml 的交互逻辑
    /// </summary>
    public partial class Information : Window
    {
        public Information()
        {
            InitializeComponent();
        }

        public bool ValidateInfo()
        {
            if ((Q1.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((Q2.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((Q3.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((Q4.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((Q5.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((Q6.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((Q3.SelectedItem as ComboBoxItem).Content.ToString() == "有" && (Q3_1.Text == "" || Q3_2.Text == "") )
                return false;
            if ((Q5.SelectedItem as ComboBoxItem).Content.ToString() == "以上都不是" && (Q5_1.Text == "" || Q5_1.Text == "我最喜欢的英语听力练习方式（自由填写）") )
                return false;
            return true;
        }

        private string FormatQuestionaire()
        {
            string questionaire = "";
            questionaire += (Q1.SelectedItem as ComboBoxItem).Content.ToString() + " | "
                + (Q2.SelectedItem as ComboBoxItem).Content.ToString() + " | "
                + (Q3.SelectedItem as ComboBoxItem).Content.ToString() + " | "
                + Q3_1.Text + " | "
                + Q3_2.Text + " | "
                + (Q4.SelectedItem as ComboBoxItem).Content.ToString() + " | ";
            if ((Q5.SelectedItem as ComboBoxItem).Content.ToString() == "以上都不是")
                questionaire += Q5_1.Text + " | ";
            else
                questionaire += (Q5.SelectedItem as ComboBoxItem).Content.ToString() + " | ";
            questionaire += (Q6.SelectedItem as ComboBoxItem).Content.ToString();
            return questionaire;
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
                parameters.Add("questionaire", FormatQuestionaire());
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "student/QueSubmit", parameters, ref isSuccess);
                if (isSuccess)
                {
                    Close();
                }
            }
        }

        private void Q3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Q3.SelectedItem as ComboBoxItem).Content.ToString() == "有")
            {
                Q3_1Part.Visibility = Visibility.Visible;
                Q3_2Part.Visibility = Visibility.Visible;
            }
            else
            {
                Q3_1Part.Visibility = Visibility.Collapsed;
                Q3_2Part.Visibility = Visibility.Collapsed;
            }
        }

        private void Q5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Q5.SelectedItem as ComboBoxItem).Content.ToString() == "以上都不是")
                Q5_1.Visibility = Visibility.Visible;
            else
                Q5_1.Visibility = Visibility.Collapsed;
        }
    }
}
