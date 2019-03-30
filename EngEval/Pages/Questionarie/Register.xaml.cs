using EngEval.Model;
using Http;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace EngEval.Pages.Questionarie
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            Loaded += Register_Loaded;
        }

        private void Register_Loaded(object sender, RoutedEventArgs e)
        {
            string rtext = "";
            Boolean isSuccess = false;
            while (!isSuccess) {
                rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "student/list_university", null, ref isSuccess);
            }
            University[] schools = JsonConvert.DeserializeObject<University[]>(rtext);
            School.ItemsSource = schools;
        }

        private bool ValidateInfo()
        {
            if ((School.SelectedItem as University) == null)
                return false;
            if ((Major.SelectedItem as ComboBoxItem) == null)
                return false;
            if ((ClassFor.SelectedItem as ComboBoxItem) == null)
                return false;
            if (ClassBack.Text == "")
                return false;
            if (StuNo.Text == "")
                return false;
            if (StuName.Text == "")
                return false;
            if (Password.Text == "")
                return false;
            if (RePassword.Text == "")
                return false;
            return true;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if(! ValidateInfo())
            {
                MessageBox.Show("请完善所有信息", "提示");
                return;
            }
            Boolean isSuccess = false;
            while (!isSuccess)
            {

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("r_username", StuNo.Text);
                parameters.Add("r_name", StuName.Text);
                parameters.Add("r_university", (School.SelectedItem as University).id.ToString());
                parameters.Add("r_password", Password.Text);
                parameters.Add("r_school", ((Major.SelectedItem as ComboBoxItem).Content as TextBlock).Text);
                parameters.Add("r_f_engclass", ((ClassFor.SelectedItem as ComboBoxItem).Content as TextBlock).Text);
                parameters.Add("r_b_engclass", ClassBack.Text);
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "student/register", parameters, ref isSuccess);
                if (isSuccess)
                {
                    Account student = JsonConvert.DeserializeObject<Account>(rtext);
                    if(student.student_no == "-1")
                    {
                        MessageBox.Show("该学生已经注册过了！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("恭喜您成功注册！", "提示");
                        Close();
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
