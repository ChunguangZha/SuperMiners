using MetaData;
using MetaData.Trade;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for StackMarketControl.xaml
    /// </summary>
    public partial class StackMarketControl : UserControl
    {
        private bool AlipayPaySucceed = false;
        public Dictionary<int, string> DicPayType = new Dictionary<int, string>();

        public StackMarketControl()
        {
            InitializeComponent();

            this.DataContext = App.StackStoneVMObject;

            BindPayTypeComboBox();
        }

        public void BindPayTypeComboBox()
        {
            DicPayType.Add((int)PayType.RMB, "灵币");

            if (GlobalData.ServerType == ServerType.Server1)
            {
                DicPayType.Add((int)PayType.Diamand, "钻石");
            }
            DicPayType.Add((int)PayType.Alipay, "支付宝");

            this.cmbPayType.ItemsSource = DicPayType;
            this.cmbPayType.SelectedValue = (int)PayType.RMB;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalData.ServerType == ServerType.Server1)
            {
                this.btnBuyStone.IsEnabled = true;
            }
            else
            {
                this.btnBuyStone.IsEnabled = false;
            }

            this.sliderPrice.Maximum = (double)App.StackStoneVMObject.TodayStackInfo.LimitUpPrice;
            this.sliderPrice.Minimum = (double)App.StackStoneVMObject.TodayStackInfo.LimitDownPrice;
            this.sliderPrice.Value = (double)App.StackStoneVMObject.TodayStackInfo.OpenPrice;
        }
        
        public void AddEventHandlers()
        {
            App.StackStoneVMObject.MarketOpened += StackStoneVMObject_MarketOpened;
            App.StackStoneVMObject.MarketClosed += StackStoneVMObject_MarketClosed;
            GlobalData.Client.DelegateBuyStoneCompleted += Client_DelegateBuyStoneCompleted;
            App.StoneOrderVMObject.DelegateBuyStoneAlipayPaySucceed += StoneOrderVMObject_DelegateBuyStoneAlipayPaySucceed;
            App.StackStoneVMObject.GetTodayStackRecordInfoCompleted += StackStoneVMObject_GetTodayStackRecordInfoCompleted;
        }

        public void RemoveEventHandlers()
        {
            App.StackStoneVMObject.MarketOpened -= StackStoneVMObject_MarketOpened;
            App.StackStoneVMObject.MarketClosed -= StackStoneVMObject_MarketClosed;
            GlobalData.Client.DelegateBuyStoneCompleted -= Client_DelegateBuyStoneCompleted;
            App.StoneOrderVMObject.DelegateBuyStoneAlipayPaySucceed -= StoneOrderVMObject_DelegateBuyStoneAlipayPaySucceed;
            App.StackStoneVMObject.GetTodayStackRecordInfoCompleted -= StackStoneVMObject_GetTodayStackRecordInfoCompleted;
        }

        void StackStoneVMObject_GetTodayStackRecordInfoCompleted(MetaData.Game.StoneStack.StoneStackDailyRecordInfo obj)
        {
            if (this.btnBuyStone.IsEnabled)
            {
                //防止重复设置
                return;
            }
            if (App.StackStoneVMObject.TodayStackInfo != null)
            {
                this.sliderPrice.Value = (double)App.StackStoneVMObject.TodayStackInfo.OpenPrice;
                this.sliderPrice.Minimum = (double)App.StackStoneVMObject.TodayStackInfo.LimitDownPrice;
                this.sliderPrice.Maximum = (double)App.StackStoneVMObject.TodayStackInfo.LimitUpPrice;
                this.numPrice.Text = this.sliderPrice.Value.ToString();
            }
        }

        void StackStoneVMObject_MarketClosed()
        {
            this.btnBuyStone.IsEnabled = false;
        }

        void StackStoneVMObject_MarketOpened()
        {
            if (this.btnBuyStone.IsEnabled)
            {
                //防止重复设置
                return;
            }
            if (App.StackStoneVMObject.TodayStackInfo != null && App.StackStoneVMObject.TodayStackInfo.MarketState != MetaData.Game.StoneStack.StackMarketState.Closed)
            {
                this.btnBuyStone.IsEnabled = true;
            }
            else
            {
                this.btnBuyStone.IsEnabled = false;
            }
        }

        //private void BindPriceUI()
        //{
        //    Binding bind = null;
        //    bind = new Binding("Top5BuyOrderList")
        //    {
        //        Source = App.StackStoneVMObject.TodayStackInfo
        //    };
        //    this.lvBuy5Price.SetBinding(ListView.ItemsSourceProperty, bind);

        //    bind = new Binding("Top5SellOrderList")
        //    {
        //        Source = App.StackStoneVMObject.TodayStackInfo
        //    };
        //    this.lvSell5Price.SetBinding(ListView.ItemsSourceProperty, bind);
        //}

        private void btnBuyStone_Click(object sender, RoutedEventArgs e)
        {
            int buyStoneHandsCount = (int)this.numStoneHandCount.Value;
            if (buyStoneHandsCount <= 0)
            {
                MyMessageBox.ShowInfo("请输入要购买的矿石手数");
                return;
            }
            decimal price = Math.Round((decimal)this.sliderPrice.Value, 2);
            if (price <= 0)
            {
                MyMessageBox.ShowInfo("请输入要价格");
                return;
            }

            PayType paytype = (PayType)this.cmbPayType.SelectedValue;
            //if (this.cmbPayType.SelectedIndex == 0)
            //{
            //    paytype = PayType.RMB;
            //}
            //else if (this.cmbPayType.SelectedIndex == 1)
            //{
            //    paytype = PayType.Alipay;
            //}
            //else
            //{
            //    MyMessageBox.ShowInfo("请选择支付方式");
            //    return;
            //}

            if (paytype == PayType.RMB)
            {
                decimal money = buyStoneHandsCount * price;
                if (money > GlobalData.CurrentUser.RMB)
                {
                    MyMessageBox.ShowInfo("账户余额不足，请充值。");
                    return;
                }
            }
            else if (paytype == PayType.Diamand)
            {
                decimal valueDiamond = buyStoneHandsCount * price / GlobalData.GameConfig.Diamonds_RMB;
                if (valueDiamond > GlobalData.CurrentUser.StockOfDiamonds)
                {
                    MyMessageBox.ShowInfo("账户余额不足，请充值。");
                    return;
                }
            }
            this.AlipayPaySucceed = false;
            App.BusyToken.ShowBusyWindow("正在提交订单...");
            GlobalData.Client.DelegateBuyStone(buyStoneHandsCount, price, paytype, paytype);
        }

        void StoneOrderVMObject_DelegateBuyStoneAlipayPaySucceed()
        {
            try
            {
                this.AlipayPaySucceed = true;
                App.StackStoneVMObject.AsyncGetAllNotFinishedBuyOrders();
            }
            catch (Exception exc)
            {

            }
        }

        void Client_DelegateBuyStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.OperResultObject> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();

                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("Client_DelegateBuyStone Server Exception", e.Error);
                    return;
                }
                if (e.UserState == null)
                {
                    return;
                }
                PayType paytype = (PayType)e.UserState;

                App.StackStoneVMObject.AsyncGetAllNotFinishedBuyOrders();
                App.UserVMObject.AsyncGetPlayerInfo();

                if (e.Result.OperResultCode == OperResult.RESULTCODE_TRUE)
                {
                    if (paytype == PayType.Alipay)
                    {
                        MyWebPage.ShowMyWebPage(e.Result.Message);
                        MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");

                        if (!AlipayPaySucceed)
                        {
                            var payResult = MyMessageBox.ShowAlipayPayQuestion();
                            if (payResult == MessageBoxAlipayPayQuestionResult.Succeed)
                            {
                                App.UserVMObject.AsyncGetPlayerInfo();

                                //if (!AlipayPaySucceed)
                                //{
                                //    System.Windows.Forms.DialogResult result = MyMessageBox.ShowQuestionOKCancel("没有接收到支付宝付款信息。如确实付款，请点击【确定】，将对订单进行申诉，同时联系管理员进行处理，否则请点击【取消】。注意：三次恶意订单申诉，请被永久封号。");
                                //    if (result == System.Windows.Forms.DialogResult.OK)
                                //    {
                                //        App.StoneOrderVMObject.AsyncSetStoneOrderPayException(LockedOrder.OrderNumber);
                                //    }
                                //}
                            }
                            else if (payResult == MessageBoxAlipayPayQuestionResult.Failed)
                            {
                                MyWebPage.ShowMyWebPage(e.Result.Message);
                                MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");
                                return;
                            }
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowInfo("挂单成功");
                    }
                }
                else
                {
                    MyMessageBox.ShowInfo("挂单失败，原因为：" + OperResult.GetMsg(e.Result.OperResultCode));
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_DelegateBuyStone Exception", exc);
            }
        }

        private void btnCancelSellOrder_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StoneDelegateSellOrderInfoUIModel sellOrder = btn.DataContext as StoneDelegateSellOrderInfoUIModel;
            App.StackStoneVMObject.AsyncCancelDelegateSellStoneOrder(sellOrder.ParentObject);
        }

        private void btnCancelBuyOrder_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StoneDelegateBuyOrderInfoUIModel buyOrder = btn.DataContext as StoneDelegateBuyOrderInfoUIModel;
            App.StackStoneVMObject.AsyncCancelDelegateBuyStoneOrder(buyOrder.ParentObject);
        }

        private void btnPayOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                StoneDelegateBuyOrderInfoUIModel buyOrder = ((Button)sender).DataContext as StoneDelegateBuyOrderInfoUIModel;

                MyWebPage.ShowMyWebPage(buyOrder.AlipayLink);
                MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");

                if (!AlipayPaySucceed)
                {
                    var payResult = MyMessageBox.ShowAlipayPayQuestion();
                    if (payResult == MessageBoxAlipayPayQuestionResult.Succeed)
                    {
                        App.UserVMObject.AsyncGetPlayerInfo();

                    }
                    else if (payResult == MessageBoxAlipayPayQuestionResult.Failed)
                    {
                        MyWebPage.ShowMyWebPage(buyOrder.AlipayLink);
                        MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");
                        return;
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Alipay Pay DelegateBuyStone Order Exception", exc);
            }
        }

        private void sliderPrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.numPrice.Text = Math.Round((decimal)this.sliderPrice.Value, 2).ToString();
        }

        //private void tabTradeOper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (this.tabTradeOper.SelectedIndex == 1)
        //    {
        //        App.StackStoneVMObject.AsyncGetAllNotFinishedBuyOrders();
        //    }
        //    if (this.tabTradeOper.SelectedIndex == 2)
        //    {
        //        App.StackStoneVMObject.AsyncGetAllNotFinishedSellOrders();
        //    }
        //}

    }
}
