using MetaData;
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
    public class NoticeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NoticeInfo> _listAllNotices = new ObservableCollection<NoticeInfo>();

        public ObservableCollection<NoticeInfo> ListAllNotices
        {
            get { return this._listAllNotices; }
        }

        public void AsyncSaveNotice(NoticeInfo notice, bool isAdd)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.SaveNotice(notice, isAdd);
            }
        }

        public void AsyncGetAllNotice()
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在加载数据...");
                GlobalData.Client.GetNotices();
            }
        }
        
        public void RegisterEvents()
        {
            GlobalData.Client.SaveNoticeCompleted += Client_SaveNoticeCompleted;
            GlobalData.Client.GetNoticesCompleted += Client_GetNoticesCompleted;
        }

        void Client_GetNoticesCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<NoticeInfo[]> e)
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
                    MessageBox.Show("获取系统公告失败。");
                    return;
                }

                this.ListAllNotices.Clear();

                if (e.Result != null)
                {
                    var listOrdered = e.Result.OrderByDescending(n => n.Time);

                    foreach (var item in listOrdered)
                    {
                        this.ListAllNotices.Add(item);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询系统公告回调处理异常。" + exc.Message);
            }
        }

        void Client_SaveNoticeCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || !e.Result)
            {
                MessageBox.Show("保存系统公告失败。");
                return;
            }

            MessageBox.Show("保存系统公告成功。");

            if (SaveNoticeCompleted != null)
            {
                SaveNoticeCompleted(e.Result);
            }
            this.AsyncGetAllNotice();
        }

        public event Action<bool> SaveNoticeCompleted;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
