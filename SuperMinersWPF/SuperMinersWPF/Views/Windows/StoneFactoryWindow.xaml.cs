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

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for StoneFactoryWindow.xaml
    /// </summary>
    public partial class StoneFactoryWindow : Window
    {
        private bool isClosed = false;

        public StoneFactoryWindow()
        {
            InitializeComponent();
            this.DataContext = App.StoneFactoryVMObject.FactoryAccount;
            this.lvProfitList.ItemsSource = App.StoneFactoryVMObject.ListProfitRecords;

            Thread thr = new Thread(ThreadDownStart);
            thr.IsBackground = true;
            thr.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isClosed = true;
        }

        private void ThreadDownStart()
        {
            while (!isClosed)
            {
                Thread.Sleep(1000);
                if (App.StoneFactoryVMObject.FactoryAccount != null)
                {
                    App.StoneFactoryVMObject.FactoryAccount.SlaveLiveDiscountms -= 1;
                    if (App.StoneFactoryVMObject.FactoryAccount.SlaveLiveDiscountms == 0)
                    {
                        App.StoneFactoryVMObject.AsyncGetPlayerFactoryAccountInfo();
                    }
                }
            }
        }

        private void btnWithdrawProfitRMB_Click(object sender, RoutedEventArgs e)
        {
            StoneFactoryProfitRMBWithdrawWindow win = new StoneFactoryProfitRMBWithdrawWindow(App.StoneFactoryVMObject.FactoryAccount.WithdrawableProfitRMB);
            if (win.ShowDialog() == true)
            {
                App.StoneFactoryVMObject.AsyncWithdrawOutputRMBFromFactory(win.WithdrawRMB);
            }
        }

        private void btnWithdrawStoneStack_Click(object sender, RoutedEventArgs e)
        {
            StoneFactoryStoneWithdrawWindow win = new StoneFactoryStoneWithdrawWindow(App.StoneFactoryVMObject.FactoryAccount.WithdrawableStackCount);
            if (win.ShowDialog() == true)
            {
                App.StoneFactoryVMObject.AsyncWithdrawStoneFromFactory(win.WithdrawStoneStack);
            }
        }

        private void btnJoinInStoneStack_Click(object sender, RoutedEventArgs e)
        {
            StoneFactoryJoinInStoneWindow win = new StoneFactoryJoinInStoneWindow(GlobalData.CurrentUser.SellableStones);
            if (win.ShowDialog() == true)
            {
                App.StoneFactoryVMObject.AsyncAddStoneToFactory(win.JoinInStoneStackCount);
            }
        }

        private void btnJoinInSlave_Click(object sender, RoutedEventArgs e)
        {
            StoneFactoryJoinInSlaveWindow win = new StoneFactoryJoinInSlaveWindow(GlobalData.CurrentUser.MinersCount);
            if (win.ShowDialog() == true)
            {
                App.StoneFactoryVMObject.AsyncAddMinersToFactory(win.JoinInSlaveGroupCount);
            }
        }

        private void btnBuyFoods_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFeedSlave_Click(object sender, RoutedEventArgs e)
        {
            App.StoneFactoryVMObject.AsyncFeedSlave();
        }
    }
}
