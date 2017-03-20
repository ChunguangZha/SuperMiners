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

        public StoneDelegateTradeViewModel()
        {
            RegisterEvents();
        }

        public ObservableCollection<StoneDelegateBuyOrderInfoUIModel> ListStoneDelegateBuyOrders = new ObservableCollection<StoneDelegateBuyOrderInfoUIModel>();

        public ObservableCollection<StoneDelegateSellOrderInfoUIModel> ListStoneDelegateSellOrders = new ObservableCollection<StoneDelegateSellOrderInfoUIModel>();

        public void AsyncGetStoneDelegateSellOrderInfo(MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在加载矿石委托出售数据...");
            GlobalData.Client.GetStoneDelegateSellOrderInfo(GlobalData.CurrentAdmin.UserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public void AsyncGetStoneDelegateBuyOrderInfo(MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在加载矿石委托购买数据...");
            GlobalData.Client.GetStoneDelegateBuyOrderInfo(GlobalData.CurrentAdmin.UserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
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

                this.ListStoneDelegateSellOrders.Clear();
                foreach (var item in e.Result)
                {
                    this.ListStoneDelegateSellOrders.Add(new StoneDelegateSellOrderInfoUIModel(item));
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

                this.ListStoneDelegateBuyOrders.Clear();
                foreach (var item in e.Result)
                {
                    this.ListStoneDelegateBuyOrders.Add(new StoneDelegateBuyOrderInfoUIModel(item));
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取委托矿石购买数据失败,服务器回调异常。信息为：" + exc.Message);
            }
        }


    }
}
