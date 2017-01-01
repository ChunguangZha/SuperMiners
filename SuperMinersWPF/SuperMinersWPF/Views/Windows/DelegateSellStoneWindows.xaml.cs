using MetaData;
using SuperMinersWPF.Utility;
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

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for DelegateSellStoneWindows.xaml
    /// </summary>
    public partial class DelegateSellStoneWindows : Window
    {
        private System.Threading.SynchronizationContext _syn;

        public DelegateSellStoneWindows()
        {
            InitializeComponent();
            this._syn = System.Threading.SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtAllStones.Text = GlobalData.CurrentUser.StockOfStones.ToString();
            this.txtForzenStones.Text = GlobalData.CurrentUser.FreezingStones.ToString();
            this.txtSellableStones.Text = GlobalData.CurrentUser.SellableStones.ToString();

            this.sliderPrice.Maximum = (double)App.StackStoneVMObject.TodayStackInfo.LimitUpPrice;
            this.sliderPrice.Minimum = (double)App.StackStoneVMObject.TodayStackInfo.LimitDownPrice;
            this.sliderPrice.Value = (double)App.StackStoneVMObject.TodayStackInfo.OpenPrice;
            this.numPrice.Text = this.sliderPrice.Value.ToString();

            GlobalData.Client.DelegateSellStoneCompleted += Client_DelegateSellStoneCompleted;

            ComputeExpense();
        }

        void Client_DelegateSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("挂单出售矿石失败。");
                return;
            }
            if (e.Result != OperResult.RESULTCODE_TRUE)
            {
                MyMessageBox.ShowInfo("挂单出售矿石失败。原因为：" + OperResult.GetMsg(e.Result));
                return;
            }

            MyMessageBox.ShowInfo("挂单出售矿石成功。");
            App.UserVMObject.AsyncGetPlayerInfo();
            App.StackStoneVMObject.AsyncGetAllNotFinishedSellOrders();

            this._syn.Post((o) =>
            {
                this.Close();
            }, null);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.DelegateSellStoneCompleted -= Client_DelegateSellStoneCompleted;
        }

        private void numSellStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.txtExpense == null)
            {
                return;
            }

            ComputeExpense();
        }

        private void ComputeExpense()
        {
            decimal allstone = GetAllStonesCount();
            decimal expense = GetExpense(allstone);
            this.txtExpense.Text = expense.ToString();
        }

        private decimal GetAllStonesCount()
        {
            return (int)this.numSellStoneHandsCount.Value * GlobalData.GameConfig.HandStoneCount;
        }

        private decimal GetExpense(decimal allstone)
        {
            decimal expense = allstone * GlobalData.GameConfig.ExchangeExpensePercent / 100;
            if (expense < GlobalData.GameConfig.ExchangeExpenseMinNumber)
            {
                expense = GlobalData.GameConfig.ExchangeExpenseMinNumber;
            }
            return expense;
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            int handCount = (int)this.numSellStoneHandsCount.Value;
            int sellStoneCount = (int)GetAllStonesCount();

            int expenseStonesCount = (int)GetExpense(sellStoneCount);
            if (GlobalData.CurrentUser.SellableStones < sellStoneCount + expenseStonesCount)
            {
                MyMessageBox.ShowInfo("没有足够的矿石出售");
                return;
            }

            decimal price = Math.Round((decimal)this.sliderPrice.Value,2);

            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.DelegateSellStone(handCount, price, null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sliderPrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.numPrice.Text = Math.Round((decimal)this.sliderPrice.Value, 2).ToString();
        }
    }
}
