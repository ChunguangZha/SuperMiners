using MetaData.Shopping;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using SuperMinersCustomServiceSystem.View.Windows;
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
    /// Interaction logic for DiamondShoppingItemListControl.xaml
    /// </summary>
    public partial class DiamondShoppingItemListControl : UserControl
    {
        public Dictionary<int, string> dicItemTypeItemsSource = new Dictionary<int, string>();

        public DiamondShoppingItemListControl()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.LiveThing, "生活用品");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.Digital, "数码产品");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.Food, "食品专区");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.HomeAppliances, "家用电器");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.PhoneFee, "话费充值");

            this.cmbItemType.ItemsSource = dicItemTypeItemsSource;
            this.cmbItemType.SelectedIndex = 0;

            this.dgRecords.ItemsSource = App.ShoppingVMObject.ListDiamondShoppingItems;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.ShoppingVMObject.AsyncGetDiamondShoppingItems((MetaData.Shopping.DiamondsShoppingItemType)this.cmbItemType.SelectedValue);
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            EditDiamondShoppingItemWindow win = new EditDiamondShoppingItemWindow((MetaData.Shopping.DiamondsShoppingItemType)this.cmbItemType.SelectedValue);
            win.ShowDialog();
        }

        private void btnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgRecords.SelectedItem == null)
            {
                MyMessageBox.ShowInfo("请选择要修改的商品");
                return;
            }

            EditDiamondShoppingItemWindow win = new EditDiamondShoppingItemWindow(this.dgRecords.SelectedItem as DiamondShoppingItemUIModel);
            win.ShowDialog();

        }
    }
}
