using DataBaseProvider;
using MetaData;
using MetaData.Shopping;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Shopping;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient
    {
        public PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecord(string token, int itemID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    var playerInfo = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                    if (playerInfo == null)
                    {
                        return null;
                    }

                    return VirtualShoppingController.Instance.GetPlayerBuyVirtualShoppingItemRecordByID(playerInfo.SimpleInfo.UserID, itemID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerBuyVirtualShoppingItemRecord Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Shopping.VirtualShoppingItem[] GetVirtualShoppingItems(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return VirtualShoppingController.Instance.GetVirtualShoppingItems(false, SellState.OnSell);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetVirtualShoppingItems Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int BuyVirtualShoppingItem(string token, VirtualShoppingItem shoppingItem)
        {
            if (RSAProvider.LoadRSA(token))
            {
                PlayerRunnable playerrunner = null;
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    playerrunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerrunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    if (!VirtualShoppingController.Instance.CheckVirtualShoppingItemBuyable(playerrunner.BasePlayer.SimpleInfo.UserID, shoppingItem))
                    {
                        return OperResult.RESULTCODE_VIRTUALSHOPPING_PLAYERCANOTBUY_THISITEM;
                    }

                    int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                    {
                        int innerResult = playerrunner.BuyVirtualShoppingItem(shoppingItem, myTrans);
                        if (innerResult != OperResult.RESULTCODE_TRUE)
                        {
                            return innerResult;
                        }

                        return VirtualShoppingController.Instance.BuyVirtualShoppingItem(playerrunner.BasePlayer.SimpleInfo.UserID, userName, shoppingItem, myTrans);
                    },
                    exc =>
                    {
                        if (playerrunner != null)
                        {
                            playerrunner.RefreshFortune();
                        }

                        if (exc != null)
                        {
                            LogHelper.Instance.AddErrorLog("玩家[" + userName + "]购买虚拟商品[" + shoppingItem.Name + "]异常", exc);
                        }
                    });

                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "]成功购买了虚拟商品[" + shoppingItem.Name + "]");
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "]购买虚拟商品[" + shoppingItem.Name + "]异常. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Shopping.DiamondShoppingItem[] GetDiamondShoppingItems(string token, DiamondsShoppingItemType itemType)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DiamondShoppingController.Instance.GetDiamondShoppingItems(false, SellState.OnSell, itemType);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetDiamondShoppingItems Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public byte[][] GetDiamondShoppingItemDetailImageBuffer(string token, int diamondShoppingItemID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DiamondShoppingController.Instance.GetDiamondShoppingItemDetailImageBuffer(diamondShoppingItemID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetDiamondShoppingItemDetailImageBuffer Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int BuyDiamondShoppingItem(string token, DiamondShoppingItem shoppingItem, PostAddress address)
        {
            if (RSAProvider.LoadRSA(token))
            {
                PlayerRunnable playerrunner = null;
                string userName = "";
                try
                {
                    if (address == null)
                    {
                        return OperResult.RESULTCODE_NEEDPOSTADDRESS;
                    }

                    userName = ClientManager.GetClientUserName(token);
                    playerrunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerrunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                    {
                        int innerResult = playerrunner.BuyDiamondShoppingItem(shoppingItem, myTrans);
                        if (innerResult != OperResult.RESULTCODE_TRUE)
                        {
                            return innerResult;
                        }

                        return DiamondShoppingController.Instance.BuyDiamondShoppingItem(playerrunner.BasePlayer.SimpleInfo.UserID, userName, shoppingItem, address, myTrans);
                    },
                    exc =>
                    {
                        if (playerrunner != null)
                        {
                            playerrunner.RefreshFortune();
                        }

                        if (exc != null)
                        {
                            LogHelper.Instance.AddErrorLog("玩家[" + userName + "]购买钻石商品[" + shoppingItem.Name + "]异常", exc);
                        }
                    });

                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "]成功购买了钻石商品[" + shoppingItem.Name + "]");
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "]购买钻石商品[" + shoppingItem.Name + "]异常. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecord(string token, int itemID, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    var playerInfo = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                    if (playerInfo == null)
                    {
                        return null;
                    }

                    return DiamondShoppingController.Instance.GetPlayerBuyDiamondShoppingItemRecordByID(playerInfo.SimpleInfo.UserID, itemID, shoppingStateInt, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家 GetPlayerBuyDiamondShoppingItemRecord 异常. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
