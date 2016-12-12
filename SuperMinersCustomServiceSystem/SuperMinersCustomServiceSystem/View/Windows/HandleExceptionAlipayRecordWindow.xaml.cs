using MetaData;
using SuperMinersCustomServiceSystem.Model;
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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for HandleExceptionAlipayRecordWindow.xaml
    /// </summary>
    public partial class HandleExceptionAlipayRecordWindow : Window
    {
        private AlipayRechargeRecordUIModel _alipayRecord = null;

        public HandleExceptionAlipayRecordWindow(AlipayRechargeRecordUIModel alipayRecord)
        {
            InitializeComponent();
            _alipayRecord = alipayRecord;

            try
            {
                this.txtAlipayOrderNumber.Text = alipayRecord.alipay_trade_no;
                this.txtBuyerEmail.Text = alipayRecord.buyer_email;
                this.txtBuyerEmail.IsReadOnly = true;
                this.txtBuyerUserName.Text = alipayRecord.user_name;
                this.txtBuyerUserName.IsReadOnly = true;
                this.txtOrderNumber.Text = alipayRecord.out_trade_no;
                this.txtOrderNumber.IsReadOnly = true;
                this.txtOrderType.Text = alipayRecord.TradeTypeText;
                this.numTotalFee.Value = (double)alipayRecord.total_fee;
                this.numTotalFee.IsReadOnly = true;
                //this.numValueRMB.Value = (double)alipayRecord.value_rmb;
                this.mydpPayTime.ValueTime = MyDateTime.FromDateTime(alipayRecord.pay_time);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public HandleExceptionAlipayRecordWindow()
        {
            InitializeComponent();
            _alipayRecord = new AlipayRechargeRecordUIModel(new MetaData.Trade.AlipayRechargeRecord());
        }

        private void txtOrderNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.txtOrderType.Text = AlipayRechargeRecordUIModel.GetTradeTypeText(this.txtOrderNumber.Text.Trim());
            }
            catch (Exception)
            {
            }
        }

        private void numTotalFee_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                this.numValueRMB.Value = (this.numTotalFee.Value * (double)GlobalData.GameConfig.Yuan_RMB);
            }
            catch (Exception)
            {
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string orderNumber = this.txtOrderNumber.Text.Trim();
            string alipayOrderNumber = this.txtAlipayOrderNumber.Text.Trim();
            string buyEmail = this.txtBuyerEmail.Text.Trim();
            string buyUserName = this.txtBuyerUserName.Text.Trim();
            double totalfee = this.numTotalFee.Value;

            if (orderNumber == "")
            {
                MessageBox.Show("请输入商品订单号");
                return;
            }
            if (alipayOrderNumber == "")
            {
                MessageBox.Show("请输入支付宝订单号");
                return;
            }
            if (buyEmail == "")
            {
                MessageBox.Show("请输入支付账户");
                return;
            }
            if (buyUserName == "")
            {
                MessageBox.Show("请输入支付玩家用户名");
                return;
            }
            if (totalfee == 0)
            {
                MessageBox.Show("请输入支付人民币");
                return;
            }
            if (this.mydpPayTime.ValueTime.IsNull)
            {
                MessageBox.Show("请输入支付时间");
                return;
            }

            this._alipayRecord.alipay_trade_no = alipayOrderNumber;
            this._alipayRecord.buyer_email = buyEmail;
            this._alipayRecord.out_trade_no = orderNumber;
            this._alipayRecord.pay_time = this.mydpPayTime.ValueTime.ToDateTime();
            this._alipayRecord.total_fee = (decimal)totalfee;
            this._alipayRecord.user_name = buyUserName;
            this._alipayRecord.value_rmb = (decimal)this.numValueRMB.Value;

            App.AlipayRechargeVMObject.AsyncHandleExceptionAlipayRechargeRecord(this._alipayRecord.ParentObject);
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
