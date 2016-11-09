using MetaData;
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

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.BuyMinerCompleted -= Client_BuyMinerCompleted;
        }

        void Client_BuyMinerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
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
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("购买矿工成功");
                    App.UserVMObject.AsyncGetPlayerInfo();
                    _syn.Post(p =>
                    {
                        this.DialogResult = true;
                    }, null);
                }
                else
                {
                    MyMessageBox.ShowInfo("购买失败。原因：" + OperResult.GetMsg(result));
                    return;
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买矿工，服务器回调处理异常。" + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
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
                    //MyMessageBox.ShowInfo("账户余额不足，请充值。");
                    return;
                }
                GlobalData.Client.BuyMiner(count, count);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Buy Miner Exception", exc);
                MyMessageBox.ShowInfo("购买矿工异常");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void numMinersCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                this.txtError.Visibility = System.Windows.Visibility.Collapsed;
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
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }
    }
}
