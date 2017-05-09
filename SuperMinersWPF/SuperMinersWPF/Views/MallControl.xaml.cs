using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using SuperMinersWPF.Views.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for MallControl.xaml
    /// </summary>
    public partial class MallControl : UserControl
    {
        public MallControl()
        {
            InitializeComponent();
            if (GlobalData.ServerType == ServerType.Server2)
            {
                this.lvPostAddress.ItemsSource = App.UserVMObject.ListPostAddress;
            }
            BindUI();
        }

        public void BindUI()
        {
            this.lvVirtualMall.ItemsSource = App.ShoppingVMObject.ListVirtualShoppingItem;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalData.ServerType == ServerType.Server2)
            {
                App.UserVMObject.AsyncGetPostAddressList();
            }

        }

        private void ButtonBuyVirtualShopping_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            VirtualShoppingItemUIModel shoppingItem = btn.DataContext as VirtualShoppingItemUIModel;
            if (shoppingItem == null)
            {
                MyMessageBox.ShowInfo("请选择要购买的商品");
                return;
            }

            App.ShoppingVMObject.AsyncBuyVirtualShoppingItem(shoppingItem.ParentObject);
        }

        private void btnDeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            if (this.lvPostAddress.SelectedItem == null)
            {
                MyMessageBox.ShowInfo("请选择要删除的地址");
                return;
            }

            if (MyMessageBox.ShowQuestionOKCancel("请确认要删除该地址？此操作不可恢复！") == System.Windows.Forms.DialogResult.OK)
            {
                PostAddressUIModel address = this.lvPostAddress.SelectedItem as PostAddressUIModel;
                if (address == null)
                {
                    MyMessageBox.ShowInfo("请选择要删除的地址");
                    return;
                }
                App.UserVMObject.AsyncDeletePostAddress(address.ParentObject.ID);
            }
        }

        private void btnUpdateAddress_Click(object sender, RoutedEventArgs e)
        {
            PostAddressUIModel address = this.lvPostAddress.SelectedItem as PostAddressUIModel;
            if (address == null)
            {
                MyMessageBox.ShowInfo("请选择要修改的地址");
                return;
            }

            EditPostAddressWindow win = new EditPostAddressWindow(address);
            win.ShowDialog();
        }

        private void btnAddNewAddress_Click(object sender, RoutedEventArgs e)
        {
            EditPostAddressWindow win = new EditPostAddressWindow();
            win.ShowDialog();
        }
    }
}
