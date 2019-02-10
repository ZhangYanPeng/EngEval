using EngEval.Common.Http;
using EngEval.Model;
using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
    /// Prepare.xaml 的交互逻辑
    /// </summary>
    public partial class Prepare : Page
    {
        public bool isLoaded;
        public Prepare()
        {
            isLoaded = false;
            InitializeComponent();
            Loaded += LoadTest;
        }

        private void LoadTest(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
                return;
            isLoaded = true;

            //加载测试
            loadFinished(false);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", JsonConvert.SerializeObject(-1));
            Boolean isSuccess = false;
            string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/loadTest", parameters, ref isSuccess);
            if (isSuccess)
            {
                ListeningTest test = JsonConvert.DeserializeObject<ListeningTest>(rtext);
                TestName.Text += test.title;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.ListeningTest = test;
                if (IsFinished(test))
                {
                    MessageBox.Show("您已完成所有测试！");
                    mainwin.FrameNavigator("funclist");
                    return;
                }

                //开启一个线程加载音频
                Task task = new Task(() => LoadAudios(test));
                task.Start();
            }
            else
            {
                MessageBox.Show("网络错误请重试！");
                return;
            }
        }

        //是否已经做过该测试
        private bool IsFinished(ListeningTest test)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("type", JsonConvert.SerializeObject(0));
            parameters.Add("tid", test.id);
            parameters.Add("uid", mainwin.User.id.ToString());
            Boolean isSuccess = false;
            while (!isSuccess)
            {
                string rtext = HttpRequestHelper.HttpGet(Setting.BASE_URL + "test/getStatus", parameters, ref isSuccess);
                if (rtext == "1")
                    return true;
                else if((rtext == "0"))
                    return false;
            }
            return false;
        }

        //加载所有音频
        private void LoadAudios(ListeningTest test)
        {
            Dispatcher.BeginInvoke(new Action(() => LoadingAudiosText.Text = "正在加载音频，请稍后...(0/16)"));
            if (Directory.Exists("TEMP/") == false)//如果不存在就创建file文件夹 
            { 
                Directory.CreateDirectory("TEMP/");
            }
            else
            {
                ClearAudios();
            }
            int loadnum = 0;
            foreach(Part part in test.parts)
            {
                foreach(PartExer partExer in part.partExers)
                {
                    Exercise exercise = partExer.exercise;
                    foreach(Question question in exercise.questions)
                    {
                        bool check = false;
                        while (!check)
                        {
                            try
                            {
                                check = LoadAuido(question.audio);
                            }
                            catch(Exception)
                            {
                                check = false;
                            }
                        }
                        loadnum++;
                        Dispatcher.BeginInvoke(new Action(() => LoadingAudiosText.Text = string.Format("正在加载音频，请稍后...({0}/16)", loadnum)));
                    }
                }
            }
            Dispatcher.BeginInvoke(new Action(()=>loadFinished(true)));
        }

        //清除缓存音频
        private void ClearAudios()
        {
            DirectoryInfo root = new DirectoryInfo("TEMP/");
            FileInfo[] files = root.GetFiles();
            foreach(FileInfo fi in files)
            {
                if (! fi.FullName.Substring(fi.FullName.LastIndexOf(".")).Contains("ini"))
                    fi.Delete();
            }
        }

        //加载单个音频
        private bool LoadAuido(Audio audio)
        {
            return DownloadFile.Download(Setting.SEVER_URL + audio.src, "TEMP/" + audio.src.Substring(audio.src.LastIndexOf("/"))); 
        }

        //开始测试
        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("Test/Dotest");
        }

        //返回首页
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.FrameNavigator("funclist");
        }

        //界面控制 根据是否加载完音频来判断
        private void loadFinished(bool check)
        {
            if (check)
            {
                LoadingFinishText.Visibility = Visibility.Visible;
                LoadingAudiosText.Visibility = Visibility.Collapsed;
                StartTestBtn.IsEnabled = true;
            }
            else
            {
                LoadingFinishText.Visibility = Visibility.Collapsed;
                LoadingAudiosText.Visibility = Visibility.Visible;
                StartTestBtn.IsEnabled = false;
            }
        }
    }
}
