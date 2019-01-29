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
    /// QuestionContent.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionContent : UserControl
    {
        public QuestionContent(Question question)
        {
            InitializeComponent();
            string tmpstr = question.options.Replace("||", "|");
            string[] options = tmpstr.Split('|');

            Option1.Text = options[1];
            Option2.Text = options[2];
            Option3.Text = options[3];
            Option4.Text = options[4];
            Option5.Text = options[5];
        }
    }
}
