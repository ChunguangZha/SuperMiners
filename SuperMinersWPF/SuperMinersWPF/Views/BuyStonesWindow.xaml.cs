using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for BuyStonesWindow.xaml
    /// </summary>
    public partial class BuyStonesWindow : Window
    {
        private LockSellStonesOrderUIModel _lockedOrder;

        public LockSellStonesOrderUIModel LockedOrder
        {
            get { return _lockedOrder; }
            set { _lockedOrder = value; }
        }


        private bool AlipayPaySucceed = false;
        private System.Threading.SynchronizationContext _syn;


        public BuyStonesWindow(LockSellStonesOrderUIModel order)
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;
            this._lockedOrder = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //GlobalData.Client.ReleaseLockOrderCompleted += Client_ReleaseLockOrderCompleted;
            //GlobalData.Client.PayOrderByAlipayCompleted += Client_PayOrderByAlipayCompleted;
            App.StoneOrderVMObject.ReleaseLockOrderCompleted += StoneOrderVMObject_ReleaseLockOrderCompleted;
            App.StoneOrderVMObject.StoneOrderPaySucceed += StoneOrderVMObject_PayOrderSucceed;
            App.StoneOrderVMObject.SetStoneOrderExceptionFinished += StoneOrderVMObject_SetStoneOrderExceptionFinished;
            
            this.DataContext = this.LockedOrder;

            decimal awardGoldCoin = this.LockedOrder.ValueRMB * GlobalData.GameConfig.RMB_GoldCoin * GlobalData.GameConfig.StoneBuyerAwardGoldCoinMultiple;
            this.txtAwardGoldCoin.Text = ((int)awardGoldCoin).ToString();
            if (GlobalData.CurrentUser.RMB < this.LockedOrder.ValueRMB)
            {
                this.chkPayType.IsChecked = true;
                this.chkPayType.IsEnabled = false;
            }
            else
            {
                this.chkPayType.IsChecked = false;
                this.chkPayType.IsEnabled = true;
            }
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            //GlobalData.Client.ReleaseLockOrderCompleted -= Client_ReleaseLockOrderCompleted;
            App.StoneOrderVMObject.ReleaseLockOrderCompleted -= StoneOrderVMObject_ReleaseLockOrderCompleted;
            App.StoneOrderVMObject.StoneOrderPaySucceed -= StoneOrderVMObject_PayOrderSucceed;
            App.StoneOrderVMObject.SetStoneOrderExceptionFinished -= StoneOrderVMObject_SetStoneOrderExceptionFinished;
        }

        void StoneOrderVMObject_SetStoneOrderExceptionFinished(bool obj)
        {
            try
            {
                if (obj)
                {
                    App.UserVMObject.AsyncGetPlayerInfo();

                    _syn.Post(o =>
                    {
                        this.Close();
                    }, null);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买矿石，申诉订单回调处理异常。" + exc.Message);
            }
        }

        void StoneOrderVMObject_PayOrderSucceed()
        {
            try
            {
                App.UserVMObject.AsyncGetPlayerInfo();

                AlipayPaySucceed = true;
                this.Close();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买矿石，支付成功服务器回调处理异常。" + exc.Message);
            }
        }

        void StoneOrderVMObject_ReleaseLockOrderCompleted(bool isOK)
        {
            try
            {
                _syn.Post(o =>
                {
                    App.UserVMObject.AsyncGetPlayerInfo();

                    this.btnOK.IsEnabled = false;
                    this.Close();
                }, null);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买矿石，锁定订单超时处理异常。" + exc.Message);
            }
        }

        //void Client_ReleaseLockOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        //{
        //    try
        //    {
        //        if (e.Cancelled)
        //        {
        //            return;
        //        }

        //        App.BusyToken.CloseBusyWindow();

        //        if (e.Error != null)
        //        {
        //            _syn.Post(o =>
        //            {
        //                MyMessageBox.ShowInfo("连接服务器失败。");
        //            }, null);
        //            return;
        //        }

        //        App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();

        //        this.Close();
        //    }
        //    catch (Exception exc)
        //    {
        //        MyMessageBox.ShowInfo("购买矿石，取消购买矿石订单，回调处理异常。" + exc.Message);
        //    }
        //}

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkPayType.IsChecked == true)//支付宝支付
                {
                    MyWebPage.ShowMyWebPage(this.LockedOrder.PayUrl);
                    MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");

                    if (!AlipayPaySucceed)
                    {
                        var payResult = MyMessageBox.ShowAlipayPayQuestion();
                        if (payResult == MessageBoxAlipayPayQuestionResult.Succeed)
                        {
                            App.UserVMObject.AsyncGetPlayerInfo();

                            if (!AlipayPaySucceed)
                            {
                                System.Windows.Forms.DialogResult result = MyMessageBox.ShowQuestionOKCancel("没有接收到支付宝付款信息。如确实付款，请点击【确定】，将对订单进行申诉，同时联系管理员进行处理，否则请点击【取消】。注意：三次恶意订单申诉，请被永久封号。");
                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    App.StoneOrderVMObject.AsyncSetStoneOrderPayException(LockedOrder.OrderNumber);
                                }
                            }
                        }
                        else if (payResult == MessageBoxAlipayPayQuestionResult.Failed)
                        {
                            MyWebPage.ShowMyWebPage(this.LockedOrder.PayUrl);
                            MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");
                            return;
                        }
                    }
                }
                else
                {
                    App.StoneOrderVMObject.AsyncPayOrderByRMB(LockedOrder.OrderNumber, LockedOrder.ValueRMB);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买矿石，处理异常。" + exc.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.StoneOrderVMObject.AsyncCancelBuyStoneOrder(this._lockedOrder.OrderNumber);
        }

        private void chkPayType_Checked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "支付宝支付";
        }

        private void chkPayType_Unchecked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "灵币支付";
        }
    }
}
