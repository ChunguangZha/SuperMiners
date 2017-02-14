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
    /// Interaction logic for BuyMineWindow.xaml
    /// </summary>
    public partial class BuyMineWindow : Window
    {
        private SynchronizationContext _syn;
        private bool AlipayPaySucceed = false;

        public BuyMineWindow()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.BuyMineCompleted += Client_BuyMineCompleted;
            App.StoneOrderVMObject.BuyMineAlipayPaySucceed += StoneOrderVMObject_BuyMineAlipayPaySucceed;
            this.txtRMB.Text = GlobalData.CurrentUser.RMB.ToString();
            this.txtRMB_Mine.Text = GlobalData.GameConfig.RMB_Mine.ToString();
        }

        void StoneOrderVMObject_BuyMineAlipayPaySucceed()
        {
            AlipayPaySucceed = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.BuyMineCompleted -= Client_BuyMineCompleted;
            App.StoneOrderVMObject.BuyMineAlipayPaySucceed -= StoneOrderVMObject_BuyMineAlipayPaySucceed;
        }

        void Client_BuyMineCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<TradeOperResult> e)
        {
            try
            {
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null || e.Result == null)
                {
                    MyMessageBox.ShowInfo("访问服务器失败。");
                    return;
                }

                TradeOperResult result = e.Result;
                if (result.ResultCode != OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("勘探矿山失败。原因：" + OperResult.GetMsg(result.ResultCode));
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
                            if (AlipayPaySucceed)
                            {
                                MyMessageBox.ShowInfo("成功收获" + e.Result.OperNumber + "的矿石储量");
                            }
                            else
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
                    else
                    {
                        MyMessageBox.ShowInfo("成功收获" + e.Result.OperNumber + "的矿石储量");
                    }
                }
                else
                {
                    MyMessageBox.ShowInfo("成功收获" + e.Result.OperNumber + "的矿石储量");
                }

                App.UserVMObject.AsyncGetPlayerInfo();

                _syn.Post(p =>
                {
                    this.DialogResult = true;
                }, null);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买矿山，服务器回调处理异常。" + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = 1;
                PayType payType = PayType.Alipay;

                if (this.cmbPayType.SelectedIndex == 0)
                {
                    payType = PayType.RMB; 
                }
                else if (this.cmbPayType.SelectedIndex == 1)
                {
                    payType = PayType.Credits; 
                }

                if (payType == PayType.RMB)
                {
                    decimal money = count * GlobalData.GameConfig.RMB_Mine;
                    if (money > GlobalData.CurrentUser.RMB)
                    {
                        MyMessageBox.ShowInfo("账户余额不足，请充值。");
                        return;
                    }
                }
                else if (payType == PayType.Credits)
                {
                    decimal valueShoppingCredits = count * GlobalData.GameConfig.RMB_Mine * GlobalData.GameConfig.Credits_RMB;
                    if (valueShoppingCredits > GlobalData.CurrentUser.ShoppingCreditsEnabled)
                    {
                        MyMessageBox.ShowInfo("账户余额不足，请充值。");
                        return;
                    }
                }
                GlobalData.Client.BuyMine(count, (int)payType);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Buy Mine Exception", exc);
                MyMessageBox.ShowInfo("勘探矿山失败");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cmbPayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbPayType.SelectedIndex == 1)//积分
            {
                decimal valueDiamond = GlobalData.GameConfig.RMB_Mine * GlobalData.GameConfig.Credits_RMB;
                this.txtRMB_Mine.Text = ((int)Math.Ceiling(valueDiamond)).ToString();
                this.txtPayUnit.Text = "积分";
            }
            else
            {
                this.txtRMB_Mine.Text = GlobalData.GameConfig.RMB_Mine.ToString();
                this.txtPayUnit.Text = "灵币";
            }
        }

        //private void chkPayType_Checked(object sender, RoutedEventArgs e)
        //{
        //    this.chkPayType.Content = "支付宝支付";
        //    this.txtError.Visibility = System.Windows.Visibility.Collapsed;
        //}

        //private void chkPayType_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    this.chkPayType.Content = "灵币支付";

        //    int count = 1;
        //    decimal spendRMB = count * GlobalData.GameConfig.RMB_Mine;
        //    if (spendRMB > GlobalData.CurrentUser.RMB)
        //    {
        //        this.txtError.Visibility = System.Windows.Visibility.Visible;
        //    }
        //    else
        //    {
        //        this.txtError.Visibility = System.Windows.Visibility.Collapsed;
        //    }
        //}
    }
}
