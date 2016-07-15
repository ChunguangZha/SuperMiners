using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public EditPlayerAlipayWindow(string username, string alipayAccount, string alipayRealName)
        {
            InitializeComponent();

            this.UserName = username;
            this.txtUserName.Text = username;
            this.txtAlipayAccount.Text = alipayAccount;
            this.txtAlipayRealName.Text = alipayRealName;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtAlipayAccount.Text == "")
            {
                MessageBox.Show("需要填写支付宝账户");
                return;
            }
            if (this.txtAlipayRealName.Text == "")
            {
                MessageBox.Show("需要填写支付宝真实姓名");
                return;
            }

            this.AlipayAccount = this.txtAlipayAccount.Text;
            this.AlipayRealName = this.txtAlipayRealName.Text;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
