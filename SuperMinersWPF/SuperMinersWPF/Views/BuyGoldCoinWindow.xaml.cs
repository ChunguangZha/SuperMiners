using MetaData;
using MetaData.Trade;
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
    /// Interaction logic for BuyGoldCoinWindow.xaml
    /// </summary>
    public partial class BuyGoldCoinWindow : Window
    {
        private SynchronizationContext _syn;
        private bool AlipayPaySucceed = false;

        public BuyGoldCoinWindow()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.GoldCoinRechargeCompleted += Client_GoldCoinRechargeCompleted;
            App.StoneOrderVMObject.BuyGoldCoinAlipayPaySucceed += StoneOrderVMObject_BuyGoldCoinAlipayPaySucceed;
            this.txtRMB.Text = GlobalData.CurrentUser.RMB.ToString();
            this.txtRMB_GoldCoin.Text = GlobalData.GameConfig.RMB_GoldCoin.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.StoneOrderVMObject.BuyGoldCoinAlipayPaySucceed -= StoneOrderVMObject_BuyGoldCoinAlipayPaySucceed;
            GlobalData.Client.GoldCoinRechargeCompleted -= Client_GoldCoinRechargeCompleted;
        }

        void StoneOrderVMObject_BuyGoldCoinAlipayPaySucceed()
        {
            AlipayPaySucceed = true;
        }

        void Client_GoldCoinRechargeCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.TradeOperResult> e)
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

            TradeOperResult result = e.Result;
            if (result.ResultCode != OperResult.RESULTCODE_TRUE)
            {
                MyMessageBox.ShowInfo("金币充值失败。原因：" + ResultCodeMsg.GetMsg(result.ResultCode));
                return;
            }
            if (result.PayType == (int)PayType.Alipay)
            {
                MyWebPage.ShowMyWebPage(result.AlipayLink);
                MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");

                var payResult = MyMessageBox.ShowAlipayPayQuestion();
                if (payResult == MessageBoxAlipayPayQuestionResult.Succeed)
                {
                    if (!AlipayPaySucceed)
                    {
                        MyMessageBox.ShowInfo("没有接收到支付宝付款信息。如确实付款，请稍后查看购买记录，或联系客服。");
                    }
                }
                else if (payResult == MessageBoxAlipayPayQuestionResult.Failed)
                {
                    MyWebPage.ShowMyWebPage(result.AlipayLink);
                    MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");
                    return;
                }
            }

            App.UserVMObject.AsyncGetPlayerInfo();
            _syn.Post(p =>
            {
                this.DialogResult = true;
            }, null);
        }
        
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int spendRMB = (int)this.numRechargeRMB.Value;
            if (spendRMB == 0)
            {
                MyMessageBox.ShowInfo("请输入有效" + Strings.RMB + "值");
                return;
            }

            decimal GainGoldCoin = spendRMB * GlobalData.GameConfig.RMB_GoldCoin;
            this.txtGainGoldCoin.Text = GainGoldCoin.ToString();
            int payType;
            if (chkPayType.IsChecked == false)
            {
                if (spendRMB > GlobalData.CurrentUser.RMB)
                {
                    MyMessageBox.ShowInfo("账户余额不足，请充值。");
                    return;
                }
                payType = (int)PayType.RMB;
            }
            else
            {
                payType = (int)PayType.Alipay;
            }
            GlobalData.Client.GoldCoinRecharge((int)GainGoldCoin, payType);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void numRechargeRMB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int spendRMB = (int)this.numRechargeRMB.Value;
            decimal gainGoldCoin = spendRMB * GlobalData.GameConfig.RMB_GoldCoin;
            this.txtGainGoldCoin.Text = gainGoldCoin.ToString();

            if (chkPayType.IsChecked == true)
            {
                this.txtError.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                if (spendRMB > GlobalData.CurrentUser.RMB)
                {
                    this.txtError.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.txtError.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void chkPayType_Checked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "支付宝支付";
            this.txtError.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void chkPayType_Unchecked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "灵币支付";

            int spendRMB = (int)this.numRechargeRMB.Value;
            if (spendRMB > GlobalData.CurrentUser.RMB)
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
