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
                NotifyPropertyChange("LastedNotice");
                NotifyPropertyChange("LastedNoticeTitle");
            }
        }

        public string LastedNoticeTitle
        {
            get
            {
                if (LastedNotice == null)
                {
                    return "";
                }

                return LastedNotice.Title;
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

            foreach (var item in e.Result)
            {
                this.ListNotices.Add(item);
            }

            this.LastedNotice = e.Result[e.Result.Length - 1];
        }

        void Client_OnSendNewNotice(string obj)
        {
            LastedNotice = new NoticeInfo() { Title = obj };

        }
    }
}
