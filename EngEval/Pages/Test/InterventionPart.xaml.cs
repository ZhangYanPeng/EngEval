using EngEval.Model;
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

namespace EngEval.Pages.Test
{
    /// <summary>
    /// Intervention.xaml 的交互逻辑
    /// </summary>
    public partial class InterventionPart : UserControl
    {
        Intervention intervention { get; set; }
        Intervention[] interventions { get; set; }
        int internum { get; set; }

        public InterventionPart()
        {
            InitializeComponent();
        }

        //设置干预
        public void SetIntervention( Intervention inter)
        {
            ForInter.Visibility = Visibility.Hidden;
            BelInter.Visibility = Visibility.Hidden;
            if(inter == null)
            {
                Inter_Text.Text = "";
                Inter_Audio.SetAuido("");
                return;
            }

            intervention = inter;
            Inter_Text.Text = intervention.text;
            if(intervention.audio != null)
            {

                string audios_path = Setting.SEVER_URL + intervention.audio.src;
                Inter_Audio.SetAuido(audios_path);
            }
            else
            {
                Inter_Audio.SetAuido("");
            }
        }

        //根据level设置interve
        public void SetIntervention(int inum)
        {
            internum = inum;
            for (int i =0; i<4; i++)
            {
                if (interventions[i].level == inum)
                    SetIntervention(interventions[i]);
            }
            SetFBButton(inum);
        }

        //设置左右按键
        private void SetFBButton(int inum)
        {
            ForInter.Visibility = Visibility.Visible;
            BelInter.Visibility = Visibility.Visible;
            if (inum == 0)
                ForInter.Visibility = Visibility.Hidden;
            else if(inum == 3)
                BelInter.Visibility = Visibility.Hidden;
        }

        //生成全部干预预览
        internal void SetInterventions(Intervention[] ints)
        {
            interventions = ints;
            SetIntervention(3);
        }

        //前一干预
        private void ForInter_Click(object sender, RoutedEventArgs e)
        {
            if (internum > 0 )
                SetIntervention(internum-1);
        }

        //后一干预
        private void BelInter_Click(object sender, RoutedEventArgs e)
        {
            if (internum <3)
                SetIntervention(internum + 1);
        }
    }
}
