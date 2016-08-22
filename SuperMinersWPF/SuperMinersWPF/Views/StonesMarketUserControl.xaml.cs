using MetaData;
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
    /// Interaction logic for StonesMarketUserControl.xaml
    /// </summary>
    public partial class StonesMarketUserControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;

        public StonesMarketUserControl()
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.listboxMyBuyOrders.ItemsSource = App.StoneOrderVMObject.MyBuyNotFinishedStoneOrders;
            this.listboxMySellOrders.ItemsSource = App.StoneOrderVMObject.MySellNotFinishedStoneOrders;
            this.listboxAllSellOrders.ItemsSource = App.StoneOrderVMObject.AllNotFinishStoneOrder;
            App.StoneOrderVMObject.AsyncGetOrderLockedBySelf();
            App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();
            App.StoneOrderVMObject.StoneOrderLockSucceed += StoneOrderVMObject_LockOrderSucceed;
            
            Binding bind = new Binding()
            {
                Source = App.StoneOrderVMObject
            };
            this.expBuyOrders.SetBinding(Expander.DataContextProperty, bind);

            this.dtpickerBegin.SelectedDate = DateTime.Now.AddDays(-7);
            this.dtpickerEnd.SelectedDate = DateTime.Now;

            GlobalData.Client.SearchUserBuyStoneOrdersCompleted += Client_SearchUserBuyStoneOrdersCompleted;
            GlobalData.Client.SearchUserSellStoneOrdersCompleted += Client_SearchUserSellStoneOrdersCompleted;
        }

        void StoneOrderVMObject_LockOrderSucceed(LockSellStonesOrderUIModel obj)
        {
            this._syn.Post(o =>
            {
                BuyStonesWindow win = new BuyStonesWindow(obj);
                if (win.ShowDialog() == true)
                {

                }
                else
                {

                }
            }, null);
        }

        private void numBuyStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void btnAutoMatchOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.numBuyStones.Value < 10)
            {
                MyMessageBox.ShowInfo("至少要购买10块矿石");
                return;
            }
            if (App.StoneOrderVMObject.GetFirstLockedStoneOrder() != null)
            {
                MyMessageBox.ShowInfo("您有未支付的订单，请先支付，再继续购买。");
                return;
            }
            App.StoneOrderVMObject.AsyncAutoMatchStonesOrder((int)this.numBuyStones.Value);
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            //支付宝支付
            var lockedOrder = App.StoneOrderVMObject.GetFirstLockedStoneOrder();
            if (lockedOrder != null)
            {
                MyWebPage.ShowMyWebPage(lockedOrder.PayUrl);
            }
        }

        private void btnAppeal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbTradeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.controlBuyOrderList == null)
            {
                return;
            }

            if (this.cmbTradeType.SelectedIndex == 0)
            {
                this.controlBuyOrderList.Visibility = System.Windows.Visibility.Visible;
                this.controlSellOrderList.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.controlBuyOrderList.Visibility = System.Windows.Visibility.Collapsed;
                this.controlSellOrderList.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnLastWeek_Click(object sender, RoutedEventArgs e)
        {
            this.dtpickerBegin.SelectedDate = DateTime.Now.AddDays(-7);
            this.dtpickerEnd.SelectedDate = DateTime.Now;
        }

        private void btnLastMonth_Click(object sender, RoutedEventArgs e)
        {
            this.dtpickerBegin.SelectedDate = DateTime.Now.AddDays(-30);
            this.dtpickerEnd.SelectedDate = DateTime.Now;
        }

        private void dtpickerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtpickerBegin.SelectedDate == null)
            {
                MyMessageBox.ShowInfo("请设置起始时间");
                return;
            }
            DateTime timeBegin = this.dtpickerBegin.SelectedDate.Value;
            if (this.dtpickerEnd.SelectedDate == null)
            {
                MyMessageBox.ShowInfo("请设置截止时间");
                return;
            }
            DateTime timeEnd = this.dtpickerEnd.SelectedDate.Value;
            if (timeBegin >= timeEnd)
            {
                MyMessageBox.ShowInfo("起始时间必须小于截止时间");
                return;
            }

            MyDateTime myBeginTime = MyDateTime.FromDateTime(timeBegin);
            MyDateTime myEndTime = MyDateTime.FromDateTime(timeEnd);

            App.BusyToken.ShowBusyWindow("正在查询订单...");
            if (this.cmbTradeType.SelectedIndex == 0)
            {
                GlobalData.Client.SearchUserBuyStoneOrders(myBeginTime, myEndTime, null);
            }
            else
            {
                GlobalData.Client.SearchUserSellStoneOrders(myBeginTime, myEndTime, null);
            }
        }

        void Client_SearchUserSellStoneOrdersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.SellStonesOrder[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("连接服务器失败。");
                LogHelper.Instance.AddErrorLog("Client_SearchUserSellStoneOrdersCompleted Exception。", e.Error);
                return;
            }

            List<SellStonesOrderUIModel> listSellOrder = new List<SellStonesOrderUIModel>();
                
            if (e.Result != null)
            {
                var listOrderTimeASC = e.Result.OrderBy(s => s.SellTime);
                foreach (var item in listOrderTimeASC)
                {
                    listSellOrder.Add(new SellStonesOrderUIModel(item));
                }
            }

            this.controlSellOrderList.ListSellStonesOrder = listSellOrder;
        }

        void Client_SearchUserBuyStoneOrdersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.BuyStonesOrder[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("连接服务器失败。");
                LogHelper.Instance.AddErrorLog("Client_SearchUserBuyStoneOrdersCompleted Exception。", e.Error);
                return;
            }

            List<BuyStonesOrderUIModel> listBuyOrder = new List<BuyStonesOrderUIModel>();

            if (e.Result != null)
            {
                var listOrderTimeASC = e.Result.OrderBy(s => s.BuyTime);
                foreach (var item in listOrderTimeASC)
                {
                    listBuyOrder.Add(new BuyStonesOrderUIModel(item));
                }
            }
            this.controlBuyOrderList.ListBuyStonesOrder = listBuyOrder;
        }

        private void btnRefreshOrderList_Click(object sender, RoutedEventArgs e)
        {
            App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Button btnBuy = sender as Button;
            SellStonesOrderUIModel stoneOrder = btnBuy.DataContext as SellStonesOrderUIModel;
            if (stoneOrder != null)
            {
                App.StoneOrderVMObject.AsyncLockStoneOrder(stoneOrder.OrderNumber);
            }
        }

    }
}
