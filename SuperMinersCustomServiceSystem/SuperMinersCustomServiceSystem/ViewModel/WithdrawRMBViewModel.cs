using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class WithdrawRMBViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "灵币提现";
            }
        }

        private object LockActiveRecords = new object();
        public ObservableCollection<WithdrawRMBRecordUIModel> ListActiveWithdrawRecords = new ObservableCollection<WithdrawRMBRecordUIModel>();

        public ObservableCollection<WithdrawRMBRecordUIModel> ListHistoryWithdrawRecords = new ObservableCollection<WithdrawRMBRecordUIModel>();


        public WithdrawRMBViewModel()
        {
            GlobalData.Client.OnSomebodyWithdrawRMB += Client_OnSomebodyWithdrawRMB;
            ListActiveWithdrawRecords.CollectionChanged += ListActiveWithdrawRecords_CollectionChanged;
        }

        void ListActiveWithdrawRecords_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.ActiveItemsCount = this.ListActiveWithdrawRecords.Count;
        }

        void Client_OnSomebodyWithdrawRMB(WithdrawRMBRecord record)
        {
            lock (LockActiveRecords)
            {
                ListActiveWithdrawRecords.Add(new WithdrawRMBRecordUIModel(record));
            }
        }
        
        public void AsyncGetWithdrawRMBRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");

            }
        }

        public void RemoveRecordFromActiveRecords(WithdrawRMBRecordUIModel record)
        {
            lock (LockActiveRecords)
            {
                for (int i = 0; i < this.ListActiveWithdrawRecords.Count; i++)
                {
                    if (record.ID == this.ListActiveWithdrawRecords[i].ID)
                    {
                        this.ListActiveWithdrawRecords.RemoveAt(i);
                        break;
                    }
                }
            }
        }

    }
}
