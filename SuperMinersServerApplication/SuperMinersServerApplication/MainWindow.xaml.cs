using MetaData.SystemConfig;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.UIModel;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
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

namespace SuperMinersServerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int MAXLISTERRORLOGSCOUNT = 100;
        private object _lockListLogs = new object();
        ObservableCollection<string> ListErrorLogsOutput = new ObservableCollection<string>();
        private System.Threading.SynchronizationContext _syn = SynchronizationContext.Current;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "服务器配置" + System.Configuration.ConfigurationManager.AppSettings["softwareversion"];

            LogHelper.Instance.LogAdded += Instance_LogAdded;
            App.ServiceToRun.ServiceStateChanged += ServiceToRun_ServiceStateChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindUI();

            StartService();
        }

        private void StartService()
        {
            this.progressbar.Visibility = System.Windows.Visibility.Visible;
#if DEBUG
            App.ServiceToRun.StartByApplication();
#else            
            ServiceBase.Run(App.ServiceToRun);
#endif

            if (App.ServiceToRun.IsStarted)
            {
                this.tabItemConfig.IsEnabled = true;
            }
            else
            {
                this.tabItemConfig.IsEnabled = false;
            }
        }

        private void BindUI()
        {
            UpdateServiceStateToUI();
            BindListLogs();

            BindNoticeLists();
        }

        private void BindNoticeLists()
        {
            Binding bind = new Binding();
            bind.Source = NoticeController.Instance.ListNotices;
            this.datagridNotices.SetBinding(DataGrid.ItemsSourceProperty, bind);
        }

        private void BindListLogs()
        {
            Binding bind = new Binding();
            bind.Source = this.ListErrorLogsOutput;
            this.listboxLogs.SetBinding(ListBox.ItemsSourceProperty, bind);
        }

        private void BindConfigToUI()
        {

        }

        void Instance_LogAdded(bool isError, string log)
        {
            lock (_lockListLogs)
            {
                if (isError)
                {
                    _syn.Post(o =>
                    {
                        if (ListErrorLogsOutput.Count >= MAXLISTERRORLOGSCOUNT)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                ListErrorLogsOutput.RemoveAt(0);
                            }
                        }

                        ListErrorLogsOutput.Add(log);
                    }, null);
                }
            }
        }

        void ServiceToRun_ServiceStateChanged(object sender, EventArgs e)
        {
            UpdateServiceStateToUI();

            if (App.ServiceToRun.IsStarted)
            {
                GameSystemConfigController.Instance.Init();
                GetSystemConfig();
            }
        }

        private void GetSystemConfig()
        {
            try
            {
                GameSystemConfigController.Instance.GetSystemConfig();

                this.tabitemAwardReferrer.DataContext = GameSystemConfigController.Instance;
                this.tabitemGameConfig.DataContext = GameSystemConfigController.Instance.InnerGameConfig;
                this.tabitemIncomeAccount.DataContext = GameSystemConfigController.Instance.InnerIncomeMoneyAccount;
                this.tabitemRegisterUser.DataContext = GameSystemConfigController.Instance.InnerRegisterPlayerConfig;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Admin UI Get System Config failed", exc);
            }
        }

        private void UpdateServiceStateToUI()
        {
            this.progressbar.Visibility = System.Windows.Visibility.Hidden;

            if (App.ServiceToRun.IsStarted)
            {
                imgStarted.Visibility = System.Windows.Visibility.Visible;
                imgStopped.Visibility = System.Windows.Visibility.Collapsed;

                lblServiceWasStarted.Visibility = System.Windows.Visibility.Visible;
                lblServiceWasStopped.Visibility = System.Windows.Visibility.Collapsed;

                btnStartService.Visibility = System.Windows.Visibility.Collapsed;
                btnStopService.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                imgStarted.Visibility = System.Windows.Visibility.Collapsed;
                imgStopped.Visibility = System.Windows.Visibility.Visible;

                lblServiceWasStarted.Visibility = System.Windows.Visibility.Collapsed;
                lblServiceWasStopped.Visibility = System.Windows.Visibility.Visible;

                btnStartService.Visibility = System.Windows.Visibility.Visible;
                btnStopService.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void btnStartService_Click(object sender, RoutedEventArgs e)
        {
            StartService();
        }

        private void btnStopService_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceToRun.Stop();
        }

        private void btnUpdateAlipay2DCodeImage_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.OpenFileDialog openDig = new System.Windows.Forms.OpenFileDialog();
            //openDig.Filter = "Image files (*.jpg)|*.jpg";
            //openDig.RestoreDirectory = true;
            //if (openDig.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string filePath = openDig.FileName;
            //    try
            //    {
            //        BitmapImage bmp = new BitmapImage();
            //        bmp.StreamSource = File.OpenRead(filePath);
            //        this.InnerIncomeMoneyAccount.Alipay2DCodeImage = bmp;
            //    }
            //    catch (Exception exc)
            //    {

            //    }
            //}
        }

        private void btnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            WinInputPassword winInputPassword = new WinInputPassword();
            if (winInputPassword.ShowDialog() == true)
            {
                if (winInputPassword.Password != GlobalData.SaveConfigPassword)
                {
                    System.Windows.MessageBox.Show("密码错误，不能保存。");

                    GetSystemConfig();
                    return;
                }

                try
                {
                    GameSystemConfigController.Instance.SaveSystemConfig();                    
                    System.Windows.MessageBox.Show("保存成功。");
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("Save System Config failed", exc);
                    System.Windows.MessageBox.Show("保存失败。");
                    GetSystemConfig();
                }
            }
            else
            {
                GetSystemConfig();
            }
        }

        private void btnCreateNotices_Click(object sender, RoutedEventArgs e)
        {
            AddNoticeWindow win = new AddNoticeWindow();
            win.ShowDialog();
        }

        private void btnDeleteNotices_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnclearAllNotices_Click(object sender, RoutedEventArgs e)
        {
            NoticeController.Instance.SetAllChecked(false);
        }

        private void btnSelectAllNotices_Click(object sender, RoutedEventArgs e)
        {
            NoticeController.Instance.SetAllChecked(true);
        }

        private void btnSelectAllPlayers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnclearAllPlayers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeletePlayers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewReferTree_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewTradeRecord_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
