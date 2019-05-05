using EngEval.Common.DateTransform;
using EngEval.Model;
using EngEval.Pages.Questionarie;
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
            answer.end_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);

            answer.SaveLocal();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ListeningTest.testno == 3 && (mainwin.User.questionaireAF == "" || mainwin.User.questionaireAF == null))
            {
                TestQuestionaire testQuestionaire = new TestQuestionaire(mainwin.ListeningTest.testno);
                testQuestionaire.Owner = mainwin;
                testQuestionaire.ShowDialog();
            }
            MessageBox.Show("恭喜您！已经完成测试！");
            mainwin.FrameNavigator("funclist");
            Close();
            return;
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
