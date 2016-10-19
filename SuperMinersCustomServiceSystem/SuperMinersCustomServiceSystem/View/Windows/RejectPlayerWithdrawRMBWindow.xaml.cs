using MetaData;
using SuperMinersCustomServiceSystem.Model;
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
    /// Interaction logic for RejectPlayerWithdrawRMBWindow.xaml
    /// </summary>
    public partial class RejectPlayerWithdrawRMBWindow : Window
    {
        public bool IsOK = false;
        private SynchronizationContext _syn;
        WithdrawRMBRecordUIModel Record = null;

        public RejectPlayerWithdrawRMBWindow(WithdrawRMBRecordUIModel record)
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;

            this.DataContext = record;
            this.Record = record;
            this.txtAdminUserName.Text = GlobalData.CurrentAdmin.UserName;

            GlobalData.Client.PayWithdrawRMBRecordCompleted += Client_PayWithdrawRMBRecordCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.PayWithdrawRMBRecordCompleted -= Client_PayWithdrawRMBRecordCompleted;
        }

        void Client_PayWithdrawRMBRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
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
                    IsOK = true;
                    MessageBox.Show("操作成功。");
                    _syn.Post(o =>
                    {
                        this.Close();
                    }, null);
                }
                else
                {
                    MessageBox.Show("操作失败。原因：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("操作异常。原因：" + exc.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string message = this.txtMessage.Text.Trim();
            if (message == "")
            {
                MessageBox.Show("请输入拒绝原因");
                return;
            }

            this.Record.AdminUserName = GlobalData.CurrentAdmin.UserName;
            this.Record.State = MetaData.Trade.RMBWithdrawState.Rejected;
            this.Record.Message = message;

            App.BusyToken.ShowBusyWindow("正在提交数据...");
            GlobalData.Client.PayWithdrawRMBRecord(Record.ParentObject);
        }
    }
}
