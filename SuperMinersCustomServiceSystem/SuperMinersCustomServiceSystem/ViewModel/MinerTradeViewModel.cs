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

        private decimal _sumListMinerBuyRecords_SpendGoldCoin;

        public decimal SumListMinerBuyRecords_SpendGoldCoin
        {
            get { return _sumListMinerBuyRecords_SpendGoldCoin; }
            set
            {
                _sumListMinerBuyRecords_SpendGoldCoin = value;
                NotifyPropertyChanged("SumListMinerBuyRecords_SpendGoldCoin");
            }
        }

        private decimal _sumListMinerBuyRecords_GotMiner;

        public decimal SumListMinerBuyRecords_GotMiner
        {
            get { return _sumListMinerBuyRecords_GotMiner; }
            set
            {
                _sumListMinerBuyRecords_GotMiner = value;
                NotifyPropertyChanged("SumListMinerBuyRecords_GotMiner");
            }
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

                this.ListMinerBuyRecords.Clear();
                decimal sumMiner = 0;
                decimal sumGoldCoin = 0;

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        sumMiner += item.GainMinersCount;
                        sumGoldCoin += item.SpendGoldCoin;
                        this.ListMinerBuyRecords.Add(new MinersBuyRecordUIModel(item));
                    }
                }

                this.SumListMinerBuyRecords_GotMiner = sumMiner;
                this.SumListMinerBuyRecords_SpendGoldCoin = sumGoldCoin;
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
