using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.View;
using System;
using System.Collections.Generic;
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

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Threading.SynchronizationContext _syn;
        public bool IsBackToLogin = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Title += "   --" + GlobalData.CurrentAdmin.UserName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._syn = System.Threading.SynchronizationContext.Current;
            //CreateTreeView();
            BindUI();

            GlobalData.Client.OnKickoutByUser += Client_OnKickoutByUser;
        }
        
        //private void CreateTreeView()
        //{
        //    CreateTradeSystemTreeView();
        //}

        //private void CreateTradeSystemTreeView()
        //{
        //    CreateTradeSystemTreeViewItem(App.MinerTradeVMObject);
        //    CreateTradeSystemTreeViewItem(App.MineTradeVMObject);
        //    CreateTradeSystemTreeViewItem(App.GoldCoinTradeVMObject);
        //    CreateTradeSystemTreeViewItem(App.StoneTradeVMObject);
        //    CreateTradeSystemTreeViewItem(App.WithdrawRMBVMObject);
        //    CreateTradeSystemTreeViewItem(App.AlipayRechargeVMObject);
        //}

        //public void CreateTradeSystemTreeViewItem(object ItemDataContext)
        //{
        //    TreeViewItem tvItem = new TreeViewItem();
        //    tvItem.SetResourceReference(TreeViewItem.StyleProperty, "TVItemL2Style");

        //    Binding bind = new Binding()
        //    {
        //        Source = ItemDataContext
        //    };
        //    tvItem.SetBinding(TreeViewItem.DataContextProperty, bind);

        //    bind = new Binding("MenuHeader");
        //    tvItem.SetBinding(TreeViewItem.HeaderProperty, bind);

        //    tvItem.Items.Add(new TreeViewItem()
        //    {
        //        Header = "实时交易",
        //    });
        //    tvItem.Items.Add(new TreeViewItem()
        //    {
        //        Header = "交易记录",
        //    });

        //    this.tvL1TradeSystem.Items.Add(tvItem);
        //}


        private void BindUI()
        {
            this.tvL2_TS_GoldCoin.DataContext = App.GoldCoinTradeVMObject;
            this.controlGoldCoinRechargeActive.DataContext = App.GoldCoinTradeVMObject;
            this.controlGoldCoinRechargeHistory.DataContext = App.GoldCoinTradeVMObject;

            this.tvL2_TS_Mine.DataContext = App.MineTradeVMObject;
            this.controlMineTradeActive.DataContext = App.MineTradeVMObject;
            this.controlMineTradeHistory.DataContext = App.MineTradeVMObject;

            this.tvL2_TS_Miner.DataContext = App.MinerTradeVMObject;
            this.controlMinerTradeActive.DataContext = App.MinerTradeVMObject;
            this.controlMinerTradeHistory.DataContext = App.MinerTradeVMObject;

            this.tvL2_TS_Stone.DataContext = App.StoneTradeVMObject;
            this.controlStoneTradeActive.DataContext = App.StoneTradeVMObject;
            this.controlStoneTradeHistory.DataContext = App.StoneTradeVMObject;

            this.tvL2_TS_WithdrawRMB.DataContext = App.WithdrawRMBVMObject;
            this.controlWithdrawRMBActive.DataContext = App.WithdrawRMBVMObject;
            this.controlWithdrawRMBHistory.DataContext = App.WithdrawRMBVMObject;
            this.controlStoneTradeShowImage.DataContext = App.WithdrawRMBVMObject;
        }

        void Client_OnKickoutByUser()
        {
            this._syn.Post(s =>
            {
                MessageBox.Show("您的账户在其它电脑登录，如非本人操作，请及时修改密码。");
                this.IsBackToLogin = true;
                this.Close();
            }, null);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.LogoutAdmin();
        }

        private void HideAllControls()
        {
            this.controlGoldCoinRechargeActive.Visibility = System.Windows.Visibility.Collapsed;
            this.controlGoldCoinRechargeHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMinerTradeActive.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMinerTradeHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMineTradeActive.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMineTradeHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlNoticeManager.Visibility = System.Windows.Visibility.Collapsed;
            this.controlPlayerManager.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStoneTradeActive.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStoneTradeHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlWithdrawRMBActive.Visibility = System.Windows.Visibility.Collapsed;
            this.controlWithdrawRMBHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStoneTradeShowImage.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void tvL1PlayerManager_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlPlayerManager.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_Miner_Active_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlMinerTradeActive.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_Miner_History_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlMinerTradeHistory.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_Mine_Active_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlMineTradeActive.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_Mine_History_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlMineTradeHistory.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_Stone_Active_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlStoneTradeActive.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_Stone_History_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlStoneTradeHistory.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_GoldCoin_Active_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlGoldCoinRechargeActive.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_GoldCoin_History_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlGoldCoinRechargeHistory.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_WithdrawRMB_Active_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlWithdrawRMBActive.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_WithdrawRMB_History_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlWithdrawRMBHistory.Visibility = System.Windows.Visibility.Visible;
        }

        private void tvL2_TS_WithdrawRMB_ShowImage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HideAllControls();
            this.controlStoneTradeShowImage.Visibility = System.Windows.Visibility.Visible;
        }

    }
}
