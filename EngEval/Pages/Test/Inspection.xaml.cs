using EngEval.Common.DateTransform;
using EngEval.Model;
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

namespace EngEval.Pages.Test
{
    /// <summary>
    /// Inspection.xaml 的交互逻辑
    /// </summary>
    public partial class Inspection : Window
    {
        Answer answer { get; set; }
        public Inspection(Answer ans)
        {
            InitializeComponent();
            answer = ans;
        }

        private void SubmitInspection_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInfo())
            {
                MessageBox.Show("请回答所有问题。", "提示");
                return;
            }
            answer.states = FormatQuestionaire();

            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            //提交试题答题记录
            answer.end_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);
            Dictionary<string, string> parameters = answer.GetParamUpload();
            bool substate = false;
            while (!substate)
            {
                substate = SubmitAnswer(parameters);
            }
            MessageBox.Show("恭喜您！已经完成测试！");
            mainwin.FrameNavigator("funclist");
            Close();
            return;
        }

        //提交答案
        private bool SubmitAnswer(Dictionary<string, string> parameters)
        {
            Boolean isSuccess = false;
            string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/finishTest", parameters, ref isSuccess);
            return isSuccess;
        }

        private string FormatQuestionaire()
        {
            string questionaire = "";
            for (int i = 1; i <= 3; i++)
            {
                questionaire += getQues(i).Content.ToString() + " | ";
            }
            return questionaire;
        }

        private bool ValidateInfo()
        {
            for (int i = 1; i <= 3; i++)
            {
                if (getQues(i).Content.ToString() == "请选择")
                    return false;
            }
            return true;
        }

        private ComboBoxItem getQues(int i)
        {
            switch (i)
            {
                case 1: return Q1.SelectedItem as ComboBoxItem;
                case 2: return Q2.SelectedItem as ComboBoxItem;
                case 3: return Q3.SelectedItem as ComboBoxItem;
            }
            return null;
        }
    }
}
