using MetaData;
using MetaData.User;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
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

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for EditPostAddressWindow.xaml
    /// </summary>
    public partial class EditPostAddressWindow : Window
    {
        private PostAddressUIModel _oldAddress = null;
        private bool isAdd = true;

        public EditPostAddressWindow(PostAddressUIModel address)
        {
            InitializeComponent();
            this._oldAddress = new PostAddressUIModel(new MetaData.User.PostAddress()
            {
                ID = address.ParentObject.ID,
                City = address.City,
                County = address.County,
                DetailAddress = address.DetailAddress,
                PhoneNumber = address.PhoneNumber,
                Province = address.Province,
                ReceiverName = address.ReceiverName,
                UserID = address.ParentObject.UserID
            });
            this.Title = "修改地址";
            this.isAdd = false;
            this.DataContext = this._oldAddress;
            GlobalData.Client.UpdateAddressCompleted += Client_UpdateAddressCompleted;
        }

        public EditPostAddressWindow()
        {
            InitializeComponent();
            this._oldAddress = new PostAddressUIModel(new MetaData.User.PostAddress()
            {
                UserID =GlobalData.CurrentUser.ParentObject.SimpleInfo.UserID
            });
            this.Title = "添加新地址";
            this.isAdd = true;
            this.DataContext = this._oldAddress;
            GlobalData.Client.AddAddressCompleted += Client_AddAddressCompleted;
        }

        public void AsyncAddPostAddress(PostAddress address)
        {
            App.BusyToken.ShowBusyWindow("正在添加新地址...");
            GlobalData.Client.AddAddress(address);
        }

        public void AsyncUpdatePostAddress(PostAddress newAddress)
        {
            App.BusyToken.ShowBusyWindow("正在修改地址...");
            GlobalData.Client.UpdateAddress(newAddress);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this._oldAddress.ReceiverName))
            {
                MyMessageBox.ShowInfo("必须填写收件人！");
                return;
            }
            if (string.IsNullOrEmpty(this._oldAddress.PhoneNumber))
            {
                MyMessageBox.ShowInfo("必须填写联系电话！");
                return;
            }
            if (string.IsNullOrEmpty(this._oldAddress.DetailAddress))
            {
                MyMessageBox.ShowInfo("必须填写详细地址！");
                return;
            }

            if (isAdd)
            {
                this.AsyncAddPostAddress(this._oldAddress.ParentObject);
            }
            else
            {
                this.AsyncUpdatePostAddress(this._oldAddress.ParentObject);
            }
        }

        void Client_UpdateAddressCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("修改地址失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("修改地址成功");
                    App.UserVMObject.AsyncGetPostAddressList();
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("修改地址失败。原因为：" + OperResult.GetMsg(e.Result));
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("修改地址失败。原因为：" + exc.Message);
            }
        }

        void Client_AddAddressCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("添加地址失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("添加地址成功");
                    App.UserVMObject.AsyncGetPostAddressList();
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("添加地址失败。原因为：" + OperResult.GetMsg(e.Result));
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("添加地址失败。原因为：" + exc.Message);
            }
        }

    }
}
