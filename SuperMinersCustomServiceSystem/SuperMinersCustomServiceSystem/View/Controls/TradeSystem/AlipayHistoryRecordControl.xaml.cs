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
    /// Interaction logic for AlipayHistoryRecordControl.xaml
    /// </summary>
    public partial class AlipayHistoryRecordControl : UserControl
    {
        public AlipayHistoryRecordControl()
        {
            InitializeComponent();
            BindUI();
        }

        private void BindUI()
        {
            if (this.txtSumAlipayYuan == null)
            {
                return;
            }

            this.datagrid.ItemsSource = App.AlipayRechargeVMObject.ListAllAlipayRecords;
            Binding bind = new Binding("SumListAllAlipayRecords_PayYuan")
            {
                Mode = BindingMode.OneWay
            };
            this.txtSumAlipayYuan.SetBinding(TextBox.TextProperty, bind); 
            
            bind = new Binding("SumListAllAlipayRecords_RMB")
            {
                Mode = BindingMode.OneWay
            };
            this.txtSumRMB.SetBinding(TextBox.TextProperty, bind);
            this.DataContext = App.AlipayRechargeVMObject;
        }

        public void SetBuyerUserName(string userName)
        {
            this.txtPlayerUserName.Text = userName;
            Search();
        }

        private void Search()
        {
            string orderNumber = this.txtOrderNumber.Text.Trim();
            string alipayOrderNumber = this.txtAlipayOrderNumber.Text.Trim();
            string buyerEmail = this.txtBuyerEmail.Text.Trim();
            string playerUserName = this.txtPlayerUserName.Text.Trim();
            MyDateTime beginPayTime = this.dpStartPayTime.ValueTime;
            MyDateTime endPayTime = this.dpEndPayTime.ValueTime;
            endPayTime.Hour = 23;
            endPayTime.Minute = 59;
            endPayTime.Second = 59;

            int pageIndex = (int)this.numPageIndex.Value;

            App.AlipayRechargeVMObject.AsyncGetAllAlipayRechargeRecords(orderNumber, alipayOrderNumber, buyerEmail, playerUserName, beginPayTime, endPayTime, GlobalData.PageItemsCount, pageIndex);
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
            if (App.AlipayRechargeVMObject.ListAllAlipayRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
