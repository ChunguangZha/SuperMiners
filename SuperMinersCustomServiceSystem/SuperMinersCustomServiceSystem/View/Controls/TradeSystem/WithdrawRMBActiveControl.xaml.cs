using MetaData.Trade;
using Microsoft.Win32;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using SuperMinersCustomServiceSystem.View.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for WithdrawRMBActiveControl.xaml
    /// </summary>
    public partial class WithdrawRMBActiveControl : UserControl
    {
        public WithdrawRMBActiveControl()
        {
            InitializeComponent();

            this.dgRecords.ItemsSource = App.WithdrawRMBVMObject.ListActiveWithdrawRecords;
            App.WithdrawRMBVMObject.GetWithdrawRMBActiveCompleted += WithdrawRMBVMObject_GetWithdrawRMBActiveCompleted;
        }

        void WithdrawRMBVMObject_GetWithdrawRMBActiveCompleted()
        {
            this.txtCount.Text = App.WithdrawRMBVMObject.ListActiveWithdrawRecords.Count.ToString();
            this.txtSumRMB.Text = App.WithdrawRMBVMObject.ListActiveWithdrawRecords.Sum(o => o.WidthdrawRMB).ToString();
            this.txtSumYuan.Text = App.WithdrawRMBVMObject.ListActiveWithdrawRecords.Sum(o => o.ValueYuan).ToString();
        }

        private void Search()
        {
            App.WithdrawRMBVMObject.AsyncGetWithdrawRMBActiveRecordList();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            WithdrawRMBRecordUIModel record = btn.DataContext as WithdrawRMBRecordUIModel;
            if (record == null)
            {
                return;
            }

            WithdrawRMBPayWindow win = new WithdrawRMBPayWindow(record);
            win.ShowDialog();
            if (win.IsOK)
            {
                App.WithdrawRMBVMObject.RemoveRecordFromActiveRecords(record);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {            
            Button btn = sender as Button;
            WithdrawRMBRecordUIModel record = btn.DataContext as WithdrawRMBRecordUIModel;
            if (record == null)
            {
                return;
            }

            RejectPlayerWithdrawRMBWindow win = new RejectPlayerWithdrawRMBWindow(record);
            win.ShowDialog();
            if (win.IsOK)
            {
                App.WithdrawRMBVMObject.RemoveRecordFromActiveRecords(record);
            }
        }

        private void btnCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDig = new SaveFileDialog();
            saveDig.Filter = "CSV文件(*.csv)|.csv";
            if (saveDig.ShowDialog() == true)
            {
                string fileName = saveDig.FileName;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("ID,用户名,提现灵币,价值人民币,提交时间,支付状态,支付宝账户,支付宝真实姓名");
                foreach (var item in App.WithdrawRMBVMObject.ListActiveWithdrawRecords)
                {
                    #region
                    builder.Append(item.ID);
                    builder.Append(",");
                    builder.Append(item.PlayerUserName);
                    builder.Append(",");
                    builder.Append(item.WidthdrawRMB);
                    builder.Append(",");
                    builder.Append(item.ValueYuan);
                    builder.Append(",");
                    builder.Append(item.CreateTime);
                    builder.Append(",");
                    builder.Append(item.StateText);
                    builder.Append(",");
                    builder.Append(item.AlipayAccount);
                    builder.Append(",");
                    builder.Append(item.AlipayRealName);
                    builder.AppendLine();
                    #endregion
                }

                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    StreamWriter writer = new StreamWriter(stream, UTF8Encoding.UTF8);
                    writer.Write(builder.ToString());
                    writer.Dispose();
                }

                MyMessageBox.ShowInfo("保存CSV文件成功");
            }
        }
    }
}
