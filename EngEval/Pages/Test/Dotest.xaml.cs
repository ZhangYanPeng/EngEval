using EngEval.Common.DateTransform;
using EngEval.Model;
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
using System.Windows.Threading;

namespace EngEval.Pages.Test
{
    /// <summary>
    /// Dotest.xaml 的交互逻辑
    /// </summary>
    public partial class Dotest : Page
    {
        public Exercise currentExercise { get; set; }
        public Question currentQuestion { get; set; }
        public int currentQn { get; set; }
        public Answer answer { get; set; }
        DispatcherTimer timer = null;

        public Dotest()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Init);
            answer = new Answer();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            answer.account = mainwin.User;
            answer.test = mainwin.ListeningTest;

            SysVolumeBar.Value = Setting.SYSTEM_VOLUME;
            SysVolumeText.Text = ((int)(Setting.SYSTEM_VOLUME * 100)).ToString() + "%";
        }

        private void Init(object sender, RoutedEventArgs e)
        {
            Answer anstemp = new Answer();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            anstemp.test = mainwin.ListeningTest;
            anstemp = anstemp.LoadAnswer();
            if ( anstemp!=null && anstemp.account.id == answer.account.id && anstemp.test.id == answer.test.id)
            {
                //之前中断过
                answer = anstemp;
                currentQn = answer.records.Count;
            }
            else {
                currentQn = 0;
                answer.start_time = DateTransform.ConvertDataTimeToLong(DateTime.Now);
            }
            ToNextQue();

            //设置计时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        //更新计时
        private void timer_tick(object sender, EventArgs e)
        {
            try
            {
                long currentTime = DateTransform.ConvertDataTimeToLong(DateTime.Now);
                long time_consume = currentTime - answer.start_time;
                if (time_consume > 0)
                {
                    long hour = 0;
                    long minute = 0;
                    long second = 0;
                    second = time_consume / 1000;
                    if (second > 60)
                    {
                        minute = second / 60;
                        second = second % 60;
                    }
                    if (minute > 60)
                    {
                        hour = minute / 60;
                        minute = minute % 60;
                    }
                    TestTimer.Text = toTimeStr(hour) + " : " + toTimeStr(minute) + " : " + toTimeStr(second);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private string toTimeStr(long val)
        {
            string v = val.ToString();
            if (v.Length == 1)
                v = "0" + v;
            return v;
        }

        //进入下一题
        internal void ToNextQue(Record record = null)
        {
            if(record != null)
            {
                //记录当前答案
                SubmitTmp(record);
            }
            Intertions.SetIntervention(null);
            ToQuestionN(currentQn + 1);
            currentQn++;
        }

        private void SubmitTmp(Record record)
        {
            answer.records.Add(record);
            answer.SaveLocal();
        }

        //加载第qn题目
        public void ToQuestionN(int qn, bool forceBegin = false)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

            Exercise exercise = mainwin.ListeningTest.getExercise(qn);
            currentQuestion = mainwin.ListeningTest.getQuestion(qn);
            int timeRemain = mainwin.ListeningTest.timeRemain(qn);
            Part part = mainwin.listeningTest.getPart(qn);
            bool isPartFirst = mainwin.listeningTest.isPartFirst(qn);
            if(!forceBegin && isPartFirst)
            {
                //查看direction
                Direction direction = new Direction(part, this, qn);
                direction.Owner = mainwin;
                direction.Show();
            }
            else
            {
                // 判断试题结束
                if (exercise == null)
                {
                    //提交试题答题记录
                    Inspection inspection = new Inspection(answer);
                    inspection.Owner = mainwin;
                    inspection.ShowDialog();
                    return;
                }
                TestProgressBar.SetProgress(qn, mainwin.ListeningTest.getExerciseMaxQn(qn), 16);

                //展示exercise
                if (currentExercise != exercise)
                {
                    currentExercise = exercise;
                    //清空显示区域
                    ExerciseDisplay.Children.Clear();
                    for (int i = 0; i < currentExercise.questions.Length; i++)
                    {
                        QuestionContent questionContent = new QuestionContent(currentExercise.questions[i], mainwin.ListeningTest.getQn(currentExercise.questions[i]), this);
                        ExerciseDisplay.Children.Add(questionContent);
                    }
                }

                //设定当前题目
                foreach (QuestionContent qc in ExerciseDisplay.Children)
                {
                    if (qc.question == currentQuestion)
                    {
                        qc.Active(timeRemain);
                    }
                    else
                    {
                        qc.DeActive();
                    }
                }
            }
        }

        internal void InterShow(Intervention inter)
        {
            Intertions.SetIntervention(inter);
        }

        internal void InterShowAll()
        {
            Intertions.SetInterventions(currentQuestion.interventions);
        }

        private void SysVolumeBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Setting.SYSTEM_VOLUME = SysVolumeBar.Value;
            SysVolumeText.Text = ((int)(SysVolumeBar.Value * 100)).ToString() +"%";
            foreach (QuestionContent qc in ExerciseDisplay.Children)
            {
                try
                {
                    if(qc.mediaPlayer != null)
                        qc.mediaPlayer.Volume = SysVolumeBar.Value;
                    if (Intertions.Inter_Audio != null)
                        Intertions.Inter_Audio.Audio.Volume = SysVolumeBar.Value;
                }
                catch(Exception)
                {
                    continue;
                }
            }
        }
    }
}
