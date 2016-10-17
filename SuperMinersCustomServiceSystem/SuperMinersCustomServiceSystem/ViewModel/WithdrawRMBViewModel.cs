using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
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

        private ObservableCollection<WithdrawRMBRecordUIModel> _listHistoryWithdrawRecords = new ObservableCollection<WithdrawRMBRecordUIModel>();

        public ObservableCollection<WithdrawRMBRecordUIModel> ListHistoryWithdrawRecords
        {
            get { return this._listHistoryWithdrawRecords; }
        }

        public WithdrawRMBViewModel()
        {
            GlobalData.Client.OnSomebodyWithdrawRMB += Client_OnSomebodyWithdrawRMB;
            GlobalData.Client.GetWithdrawRMBRecordListCompleted += Client_GetWithdrawRMBRecordListCompleted;
            ListActiveWithdrawRecords.CollectionChanged += ListActiveWithdrawRecords_CollectionChanged;
        }

        void Client_GetWithdrawRMBRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<WithdrawRMBRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询灵币提现记录失败。" + e.Error.Message);
                    return;
                }

                this.ListActiveWithdrawRecords.Clear();

                if (e.Result != null)
                {
                    string userState = e.UserState as string;
                    if (userState == "ACTIVE")
                    {
                        lock (LockActiveRecords)
                        {
                            foreach (var item in e.Result)
                            {
                                this.ListActiveWithdrawRecords.Add(new WithdrawRMBRecordUIModel(item));
                            }
                        }
                    }
                    else if (userState == "HISTORY")
                    {
                        foreach (var item in e.Result)
                        {
                            this.ListHistoryWithdrawRecords.Add(new WithdrawRMBRecordUIModel(item));
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询灵币提现记录回调处理异常。" + exc.Message);
            }
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
        
        public void AsyncGetWithdrawRMBRecordList(bool isPayed, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在查找数据...");
                ListHistoryWithdrawRecords.Clear();
                GlobalData.Client.GetWithdrawRMBRecordList(isPayed, playerUserName, beginCreateTime, endCreateTime, 
                    adminUserName, beginPayTime, endPayTime, pageItemCount, pageIndex, "HISTORY");
            }
        }

        public void AsyncGetWithdrawRMBActiveRecordList()
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在查找数据...");
                lock (this.LockActiveRecords)
                {
                    ListActiveWithdrawRecords.Clear();
                }
                GlobalData.Client.GetWithdrawRMBRecordList(false, "", null, null,
                "", null, null, 0, 0, "ACTIVE");
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
