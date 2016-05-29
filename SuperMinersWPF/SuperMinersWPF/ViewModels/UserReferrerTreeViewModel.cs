using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class UserReferrerTreeViewModel : BaseModel
    {
        private UserReferrerTreeItemUIModel _UpRefrerrer;
        
        /// <summary>
        /// 上线
        /// </summary>
        public UserReferrerTreeItemUIModel UpRefrerrer
        {
            get { return _UpRefrerrer; }
            set
            {
                _UpRefrerrer = value;
                NotifyPropertyChange("UpReferrerUserName");
            }
        }

        public string UpReferrerUserName
        {
            get
            {
                if (this.UpRefrerrer != null)
                {
                    return this.UpRefrerrer.UserName;
                }

                return "";
            }
        }

        private ObservableCollection<UserReferrerTreeItemUIModel> _listDownRefrerrerTree = new ObservableCollection<UserReferrerTreeItemUIModel>();

        /// <summary>
        /// 下线
        /// </summary>
        public ObservableCollection<UserReferrerTreeItemUIModel> ListDownRefrerrerTree
        {
            get { return _listDownRefrerrerTree; }
            set { _listDownRefrerrerTree = value; }
        }

        public int DownRefrerrerCount
        {
            get
            {
                if (this.ListDownRefrerrerTree == null)
                {
                    return 0;
                }

                return this.ListDownRefrerrerTree.Count;
            }
        }

        public void AsyncGetUserReferrerTree()
        {
            GlobalData.Client.GetUserReferrerTree(GlobalData.CurrentUser.UserName, null);
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetUserReferrerTreeCompleted += Client_GetUserReferrerTreeCompleted;
        }

        void Client_GetUserReferrerTreeCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.UserReferrerTreeItem[]> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MyMessageBox.ShowInfo("查询推荐信息失败。");
                return;
            }

            if (e.Result.Length == 0)
            {
                return;
            }

            this.UpRefrerrer = null;

            var up = e.Result.FirstOrDefault(u => u.Level < 0);
            if (up != null)
            {
                this.UpRefrerrer = new UserReferrerTreeItemUIModel(up);
            }

            this.ListDownRefrerrerTree.Clear();
            foreach (var item in e.Result.Where(u => u.Level > 0))
            {
                this.ListDownRefrerrerTree.Add(new UserReferrerTreeItemUIModel(item));
            }

            NotifyPropertyChange("DownRefrerrerCount");
        }

    }
}
