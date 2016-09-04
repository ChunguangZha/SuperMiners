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
            this.txtMessage.Text = string.Format("请绑定正确的支付宝账户和真实姓名，否则您将无法提现。如需修改支付宝信息，请联系客服，修改一次需支付{0}矿石。", 50 * GlobalData.GameConfig.Yuan_RMB * GlobalData.GameConfig.Stones_RMB);
            this.txtUserName.Text = GlobalData.CurrentUser.UserName;
            this.txtNickName.Text = GlobalData.CurrentUser.NickName;
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

            GlobalData.Client.CheckUserAlipayExistCompleted += Client_CheckUserAlipayExistCompleted;
            GlobalData.Client.ChangePlayerSimpleInfoCompleted += Client_ChangePlayerSimpleInfoCompleted;
        }

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Client_CheckUserAlipayExistCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("修改失败。");
                return;
            }

            if (e.Result <= -2)
            {
                MyMessageBox.ShowInfo("支付宝账户或者真实姓名无效，请重新输入。");
                return;
            }
            if (e.Result == -1)
            {
                MyMessageBox.ShowInfo("与服务器通信异常，请稍候再试。");
                return;
            }
            if (e.Result > 0)
            {
                MyMessageBox.ShowInfo("支付宝账户已经被其它玩家绑定，请输入其它账户。");
                return;
            }

            AsyncChangePlayerSimpleInfo(nickName, alipay, alipayRealName, email, qq);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.CheckUserAlipayExistCompleted -= Client_CheckUserAlipayExistCompleted;
            GlobalData.Client.ChangePlayerSimpleInfoCompleted -= Client_ChangePlayerSimpleInfoCompleted;
        }

        void Client_ChangePlayerSimpleInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || !e.Result)
            {
                MyMessageBox.ShowInfo("修改失败。");
                return;
            }

            GlobalData.CurrentUser.ParentObject.SimpleInfo.Alipay = alipay;
            GlobalData.CurrentUser.ParentObject.SimpleInfo.AlipayRealName = alipayRealName;

            MyMessageBox.ShowInfo("修改成功。");

            _syn.Post(p =>
            {
                this.Close();
            }, null);
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow win = new ChangePasswordWindow();
            win.ShowDialog();
        }

        string nickName = "";
        string alipay = "";
        string alipayRealName = "";
        string email = "";
        string qq = "";

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            nickName = this.txtNickName.Text;
            if (string.IsNullOrEmpty(nickName))
            {
                MyMessageBox.ShowInfo("请填写昵称。");
                return;
            }

            email = this.txtEmail.Text;
            if (string.IsNullOrEmpty(email))
            {
                MyMessageBox.ShowInfo("请填写邮箱。");
                return;
            }
            qq = this.txtQQ.Text;
            if (string.IsNullOrEmpty(qq))
            {
                MyMessageBox.ShowInfo("请填写QQ。");
                return;
            }

            alipay = this.txtAlipayAccount.Text;
            if (string.IsNullOrEmpty(alipay))
            {
                MyMessageBox.ShowInfo("请填写支付宝账户。");
                return;
            }

            alipayRealName = this.txtAlipayRealName.Text;
            if (string.IsNullOrEmpty(alipayRealName))
            {
                MyMessageBox.ShowInfo("请填写支付宝实名认证的真实姓名。");
                return;
            }

            App.BusyToken.ShowBusyWindow("正在验证...");
            GlobalData.Client.CheckUserAlipayExist(alipay, alipayRealName, null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public void AsyncChangePlayerSimpleInfo(string nickName, string alipayAccount, string alipayRealName, string email, string qq)
        {
            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.ChangePlayerSimpleInfo(nickName, alipayAccount, alipayRealName, email, qq, new string[] { alipayAccount, alipayRealName });
        }

    }
}
