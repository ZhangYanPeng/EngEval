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
    /// MediaPlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayerControl : UserControl
    {
        public string audio { get; set; }
        DispatcherTimer timer = null;

        public MediaPlayerControl()
        {
            InitializeComponent();
            Btn_Play.Visibility = Visibility.Visible;
            //Btn_Play.IsEnabled = false;
            Btn_Pause.Visibility = Visibility.Collapsed;
            Audio.Volume = Setting.SYSTEM_VOLUME;
        }

        //设置音频源
        public void SetAuido(string audio_path)
        {
            if (audio_path == "")
            {
                Visibility = Visibility.Collapsed;
                return;
            }
            else
                Visibility = Visibility.Visible;
            Audio.Position = TimeSpan.Zero;
            Audio.Pause();
            Btn_Play.Visibility = Visibility.Visible;
            Btn_Pause.Visibility = Visibility.Collapsed;
            try
            {
                SetProgressBar();
            }
            catch (Exception exp)
            {
                return;
            }
            audio = audio_path;
            Audio.Source = new Uri(audio_path, UriKind.RelativeOrAbsolute);
            //Audio.Play();
        }

        private void Audio_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                Progress_Bar.Maximum = Audio.NaturalDuration.TimeSpan.TotalSeconds;
            }
            catch (Exception exp)
            {
                Progress_Bar.Maximum = 100;
            }
            //Btn_Play.IsEnabled = true;
            SetProgressBar();
        }

        //播放音频
        private void Btn_Play_Click(object sender, RoutedEventArgs e)
        {
            Btn_Play.Visibility = Visibility.Collapsed;
            Btn_Pause.Visibility = Visibility.Visible;
            Audio.Play();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        //更新播放进度
        private void timer_tick(object sender, EventArgs e)
        {
            try
            {
                SetProgressBar();
            }catch(Exception exp)
            {
                return;
            }
        }

        private void SetProgressBar()
        {
            try
            {
                Progress_Bar.Value = Audio.Position.TotalSeconds;
                Progress_Text.Text = formatTimeSpan(Audio.Position.ToString()) + " / " + formatTimeSpan(Audio.NaturalDuration.TimeSpan.ToString());
            }
            catch(Exception e)
            {
                Progress_Text.Text = ".../...";
            }
        }

        private string formatTimeSpan(string str)
        {
            try
            {
                return str.Substring(0, str.LastIndexOf(":") + 3);
            }
            catch (Exception exp)
            {
                return "";
            }
        }

        //暂停音频
        private void Btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            Btn_Play.Visibility = Visibility.Visible;
            Btn_Pause.Visibility = Visibility.Collapsed;
            Audio.Pause();
        }

        //音频播放结束
        private void Audio_MediaEnded(object sender, RoutedEventArgs e)
        {
            Audio.Position = TimeSpan.Zero;
            Audio.Pause();
            Btn_Play.Visibility = Visibility.Visible;
            Btn_Pause.Visibility = Visibility.Collapsed;
            try
            {
                SetProgressBar();
            }
            catch (Exception exp)
            {
                return;
            }
        }

        //拖动进度条
        private void Progress_Bar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Audio.Position = TimeSpan.FromSeconds(Progress_Bar.Value);
            }catch(Exception exp)
            {
                Audio.Position = TimeSpan.Zero;
            }
        }
    }
}
