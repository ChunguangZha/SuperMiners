using SuperMinersCustomServiceSystem.Uility;
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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for EditPlayerPasswordWindow.xaml
    /// </summary>
    public partial class EditPlayerPasswordWindow : Window
    {
        public string NewPassword = "";
        private string _userName;

        public EditPlayerPasswordWindow(string userName)
        {
            InitializeComponent();
            _userName = userName;
            this.txtUserName.Text = userName;
            GlobalData.Client.ChangePlayerPasswordCompleted += Client_ChangePlayerPasswordCompleted;
        }

        void Client_ChangePlayerPasswordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("修改玩家密码失败。原因为：" + e.Error);
                    return;
                }

                if (e.Result)
                {
                    MyMessageBox.ShowInfo("修改玩家密码成功");
                }
                else
                {
                    MyMessageBox.ShowInfo("修改玩家密码失败。");
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("修改玩家密码失败。原因为：" + exc);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.ChangePlayerPasswordCompleted -= Client_ChangePlayerPasswordCompleted;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtNewPassword.Password.Length < 6)
            {
                MessageBox.Show("请输入至少6位密码");
                return;
            }
            if (this.txtNewPassword.Password != this.txxtConfirmPassword.Password)
            {
                MessageBox.Show("两次密码不一至，请重新输入");
                return;
            }

            if (MyMessageBox.ShowQuestionOKCancel("请确认要修改玩家密码？") == System.Windows.Forms.DialogResult.OK)
            {
                this.NewPassword = this.txtNewPassword.Password;
                App.BusyToken.ShowBusyWindow("正在修改玩家密码");
                GlobalData.Client.ChangePlayerPassword(this._userName, NewPassword);
            }

        }
    }
}
