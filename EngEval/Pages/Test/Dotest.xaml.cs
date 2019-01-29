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
    /// Dotest.xaml 的交互逻辑
    /// </summary>
    public partial class Dotest : Page
    {
        int i = 1;
        public Dotest()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Init);
        }

        private void Init(object sender, RoutedEventArgs e)
        {
            ToQuestionN(1);
        }

        public void ToQuestionN(int qn)
        {
            TestProgressBar.SetProgress(qn, 1, 16);
            ExerciseDisplay.Children.Clear();

            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Exercise exe = mainwin.ListeningTest.parts[0].partExers[0].exercise;
            foreach(Question que in exe.questions)
            {
                QuestionContent qc = new QuestionContent(que);
                ExerciseDisplay.Children.Add(qc);
                QuestionContent qc1 = new QuestionContent(que);
                ExerciseDisplay.Children.Add(qc1);
                QuestionContent qc2 = new QuestionContent(que);
                ExerciseDisplay.Children.Add(qc2);
                QuestionContent qc3 = new QuestionContent(que);
                ExerciseDisplay.Children.Add(qc3);
                QuestionContent qc4 = new QuestionContent(que);
                ExerciseDisplay.Children.Add(qc4);
                QuestionContent qc5 = new QuestionContent(que);
                ExerciseDisplay.Children.Add(qc5);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            i++;
            TestProgressBar.SetProgress(i, i, 16);
        }
    }
}
