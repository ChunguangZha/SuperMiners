using MetaData;
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
    /// Interaction logic for VirtualShoppingBuyRecordControl.xaml
    /// </summary>
    public partial class VirtualShoppingBuyRecordControl : UserControl
    {
        public VirtualShoppingBuyRecordControl()
        {
            InitializeComponent();
            BindUI();
        }

        private void BindUI()
        {
            this.dgRecords.ItemsSource = App.ShoppingVMObject.ListVirtualShoppingBuyRecords;
        }

        public void SetBuyerUserName(string buyerUserName)
        {
            this.txtBuyUserName.Text = buyerUserName;
            Search();
        }

        private void Search()
        {
            string buyerUserName = this.txtBuyUserName.Text.Trim();
            string shoppingName = this.txtShoppingName.Text.Trim();

            MyDateTime beginPayTime = this.dpStartPayTime.ValueTime;
            MyDateTime endPayTime = this.dpEndPayTime.ValueTime;

            int pageIndex = (int)this.numPageIndex.Value;

            App.ShoppingVMObject.AsyncGetPlayerBuyVirtualShoppingItemRecord(buyerUserName, shoppingName, beginPayTime, endPayTime, GlobalData.PageItemsCount, pageIndex);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.numPageIndex.Value > 1)
            {
                this.numPageIndex.Value = this.numPageIndex.Value - 1;
                Search();
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (App.StoneTradeVMObject.ListBuyStoneOrderRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
