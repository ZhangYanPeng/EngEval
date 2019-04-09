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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EngEval.Pages.Guide
{
    /// <summary>
    /// Guide.xaml 的交互逻辑
    /// </summary>
    public partial class Guide : Page
    {
        int submitTime { get; set; }
        List<Intervention> Interventions { get; set; }

        public Guide()
        {
            InitializeComponent();
            submitTime = 0;
            initInterventions();
            Loaded += Guide_Loaded;
        }

        private void initInterventions()
        {
            Interventions = new List<Intervention>();
            Intervention in_1 = new Intervention();
            in_1.text = "再听一遍女士的回答，根据上下文推断她的真正意图。";
            in_1.level = 0;
            Audio audio_1 = new Audio();
            audio_1.src = @"/Evaluation/sample/sampleinter1.mp3";
            in_1.audio = audio_1;
            Interventions.Add(in_1);
            Intervention in_2 = new Intervention();
            in_2.text = "这道题考察听力推理能力，听者需要对说话者传递的字面意思进行深加工，推理其深层含义。这时请你调动你脑海中的词汇、逻辑和情境知识，再次推理女士话语的言外之意。";
            in_2.level = 1;
            Interventions.Add(in_2);
            Intervention in_3 = new Intervention();
            in_3.text = "请先听男士的录音，其中“keep the noise down”的意思是保持安静、声音小点。了解这个短语后，你应该了解男士对女士的请求了。\r\n再听听女士的回答，想想她为什么要提到楼下开party的事情呢？同时对比选项看看哪个能体现女士的真正意图！";
            in_3.level = 2;
            Audio audio_3 = new Audio();
            audio_3.src = @"/Evaluation/sample/sampleinter3.mp3";
            in_3.audio = audio_3;
            Interventions.Add(in_3);
            Intervention in_4 = new Intervention();
            in_4.text = "正确答案：B\r\n对话中的男士说他正在听录音，希望女士声音小点。女士回答说不是噪音不是她发出，进而说到楼下正在开Party。\r\n面对男士的抱怨，女士道歉并提及楼下开派对一事，可知发出的嘈杂声不是因为她，而是Party，言下之意是她不应对此负责。这样既解释了误会，也不会让男士难堪。";
            in_4.level = 3;
            Interventions.Add(in_4);
        }

        private void Guide_Loaded(object sender, RoutedEventArgs e)
        {
            Intertions.SetIntervention(null);
            Task totalGuide = new Task(() =>
            {
                //模拟工作过程
                Thread.Sleep(500);
                Action TopTextGuideAction = new Action(ShowTopGuide);
                Dispatcher.BeginInvoke(TopTextGuideAction);
                Thread.Sleep(4000);
                Action QueTextGuideAction = new Action(ShowQueGuide);
                Dispatcher.BeginInvoke(QueTextGuideAction);
                Thread.Sleep(4000);
                Action InterTextGuideAction = new Action(ShowInterGuide);
                Dispatcher.BeginInvoke(InterTextGuideAction);
                Thread.Sleep(4000);
                Action HideTextGuideAction = new Action(HideTextGuide);
                Dispatcher.BeginInvoke(HideTextGuideAction);
                Thread.Sleep(4000);
                Action SetGuideBFAction = new Action(SetGuideBF);
                Dispatcher.BeginInvoke(SetGuideBFAction);
                Thread.Sleep(5000);
                Action PlayQueSoundAction = new Action(PlayQueAudio);
                Dispatcher.BeginInvoke(PlayQueSoundAction);
            });
            totalGuide.Start();
        }
        
        #region 显示顶部提示动画
        private void HideTextGuide()
        {
            TopGuide(0);
            QueGuide(0);
            InterGuide(0);
        }

        private void ShowTopGuide()
        {
            TopGuide(1);
        }

        private void ShowQueGuide()
        {
            QueGuide(1);
        }

        private void ShowInterGuide()
        {
            InterGuide(1);
        }

        private void TopGuide(int aim)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = TopAdorner.Opacity;
            doubleAnimation.To = aim;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            TopAdorner.BeginAnimation(AdornerDecorator.OpacityProperty, doubleAnimation);
        }

        private void QueGuide(int aim)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = QueAdorner.Opacity;
            doubleAnimation.To = aim;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            QueAdorner.BeginAnimation(AdornerDecorator.OpacityProperty, doubleAnimation);
        }

        private void InterGuide(int aim)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = InterAdorner.Opacity;
            doubleAnimation.To = aim;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            InterAdorner.BeginAnimation(AdornerDecorator.OpacityProperty, doubleAnimation);
        }
        #endregion

        #region 播放听力
        private void PlayQueAudio()
        {
            GuideText.Text = "听力播放过程中，提交答案按钮被禁用，需要听力播放完成后，方能提交答案";
            QueAudio.MediaEnded += QueAudio_MediaEnded;
            QueAudio.Play();
        }
        
        //听力播放结束
        private void QueAudio_MediaEnded(object sender, RoutedEventArgs e)
        {
            Btn_Submit.IsEnabled = true;
            rb_opt5.IsChecked = true;
            GuideText.Text = "选择答案并提交，请选择E并提交";
            QueAdorner.Visibility = Visibility.Collapsed;
            TopAdorner.Visibility = Visibility.Collapsed;
            InterAdorner.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region 设置右侧文本提示
        private void SetGuideBF()
        {
            GuideText.Text = "在听力开始前，你将会有5~32秒不等的时间，阅读题目选项";
        }
        #endregion

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if(submitTime == 0)
            {
                submitTime++;
                GuideText.Text = "E选项为错误答案，您可以查看下方第一次提示信息，重新作出选择，这次请选择B";
                rb_opt2.IsChecked = true;
                Intertions.SetIntervention(Interventions[0]);
            }
            else
            {
                GuideText.Text = "恭喜回答正确！下方提示区域将显示所有四次提示内容，可前后翻阅查看";
                Intertions.SetInterventions(Interventions.ToArray());
                Btn_Submit.Visibility = Visibility.Hidden;
                Btn_Next.Visibility = Visibility.Visible;
            }
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("你已完成所有教程，可以开始使用系统了","恭喜",MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.FrameNavigator("funclist");
            }
        }
    }
}
