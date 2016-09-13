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
    public class MineTradeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "矿山交易";
            }
        }

        private ObservableCollection<MinesBuyRecordUIModel> _listMineBuyRecords = new ObservableCollection<MinesBuyRecordUIModel>();

        public ObservableCollection<MinesBuyRecordUIModel> ListMineBuyRecords
        {
            get { return _listMineBuyRecords; }
            set { _listMineBuyRecords = value; }
        }


        public MineTradeViewModel()
        {
            GlobalData.Client.GetBuyMineFinishedRecordListCompleted += Client_GetBuyMineFinishedRecordListCompleted;
        }

        void Client_GetBuyMineFinishedRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.MinesBuyRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询矿山购买记录失败。" + e.Error.Message);
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
                MessageBox.Show("查询矿山购买记录回调处理异常。" + exc.Message);
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
    }
}
