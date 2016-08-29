using MetaData.Trade;
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

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for FillAlipayInfoWindow.xaml
    /// </summary>
    public partial class FillAlipayInfoWindow : Window
    {
        public AlipayRechargeRecord AlipayPayInfo = null;

        public FillAlipayInfoWindow(string buyerUserName, string orderNumber)
        {
            InitializeComponent();
            this.txtBuyerUserName.Text = buyerUserName;
            this.txtMyTradeNo.Text = orderNumber;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtAlipayTradeNo.Text == "")
            {
                MessageBox.Show("请填写支付宝订单号");
                return;
            }
            if (this.txtMyTradeNo.Text == "")
            {
                MessageBox.Show("请填写交易订单号");
                return;
            }
            if (this.txtAlipayAccount.Text == "")
            {
                MessageBox.Show("请填写支付账户");
                return;
            }
            if (this.numTotalFee.Value == 0)
            {
                MessageBox.Show("请输入支付金额（人民币元）");
                return;
            }
            if (this.dpPayTime.SelectedDate == null)
            {
                MessageBox.Show("请选择支付日期和时间");
                return;
            }
            if (this.numHour.Value == 0 && this.numMinute.Value == 0 && this.numSecond.Value == 0)
            {
                MessageBox.Show("请选择支付日期和时间");
                return;
            }

            DateTime selectedDate = this.dpPayTime.SelectedDate.Value;
            DateTime payTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day,
                (int)this.numHour.Value, (int)this.numMinute.Value, (int)this.numSecond.Value);

            AlipayPayInfo = new AlipayRechargeRecord()
            {
                out_trade_no = this.txtMyTradeNo.Text,
                alipay_trade_no = this.txtAlipayTradeNo.Text,
                buyer_email = this.txtAlipayAccount.Text,
                user_name = this.txtBuyerUserName.Text,
                total_fee = (decimal)this.numTotalFee.Value,
                value_rmb = (decimal)this.numValueRMB.Value,
                pay_time = payTime
            };

            this.DialogResult = true;
        }

        private void numTotalFee_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            decimal rmb = GlobalData.GameConfig.Yuan_RMB * (decimal)this.numTotalFee.Value;
            this.numValueRMB.Value = (double)rmb;
        }
    }
}
