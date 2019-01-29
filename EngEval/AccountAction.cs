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
                if(user != null)
                {
                    if (user.id != -1) {
                        TopBar.Visibility = Visibility.Visible;
                        USER_NAME.Text = user.name;
                        SUT_NO.Text = user.student_no;
                        return;
                    }
                }
                USER_NAME.Text = user.name;
                SUT_NO.Text = user.student_no;
                TopBar.Visibility = Visibility.Collapsed;
                FrameNavigator("login");
            }
        }
    }
}
