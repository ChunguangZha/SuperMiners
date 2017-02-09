using MetaData;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for MySettingControl.xaml
    /// </summary>
    public partial class MySettingControl : UserControl
    {
        private SynchronizationContext _syn;

        public MySettingControl()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void Init()
        {
            if (GlobalData.GameConfig == null)
            {
                return;
            }
            this.txtMessage.Text = string.Format("请绑定正确的支付宝账户和真实姓名，否则您将无法提现。如需修改支付宝信息，请联系客服，修改一次需支付{0}矿石。", 50 * GlobalData.GameConfig.Yuan_RMB * GlobalData.GameConfig.Stones_RMB);
            this.txtUserName.Text = GlobalData.CurrentUser.UserName;
            this.txtNickName.Text = GlobalData.CurrentUser.NickName;
            this.txtAlipayAccount.Text = GlobalData.CurrentUser.Alipay;
            this.txtAlipayRealName.Text = GlobalData.CurrentUser.AlipayRealName;
            this.txtEmail.Text = GlobalData.CurrentUser.Email;
            this.txtQQ.Text = GlobalData.CurrentUser.QQ;
            this.txtIDCardNo.Text = GlobalData.CurrentUser.IDCardNo;

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

            //GlobalData.Client.CheckUserAlipayExistCompleted += Client_CheckUserAlipayExistCompleted;
            GlobalData.Client.ChangePlayerSimpleInfoCompleted += Client_ChangePlayerSimpleInfoCompleted;
        }

        void Client_ChangePlayerSimpleInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
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
                    MyMessageBox.ShowInfo("修改失败。原因为：" + e.Error);
                    return;
                }
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("修改成功。");

                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("修改失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow win = new ChangePasswordWindow();
            win.ShowDialog();
        }

        string nickName = "";
        string alipay = "";
        string alipayRealName = "";
        string IDCardNo = "";
        string email = "";
        string qq = "";

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            nickName = this.txtNickName.Text.Trim();
            if (string.IsNullOrEmpty(nickName))
            {
                MyMessageBox.ShowInfo("请填写昵称。");
                return;
            }

            email = this.txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MyMessageBox.ShowInfo("请填写邮箱。");
                return;
            }
            bool matchValue = Regex.IsMatch(email, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");
            if (!matchValue)
            {
                MyMessageBox.ShowInfo("请输入正确的邮箱");
                return;
            }

            qq = this.txtQQ.Text.Trim();
            if (string.IsNullOrEmpty(qq))
            {
                MyMessageBox.ShowInfo("请填写QQ。");
                return;
            }
            matchValue = Regex.IsMatch(qq, @"^([1-9][0-9]*)$");
            if (!matchValue)
            {
                MyMessageBox.ShowInfo("请输入正确的QQ号");
                return;
            }


            alipay = this.txtAlipayAccount.Text.Trim();
            if (string.IsNullOrEmpty(alipay))
            {
                MyMessageBox.ShowInfo("请填写支付宝账户。");
                return;
            }
            matchValue = Regex.IsMatch(alipay, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");
            if (!matchValue)
            {
                matchValue = Regex.IsMatch(alipay, @"^([1-9][0-9]*)$");
                if (!matchValue)
                {
                    MyMessageBox.ShowInfo("支付宝账户只能为电子邮箱或者手机号");
                    return;
                }
                else
                {
                    if (alipay.Length != 11)
                    {
                        MyMessageBox.ShowInfo("支付宝账户只能为电子邮箱或者手机号");
                        return;
                    }
                }
            }

            alipayRealName = this.txtAlipayRealName.Text.Trim();
            if (string.IsNullOrEmpty(alipayRealName))
            {
                MyMessageBox.ShowInfo("请填写支付宝实名认证的真实姓名。");
                return;
            }
            matchValue = Regex.IsMatch(alipayRealName, @"^[\u4E00-\u9FA5\uF900-\uFA2D]");
            if (!matchValue)
            {
                MyMessageBox.ShowInfo("支付宝实名只能为汉字");
                return;
            }

            IDCardNo = this.txtIDCardNo.Text.Trim();
            if (string.IsNullOrEmpty(IDCardNo))
            {
                MyMessageBox.ShowInfo("请填写身份证号。");
                return;
            }
            matchValue = Regex.IsMatch(IDCardNo, @"^([1-9][0-9]*X{0,1})$");
            if (!matchValue)
            {
                MyMessageBox.ShowInfo("请输入正确的身份证号");
                return;
            }
            else
            {
                if (IDCardNo.Length != 18)
                {
                    MyMessageBox.ShowInfo("请输入正确的身份证号");
                    return;
                }
            }

            AsyncChangePlayerSimpleInfo(nickName, alipay, alipayRealName, IDCardNo, email, qq);
            //App.BusyToken.ShowBusyWindow("正在验证...");
            //GlobalData.Client.CheckUserAlipayExist(alipay, alipayRealName, null);
        }

        public void AsyncChangePlayerSimpleInfo(string nickName, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq)
        {
            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.ChangePlayerSimpleInfo(nickName, alipayAccount, alipayRealName, IDCardNo, email, qq, new string[] { alipayAccount, alipayRealName });
        }

    }
}
