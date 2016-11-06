using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for EditPlayerAlipayWindow.xaml
    /// </summary>
    public partial class EditPlayerAlipayWindow : Window
    {
        public string UserName { get; private set; }

        public string AlipayAccount { get; private set; }

        public string AlipayRealName { get; private set; }

        public string NewIDCardNo { get; private set; }

        public EditPlayerAlipayWindow(string username, string alipayAccount, string alipayRealName, string IDCardNo)
        {
            InitializeComponent();

            this.UserName = username;
            this.txtUserName.Text = username;
            this.txtAlipayAccount.Text = alipayAccount;
            this.txtAlipayRealName.Text = alipayRealName;
            this.txtIDCardNo.Text = IDCardNo;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string alipay = "";
            string alipayRealName = "";
            string IDCardNo = "";

            alipay = this.txtAlipayAccount.Text.Trim();
            if (string.IsNullOrEmpty(alipay))
            {
                MyMessageBox.ShowInfo("需要填写支付宝账户。");
                return;
            }
            bool matchValue = Regex.IsMatch(alipay, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");
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
                MessageBox.Show("需要填写支付宝真实姓名");
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
                MessageBox.Show("需要填写身份证号");
                return;
            }
            matchValue = Regex.IsMatch(IDCardNo, @"^([1-9][0-9]*)$");
            if (!matchValue)
            {
                MyMessageBox.ShowInfo("身份证号必须为18位数字");
                return;
            }
            else
            {
                if (IDCardNo.Length != 18)
                {
                    MyMessageBox.ShowInfo("身份证号必须为18位数字");
                    return;
                }
            }

            this.AlipayAccount = alipay;
            this.AlipayRealName = alipayRealName;
            this.NewIDCardNo = IDCardNo;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
