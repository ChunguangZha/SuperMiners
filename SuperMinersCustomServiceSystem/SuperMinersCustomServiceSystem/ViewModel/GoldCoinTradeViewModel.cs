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
    public class GoldCoinTradeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "金币充值";
            }
        }

        private ObservableCollection<GoldCoinRechargeRecordUIModel> _listGoldCoinRechargeRecords = new ObservableCollection<GoldCoinRechargeRecordUIModel>();

        public ObservableCollection<GoldCoinRechargeRecordUIModel> ListGoldCoinRechargeRecords
        {
            get { return _listGoldCoinRechargeRecords; }
            set { _listGoldCoinRechargeRecords = value; }
        }

        public GoldCoinTradeViewModel()
        {
            GlobalData.Client.GetFinishedGoldCoinRechargeRecordListCompleted += Client_GetFinishedGoldCoinRechargeRecordListCompleted;
        }

        void Client_GetFinishedGoldCoinRechargeRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.GoldCoinRechargeRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询金币充值记录失败。" + e.Error.Message);
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
                MessageBox.Show("查询金币充值记录回调处理异常。" + exc.Message);
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
    }
}
