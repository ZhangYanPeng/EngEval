using EngEval.Common.DateTransform;
using EngEval.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    /// QuestionContent.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionContent : UserControl
    {
        public Question question { get; set; }
        public Dotest parentPage { get; set; }
        public MediaPlayer mediaPlayer { get; set; }
        Record record { get; set; } //当前题目答题情况
        bool actived = false;

        //切换到当前题目显示
        private void ScrollToThis()
        {
            Point point = TranslatePoint(new Point(0, 0), parentPage.ExerciseDisplay);
            parentPage.ExerciseDisplayScroll.ScrollToHorizontalOffset(point.X);
        }

        public QuestionContent(Question q, int qn, Dotest dotest)
        {
            InitializeComponent();
            string tmpstr = q.options.Replace("||", "|");
            string[] options = tmpstr.Split('|');
            question = q;
            parentPage = dotest;
            record = new Record();
            QueNo.Text = qn.ToString();

            Option1.Text = options[1];
            Option2.Text = options[2];
            Option3.Text = options[3];
            Option4.Text = options[4];
            Option5.Text = options[5];

            Loaded += QuestionContent_Loaded;
        }

        private void QuestionContent_Loaded(object sender, RoutedEventArgs e)
        {
            if(actived)
                ScrollToThis();
        }

        //当前题目激活
        internal void Active()
        {
            actived = true;
            Btn_Submit.Visibility = Visibility.Visible;
            Btn_Submit.IsEnabled = false;
            Btn_Next.Visibility = Visibility.Collapsed;
            record.start_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);
            QCBox.BorderBrush = Brushes.ForestGreen;
            QCBox.BorderThickness = new Thickness(5);
            CanAnswer();

             //播放当前题目音频
            mediaPlayer = new MediaPlayer();
            string audios_path = "TEMP/" + question.audio.src.Substring(question.audio.src.LastIndexOf("/"));
            mediaPlayer.Open(new Uri(audios_path, UriKind.Relative));
            mediaPlayer.MediaEnded += Player_MediaEnded;
            mediaPlayer.Volume = Setting.SYSTEM_VOLUME;

            Task t = new Task(() =>
            {
                //模拟工作过程
                Thread.Sleep(10000);
                Action PlaySoundAction = new Action(PlaySound);
                Dispatcher.BeginInvoke(PlaySoundAction);
            });
            t.Start();
        }

        private void PlaySound()
        {
            mediaPlayer.Play();
        }

        //播放结束事件
        private void Player_MediaEnded(object sender, EventArgs e)
        {
            //播放结束
            CanAnswer();
            record.start_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);
        }

        //当前题目可以作答
        internal void CanAnswer()
        {
            Btn_Submit.Visibility = Visibility.Visible;
            Btn_Submit.IsEnabled = true;
            Btn_Next.Visibility = Visibility.Collapsed;
        }

        //当前题目做大完毕
        internal void Answered()
        {
            Btn_Submit.Visibility = Visibility.Collapsed;
            Btn_Submit.IsEnabled = true;
            Btn_Next.Visibility = Visibility.Visible;
        }

        //当前题目非激活
        internal void DeActive()
        {
            QCBox.BorderBrush = Brushes.LightGray;
            QCBox.BorderThickness = new Thickness(2);
            Btn_Submit.Visibility = Visibility.Collapsed;
            Btn_Next.Visibility = Visibility.Collapsed;
        }

        //当前题目提交答案
        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //回答
            //第一次回答，记录时间
            if(record.answers.Count == 0)
                record.answer_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);

            int op = 0;
            //查找选项
            for(int i=1; i<=5; i++)
            {
                RadioButton rb = FindName("rb_opt" + i.ToString()) as RadioButton;
                if (rb.IsEnabled == true && rb.IsChecked == true)
                    op = i;
            }
            //未回答
            if(op == 0)
            {
                MessageBox.Show("请作答后再提交答案！", "提示");
                return;
            }
            //记录回答过程
            record.answers.Add(op);
            //是否答对
            if ( op == question.answer )
            {
                //记录时间
                record.end_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);
                Answered();
                ShowIntervention(0);
            }
            else
            {
                RadioButton rb = FindName("rb_opt" + op.ToString()) as RadioButton;
                rb.IsEnabled = false;
                rb.IsChecked = false;
                ShowIntervention(record.answers.Count);
            }
        }

        /// <summary>
        /// v 0显示所有， 其他值，显示对应干预
        /// </summary>
        /// <param name="v"></param>
        private void ShowIntervention(int v)
        {
            if (v > 0)
            {
                foreach(Intervention inter in question.interventions)
                {
                    if (inter.level == v - 1)
                        parentPage.InterShow(inter);
                }
            }
            else
            {
                parentPage.InterShowAll();
            }
        }

        //当前题目跳转下一题
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            parentPage.ToNextQue(record);
        }
    }
}
