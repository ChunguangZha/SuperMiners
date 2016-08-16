using SuperMinersWPF.StringResources;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for BuyMinerWindow.xaml
    /// </summary>
    public partial class BuyMinerWindow : Window
    {
        private SynchronizationContext _syn;
        public BuyMinerWindow()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.BuyMinerCompleted += Client_BuyMinerCompleted;
            this.txtRMB.Text = GlobalData.CurrentUser.RMB.ToString();
            this.txtGoldCoin.Text = GlobalData.CurrentUser.GoldCoin.ToString();
            this.txtGoldCoin_Miner.Text = GlobalData.GameConfig.GoldCoin_Miner.ToString();
        }

        void Client_BuyMinerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("访问服务器失败。");
                return;
            }

            int result = e.Result;
            if (result < 0)
            {
                MyMessageBox.ShowInfo("服务器不存在当前用户，请联系平台客服。");
                return;
            }
            if (result == 0)
            {
                MyMessageBox.ShowInfo("购买失败。");
                return;
            }

            MyMessageBox.ShowInfo("成功购买 " + result.ToString() + "位矿工。");
            App.UserVMObject.AsyncGetPlayerInfo();

            _syn.Post(p =>
            {
                this.DialogResult = true;
            }, null);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int count = (int)this.numMinersCount.Value;
            if (count == 0)
            {
                MyMessageBox.ShowInfo("请输入有效" + Strings.Miner + "数");
                return;
            }

            decimal money = count * GlobalData.GameConfig.GoldCoin_Miner;
            this.txtNeedMoney.Text = money.ToString();
            if (money > GlobalData.CurrentUser.GoldCoin)
            {
                MyMessageBox.ShowInfo("账户余额不足，请充值。");
                return;
            }
            GlobalData.Client.BuyMiner(count, count);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void numMinersCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int count = (int)this.numMinersCount.Value;
            decimal money = count * GlobalData.GameConfig.GoldCoin_Miner;
            this.txtNeedMoney.Text = money.ToString();
            if (money > GlobalData.CurrentUser.GoldCoin)
            {
                decimal allGoldcoin = GlobalData.CurrentUser.GoldCoin + GlobalData.CurrentUser.RMB * GlobalData.GameConfig.RMB_GoldCoin;
                if (money > allGoldcoin)
                {
                    this.txtError.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.txtError.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
