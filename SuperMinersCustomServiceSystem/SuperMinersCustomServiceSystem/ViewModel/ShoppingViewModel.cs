using MetaData;
using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class ShoppingViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "商城交易";
            }
        }

        private ObservableCollection<VirtualShoppingItemUIModel> _listVirtualShoppingItems = new ObservableCollection<VirtualShoppingItemUIModel>();

        public ObservableCollection<VirtualShoppingItemUIModel> ListVirtualShoppingItems
        {
            get { return _listVirtualShoppingItems; }
        }

        private ObservableCollection<PlayerBuyVirtualShoppingItemRecordUIModel> _listVirtualShoppingBuyRecords = new ObservableCollection<PlayerBuyVirtualShoppingItemRecordUIModel>();

        public ObservableCollection<PlayerBuyVirtualShoppingItemRecordUIModel> ListVirtualShoppingBuyRecords
        {
            get { return _listVirtualShoppingBuyRecords; }
        }


        public void AsyncGetAllVirtualShoppingItems()
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在加载虚拟商品...");
                GlobalData.Client.GetVirtualShoppingItems(true, MetaData.Shopping.SellState.OnSell);
            }
        }

        public void AsyncGetPlayerBuyVirtualShoppingItemRecord(string playerUserName, string shoppingItemName, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询虚拟商品购买记录...");
                GlobalData.Client.GetPlayerBuyVirtualShoppingItemRecord(playerUserName, shoppingItemName, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
            }
        }

        public ShoppingViewModel()
        {
            GlobalData.Client.GetVirtualShoppingItemsCompleted += Client_GetVirtualShoppingItemsCompleted;
            GlobalData.Client.GetPlayerBuyVirtualShoppingItemRecordCompleted += Client_GetPlayerBuyVirtualShoppingItemRecordCompleted;
        }

        void Client_GetPlayerBuyVirtualShoppingItemRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Shopping.PlayerBuyVirtualShoppingItemRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询虚拟商品购买记录失败。" + e.Error.Message);
                    return;
                }

                this.ListVirtualShoppingBuyRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListVirtualShoppingBuyRecords.Add(new PlayerBuyVirtualShoppingItemRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询虚拟商品购买记录回调处理异常。" + exc.Message);
            }
        }

        void Client_GetVirtualShoppingItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Shopping.VirtualShoppingItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询所有虚拟商品失败。" + e.Error.Message);
                    return;
                }

                this.ListVirtualShoppingItems.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListVirtualShoppingItems.Add(new VirtualShoppingItemUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询所有虚拟商品回调处理异常。" + exc.Message);
            }
        }

    }
}
