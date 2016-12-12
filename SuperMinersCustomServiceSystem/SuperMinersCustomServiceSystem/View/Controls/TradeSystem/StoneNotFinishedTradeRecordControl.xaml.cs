using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
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
    /// Interaction logic for StoneNotFinishedTradeRecordControl.xaml
    /// </summary>
    public partial class StoneNotFinishedTradeRecordControl : UserControl
    {
        public StoneNotFinishedTradeRecordControl()
        {
            InitializeComponent();
            this.dgRecords.ItemsSource = App.StoneTradeVMObject.ListLockedStoneOrderRecords;
        }

        public void SetSellerUserName(string sellerUserName)
        {
            this.txtSellerUserName.Text = sellerUserName;
            Search();
        }

        public void SetBuyerUserName(string buyerUserName)
        {
            this.txtBuyerUserName.Text = buyerUserName;
            Search();
        }

        private void Search()
        {
            string sellerUserName = this.txtSellerUserName.Text.Trim();
            string orderNumber = this.txtOrderNumber.Text.Trim();
            string buyerUserName = this.txtBuyerUserName.Text.Trim();
            int orderState = 0;
            if (this.cmbOrderState.SelectedIndex == 1)
            {
                orderState = (int)SellOrderState.Lock;
            }
            else if (this.cmbOrderState.SelectedIndex == 2)
            {
                orderState = (int)SellOrderState.Exception;
            }
            
            int pageIndex = (int)this.numPageIndex.Value;

            App.StoneTradeVMObject.AsyncGetLockedStonesOrderList(sellerUserName, orderNumber, buyerUserName, orderState);
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
            if (App.StoneTradeVMObject.ListLockedStoneOrderRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }

        private void btnHandleExceptionStoneOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                LockSellStonesOrderUIModel lockedStoneOrder = btn.DataContext as LockSellStonesOrderUIModel;
                if (lockedStoneOrder != null)
                {
                    HandleExceptionStoneOrderWindow win = new HandleExceptionStoneOrderWindow(lockedStoneOrder);
                    if (win.ShowDialog() == true)
                    {
                        Search();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
