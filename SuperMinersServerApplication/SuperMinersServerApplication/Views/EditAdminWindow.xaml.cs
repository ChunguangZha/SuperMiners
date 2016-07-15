using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.UIModel;
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

namespace SuperMinersServerApplication.Views
{
    /// <summary>
    /// Interaction logic for EditAdminWindow.xaml
    /// </summary>
    public partial class EditAdminWindow : Window
    {
        AdminUIModel _admin = null;
        bool _isAdd;

        public EditAdminWindow(bool isAdd, AdminUIModel admin)
        {
            InitializeComponent();

            this._isAdd = isAdd;
            if (isAdd)
            {
                this.txtUserName.IsReadOnly = false;
                this.Title = "添加管理员";
            }
            else
            {
                this._admin = admin;
                this.txtUserName.Text = admin.UserName;
                this.txtMac.Text = admin.Mac;
                this.txtUserName.IsReadOnly = true;
                this.Title = "修改管理员";
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (txtLoginPassword.Password.Length < 6)
            {
                MessageBox.Show("密码至少6位");
                return;
            }
            if (txtLoginPassword.Password != txtConfirmLoginPassword.Password)
            {
                MessageBox.Show("两次登录密码不一至");
                return;
            }
            if (txtActionPassword.Password.Length < 6)
            {
                MessageBox.Show("密码至少6位");
                return;
            }
            if (txtActionPassword.Password != txtConfirmActionPassword.Password)
            {
                MessageBox.Show("两次操作密码不一至");
                return;
            }
            if (this.txtMac.Text.Trim() == "")
            {
                MessageBox.Show("请输入MAC地址");
                return;
            }
            if (this.txtMac.Text.Trim().Length != 17)
            {
                MessageBox.Show("MAC地址长度不对，请重新输入");
                return;
            }

            if (_isAdd)
            {
                bool isOK = AdminController.Instance.AddAdmin(this.txtUserName.Text.Trim(), this.txtLoginPassword.Password, this.txtActionPassword.Password, this.txtMac.Text);
                if (isOK)
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("添加管理员失败。");
                    return;
                }
            }
            else
            {
                this._admin.LoginPassword = this.txtLoginPassword.Password;
                this._admin.ActionPassword = this.txtActionPassword.Password;
                this._admin.Mac = this.txtMac.Text;

                bool isOK = AdminController.Instance.EditAdmin(this._admin);
                if (isOK)
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("修改管理员失败。");
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
