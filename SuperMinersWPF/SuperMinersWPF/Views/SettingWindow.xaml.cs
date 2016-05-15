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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
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
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow win = new ChangePasswordWindow();
            win.ShowDialog();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string newAA = this.txtAlipayAccount.Text;
            string newAR = this.txtAlipayRealName.Text;
            if (newAA != "" && newAR != "")
            {
                if (newAA != GlobalData.CurrentUser.Alipay || newAR != GlobalData.CurrentUser.AlipayRealName)
                {

                }
            }
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
