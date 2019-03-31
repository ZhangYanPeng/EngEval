using EngEval.Model;
using EngEval.Pages.Questionarie;
using Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace EngEval.Pages
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            if (username == null || username.Length == 0)
            {
                MessageBox.Show("学号不能为空！");
                return;
            }
            if (password == null || password.Length == 0)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("username", username);
            parameters.Add("password", password);

            Boolean isSuccess = false;
            string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "student/login", parameters, ref isSuccess);
            if (isSuccess)
            {
                Account user = JsonConvert.DeserializeObject<Account>(rtext);
                if(user.id==-1)
                {
                    MessageBox.Show("用户名或密码错误，请重试！");
                    return;
                }
                else
                {
                    //登陆成功
                    MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                    mainwin.FrameNavigator("funclist");
                    mainwin.User = user;
                    if(user.questionaire == null)
                    {
                        Information information = new Information();
                        information.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("网络错误请重试！");
                return;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.ShowDialog();
        }

        
    }
}
