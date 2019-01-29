using EngEval.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EngEval
{
    public partial class MainWindow : Window
    {
        //正在进行的测试
        public ListeningTest listeningTest;
        public ListeningTest ListeningTest
        {
            get
            {
                return listeningTest;
            }
            set
            {
                listeningTest = value;
                listeningTest.Sort();
            }
        }
    }
}
