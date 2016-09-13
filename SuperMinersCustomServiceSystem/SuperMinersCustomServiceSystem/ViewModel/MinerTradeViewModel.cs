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
    public class MinerTradeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "矿工交易";
            }
        }

        private ObservableCollection<MinersBuyRecordUIModel> _listMinerBuyRecords = new ObservableCollection<MinersBuyRecordUIModel>();

        public ObservableCollection<MinersBuyRecordUIModel> ListMinerBuyRecords
        {
            get { return _listMinerBuyRecords; }
            set { _listMinerBuyRecords = value; }
        }


        public MinerTradeViewModel()
        {
            GlobalData.Client.GetBuyMinerFinishedRecordListCompleted += Client_GetBuyMinerFinishedRecordListCompleted;
        }

        void Client_GetBuyMinerFinishedRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.MinersBuyRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询矿工购买记录失败。" + e.Error.Message);
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
                MessageBox.Show("查询矿工购买记录回调处理异常。" + exc.Message);
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
    }
}
