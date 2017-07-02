using MetaData;
using MetaData.SystemConfig;
using MetaData.Trade;
using Microsoft.Win32;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Game;
using SuperMinersServerApplication.Controller.Trade;
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
        private int MAXLISTERRORLOGSCOUNT = 2000;
        private object _lockListLogs = new object();
        ObservableCollection<string> ListErrorLogsOutput = new ObservableCollection<string>();
        private System.Threading.SynchronizationContext _syn = SynchronizationContext.Current;

        public MainWindow()
        {
            InitializeComponent();

#if V1

            this.Title = "迅灵矿场一区 服务器" + System.Configuration.ConfigurationManager.AppSettings["softwareversion"];

#else

            this.Title = "迅灵矿场二区 服务器" + System.Configuration.ConfigurationManager.AppSettings["softwareversion"];

#endif

            LogHelper.Instance.LogAdded += Instance_LogAdded;
            App.ServiceToRun.ServiceStateChanged += ServiceToRun_ServiceStateChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindUI();

            //LoadActionLogs();
            StartService();

            string clientVersion = LoadValidClientVersion();
            if (!string.IsNullOrEmpty(clientVersion))
            {
                this.txtClientVersion.Text = clientVersion;
                GlobalConfig.CurrentClientVersion = clientVersion;
            }

            PlayerActionController.Instance.LoadActionLogs();
            AdminController.Instance.GetAllAdmin();
            LogHelper.Instance.AddInfoLog("服务器启动成功");
        }

        private bool SaveValidClientVersion(string clientVersion)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateElement("root");
                XmlElement node = doc.CreateElement("clientVersion");
                node.InnerText = clientVersion;
                root.AppendChild(node);
                doc.AppendChild(root);
                doc.Save(GlobalData.ClientVersionFile);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string LoadValidClientVersion()
        {
            try
            {
                if (File.Exists(GlobalData.ClientVersionFile))
                {
                    using (FileStream stream = File.Open(GlobalData.ClientVersionFile, FileMode.Open))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(stream);
                        XmlNode root = doc.SelectSingleNode("root");
                        if (root == null)
                        {
                            return null;
                        }
                        XmlNode node = root.SelectSingleNode("clientVersion");
                        if (node == null)
                        {
                            return null;
                        }
                        string version = node.InnerText;
                        return version;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
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
            LogHelper.Instance.AddInfoLog("正在停止服务...");
            PlayerActionController.Instance.SaveActionLogs();
            RouletteAwardController.Instance.SaveRouletteRoundInfoToData();
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

        private void btnUpdateNotices_Click(object sender, RoutedEventArgs e)
        {
            NoticeInfo notice = this.datagridNotices.SelectedItem as NoticeInfo;
            //var notices = NoticeController.Instance.GetCheckedNotices();
            if (notice == null)
            {
                MessageBox.Show("请选择公告");
                return;
            }
            //if (notices.Count == 1)
            //{
            //    MessageBox.Show("只能选择一个公告");
            //    return;
            //}
            AddNoticeWindow win = new AddNoticeWindow(notice);
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

        private void btnSaveClientVersion_Click(object sender, RoutedEventArgs e)
        {
            string clientVersion = this.txtClientVersion.Text.Trim();
            if (clientVersion == "")
            {
                MessageBox.Show("请输入客户端版本号");
                return;
            }

            bool isOK = this.SaveValidClientVersion(clientVersion);
            if (isOK)
            {
                GlobalConfig.CurrentClientVersion = clientVersion;
                MessageBox.Show("客户端版本号保存成功");
            }
            else
            {
                MessageBox.Show("客户端版本号保存失败");
            }
        }

        private void btnLogsOutput_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDig = new SaveFileDialog();
            saveDig.Filter = "txt文件(*.txt)|*.txt";
            if (saveDig.ShowDialog() == true)
            {
                lock (_lockListLogs)
                {
                    using (Stream stream = saveDig.OpenFile())
                    {
                        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
                        foreach (var item in this.ListErrorLogsOutput)
                        {
                            writer.WriteLine(item);
                        }
                        writer.Close();
                        //stream.Flush();
                        
                    }
                }

                MessageBox.Show("输出完成");
            }
        }

        private void btnRejectAllSellStoneOrders_Click(object sender, RoutedEventArgs e)
        {
            var waitOrderDBObjects = DBProvider.StoneOrderDBProvider.GetSellOrderList("", "", (int)SellOrderState.Wait, null, null, 0, 0);

            int errorCount = 0;

            if (waitOrderDBObjects != null)
            {
                foreach (var item in waitOrderDBObjects)
                {
                    //int result = OrderController.Instance.StoneOrderController.CancelSellOrder(item.SellerUserName, item.OrderNumber);
                    //if (result != OperResult.RESULTCODE_TRUE)
                    //{
                    //    MessageBox.Show(OperResult.GetMsg(result));
                    //}
                }

                MessageBox.Show("成功拒绝矿石出售订单：" + waitOrderDBObjects.Length.ToString());
            }
        }

        private void btnDeletePlayers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = PlayerController.Instance.AutoDeletePlayer();
                MessageBox.Show("成功删除" + count + "玩家");
                PlayerController.Instance.Init();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void chkPerDayAutoDelete_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkPerDayAutoDelete_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckStackMarketState_Click(object sender, RoutedEventArgs e)
        {
            CheckStackMarketState();
        }

        private void CheckStackMarketState()
        {
            var todayInfo = OrderController.Instance.StoneStackController.GetTodayStackInfo();
            if (todayInfo == null)
            {
                this.txtStackMarketState.Text = "闭市";
            }
            else
            {
                this.txtStackMarketState.Text = todayInfo.MarketState.ToString();
            }
        }

        private void btnOpenMarket_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.StoneStackController.MarketOpen();
            CheckStackMarketState();
        }

        private void btnSuspendMarket_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.StoneStackController.MarketSuspend();
            CheckStackMarketState();
        }

        private void btnResumeMarket_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.StoneStackController.MarketResume();
            CheckStackMarketState();
        }

        private void btnCloseMarket_Click(object sender, RoutedEventArgs e)
        {
            OrderController.Instance.StoneStackController.MarketClose();
            CheckStackMarketState();
        }

        private void btnDeleteAllSellStones_Click(object sender, RoutedEventArgs e)
        {
            DeleteAllStoneSellOrderWindow win = new DeleteAllStoneSellOrderWindow();
            win.ShowDialog();
        }


    }
}
