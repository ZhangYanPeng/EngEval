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
        //当前用户信息
        public Account user;
        public Account User {
            get
            {
                return user;
            }
            set
            {
                user = value;
                if (user != null)
                {
                    if (user.id != -1) {
                        TopBar.Visibility = Visibility.Visible;
                        USER_NAME.Text = user.name;
                        SUT_NO.Text = user.student_no;
                        return;
                    }
                }
                USER_NAME.Text = "";
                SUT_NO.Text = "";
                TopBar.Visibility = Visibility.Collapsed;
                FrameNavigator("login");
            }
        }

        public string Questionaire{get;set;}

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确认注销?  (未完成测试会受到影响)", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                User = null;
            }
        }
    }
}
