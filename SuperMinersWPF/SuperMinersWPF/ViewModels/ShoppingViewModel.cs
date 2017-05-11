using MetaData;
using MetaData.Shopping;
using MetaData.User;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersWPF.ViewModels
{
    public class ShoppingViewModel
    {
        private ObservableCollection<VirtualShoppingItemUIModel> _listVirtualShoppingItem = new ObservableCollection<VirtualShoppingItemUIModel>();

        public ObservableCollection<VirtualShoppingItemUIModel> ListVirtualShoppingItem
        {
            get { return _listVirtualShoppingItem; }
        }

        private ObservableCollection<DiamondShoppingItemUIModel> _listDiamondShoppingItem = new ObservableCollection<DiamondShoppingItemUIModel>();

        public ObservableCollection<DiamondShoppingItemUIModel> ListDiamondShoppingItem
        {
            get { return _listDiamondShoppingItem; }
        }

        public void AsyncGetVirtualShoppingItem()
        {
            App.BusyToken.ShowBusyWindow("正在加载虚拟商品...");
            GlobalData.Client.GetVirtualShoppingItems();
        }

        public void AsyncBuyVirtualShoppingItem(VirtualShoppingItem shoppingItem)
        {
            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.BuyVirtualShoppingItem(shoppingItem);
        }

        public void AsyncGetDiamondShoppingItem()
        {
            App.BusyToken.ShowBusyWindow("正在加载钻石商品...");
            GlobalData.Client.GetDiamondShoppingItems(DiamondsShoppingItemType.Digital);
        }
        
        public void AsyncBuyDiamondShoppingItem(DiamondShoppingItem shoppingItem, PostAddress address)
        {
            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.BuyDiamondShoppingItem(shoppingItem, address);
        }

        public ShoppingViewModel()
        {
            GlobalData.Client.GetVirtualShoppingItemsCompleted += Client_GetVirtualShoppingItemsCompleted;
            GlobalData.Client.BuyVirtualShoppingItemCompleted += Client_BuyVirtualShoppingItemCompleted;

            GlobalData.Client.GetDiamondShoppingItemsCompleted += Client_GetDiamondShoppingItemsCompleted;
            GlobalData.Client.BuyDiamondShoppingItemCompleted += Client_BuyDiamondShoppingItemCompleted;
        }

        void Client_GetDiamondShoppingItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<DiamondShoppingItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("加载钻石商品失败。" + e.Error.Message);
                    return;
                }

                this.ListDiamondShoppingItem.Clear();

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListDiamondShoppingItem.Add(new DiamondShoppingItemUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("加载钻石商品失败，回调处理异常。" + exc.Message);
            }
        }

        void Client_BuyDiamondShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("购买商品失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("购买商品成功");
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MessageBox.Show("购买商品失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("购买商品失败，回调处理异常。" + exc.Message);
            }
        }

        void Client_BuyVirtualShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("购买商品失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("购买商品成功");
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MessageBox.Show("购买商品失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("购买商品失败，回调处理异常。" + exc.Message);
            }
        }

        void Client_GetVirtualShoppingItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Shopping.VirtualShoppingItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("加载钻石商品失败。" + e.Error.Message);
                    return;
                }

                this.ListVirtualShoppingItem.Clear();

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListVirtualShoppingItem.Add(new VirtualShoppingItemUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("加载钻石商品失败，回调处理异常。" + exc.Message);
            }
        }
    }
}
