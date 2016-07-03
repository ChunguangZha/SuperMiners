﻿using MetaData;
using MetaData.Trade;
using MetaData.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    class MetaDBAdapter<T> where T : class
    {
        internal MySqlCommand ConvertToInsertSqlCommand(string tableName, T obj)
        {
            try
            {
                MySqlCommand mycmd = new MySqlCommand();
                string cmdHead = "insert into " + tableName;
                StringBuilder strbuilderColumns = new StringBuilder(" ( ");
                StringBuilder strbuilderParams = new StringBuilder(" ( ");

                Type type = typeof(T);
                FieldInfo[] fields = type.GetFields();
                if (fields != null)
                {
                    foreach (var field in fields)
                    {
                        Attribute attr = field.GetCustomAttribute(typeof(DatabaseFieldAttribute));
                        if (attr != null)
                        {
                            strbuilderColumns.Append(field.Name + ",");

                            object value = field.GetValue(obj);
                            strbuilderParams.Append("@" + field.Name + ",");
                            mycmd.Parameters.AddWithValue("@" + field.Name, value);
                        }
                    }

                    string cmdText = cmdHead + strbuilderColumns.ToString(0, strbuilderColumns.Length - 1) + ")"
                                     + " values " + strbuilderParams.ToString(0, strbuilderParams.Length - 1) + ")";
                    mycmd.CommandText = cmdText;
                    return mycmd;
                }

                return null;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        internal static TopListInfo[] GetTopListInfo(string valueType, DataTable dt)
        {
            TopListInfo[] toplistInfos = new TopListInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TopListInfo info = new TopListInfo();

                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                string encryptedNickName = dt.Rows[i]["NickName"] == DBNull.Value ? "" : dt.Rows[i]["NickName"].ToString();

                info.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                info.NickName = string.IsNullOrEmpty(encryptedNickName) ? info.UserName : DESEncrypt.DecryptDES(encryptedNickName);
                info.Value = Convert.ToSingle(dt.Rows[i][valueType]);

                toplistInfos[i] = info;
            }

            return toplistInfos;
        }

        internal static UserReferrerTreeItem[] GetUserReferrerTreeItem(DataTable dt)
        {
            UserReferrerTreeItem[] players = new UserReferrerTreeItem[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserReferrerTreeItem player = new UserReferrerTreeItem();

                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                string encryptedNickName = dt.Rows[i]["NickName"] == DBNull.Value ? "" : dt.Rows[i]["NickName"].ToString();
                
                player.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                player.NickName = string.IsNullOrEmpty(encryptedNickName) ? player.UserName : DESEncrypt.DecryptDES(encryptedNickName);
                player.RegisterIP = dt.Rows[i]["RegisterIP"].ToString();
                player.RegisterTime = Convert.ToDateTime(dt.Rows[i]["RegisterTime"]);

                players[i] = player;
            }

            return players;
        }

        internal static PlayerInfo[] GetPlayerInfoFromDataTable(DataTable dt)
        {
            PlayerInfo[] players = new PlayerInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PlayerInfo player = new PlayerInfo();

                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                string encryptedNickName = dt.Rows[i]["NickName"] == DBNull.Value ? "" : dt.Rows[i]["NickName"].ToString();
                string encryptedUserPassword = dt.Rows[i]["Password"].ToString();
                string encryptedAlipay = dt.Rows[i]["Alipay"] == DBNull.Value ? "" : dt.Rows[i]["Alipay"].ToString();
                string encryptedAlipayRealName = dt.Rows[i]["AlipayRealName"] == DBNull.Value ? "" : dt.Rows[i]["AlipayRealName"].ToString();
                string encryptedEmail = dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString();
                string encryptedQQ = dt.Rows[i]["QQ"] == DBNull.Value ? "" : dt.Rows[i]["QQ"].ToString();
                string encryptedInvitationCode = dt.Rows[i]["InvitationCode"].ToString();

                player.SimpleInfo.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                player.SimpleInfo.NickName = string.IsNullOrEmpty(encryptedNickName) ? player.SimpleInfo.UserName : DESEncrypt.DecryptDES(encryptedNickName);
                player.SimpleInfo.Password = DESEncrypt.DecryptDES(encryptedUserPassword);
                player.SimpleInfo.Alipay = DESEncrypt.DecryptDES(encryptedAlipay);
                player.SimpleInfo.AlipayRealName = DESEncrypt.DecryptDES(encryptedAlipayRealName);
                player.SimpleInfo.Email = DESEncrypt.DecryptDES(encryptedEmail);
                player.SimpleInfo.QQ = DESEncrypt.DecryptDES(encryptedQQ);
                player.SimpleInfo.RegisterIP = dt.Rows[i]["RegisterIP"].ToString();
                player.SimpleInfo.InvitationCode = DESEncrypt.DecryptDES(encryptedInvitationCode);
                player.SimpleInfo.RegisterTime = Convert.ToDateTime(dt.Rows[i]["RegisterTime"]);
                if (dt.Rows[i]["LastLoginTime"] == DBNull.Value)
                {
                    player.SimpleInfo.LastLoginTime = null;
                }
                else
                {
                    player.SimpleInfo.LastLoginTime = Convert.ToDateTime(dt.Rows[i]["LastLoginTime"]);
                }
                if (dt.Rows[i]["LastLogOutTime"] == DBNull.Value)
                {
                    player.SimpleInfo.LastLogOutTime = null;
                }
                else
                {
                    player.SimpleInfo.LastLogOutTime = Convert.ToDateTime(dt.Rows[i]["LastLogOutTime"]);
                }
                if (dt.Rows[i]["ReferrerUserName"] != DBNull.Value)
                {
                    string encryptedReferrerUserName = dt.Rows[i]["ReferrerUserName"].ToString();
                    player.SimpleInfo.ReferrerUserName = DESEncrypt.DecryptDES(encryptedReferrerUserName);
                }

                player.FortuneInfo.UserName = player.SimpleInfo.UserName;
                player.FortuneInfo.Exp = Convert.ToSingle(dt.Rows[i]["Exp"]);
                player.FortuneInfo.RMB = Convert.ToSingle(dt.Rows[i]["RMB"]);
                player.FortuneInfo.FreezingRMB = Convert.ToSingle(dt.Rows[i]["FreezingRMB"]);
                player.FortuneInfo.GoldCoin = Convert.ToSingle(dt.Rows[i]["GoldCoin"]);
                player.FortuneInfo.MinesCount = Convert.ToSingle(dt.Rows[i]["MinesCount"]);
                player.FortuneInfo.MinersCount = Convert.ToSingle(dt.Rows[i]["MinersCount"]);
                player.FortuneInfo.StonesReserves = Convert.ToSingle(dt.Rows[i]["StonesReserves"]);
                player.FortuneInfo.TotalProducedStonesCount = Convert.ToSingle(dt.Rows[i]["TotalProducedStonesCount"]);
                player.FortuneInfo.StockOfStones = Convert.ToSingle(dt.Rows[i]["StockOfStones"]);
                if (dt.Rows[i]["TempOutputStonesStartTime"] == DBNull.Value)
                {
                    player.FortuneInfo.TempOutputStonesStartTime = null;
                }
                else
                {
                    player.FortuneInfo.TempOutputStonesStartTime = Convert.ToDateTime(dt.Rows[i]["TempOutputStonesStartTime"]);
                }
                player.FortuneInfo.TempOutputStones = Convert.ToSingle(dt.Rows[i]["TempOutputStones"]);
                player.FortuneInfo.FreezingStones = Convert.ToSingle(dt.Rows[i]["FreezingStones"]);
                player.FortuneInfo.StockOfDiamonds = Convert.ToSingle(dt.Rows[i]["StockOfDiamonds"]);
                player.FortuneInfo.FreezingDiamonds = Convert.ToSingle(dt.Rows[i]["FreezingDiamonds"]);

                players[i] = player;
            }
            dt.Dispose();

            return players;
        }

        internal static LockSellStonesOrder[] GetLockStonesOrderListFromDataTable(DataTable dt)
        {
            LockSellStonesOrder[] orders = new LockSellStonesOrder[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LockSellStonesOrder order = new LockSellStonesOrder();
                order.StonesOrder = new SellStonesOrder();
                order.StonesOrder.OrderNumber = Convert.ToString(dt.Rows[i]["OrderNumber"]);
                string encryptedSellerUserName = dt.Rows[i]["SellerUserName"].ToString();
                order.StonesOrder.SellerUserName = DESEncrypt.DecryptDES(encryptedSellerUserName);
                order.StonesOrder.SellStonesCount = Convert.ToInt32(dt.Rows[i]["SellStonesCount"]);
                order.StonesOrder.Expense = Convert.ToSingle(dt.Rows[i]["Expense"]);
                order.StonesOrder.ValueRMB = Convert.ToSingle(dt.Rows[i]["ValueRMB"]);
                order.StonesOrder.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.StonesOrder.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);

                string encryptedLockedByUserName = dt.Rows[i]["LockedByUserName"].ToString();
                order.LockedByUserName = DESEncrypt.DecryptDES(encryptedLockedByUserName);
                order.LockedTime = Convert.ToDateTime(dt.Rows[i]["LockedTime"]);

                orders[i] = order;
            }

            return orders;
        }

        internal static BuyStonesOrder[] GetBuyStonesOrderFromDataTable(DataTable dt)
        {
            BuyStonesOrder[] orders = new BuyStonesOrder[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyStonesOrder order = new BuyStonesOrder();
                order.StonesOrder = new SellStonesOrder();
                order.StonesOrder.OrderNumber = Convert.ToString(dt.Rows[i]["OrderNumber"]);
                string encryptedSellerUserName = dt.Rows[i]["SellerUserName"].ToString();
                order.StonesOrder.SellerUserName = DESEncrypt.DecryptDES(encryptedSellerUserName);
                order.StonesOrder.SellStonesCount = Convert.ToInt32(dt.Rows[i]["SellStonesCount"]);
                order.StonesOrder.Expense = Convert.ToSingle(dt.Rows[i]["Expense"]);
                order.StonesOrder.ValueRMB = Convert.ToSingle(dt.Rows[i]["ValueRMB"]);
                order.StonesOrder.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.StonesOrder.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);

                string encryptedBuyerUserName = dt.Rows[i]["BuyerUserName"].ToString();
                order.BuyerUserName = DESEncrypt.DecryptDES(encryptedBuyerUserName);
                order.BuyTime = Convert.ToDateTime(dt.Rows[i]["BuyTime"]);
                order.AwardGoldCoin = Convert.ToSingle(dt.Rows[i]["AwardGoldCoin"]);

                orders[i] = order;
            }

            return orders;
        }

        internal static SellStonesOrder[] GetSellStonesOrderFromDataTable(DataTable dt)
        {
            SellStonesOrder[] orders = new SellStonesOrder[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SellStonesOrder order = new SellStonesOrder();
                order.OrderNumber = Convert.ToString(dt.Rows[i]["OrderNumber"]);
                string encryptedSellerUserName = dt.Rows[i]["SellerUserName"].ToString();
                order.SellerUserName = DESEncrypt.DecryptDES(encryptedSellerUserName);
                order.SellStonesCount = Convert.ToInt32(dt.Rows[i]["SellStonesCount"]);
                order.Expense = Convert.ToSingle(dt.Rows[i]["Expense"]);
                order.ValueRMB = Convert.ToSingle(dt.Rows[i]["ValueRMB"]);
                order.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);
                
                orders[i] = order;
            }

            return orders;
        }

    }
}
