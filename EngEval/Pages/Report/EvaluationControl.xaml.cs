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

namespace EngEval.Pages.Report
{
    /// <summary>
    /// EvaluationControl.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluationControl : UserControl
    {
        private List<String> labels;
        public List<String> Labels
        {
            get
            {
                return labels;
            }
            set
            {
                labels = value;
                if (labels.Count != 5)
                    return;
                label1.Text = labels[0];
                label2.Text = labels[1];
                label3.Text = labels[2];
                label4.Text = labels[3];
                label5.Text = labels[4];
            }
        }

        public int GetSelectionValue()
        {
            return (int)SliderBar.Value;
        }

        public EvaluationControl()
        {
            InitializeComponent();
            Loaded += EvaluationControl_Loaded;
        }

        private void EvaluationControl_Loaded(object sender, RoutedEventArgs e)
        {
            SliderBar.Value = 0;
        }
    }
}
