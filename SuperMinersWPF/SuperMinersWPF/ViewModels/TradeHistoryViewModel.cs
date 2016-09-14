using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    public class TradeHistoryViewModel
    {
        private ObservableCollection<AlipayRechargeRecordUIModel> _listAllAlipayRecords = new ObservableCollection<AlipayRechargeRecordUIModel>();

        public ObservableCollection<AlipayRechargeRecordUIModel> ListAllAlipayRecords
        {
            get { return _listAllAlipayRecords; }
        }

        private ObservableCollection<GoldCoinRechargeRecordUIModel> _listGoldCoinRechargeRecords = new ObservableCollection<GoldCoinRechargeRecordUIModel>();

        public ObservableCollection<GoldCoinRechargeRecordUIModel> ListGoldCoinRechargeRecords
        {
            get { return _listGoldCoinRechargeRecords; }
        }

        private ObservableCollection<MinersBuyRecordUIModel> _listMinerBuyRecords = new ObservableCollection<MinersBuyRecordUIModel>();

        public ObservableCollection<MinersBuyRecordUIModel> ListMinerBuyRecords
        {
            get { return _listMinerBuyRecords; }
        }

        private ObservableCollection<MinesBuyRecordUIModel> _listMineBuyRecords = new ObservableCollection<MinesBuyRecordUIModel>();

        public ObservableCollection<MinesBuyRecordUIModel> ListMineBuyRecords
        {
            get { return _listMineBuyRecords; }
        }

        private ObservableCollection<WithdrawRMBRecordUIModel> _listHistoryWithdrawRecords = new ObservableCollection<WithdrawRMBRecordUIModel>();

        public ObservableCollection<WithdrawRMBRecordUIModel> ListHistoryWithdrawRecords
        {
            get { return this._listHistoryWithdrawRecords; }
        }

        public TradeHistoryViewModel()
        {
            GlobalData.Client.GetAllAlipayRechargeRecordsCompleted += Client_GetAllAlipayRechargeRecordsCompleted;
            GlobalData.Client.GetFinishedGoldCoinRechargeRecordListCompleted += Client_GetFinishedGoldCoinRechargeRecordListCompleted;
            GlobalData.Client.GetBuyMinerFinishedRecordListCompleted += Client_GetBuyMinerFinishedRecordListCompleted;
            GlobalData.Client.GetBuyMineFinishedRecordListCompleted += Client_GetBuyMineFinishedRecordListCompleted;
            GlobalData.Client.GetWithdrawRMBRecordListCompleted += Client_GetWithdrawRMBRecordListCompleted;

        }

        void Client_GetAllAlipayRechargeRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<AlipayRechargeRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询支付宝付款记录失败。" + e.Error);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListAllAlipayRecords.Add(new AlipayRechargeRecordUIModel(item));
                    }
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("处理支付宝订单回调异常。" + exc.Message);
            }
        }

        void Client_GetFinishedGoldCoinRechargeRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.GoldCoinRechargeRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询金币充值记录失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListGoldCoinRechargeRecords.Add(new GoldCoinRechargeRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询金币充值记录回调处理异常。" + exc.Message);
            }
        }

        void Client_GetBuyMinerFinishedRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.MinersBuyRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询矿工购买记录失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListMinerBuyRecords.Add(new MinersBuyRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询矿工购买记录回调处理异常。" + exc.Message);
            }
        }

        void Client_GetBuyMineFinishedRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.MinesBuyRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询矿山购买记录失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListMineBuyRecords.Add(new MinesBuyRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询矿山购买记录回调处理异常。" + exc.Message);
            }
        }

        void Client_GetWithdrawRMBRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<WithdrawRMBRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询灵币提现记录失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListHistoryWithdrawRecords.Add(new WithdrawRMBRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询灵币提现记录回调处理异常。" + exc.Message);
            }
        }

        public void AsyncGetWithdrawRMBRecordList(bool isPayed, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查找数据...");
                ListHistoryWithdrawRecords.Clear();
                GlobalData.Client.GetWithdrawRMBRecordList(isPayed, playerUserName, beginCreateTime, endCreateTime, adminUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);
            }
        }

        public void AsyncGetBuyMineFinishedRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询矿山购买记录...");
                ListMineBuyRecords.Clear();
                GlobalData.Client.GetBuyMineFinishedRecordList(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
            }
        }

        public void AsyncGetBuyMinerFinishedRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询矿工购买记录...");
                ListMinerBuyRecords.Clear();
                GlobalData.Client.GetBuyMinerFinishedRecordList(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
            }
        }

        public void AsyncGetGoldCoinRechargeFinishedRecords(string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询金币充值记录...");
                this.ListGoldCoinRechargeRecords.Clear();
                GlobalData.Client.GetFinishedGoldCoinRechargeRecordList(playerUserName, orderNumber, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
            }
        }

        public void AsyncGetAllAlipayRechargeRecords(string orderNumber, string alipayOrderNumber, string payEmail, string playerUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询支付宝付款记录...");
                this.ListAllAlipayRecords.Clear();
                GlobalData.Client.GetAllAlipayRechargeRecords(orderNumber, alipayOrderNumber, payEmail, playerUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);

            }
        }
    }
}
