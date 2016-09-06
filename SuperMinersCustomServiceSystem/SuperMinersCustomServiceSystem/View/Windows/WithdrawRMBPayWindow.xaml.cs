using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for WithdrawRMBPayWindow.xaml
    /// </summary>
    public partial class WithdrawRMBPayWindow : Window
    {
        private SynchronizationContext _syn;
        WithdrawRMBRecordUIModel Record = null;

        public WithdrawRMBPayWindow(WithdrawRMBRecordUIModel record)
        {
            InitializeComponent();

            _syn = SynchronizationContext.Current;

            this.DataContext = record;
            this.Record = record;
            this.txtAdminUserName.Text = GlobalData.CurrentAdmin.UserName;

            GlobalData.Client.PayWithdrawRMBRecordCompleted += Client_PayWithdrawRMBRecordCompleted;
        }

        void Client_PayWithdrawRMBRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("操作失败。");
                return;
            }

            if (e.Result == OperResult.RESULTCODE_TRUE)
            {
                MessageBox.Show("操作成功。");
                _syn.Post(o =>
                {
                    this.DialogResult = true;
                }, null);
            }
            else
            {
                MessageBox.Show(ResultCodeMsg.GetMsg(e.Result));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtAlipayOrderNumber.Text == "")
            {
                MessageBox.Show("请输入支付宝订单号");
                return;
            }

            App.BusyToken.ShowBusyWindow("正在提交数据...");
            GlobalData.Client.PayWithdrawRMBRecord(Record.ParentObject);
        }
    }
}
