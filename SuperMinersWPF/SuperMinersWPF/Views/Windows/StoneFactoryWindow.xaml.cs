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

            App.ShoppingVMObject.VirtualShoppingBuySucceed += ShoppingVMObject_VirtualShoppingBuySucceed;

            Thread thr = new Thread(ThreadDownStart);
            thr.IsBackground = true;
            thr.Start();
        }

        void ShoppingVMObject_VirtualShoppingBuySucceed()
        {
            App.StoneFactoryVMObject.AsyncGetPlayerFactoryAccountInfo();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.ShoppingVMObject.VirtualShoppingBuySucceed -= ShoppingVMObject_VirtualShoppingBuySucceed;

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
            try
            {
                var foodsShoppingItem = App.ShoppingVMObject.ListVirtualShoppingItem.FirstOrDefault(s => s.ItemType == MetaData.Shopping.VirtualShoppingItemType.FactorySlaveFoods30Days);
                if (foodsShoppingItem != null)
                {
                    if (foodsShoppingItem.ValueShoppingCredits > GlobalData.CurrentUser.ShoppingCreditsEnabled)
                    {
                        MyMessageBox.ShowInfo("您的积分不足，无法购买。");
                        return;
                    }
                    App.ShoppingVMObject.AsyncBuyVirtualShoppingItem(foodsShoppingItem.ParentObject);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("购买失败。");
            }
        }

        private void btnFeedSlave_Click(object sender, RoutedEventArgs e)
        {
            App.StoneFactoryVMObject.AsyncFeedSlave();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.StoneFactoryVMObject.AsyncGetStoneFactoryProfitRMBChangedRecordList(null, null, GlobalData.PageItemsCount, 1);
        }
    }
}
