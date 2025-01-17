﻿using AdvancedSharpAdbClient.SampleApp.Common;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace AdvancedSharpAdbClient.SampleApp.ControlPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ADBServerPage : Page
    {
        public ADBServerPage()
        {
            this.InitializeComponent();
        }

        private async void Control1_Click(object sender, RoutedEventArgs e)
        {
            CrossPlatformFunc.RunProcess = UWPPlatformFunc.RunProcess;
            Control1Progress.Visibility = Visibility.Visible;
            Control1.Content = "Starting";
            StorageFile adb = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/ADB/adb.exe"));
            StartServerResult result = new AdbServer().StartServer(adb.Path, false);
            AdbServerStatus status = await Task.Run(AdbServer.Instance.GetStatus);
            Control1Output.Text = status.IsRunning ? "Succeed" : "Failed";
            Control1.Content = "Restart";
            Control1Progress.Visibility = Visibility.Collapsed;
        }

        private async void Control2_Click(object sender, RoutedEventArgs e)
        {
            Control2Progress.Visibility = Visibility.Visible;
            Control2.Content = "Checking";
            AdbServerStatus status = await Task.Run(AdbServer.Instance.GetStatus);
            Control2Output1.Text = $"Version: {status.Version}";
            Control2Output2.Text = $"IsRunning: {status.IsRunning}";
            Control2Output1.Visibility = status.IsRunning ? Visibility.Visible : Visibility.Collapsed;
            Control2.Content = "Checked";
            Control2Progress.Visibility = Visibility.Collapsed;
        }
    }
}
