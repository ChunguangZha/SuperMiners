using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
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
    /// Interaction logic for GameRouletteWinAwardWindow.xaml
    /// </summary>
    public partial class GameRouletteWinAwardWindow : Window
    {
        private SynchronizationContext _syn;
        public GameRouletteWinAwardWindow(RouletteWinnerRecordUIModel record)
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
            this.DataContext = record;
            GlobalData.Client.PayAwardCompleted += Client_PayAwardCompleted;
        }

        void Client_PayAwardCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("提交幸运大转盘中奖支付，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("幸运大转盘中奖支付成功");
                    _syn.Post(o =>
                    {
                        this.DialogResult = true;
                    }, null);
                }
                else
                {
                    MyMessageBox.ShowInfo("幸运大转盘中奖支付失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("提交幸运大转盘中奖支付，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            RouletteWinnerRecordUIModel record = this.DataContext as RouletteWinnerRecordUIModel;

            App.BusyToken.ShowBusyWindow("正在提交数据...");
            GlobalData.Client.PayAward(GlobalData.CurrentAdmin.UserName, record.UserName, record.RecordID);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
