using SuperMinersServerApplication.UIModel;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class AdminController
    {
        #region Single

        private static AdminController _instance = new AdminController();

        internal static AdminController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        private ObservableCollection<AdminUIModel> _listAdmin = new ObservableCollection<AdminUIModel>();

        public ObservableCollection<AdminUIModel> ListAdmin
        {
            get { return this._listAdmin; }
        }

        public int GetCheckedItemsCount()
        {
            int count = 0;
            foreach (var item in ListAdmin)
            {
                if (item.IsChecked)
                {
                    count++;
                }
            }

            return count;
        }

        public AdminUIModel GetFirstCheckedAdmin()
        {
            foreach (var item in ListAdmin)
            {
                if (item.IsChecked)
                {
                    return item;
                }
            }

            return null;
        }

        public void GetAllAdmin()
        {
            try
            {
                var admins = DBProvider.AdminDBProvider.GetAllAdmin();
                this.ListAdmin.Clear();
                if (admins != null)
                {
                    foreach (var item in admins)
                    {
                        this.ListAdmin.Add(new AdminUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Get All Admin List Exception", exc);
            }
        }

        public bool AddAdmin(string adminUserName, string loginPassword, string actionPassword, string mac)
        {
            try
            {
                bool isOK = DBProvider.AdminDBProvider.AddAdmin(adminUserName, loginPassword, actionPassword, mac);
                if (isOK)
                {
                    this.ListAdmin.Add(new AdminUIModel(new MetaData.User.AdminInfo()
                    {
                        UserName = adminUserName,
                        LoginPassword = loginPassword,
                        ActionPassword = actionPassword,
                        Macs = new string[] { mac }
                    }));
                }

                return isOK;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Add Admin Exception", exc);
                return false;
            }
        }

        public bool EditAdmin(AdminUIModel admin)
        {
            try
            {
                return DBProvider.AdminDBProvider.EditAdmin(admin.UserName, admin.LoginPassword, admin.ActionPassword, admin.Mac);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Edit Admin Exception", exc);
                return false;
            }
        }

        public bool DeleteAdmin(string adminUserName)
        {
            try
            {
                return DBProvider.AdminDBProvider.DeleteAdmin(adminUserName);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Delete Admin Exception.", exc);
                return false;
            }
        }
    }
}
