using MetaData.Shopping;
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
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
