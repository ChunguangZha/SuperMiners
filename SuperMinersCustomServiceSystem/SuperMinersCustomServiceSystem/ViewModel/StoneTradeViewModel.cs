using MetaData;
using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class StoneTradeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "矿石交易";
            }
        }

        private ObservableCollection<SellStonesOrderUIModel> _listSellStoneOrderRecords = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> ListSellStoneOrderRecords
        {
            get { return _listSellStoneOrderRecords; }
        }

        private ObservableCollection<BuyStonesOrderUIModel> _listBuyStoneOrderRecords = new ObservableCollection<BuyStonesOrderUIModel>();

        public ObservableCollection<BuyStonesOrderUIModel> ListBuyStoneOrderRecords
        {
            get { return _listBuyStoneOrderRecords; }
        }

        private ObservableCollection<LockSellStonesOrderUIModel> _listLockedStoneOrderRecords = new ObservableCollection<LockSellStonesOrderUIModel>();

        public ObservableCollection<LockSellStonesOrderUIModel> ListLockedStoneOrderRecords
        {
            get { return _listLockedStoneOrderRecords; }
        }


        public StoneTradeViewModel()
        {
            GlobalData.Client.GetBuyStonesOrderListCompleted += Client_GetBuyStonesOrderListCompleted;
            GlobalData.Client.GetSellStonesOrderListCompleted += Client_GetSellStonesOrderListCompleted;
            GlobalData.Client.GetLockedStonesOrderListCompleted += Client_GetLockedStonesOrderListCompleted;
        }

        void Client_GetLockedStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.LockSellStonesOrder[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询未完成的矿石订单失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListLockedStoneOrderRecords.Add(new LockSellStonesOrderUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询未完成的矿石订单回调处理异常。" + exc.Message);
            }
        }

        void Client_GetSellStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.SellStonesOrder[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询矿石出售订单失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListSellStoneOrderRecords.Add(new SellStonesOrderUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询矿石出售订单回调处理异常。" + exc.Message);
            }
        }

        void Client_GetBuyStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.BuyStonesOrder[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询矿石买入订单失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListBuyStoneOrderRecords.Add(new BuyStonesOrderUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询矿石买入订单回调处理异常。" + exc.Message);
            }
        }

        public void AsyncGetBuyStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询矿石买入订单...");
                ListBuyStoneOrderRecords.Clear();
                GlobalData.Client.GetBuyStonesOrderList(sellerUserName, orderNumber, buyUserName, orderState, myBeginCreateTime, myEndCreateTime, myBeginBuyTime, myEndBuyTime, pageItemCount, pageIndex);
            }
        }

        public void AsyncGetSellStonesOrderList(string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询矿石出售订单...");
                ListSellStoneOrderRecords.Clear();
                GlobalData.Client.GetSellStonesOrderList(sellerUserName, orderNumber, orderState, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
            }
        }

        public void AsyncGetLockedStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderState)
        {
            App.BusyToken.ShowBusyWindow("正在查询未完成的矿石订单");
            ListLockedStoneOrderRecords.Clear();
            GlobalData.Client.GetLockedStonesOrderList(sellerUserName, orderNumber, buyUserName, orderState);
        }
    }
}
