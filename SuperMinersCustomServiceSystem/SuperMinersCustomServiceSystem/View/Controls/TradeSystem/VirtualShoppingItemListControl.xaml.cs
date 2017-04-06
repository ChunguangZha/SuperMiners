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

namespace SuperMinersCustomServiceSystem.View.Controls.TradeSystem
{
    /// <summary>
    /// Interaction logic for VirtualShoppingItemListControl.xaml
    /// </summary>
    public partial class VirtualShoppingItemListControl : UserControl
    {
        public VirtualShoppingItemListControl()
        {
            InitializeComponent();
            BindUI();
        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.ShoppingVMObject.ListVirtualShoppingItems
            };
            this.dgRecords.SetBinding(DataGrid.ItemsSourceProperty, bind);

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.ShoppingVMObject.AsyncGetAllVirtualShoppingItems();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgRecords.SelectedItem == null)
            {
                MessageBox.Show("请选择需要修改的虚拟商品");
                return;
            }
        }
    }
}
