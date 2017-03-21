using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class StoneDelegateTradeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get { return "矿石委托交易"; }
        }

        private decimal _sumListBuyStoneOrderRecords_StoneHand;

        public decimal SumListBuyStoneOrderRecords_StoneHand
        {
            get { return _sumListBuyStoneOrderRecords_StoneHand; }
            set
            {
                _sumListBuyStoneOrderRecords_StoneHand = value;
                NotifyPropertyChanged("SumListBuyStoneOrderRecords_StoneHand");
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


        private decimal _sumListSellStoneOrderRecords_StoneHand;

        public decimal SumListSellStoneOrderRecords_StoneHand
        {
            get { return _sumListSellStoneOrderRecords_StoneHand; }
            set
            {
                _sumListSellStoneOrderRecords_StoneHand = value;
                NotifyPropertyChanged("SumListSellStoneOrderRecords_StoneHand");
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


        public StoneDelegateTradeViewModel()
        {
            RegisterEvents();
        }

        private ObservableCollection<StoneDelegateBuyOrderInfoUIModel> _listStoneDelegateBuyOrders = new ObservableCollection<StoneDelegateBuyOrderInfoUIModel>();

        public ObservableCollection<StoneDelegateBuyOrderInfoUIModel> ListStoneDelegateBuyOrders
        {
            get
            {
                return this._listStoneDelegateBuyOrders;
            }
        }
        
        private ObservableCollection<StoneDelegateSellOrderInfoUIModel> _listStoneDelegateSellOrders = new ObservableCollection<StoneDelegateSellOrderInfoUIModel>();
        public ObservableCollection<StoneDelegateSellOrderInfoUIModel> ListStoneDelegateSellOrders
        {
            get
            {
                return this._listStoneDelegateSellOrders;
            }
        }

        public void AsyncGetStoneDelegateSellOrderInfo(string sellerUserName, MyDateTime beginFinishedTime, MyDateTime endFinishedTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在加载矿石委托出售数据...");
            GlobalData.Client.GetStoneDelegateSellOrderInfo(sellerUserName, beginFinishedTime, endFinishedTime, pageItemCount, pageIndex);
        }

        public void AsyncGetStoneDelegateBuyOrderInfo(string buyerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在加载矿石委托购买数据...");
            GlobalData.Client.GetStoneDelegateBuyOrderInfo(buyerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }


        public void RegisterEvents()
        {
            GlobalData.Client.GetStoneDelegateBuyOrderInfoCompleted += Client_GetStoneDelegateBuyOrderInfoCompleted;
            GlobalData.Client.GetStoneDelegateSellOrderInfoCompleted += Client_GetStoneDelegateSellOrderInfoCompleted;
        }

        void Client_GetStoneDelegateSellOrderInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.StoneStack.StoneDelegateSellOrderInfo[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取委托矿石出售数据失败。");
                    return;
                }
                this.SumListSellStoneOrderRecords_RMB = 0;
                this.SumListSellStoneOrderRecords_StoneHand = 0;
                this.ListStoneDelegateSellOrders.Clear();

                if (e.Result == null)
                {
                    return;
                }

                foreach (var item in e.Result)
                {
                    this.ListStoneDelegateSellOrders.Add(new StoneDelegateSellOrderInfoUIModel(item));
                    if (item.SellUnit != null)
                    {
                        this.SumListSellStoneOrderRecords_RMB += item.FinishedStoneTradeHandCount * item.SellUnit.Price;
                    }
                    this.SumListSellStoneOrderRecords_StoneHand += item.FinishedStoneTradeHandCount;
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取委托矿石出售数据失败,服务器回调异常。信息为：" + exc.Message);
            }
        }

        void Client_GetStoneDelegateBuyOrderInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.StoneStack.StoneDelegateBuyOrderInfo[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取委托矿石购买数据失败。");
                    return;
                }
                this.SumListBuyStoneOrderRecords_RMB = 0;
                this.SumListBuyStoneOrderRecords_StoneHand = 0;
                this.ListStoneDelegateBuyOrders.Clear();
                if (e.Result == null)
                {
                    return;
                }

                foreach (var item in e.Result)
                {
                    this.ListStoneDelegateBuyOrders.Add(new StoneDelegateBuyOrderInfoUIModel(item));
                    if (item.BuyUnit != null)
                    {
                        this.SumListBuyStoneOrderRecords_RMB += item.FinishedStoneTradeHandCount * item.BuyUnit.Price;
                    }
                    this.SumListBuyStoneOrderRecords_StoneHand += item.FinishedStoneTradeHandCount;
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取委托矿石购买数据失败,服务器回调异常。信息为：" + exc.Message);
            }
        }


    }
}
