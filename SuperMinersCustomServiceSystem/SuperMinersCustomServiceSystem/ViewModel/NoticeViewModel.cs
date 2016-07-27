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

        public void AsyncCreateNotice(NoticeInfo notice)
        {
            App.BusyToken.ShowBusyWindow("正在向服务器提交新的系统消息...");
            GlobalData.Client.CreateNotice(notice);
        }

        public void AsyncGetAllNotice()
        {
            App.BusyToken.ShowBusyWindow("正在加载历史系统消息...");
            GlobalData.Client.GetNotices();
        }
        
        public void RegisterEvents()
        {
            GlobalData.Client.CreateNoticeCompleted += Client_CreateNoticeCompleted;
            GlobalData.Client.GetNoticesCompleted += Client_GetNoticesCompleted;
        }

        void Client_GetNoticesCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<NoticeInfo[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("获取系统消息失败。");
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

        void Client_CreateNoticeCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || !e.Result)
            {
                MessageBox.Show("创建系统消息失败。");
                return;
            }

            MessageBox.Show("创建系统消息成功。");

            if (CreateNoticeCompleted != null)
            {
                CreateNoticeCompleted(e.Result);
            }
            this.AsyncGetAllNotice();
        }

        public event Action<bool> CreateNoticeCompleted;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
