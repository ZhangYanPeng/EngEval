﻿using System;
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
            TestProgressBar.SetProgress(1,2,16);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            i++;
            TestProgressBar.SetProgress(i, i+1, 16);
        }
    }
}
