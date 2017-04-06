using MetaData;
using MetaData.Trade;
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
    public class RemoteServiceViewModel :BaseViewModel
    {
        public override string MenuHeader
        {
            get { return "远程服务"; }
        }

        private int _sumListUserBuyRemoteServiceRecords_PayMoneyYuan;

        public int SumListUserBuyRemoteServiceRecords_PayMoneyYuan
        {
            get { return _sumListUserBuyRemoteServiceRecords_PayMoneyYuan; }
            set { _sumListUserBuyRemoteServiceRecords_PayMoneyYuan = value; }
        }

        private int _sumListUserBuyRemoteServiceRecords_GetShoppingCredits;

        public int SumListUserBuyRemoteServiceRecords_GetShoppingCredits
        {
            get { return _sumListUserBuyRemoteServiceRecords_GetShoppingCredits; }
            set { _sumListUserBuyRemoteServiceRecords_GetShoppingCredits = value; }
        }


        private ObservableCollection<UserRemoteServerBuyRecordUIModel> _listUserBuyRemoteServiceRecords = new ObservableCollection<UserRemoteServerBuyRecordUIModel>();

        public ObservableCollection<UserRemoteServerBuyRecordUIModel> ListUserBuyRemoteServiceRecords
        {
            get { return _listUserBuyRemoteServiceRecords; }
        }

        private ObservableCollection<UserRemoteHandleServiceRecordUIModel> _listUserRemoteHandleServiceRecords = new ObservableCollection<UserRemoteHandleServiceRecordUIModel>();

        public ObservableCollection<UserRemoteHandleServiceRecordUIModel> ListUserRemoteHandleServiceRecord
        {
            get { return _listUserRemoteHandleServiceRecords; }
        }
        

        public RemoteServiceViewModel()
        {
            GlobalData.Client.GetUserRemoteServerBuyRecordsCompleted += Client_GetUserRemoteServerBuyRecordsCompleted;
            GlobalData.Client.GetUserRemoteHandleServiceRecordsCompleted += Client_GetUserRemoteHandleServiceRecordsCompleted;
        }

        #region AsyncGetUserRemoteHandleServiceRecords

        public void AsyncGetUserRemoteHandleServiceRecords(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在查询数据...");
                GlobalData.Client.GetUserRemoteHandleServiceRecords(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
            }
        }

        void Client_GetUserRemoteHandleServiceRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<UserRemoteHandleServiceRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();

                this.ListUserRemoteHandleServiceRecord.Clear();

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("服务器返回异常。异常信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListUserRemoteHandleServiceRecord.Add(new UserRemoteHandleServiceRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("提交玩家远程服务处理信息失败，服务器回调异常。信息为：" + exc.Message);
            }
        }

        #endregion

        #region AsyncGetUserRemoteServerBuyRecords

        public void AsyncGetUserRemoteServerBuyRecords(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在查询数据...");
                GlobalData.Client.GetUserRemoteServerBuyRecords(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
            }
        }

        void Client_GetUserRemoteServerBuyRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.UserRemoteServerBuyRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();

                this.ListUserBuyRemoteServiceRecords.Clear();

                this.SumListUserBuyRemoteServiceRecords_GetShoppingCredits = 0;
                this.SumListUserBuyRemoteServiceRecords_PayMoneyYuan = 0;

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("服务器返回异常。异常信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListUserBuyRemoteServiceRecords.Add(new UserRemoteServerBuyRecordUIModel(item));
                        this.SumListUserBuyRemoteServiceRecords_GetShoppingCredits += item.GetShoppingCredits;
                        this.SumListUserBuyRemoteServiceRecords_PayMoneyYuan += item.PayMoneyYuan;
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("提交玩家远程服务处理信息失败，服务器回调异常。信息为：" + exc.Message);
            }
        }

        #endregion

    }
}
