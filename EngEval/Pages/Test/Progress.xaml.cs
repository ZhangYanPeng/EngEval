﻿using EngEval.Model;
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
    /// Progress.xaml 的交互逻辑
    /// </summary>
    public partial class Progress : UserControl
    {
        public Progress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="current">当前在做大体第一小题编号 之前题目为已做过的</param>
        /// <param name="future">当前在做大体最后一小题编号</param>
        /// <param name="total">题目总数</param>
        public void SetProgress(int current, int future, int total)
        {
            //清空进度条，重绘
            ProgressBar.Children.Clear();
            string tmp = "";
            for(int i = current; i <= future; i++){
                tmp += i.ToString();
                if (i < future)
                    tmp += "、";
            }
            ProgressInfo.Text = String.Format("本套测试一共含{0}个小题，您现在位于第{1}小题。", total, tmp);
            //更新大题部分
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            int pi = 0;
            foreach(Part part in mainwin.ListeningTest.parts)
            {
                string str = part.partExers[0].exercise.type.name;
                int qn = 0;
                foreach (PartExer pe in part.partExers)
                {
                    qn += pe.exercise.questions.Length;
                }

                Border border = new Border();
                border.Width = qn * 60;
                if(pi == 0)
                    border.BorderThickness = new Thickness(10, 0, 5, 0);
                else if(pi == mainwin.ListeningTest.parts.Length-1)
                    border.BorderThickness = new Thickness(5, 0, 10, 0);
                else
                    border.BorderThickness = new Thickness(5, 0, 5, 0);
                border.BorderBrush = Brushes.Black;
                TextBlock text = new TextBlock();
                text.Text = str;
                text.TextAlignment = TextAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.FontSize = 10;
                border.Child = text;
                PartText.Children.Add(border);
                pi++;
            }

            int num = 1;
            while(num <= total)
            {
                //题号
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (num).ToString();
                textBlock.FontSize = 30;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;

                Border border = new Border();
                border.Width = 50;
                border.Height = 80;
                border.CornerRadius = new CornerRadius(10);
                border.BorderThickness = new Thickness(5);
                border.Margin = new Thickness(5, 5, 5, 5);

                Color color;
                //判定颜色
                if (num < current) { 
                    color = Color.FromRgb(180, 238, 180);
                    textBlock.Foreground = Brushes.White;
                    border.Background = new SolidColorBrush(color);
                    border.BorderBrush = new SolidColorBrush(color);
                }
                else if(num <= future)
                {
                    color = Color.FromRgb(205, 91, 69);
                    textBlock.Foreground = new SolidColorBrush(color);
                    border.Background = Brushes.White;
                    border.BorderBrush = new SolidColorBrush(color);
                }
                else
                {
                    color = Color.FromRgb(204, 204, 204);
                    textBlock.Foreground = new SolidColorBrush(color);
                    border.Background = Brushes.White;
                    border.BorderBrush = new SolidColorBrush(color);
                }
                border.Child = textBlock;
                ProgressBar.Children.Add(border);
                num++;
            }
        }
    }
}
