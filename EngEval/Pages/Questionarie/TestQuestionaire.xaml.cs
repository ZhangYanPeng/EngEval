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
    public class QuesContent
    {
        public String Text { get; set; }
        public String QNO { get; set; }
        public ComboBoxItem Selection { get; set; }
        public QuesContent(string t, string qno)
        {
            Text = t;
            QNO = qno;
        }
    }


    /// <summary>
    /// TestQuestionaire.xaml 的交互逻辑
    /// </summary>
    public partial class TestQuestionaire : Window
    {
        List<QuesContent> QuesContents { get; set; }
        int testno { get; set; }
        public TestQuestionaire(int tno)
        {
            InitializeComponent();
            testno = tno;
            Loaded += TestQuestionaire_Loaded;
        }

        private void TestQuestionaire_Loaded(object sender, RoutedEventArgs e)
        {
            QuesContents = new List<QuesContent>();
            if (testno == 1)
            {
                Guides.Text = "本问卷旨在调查大学生英语听力策略。请你根据数字所代表的意思，在问题后填写相应的数字。请根据自己的实际情况填写。此问卷仅作为学术研究，你所提供的信息将被保密，谢谢合作！";
                QuesContents.Add(new QuesContent("我能理解听力中的特殊句式，如虚拟语气，倒装句、否定句等。","1"));
                QuesContents.Add(new QuesContent("我能获取跟听力目的有关的信息。","2"));
                QuesContents.Add(new QuesContent("我能获取数字、时间和地点等信息。","3"));
                QuesContents.Add(new QuesContent("我能根据上下文推测文中隐含的信息。","4"));
                QuesContents.Add(new QuesContent("我能理解听力的交际功能，如命令、请求、建议等。","5"));
                QuesContents.Add(new QuesContent("我能理解听力任务中的关键词汇或短语。","6"));
                QuesContents.Add(new QuesContent("我能识别对话或篇章的主题。","7"));
                QuesContents.Add(new QuesContent("我能获取人物关系、事情发生原因和结果等信息。","8"));
                QuesContents.Add(new QuesContent("我能理解英语听力中常见的固定搭配，如by the way。","9"));
                QuesContents.Add(new QuesContent("我能通过衔接词，如 firstly, therefore, however, moreover, on the other hand等获取相关信息。","10"));
                QuesContents.Add(new QuesContent("我能理解说话者的观点。","11"));
                QuesContents.Add(new QuesContent("我能依据上下文推测不熟悉词汇的意义。","12"));
                QuesContents.Add(new QuesContent("我能根据听到的内容推测下面将发生什么。","13"));
                QuesContents.Add(new QuesContent("我能理解英语口语中常见的俚语，如one’s cup of tea。","14"));
                QuesContents.Add(new QuesContent("我能理解词汇或短语在具体情境下的意义，如book在短语read a book 和book a ticket中的意思不同。","15"));
                QuesContents.Add(new QuesContent("我能通过连接词，如also, but, still, then, because, before等获取相关信息。","16"));
                QuesContents.Add(new QuesContent("我能对所听内容进行总结和概括。","17"));
                QuesContents.Add(new QuesContent("我能从听力中辨别出重要信息。","18"));
                QuesContents.Add(new QuesContent("我能推断出说话人的立场、情感和态度。","19"));
            }
            if (testno == 3)
            {
                Guides.Text = "亲爱的同学，恭喜你完成三次测试！也非常感谢你参与本次问卷调研。本问卷旨在调查大学生英语听力策略。请你根据数字所代表的意思，在问题后填写相应的数字。请根据自己的实际情况填写。此问卷仅作为学术研究，你所提供的信息将被保密，谢谢合作！";
                QuesContents.Add(new QuesContent("我能理解词汇或短语在具体情境下的意义，如book在短语read a book 和book a ticket中的意思不同。", "1"));
                QuesContents.Add(new QuesContent("我能理解听力任务中的关键词汇或短语。", "2"));
                QuesContents.Add(new QuesContent("我能理解英语听力中常见的固定搭配，如by the way。", "3"));
                QuesContents.Add(new QuesContent("我能理解英语口语中常见的俚语，如one’s cup of tea。", "4"));
                QuesContents.Add(new QuesContent("我能理解听力中的特殊句式，如虚拟语气，倒装句、否定句等。", "5"));
                QuesContents.Add(new QuesContent("我能通过连接词，如also, but, still, then, because, before等获取相关信息。", "6"));
                QuesContents.Add(new QuesContent("我能通过衔接词，如 firstly, therefore, however, moreover, on the other hand等获取相关信息。", "7"));
                QuesContents.Add(new QuesContent("我能获取跟听力目的有关的信息。", "8"));
                QuesContents.Add(new QuesContent("我能识别对话或篇章的主题。", "9"));
                QuesContents.Add(new QuesContent("我能理解说话者的观点。", "10"));
                QuesContents.Add(new QuesContent("我能对所听内容进行总结和概括。", "11"));
                QuesContents.Add(new QuesContent("我能获取数字、时间和地点等细节。", "12"));
                QuesContents.Add(new QuesContent("我能获取人物关系、事情发生原因和结果等细节。", "13"));
                QuesContents.Add(new QuesContent("我能从听力中辨别出重要细节。", "14"));
                QuesContents.Add(new QuesContent("我能推断出说话人的立场、情感和态度。 ", "15"));
                QuesContents.Add(new QuesContent("我能理解听力的交际功能，如命令、请求、建议等。", "16"));
                QuesContents.Add(new QuesContent("我能依据上下文推测不熟悉词汇的意义。", "17"));
                QuesContents.Add(new QuesContent("我能根据上下文推测文中隐含的信息。", "18"));
                QuesContents.Add(new QuesContent("我能根据听到的内容推测下面将发生什么。", "19"));
            }
            QuestionaireContent.ItemsSource = QuesContents;
        }

        private bool ValidateInfo()
        {
            foreach(QuesContent qc in QuesContents)
            {
                if (qc.Selection.Content.ToString() == "请选择")
                    return false;
            }
            return true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInfo())
            {
                MessageBox.Show("请回答所有问题。","提示");
                return;
            }

            Boolean isSuccess = false;
            while (!isSuccess)
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("uid", mainwin.User.id.ToString());
                if(testno == 1)
                    parameters.Add("quesn", "0");
                else
                    parameters.Add("quesn", "1");
                parameters.Add("ques", FormatQuestionaire());
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/submitQuestionaire", parameters, ref isSuccess);
                if (isSuccess)
                {
                    if (testno == 1)
                        mainwin.User.questionaireBF = "1";
                    if (testno ==3)
                        mainwin.User.questionaireAF = "1";
                    Close();
                }
            }
        }

        private string FormatQuestionaire()
        {
            string questionaire = "";
            foreach (QuesContent qc in QuesContents)
            {
                questionaire += qc.QNO + ":" + qc.Selection.Content.ToString().Substring(0,1) + " | ";
            }
            return questionaire;
        }
    }

    
}
