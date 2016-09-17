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
using System.Xml;

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

            //LoadActionLogs();
            StartService();

            PlayerActionController.Instance.LoadActionLogs();
            AdminController.Instance.GetAllAdmin();
            LogHelper.Instance.AddInfoLog("服务器启动成功");
        }

        private void StartService()
        {
            this.progressbar.Visibility = System.Windows.Visibility.Visible;
//#if DEBUG
//            App.ServiceToRun.StartByApplication();
//#else            
//            ServiceBase.Run(App.ServiceToRun);
//#endif

            App.ServiceToRun.StartByApplication();

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

            Binding bind = new Binding();
            bind.Source = AdminController.Instance.ListAdmin;
            this.datagridAdmin.SetBinding(DataGrid.ItemsSourceProperty, bind);
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
                                ListErrorLogsOutput.RemoveAt(ListErrorLogsOutput.Count - 1);
                            }
                        }

                        ListErrorLogsOutput.Insert(0, log);
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
            PlayerActionController.Instance.SaveActionLogs();
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

        private void btnDeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (AdminController.Instance.GetCheckedItemsCount() != 1)
            {
                MessageBox.Show("请选择一个管理员进行删除。");
                return;
            }

            var admin = AdminController.Instance.GetFirstCheckedAdmin();
            if (admin != null)
            {
                var result = MessageBox.Show("请确定是否删除该管理员？", "删除管理员", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    AdminController.Instance.DeleteAdmin(admin.UserName);
                }
            }
        }

        private void btnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            EditAdminWindow win = new EditAdminWindow(true, null);
            if (win.ShowDialog() == true)
            {
                AdminController.Instance.GetAllAdmin();
            }
        }

        private void btnEditAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (this.datagridAdmin.SelectedItem is AdminUIModel)
            {

                MessageBox.Show("abc行修改。");
            }
            if (AdminController.Instance.GetCheckedItemsCount() != 1)
            {
                MessageBox.Show("请选择一个管理员进行修改。");
                return;
            }

            var admin = AdminController.Instance.GetFirstCheckedAdmin();
            if (admin != null)
            {
                EditAdminWindow win = new EditAdminWindow(false, admin);
                if (win.ShowDialog() == true)
                {
                    AdminController.Instance.GetAllAdmin();
                }
            }
        }


    }
}
