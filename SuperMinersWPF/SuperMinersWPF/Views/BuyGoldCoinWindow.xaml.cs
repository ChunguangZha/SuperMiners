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

            //if (GlobalData.CurrentUser.RMB <= 0)
            //{
            //    this.chkPayType.IsChecked = true;
            //    this.chkPayType.IsEnabled = false;
            //}
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

                TradeOperResult result = e.Result;
                if (result.ResultCode != OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("金币充值失败。原因：" + OperResult.GetMsg(result.ResultCode));
                    return;
                }
                if (result.PayType == (int)PayType.Alipay)
                {
                    MyWebPage.ShowMyWebPage(result.AlipayLink);
                    MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");

                    if (!AlipayPaySucceed)
                    {
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
                }

                App.UserVMObject.AsyncGetPlayerInfo();
                _syn.Post(p =>
                {
                    this.DialogResult = true;
                }, null);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("充值金币，服务器回调处理异常。" + exc.Message);
            }
        }

        private PayType GetPayType()
        {
            PayType payType = PayType.RMB;

            if (this.cmbPayType.SelectedIndex == 0)
            {
                payType = PayType.RMB;
            }
            else if (this.cmbPayType.SelectedIndex == 1)
            {
                payType = PayType.Diamand;
            }
            else if (this.cmbPayType.SelectedIndex == 2)
            {
                payType = PayType.Alipay;
            }

            return payType;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PayType payType = GetPayType();

                //该值可能是灵币，也可能是钻石
                int payMoney = (int)this.numRechargeRMB.Value;
                if (payMoney == 0)
                {
                    MyMessageBox.ShowInfo("请输入有效值");
                    return;
                }

                decimal GainGoldCoin = 0;
                int spendRMB = 0;

                if (payType == PayType.RMB)
                {
                    spendRMB = payMoney;
                    GainGoldCoin = spendRMB * GlobalData.GameConfig.RMB_GoldCoin;
                    this.txtGainGoldCoin.Text = GainGoldCoin.ToString();

                    if (payMoney > GlobalData.CurrentUser.RMB)
                    {
                        MyMessageBox.ShowInfo("账户余额不足，请选择其它支付方式。");
                        return;
                    }
                }
                else if (payType == PayType.Diamand)
                {
                    spendRMB = (int)Math.Ceiling(payMoney / GlobalData.GameConfig.Diamonds_RMB);
                    GainGoldCoin = spendRMB * GlobalData.GameConfig.RMB_GoldCoin;
                    this.txtGainGoldCoin.Text = GainGoldCoin.ToString();

                    if (payMoney > GlobalData.CurrentUser.StockOfDiamonds)
                    {
                        MyMessageBox.ShowInfo("账户余额不足，请选择其它支付方式。");
                        return;
                    }
                }
                else if (payType == PayType.Alipay)
                {
                    spendRMB = payMoney;
                    GainGoldCoin = spendRMB * GlobalData.GameConfig.RMB_GoldCoin;
                    this.txtGainGoldCoin.Text = GainGoldCoin.ToString();
                }

                GlobalData.Client.GoldCoinRecharge((int)GainGoldCoin, (int)payType);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Buy GoldCoin Exception", exc);
                MyMessageBox.ShowInfo("充值金币异常");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void numRechargeRMB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                ComputeGainGoldcoin();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("充值金币，输入灵币时异常。" + exc.Message);
            }
        }

        private void ComputeGainGoldcoin()
        {
            PayType payType = GetPayType();

            int payMoney = (int)this.numRechargeRMB.Value;
            decimal gainGoldCoin;
            if (payType == PayType.Diamand)
            {
                int spendRMB = (int)Math.Ceiling(payMoney / GlobalData.GameConfig.Diamonds_RMB);
                gainGoldCoin = spendRMB * GlobalData.GameConfig.RMB_GoldCoin;
            }
            else
            {
                gainGoldCoin = payMoney * GlobalData.GameConfig.RMB_GoldCoin;
            }
            if (this.txtGainGoldCoin != null)
            {
                this.txtGainGoldCoin.Text = gainGoldCoin.ToString();
            }
        }

        private void cmbPayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.txtGainGoldCoin == null)
            {
                return;
            }
            ComputeGainGoldcoin();
        }
    }
}
