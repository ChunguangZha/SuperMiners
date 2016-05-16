using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private SynchronizationContext _syn;
        public ChangePasswordWindow()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.ChangePasswordCompleted += Client_ChangePasswordCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.ChangePasswordCompleted -= Client_ChangePasswordCompleted;
        }

        void Client_ChangePasswordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || !e.Result)
            {
                MyMessageBox.ShowInfo("密码修改失败。");
                return;
            }

            if (e.UserState != null)
            {
                string newPassword = Convert.ToString(e.UserState);
                GlobalData.CurrentUser.ParentObject.SimpleInfo.Password = newPassword;
            }

            MyMessageBox.ShowInfo("密码修改成功。");

            _syn.Post(p =>
            {
                this.DialogResult = true;
            }, null);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password != txtConfirmNewPassword.Password)
            {
                MyMessageBox.ShowInfo("两次输入密码不一致，请重新输入。");
                return;
            }

            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;

            GlobalData.Client.ChangePassword(oldPassword, newPassword, newPassword);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
