using System;
using System.Windows;
using System.Windows.Controls;

namespace EngEval.Pages.Report
{
    /// <summary>
    /// PdfViewer.xaml 的交互逻辑
    /// </summary>
    public partial class PdfViewer : UserControl
    {
        private bool _isLoaded = false;
        public PdfViewer()
        {
            InitializeComponent();
        }

        public void LoadFile(string file_path)
        {
            try
            {
                moonPdfPanel.OpenFile(file_path);
                _isLoaded = true;
            }
            catch (Exception e)
            {
                _isLoaded = false;
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomIn();
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomOut();
            }
        }

        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.Zoom(1.0);
            }
        }

        private void FitToHeightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomToHeight();
            }
        }

        private void FacingButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ViewType = MoonPdfLib.ViewType.Facing;
            }
        }

        private void SinglePageButton_Click(object sender, RoutedEventArgs e)
            {
                if (_isLoaded)
                {
                    moonPdfPanel.ViewType = MoonPdfLib.ViewType.SinglePage;
            }
        }

    }
}
