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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private SynchronizationContext _syn;

        public SettingWindow()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtUserName.Text = GlobalData.CurrentUser.UserName;
            this.txtAlipayAccount.Text = GlobalData.CurrentUser.Alipay;
            this.txtAlipayRealName.Text = GlobalData.CurrentUser.AlipayRealName;
            
            if (string.IsNullOrEmpty(GlobalData.CurrentUser.Alipay))
            {
                this.txtAlipayAccount.IsReadOnly = false;
            }
            else
            {
                this.txtAlipayAccount.IsReadOnly = true;
            }

            if (string.IsNullOrEmpty(GlobalData.CurrentUser.AlipayRealName))
            {
                this.txtAlipayRealName.IsReadOnly = false;
            }
            else
            {
                this.txtAlipayRealName.IsReadOnly = true;
            }
            GlobalData.Client.ChangeAlipayCompleted += Client_ChangeAlipayCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.ChangeAlipayCompleted += Client_ChangeAlipayCompleted;
        }

        void Client_ChangeAlipayCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || !e.Result)
            {
                MyMessageBox.ShowInfo("修改失败。");
                return;
            }

            if (e.UserState != null)
            {
                string[] states = e.UserState as string[];
                if (states != null && states.Length == 2)
                {
                    GlobalData.CurrentUser.ParentObject.SimpleInfo.Alipay = states[0];
                    GlobalData.CurrentUser.ParentObject.SimpleInfo.AlipayRealName = states[1];
                }
            }

            MyMessageBox.ShowInfo("修改成功。");

            _syn.Post(p =>
            {
                this.DialogResult = true;
            }, null);
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow win = new ChangePasswordWindow();
            win.ShowDialog();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string nickName = this.txtNickName.Text;
            if (string.IsNullOrEmpty(nickName))
            {
                MyMessageBox.ShowInfo("请填写昵称。");
                return;
            }

            string newAA = this.txtAlipayAccount.Text;
            if (string.IsNullOrEmpty(newAA))
            {
                MyMessageBox.ShowInfo("请填写支付宝账户。");
                return;
            }

            string newAR = this.txtAlipayRealName.Text;
            if (string.IsNullOrEmpty(newAR))
            {
                MyMessageBox.ShowInfo("请填写支付宝实名认证的真实姓名。");
                return;
            }
            AsyncChangePlayerSimpleInfo(newAA, newAR);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public void AsyncChangePlayerSimpleInfo(string alipayAccount, string alipayRealName)
        {
            GlobalData.Client.ChangeAlipay(alipayAccount, alipayRealName, new string[] { alipayAccount, alipayRealName });
        }

    }
}
