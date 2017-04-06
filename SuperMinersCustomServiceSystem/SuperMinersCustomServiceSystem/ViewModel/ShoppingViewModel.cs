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

        public void AsyncAddVirtualShoppingItem(string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在添加新的虚拟商品...");
                GlobalData.Client.AddVirtualShoppingItem(actionPassword, item);
            }
        }

        public void AsyncUpdateVirtualShoppingItem(string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在修改虚拟商品...");
                GlobalData.Client.UpdateVirtualShoppingItem(actionPassword, item);
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
            GlobalData.Client.AddVirtualShoppingItemCompleted += Client_AddVirtualShoppingItemCompleted;
            GlobalData.Client.GetVirtualShoppingItemsCompleted += Client_GetVirtualShoppingItemsCompleted;
            GlobalData.Client.UpdateVirtualShoppingItemCompleted += Client_UpdateVirtualShoppingItemCompleted;
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

        void Client_AddVirtualShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("添加新的虚拟商品失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("虚拟商品添加成功。");
                    this.AsyncGetAllVirtualShoppingItems();
                }
                else
                {
                    MessageBox.Show("添加新的虚拟商品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("添加新的虚拟商品回调处理异常。" + exc.Message);
            }
        }

        void Client_UpdateVirtualShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("修改虚拟商品失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("虚拟商品修改成功。");
                    this.AsyncGetAllVirtualShoppingItems();
                }
                else
                {
                    MessageBox.Show("修改虚拟商品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("修改虚拟商品回调处理异常。" + exc.Message);
            }
        }

    }
}
