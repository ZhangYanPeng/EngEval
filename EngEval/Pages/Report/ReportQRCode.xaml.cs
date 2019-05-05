using EngEval.Common.Http;
using Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
using Color = System.Drawing.Color;

namespace EngEval.Pages.Report
{
    /// <summary>
    /// ReportQRCode.xaml 的交互逻辑
    /// </summary>
    public partial class ReportQRCode : Page
    {
        public int Type { get; set; }
        public string No { get; set; }
        string url { get; set; }

        public ReportQRCode(string no, int t)
        {
            InitializeComponent();
            Type = t;
            No = no;
            Loaded += CreateQRCode;
        }

        private void CreateQRCode(object sender, RoutedEventArgs e) {
            // 生成二维码的内容
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            string strCode = Setting.BASE_URL + "pdf/";
            if(Type == 1)
            {
                Boolean isSuccess = false;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("uid", mainwin.User.id.ToString());
                string url = Setting.BASE_URL + "test/getOverallReport";
                string rtext = HttpRequestHelper.HttpGet(url, parameters, ref isSuccess);
                strCode += No + "overall.pdf";
            }
            else
            {
                Boolean isSuccess = false;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("uid", mainwin.User.id.ToString());
                parameters.Add("tid", No);
                string url = Setting.BASE_URL + "test/getSingleReport";
                string rtext = HttpRequestHelper.HttpGet(url, parameters, ref isSuccess);
                strCode += mainwin.User.id + No + "test.pdf";
            }
            url = strCode;

            QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);

            MemoryStream stream = new MemoryStream();
            Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, false);
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            ImageBrush imageBrush = new ImageBrush();
            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
            QRCode.Source = (ImageSource)imageSourceConverter.ConvertFrom(stream);
        }

        private void ReporterViewer_Loaded(object sender, RoutedEventArgs e)
        {
            try { 
                string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Directory.Exists("TEMP/") == false)//如果不存在就创建file文件夹 
                {
                    Directory.CreateDirectory("TEMP/");
                }
                if (File.Exists(BaseDirectoryPath + "TEMP\\" + url.Substring(url.LastIndexOf("/")+1) + "Reporter.pdf"))
                {
                    FileInfo fi = new FileInfo(BaseDirectoryPath + "TEMP\\" + url.Substring(url.LastIndexOf("/") + 1) + "Reporter.pdf");
                    fi.Attributes = FileAttributes.Normal;
                    fi.IsReadOnly = false;
                    fi.Delete();
                }
                bool check = false;
                while (!check)
                {
                    check = DownloadFile.Download(url, "TEMP/" + url.Substring(url.LastIndexOf("/") + 1) + "Reporter.pdf");
                }
                ReporterViewer.LoadFile(BaseDirectoryPath + "/TEMP/" + url.Substring(url.LastIndexOf("/") + 1) + "Reporter.pdf");
            }
            catch(Exception exp)
            {
                MessageBox.Show("文件操作异常，可能文件已经打开，请检查确认！");
                return;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

            if (Type == 1 && mainwin.ListeningTest !=null && mainwin.ListeningTest.testno == 3)
            {
                if (mainwin.user.systemFeedback == null || mainwin.user.systemFeedback == "")
                {
                    Pages.Report.SystemFeedback sfd = new Pages.Report.SystemFeedback();
                    sfd.ShowDialog();
                }
            }
            mainwin.FrameNavigator("funclist");
        }

        private void LoadReportUrl(object sender, RoutedEventArgs e)
        {
            //hyperlink.NavigateUri = url;
            System.Diagnostics.Process.Start(url);
        }
    }
}
