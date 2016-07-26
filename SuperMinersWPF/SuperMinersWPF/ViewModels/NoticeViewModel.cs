using MetaData;
using SuperMinersWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class NoticeViewModel : BaseModel
    {
        private List<NoticeInfo> _listNotices = new List<NoticeInfo>();

        public List<NoticeInfo> ListNotices
        {
            get { return _listNotices; }
        }

        private NoticeInfo _lastedNotice;

        public NoticeInfo LastedNotice
        {
            get
            {
                return this._lastedNotice;
            }
            set
            {
                this._lastedNotice = value;

                if (LastNoticeChanged != null)
                {
                    LastNoticeChanged();
                }
            }
        }

        public void AsyncGetNewNotices()
        {
            GlobalData.Client.GetNotices(0, 0, 0, 0, 0, 0);
        }

        public void RegisterEvent()
        {
            GlobalData.Client.OnSendNewNotice += Client_OnSendNewNotice;
            GlobalData.Client.GetNoticesCompleted += Client_GetNoticesCompleted;
        }

        void Client_GetNoticesCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<NoticeInfo[]> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null || e.Result.Length == 0)
            {
                return;
            }

            this.ListNotices.Clear();

            var listOrdered = e.Result.OrderByDescending(n => n.Time);
            foreach (var item in listOrdered)
            {
                this.ListNotices.Add(item);
            }

            this.LastedNotice = e.Result[e.Result.Length - 1];
        }

        void Client_OnSendNewNotice(string obj)
        {
            this.AsyncGetNewNotices();
        }

        public event Action LastNoticeChanged;
    }
}
