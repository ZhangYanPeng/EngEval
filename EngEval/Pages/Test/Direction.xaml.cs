using EngEval.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace EngEval.Pages.Test
{
    /// <summary>
    /// Direction.xaml 的交互逻辑
    /// </summary>
    public partial class Direction : Window
    {
        Part part { get; set; }
        Dotest _parent { get; set; }
        int nextQueNo { get; set; }

        public Direction(Part p, Dotest parent, int nqn)
        {
            InitializeComponent();
            part = p;
            nextQueNo = nqn;
            _parent = parent;
            Loaded += Direction_Loaded;
        }

        private void Direction_Loaded(object sender, RoutedEventArgs e)
        {
            partType.Text = part.partExers[0].exercise.type.name;
            try
            {
                directText.Text = part.directStr;
            }
            catch (Exception)
            {
                directText.Text = "Directions";
            }
            try
            {
                directAudio.Source = new Uri(Setting.SEVER_URL + part.directAudio.src, UriKind.RelativeOrAbsolute);
                //directAudio.Source = new Uri("http://202.117.216.251:8080/" + part.directAudio.src, UriKind.RelativeOrAbsolute);
                directAudio.MediaEnded += DirectAudio_MediaEnded;
                directAudio.Play();
            }
            catch (Exception)
            {
                Task t = new Task(() =>
                {
                    //模拟工作过程
                    Thread.Sleep(10000);
                    Dispatcher.Invoke((Action)(() =>
                    {
                        Close();
                    }));

                });
                t.Start();
            }
        }

        private void DirectAudio_MediaEnded(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _parent.ToQuestionN(nextQueNo, true);
            base.OnClosing(e);
        }
    }
}
