using SuperMinersCustomServiceSystem.Model;
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
            BindUI();
        }

        public void BindUI()
        {
            this.lvVirtualMall.ItemsSource = App.ShoppingVMObject.ListVirtualShoppingItem;
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
    }
}
