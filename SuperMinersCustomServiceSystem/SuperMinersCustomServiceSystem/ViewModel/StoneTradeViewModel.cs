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

        private decimal _sumListSellStoneOrderRecords_Stone;

        public decimal SumListSellStoneOrderRecords_Stone
        {
            get { return _sumListSellStoneOrderRecords_Stone; }
            set
            {
                _sumListSellStoneOrderRecords_Stone = value;
                NotifyPropertyChanged("SumListSellStoneOrderRecords_Stone");
            }
        }

        private decimal _sumListSellStoneOrderRecords_RMB;

        public decimal SumListSellStoneOrderRecords_RMB
        {
            get { return _sumListSellStoneOrderRecords_RMB; }
            set
            {
                _sumListSellStoneOrderRecords_RMB = value;
                NotifyPropertyChanged("SumListSellStoneOrderRecords_RMB");
            }
        }

        private decimal _sumListSellStoneOrderRecords_Fee;

        public decimal SumListSellStoneOrderRecords_Fee
        {
            get { return _sumListSellStoneOrderRecords_Fee; }
            set
            {
                _sumListSellStoneOrderRecords_Fee = value;
                NotifyPropertyChanged("SumListSellStoneOrderRecords_Fee");
            }
        }

        private ObservableCollection<BuyStonesOrderUIModel> _listBuyStoneOrderRecords = new ObservableCollection<BuyStonesOrderUIModel>();

        public ObservableCollection<BuyStonesOrderUIModel> ListBuyStoneOrderRecords
        {
            get { return _listBuyStoneOrderRecords; }
        }

        private decimal _sumListBuyStoneOrderRecords_Stone;

        public decimal SumListBuyStoneOrderRecords_Stone
        {
            get { return _sumListBuyStoneOrderRecords_Stone; }
            set
            {
                _sumListBuyStoneOrderRecords_Stone = value;
                NotifyPropertyChanged("SumListBuyStoneOrderRecords_Stone");
            }
        }

        private decimal _sumListBuyStoneOrderRecords_RMB;

        public decimal SumListBuyStoneOrderRecords_RMB
        {
            get { return _sumListBuyStoneOrderRecords_RMB; }
            set
            {
                _sumListBuyStoneOrderRecords_RMB = value;
                NotifyPropertyChanged("SumListBuyStoneOrderRecords_RMB");
            }
        }

        private decimal _sumListBuyStoneOrderRecords_Fee;

        public decimal SumListBuyStoneOrderRecords_Fee
        {
            get { return _sumListBuyStoneOrderRecords_Fee; }
            set
            {
                _sumListBuyStoneOrderRecords_Fee = value;
                NotifyPropertyChanged("SumListBuyStoneOrderRecords_Fee");
            }
        }

        private decimal _sumListBuyStoneOrderRecords_AwardGoldCoin;

        public decimal SumListBuyStoneOrderRecords_AwardGoldCoin
        {
            get { return _sumListBuyStoneOrderRecords_AwardGoldCoin; }
            set
            {
                _sumListBuyStoneOrderRecords_AwardGoldCoin = value;
                NotifyPropertyChanged("SumListBuyStoneOrderRecords_AwardGoldCoin");
            }
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

                this.ListLockedStoneOrderRecords.Clear();
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

                this.ListSellStoneOrderRecords.Clear();
                decimal sumStone = 0;
                decimal sumRMB = 0;
                decimal sumFee = 0;


                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        sumStone += item.SellStonesCount;
                        sumRMB += item.ValueRMB;
                        sumFee += item.Expense;
                        ListSellStoneOrderRecords.Add(new SellStonesOrderUIModel(item));
                    }
                }

                this.SumListSellStoneOrderRecords_Fee = sumFee;
                this.SumListSellStoneOrderRecords_RMB = sumRMB;
                this.SumListSellStoneOrderRecords_Stone = sumStone;
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

                this.ListBuyStoneOrderRecords.Clear();
                decimal sumStone = 0;
                decimal sumRMB = 0;
                decimal sumFee = 0;
                decimal sumGoldCoin = 0;

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        sumStone += item.StonesOrder.SellStonesCount;
                        sumRMB += item.StonesOrder.ValueRMB;
                        sumFee += item.StonesOrder.Expense;
                        sumGoldCoin += item.AwardGoldCoin;
                        ListBuyStoneOrderRecords.Add(new BuyStonesOrderUIModel(item));
                    }
                }

                this.SumListBuyStoneOrderRecords_AwardGoldCoin = sumGoldCoin;
                this.SumListBuyStoneOrderRecords_Fee = sumFee;
                this.SumListBuyStoneOrderRecords_RMB = sumRMB;
                this.SumListBuyStoneOrderRecords_Stone = sumStone;
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
