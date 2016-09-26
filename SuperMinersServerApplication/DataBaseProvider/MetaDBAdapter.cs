using MetaData;
using MetaData.Game.Roulette;
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
                info.Value = Convert.ToDecimal(dt.Rows[i][valueType]);

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

                player.SimpleInfo.UserID = Convert.ToInt32(dt.Rows[i]["id"]);
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

                if (dt.Rows[i]["LockedLogin"] != DBNull.Value)
                {
                    player.SimpleInfo.LockedLogin = Convert.ToBoolean(dt.Rows[i]["LockedLogin"]);
                }
                if (dt.Rows[i]["LockedLoginTime"] != DBNull.Value)
                {
                    player.SimpleInfo.LockedLoginTime = Convert.ToDateTime(dt.Rows[i]["LockedLoginTime"]);
                }
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
                player.FortuneInfo.Exp = Convert.ToDecimal(dt.Rows[i]["Exp"]);
                player.FortuneInfo.RMB = Convert.ToDecimal(dt.Rows[i]["RMB"]);
                player.FortuneInfo.FreezingRMB = Convert.ToDecimal(dt.Rows[i]["FreezingRMB"]);
                player.FortuneInfo.GoldCoin = Convert.ToDecimal(dt.Rows[i]["GoldCoin"]);
                player.FortuneInfo.MinesCount = Convert.ToDecimal(dt.Rows[i]["MinesCount"]);
                player.FortuneInfo.MinersCount = Convert.ToDecimal(dt.Rows[i]["MinersCount"]);
                player.FortuneInfo.StonesReserves = Convert.ToDecimal(dt.Rows[i]["StonesReserves"]);
                player.FortuneInfo.TotalProducedStonesCount = Convert.ToDecimal(dt.Rows[i]["TotalProducedStonesCount"]);
                player.FortuneInfo.StockOfStones = Convert.ToDecimal(dt.Rows[i]["StockOfStones"]);
                if (dt.Rows[i]["TempOutputStonesStartTime"] == DBNull.Value)
                {
                    player.FortuneInfo.TempOutputStonesStartTime = null;
                }
                else
                {
                    player.FortuneInfo.TempOutputStonesStartTime = Convert.ToDateTime(dt.Rows[i]["TempOutputStonesStartTime"]);
                }
                player.FortuneInfo.TempOutputStones = Convert.ToDecimal(dt.Rows[i]["TempOutputStones"]);
                player.FortuneInfo.FreezingStones = Convert.ToDecimal(dt.Rows[i]["FreezingStones"]);
                player.FortuneInfo.StockOfDiamonds = Convert.ToDecimal(dt.Rows[i]["StockOfDiamonds"]);
                player.FortuneInfo.FreezingDiamonds = Convert.ToDecimal(dt.Rows[i]["FreezingDiamonds"]);
                player.FortuneInfo.FirstRechargeGoldCoinAward = Convert.ToBoolean(dt.Rows[i]["FirstRechargeGoldCoinAward"]);

                players[i] = player;
            }
            dt.Dispose();

            return players;
        }

        internal static AdminInfo[] GetAdminInfoListFromDataTable(DataTable dt)
        {
            AdminInfo[] admins = new AdminInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AdminInfo admin = new AdminInfo();
                admin.UserName = DESEncrypt.DecryptDES(dt.Rows[i]["UserName"].ToString());
                admin.LoginPassword = DESEncrypt.DecryptDES(dt.Rows[i]["LoginPassword"].ToString());
                admin.ActionPassword = DESEncrypt.DecryptDES(dt.Rows[i]["ActionPassword"].ToString());
                string macs = dt.Rows[i]["Mac"].ToString();
                admin.Macs = macs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                admins[i] = admin;
            }

            return admins;
        }

        internal static ExpChangeRecord[] GetExpChangeRecordListFromDataTable(DataTable dt)
        {
            ExpChangeRecord[] records = new ExpChangeRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ExpChangeRecord record = new ExpChangeRecord();
                record.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                record.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                record.AddExp = Convert.ToDecimal(dt.Rows[i]["AddExp"]);
                record.NewExp = Convert.ToDecimal(dt.Rows[i]["NewExp"]);
                record.Time = Convert.ToDateTime(dt.Rows[i]["Time"]);
                record.OperContent = dt.Rows[i]["OperContent"].ToString();

                records[i] = record;
            }

            return records;
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
                order.StonesOrder.Expense = Convert.ToDecimal(dt.Rows[i]["Expense"]);
                order.StonesOrder.ValueRMB = Convert.ToDecimal(dt.Rows[i]["ValueRMB"]);
                order.StonesOrder.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.StonesOrder.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);

                string encryptedLockedByUserName = dt.Rows[i]["LockedByUserName"].ToString();
                order.LockedByUserName = DESEncrypt.DecryptDES(encryptedLockedByUserName);
                order.PayUrl = dt.Rows[i]["PayUrl"].ToString();
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
                order.StonesOrder.Expense = Convert.ToDecimal(dt.Rows[i]["Expense"]);
                order.StonesOrder.ValueRMB = Convert.ToDecimal(dt.Rows[i]["ValueRMB"]);
                order.StonesOrder.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.StonesOrder.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);

                string encryptedBuyerUserName = dt.Rows[i]["BuyerUserName"].ToString();
                order.BuyerUserName = DESEncrypt.DecryptDES(encryptedBuyerUserName);
                order.BuyTime = Convert.ToDateTime(dt.Rows[i]["BuyTime"]);
                order.AwardGoldCoin = Convert.ToDecimal(dt.Rows[i]["AwardGoldCoin"]);

                orders[i] = order;
            }

            return orders;
        }

        internal static RouletteAwardItem[] GetRouletteAwardItemFromDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            RouletteAwardItem[] items = new RouletteAwardItem[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RouletteAwardItem item = new RouletteAwardItem();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.AwardName = Convert.ToString(dt.Rows[i]["AwardName"]);
                item.AwardNumber = Convert.ToInt32(dt.Rows[i]["AwardNumber"]);
                item.IsLargeAward = Convert.ToBoolean(dt.Rows[i]["IsLargeAward"]);
                item.IsRealAward = Convert.ToBoolean(dt.Rows[i]["IsRealAward"]);
                item.RouletteAwardType = (RouletteAwardType)Convert.ToInt32(dt.Rows[i]["RouletteAwardType"]);
                item.ValueMoneyYuan = Convert.ToSingle(dt.Rows[i]["ValueMoneyYuan"]);
                item.WinProbability = Convert.ToSingle(dt.Rows[i]["WinProbability"]);

                items[i] = item;
            }

            return items;
        }

        internal static RouletteWinnerRecord[] GetRouletteWinnerRecordFromDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            RouletteWinnerRecord[] records = new RouletteWinnerRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RouletteWinnerRecord record = new RouletteWinnerRecord();
                record.RecordID = Convert.ToInt32(dt.Rows[i]["id"]);
                record.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                record.UserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["UserName"]));
                if (dt.Rows[i]["UserNickName"] == DBNull.Value)
                {
                    record.UserNickName = record.UserName;
                }
                else
                {
                    record.UserNickName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["UserNickName"]));
                }
                record.RouletteAwardItemID = Convert.ToInt32(dt.Rows[i]["AwardItemID"]);
                record.WinTime = Convert.ToDateTime(dt.Rows[i]["WinTime"]);
                record.IsGot = Convert.ToBoolean(dt.Rows[i]["IsGot"]);
                if (dt.Rows[i]["GotTime"] != DBNull.Value)
                {
                    record.GotTime = Convert.ToDateTime(dt.Rows[i]["GotTime"]);
                }
                record.IsPay = Convert.ToBoolean(dt.Rows[i]["IsPay"]);
                if (dt.Rows[i]["PayTime"] != DBNull.Value)
                {
                    record.PayTime = Convert.ToDateTime(dt.Rows[i]["PayTime"]);
                }
                if (dt.Rows[i]["GotInfo1"] != DBNull.Value)
                {
                    record.GotInfo1 = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["GotInfo1"]));
                }
                if (dt.Rows[i]["GotInfo2"] != DBNull.Value)
                {
                    record.GotInfo2 = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["GotInfo2"]));
                }

                records[i] = record;
            }

            return records;
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
                order.Expense = Convert.ToDecimal(dt.Rows[i]["Expense"]);
                order.ValueRMB = Convert.ToDecimal(dt.Rows[i]["ValueRMB"]);
                order.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);
                
                orders[i] = order;
            }

            return orders;
        }

        internal static TestUserLogState[] GetTestUserLogStateFromDataTable(DataTable dt)
        {
            TestUserLogState[] records = new TestUserLogState[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TestUserLogState record = new TestUserLogState();
                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                record.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                record.Mac = dt.Rows[i]["Mac"].ToString();
                record.IP = dt.Rows[i]["IP"].ToString();

                records[i] = record;
            }

            return records;
        }

        internal static MinesBuyRecord[] GetMinesBuyRecordFromDataTable(DataTable dt)
        {
            MinesBuyRecord[] records = new MinesBuyRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MinesBuyRecord record = new MinesBuyRecord();
                record.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"]);
                record.GainMinesCount = Convert.ToDecimal(dt.Rows[i]["GainMinesCount"]);
                record.GainStonesReserves = Convert.ToInt32(dt.Rows[i]["GainStonesReserves"]);
                record.OrderNumber = dt.Rows[i]["OrderNumber"].ToString();
                record.SpendRMB = Convert.ToInt32(dt.Rows[i]["SpendRMB"]);
                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                record.UserName = DESEncrypt.DecryptDES(encryptedUserName);

                records[i] = record;
            }

            return records;
        }

        internal static GoldCoinRechargeRecord[] GetGoldCoinRechargeRecordFromDataTable(DataTable dt)
        {
            GoldCoinRechargeRecord[] records = new GoldCoinRechargeRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GoldCoinRechargeRecord record = new GoldCoinRechargeRecord();
                record.CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"]);
                record.GainGoldCoin = Convert.ToDecimal(dt.Rows[i]["GainGoldCoin"]);
                record.OrderNumber = dt.Rows[i]["OrderNumber"].ToString();
                record.SpendRMB = Convert.ToInt32(dt.Rows[i]["SpendRMB"]);
                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                record.UserName = DESEncrypt.DecryptDES(encryptedUserName);

                records[i] = record;
            }

            return records;
        }

        internal static AlipayRechargeRecord[] GetAlipayRechargeRecordListFromDataTable(DataTable dt)
        {
            AlipayRechargeRecord[] orders = new AlipayRechargeRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AlipayRechargeRecord record = new AlipayRechargeRecord();
                record.out_trade_no = Convert.ToString(dt.Rows[i]["out_trade_no"]);
                record.alipay_trade_no = Convert.ToString(dt.Rows[i]["alipay_trade_no"]);
                string encryptedSellerUserName = dt.Rows[i]["user_name"].ToString();
                record.user_name = DESEncrypt.DecryptDES(encryptedSellerUserName);
                record.buyer_email = Convert.ToString(dt.Rows[i]["buyer_email"]);
                record.total_fee = Convert.ToDecimal(dt.Rows[i]["total_fee"]);
                record.value_rmb = Convert.ToDecimal(dt.Rows[i]["value_rmb"]);
                record.pay_time = Convert.ToDateTime(dt.Rows[i]["pay_time"]);

                orders[i] = record;
            }

            return orders;
        }

        internal static WaitToAwardExpRecord[] GetWaitToAwardExpRecordListFromDataTable(DataTable dt)
        {
            WaitToAwardExpRecord[] records = new WaitToAwardExpRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                records[i] = new WaitToAwardExpRecord()
                {
                    AwardLevel = Convert.ToInt32(dt.Rows[i]["AwardLevel"]),
                    NewRegisterUserNme = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["NewRegisterUserNme"])),
                    ReferrerUserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["ReferrerUserName"]))
                };
            }

            return records;
        }

        internal static WithdrawRMBRecord[] GetWithdrawRMBRecordListFromDataTable(DataTable dt)
        {
            WithdrawRMBRecord[] records = new WithdrawRMBRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string payerUserName = "";
                string adminUserName = "";
                if (dt.Rows[i]["PlayerUserName"] != DBNull.Value)
                {
                    payerUserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["PlayerUserName"]));
                }
                if (dt.Rows[i]["AdminUserName"] != DBNull.Value)
                {
                    adminUserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["AdminUserName"]));
                }

                string alipayAccount = "";
                string alipayRealName = "";
                if (dt.Rows[i]["AlipayAccount"] != DBNull.Value)
                {
                    alipayAccount = DESEncrypt.DecryptDES(dt.Rows[i]["AlipayAccount"].ToString());
                }
                if (dt.Rows[i]["AlipayRealName"] != DBNull.Value)
                {
                    alipayRealName = DESEncrypt.DecryptDES(dt.Rows[i]["AlipayRealName"].ToString());
                }

                DateTime? payTime = null;
                if (dt.Rows[i]["PayTime"] != DBNull.Value)
                {
                    payTime = Convert.ToDateTime(dt.Rows[i]["PayTime"]);
                }
                string alipayOrderNumber = "";
                if (dt.Rows[i]["AlipayOrderNumber"] != DBNull.Value)
                {
                    alipayOrderNumber = dt.Rows[i]["AlipayOrderNumber"] as string;
                }

                records[i] = new WithdrawRMBRecord()
                {
                    id = Convert.ToInt32(dt.Rows[i]["id"]),
                    PlayerUserName = payerUserName,
                    AlipayAccount = alipayAccount,
                    AlipayRealName = alipayRealName,
                    CreateTime = Convert.ToDateTime(dt.Rows[i]["CreateTime"]),
                    WidthdrawRMB = (decimal)Convert.ToSingle(dt.Rows[i]["WidthdrawRMB"]),
                    ValueYuan = Convert.ToInt32(dt.Rows[i]["ValueYuan"]),
                    IsPayedSucceed = Convert.ToBoolean(dt.Rows[i]["IsPayedSucceed"]),
                    AdminUserName = adminUserName,
                    AlipayOrderNumber = alipayOrderNumber,
                    PayTime = payTime,
                };
            }

            return records;
        }

        internal static MinersBuyRecord[] GetMinersBuyRecordListFromDataTable(DataTable dt)
        {
            MinersBuyRecord[] records = new MinersBuyRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MinersBuyRecord record = new MinersBuyRecord();
                record.UserName = DESEncrypt.DecryptDES(dt.Rows[i]["UserName"].ToString());
                record.SpendGoldCoin = Convert.ToDecimal(dt.Rows[i]["SpendGoldCoin"]);
                record.GainMinersCount = Convert.ToInt32(dt.Rows[i]["GainMinersCount"]);
                record.Time = Convert.ToDateTime(dt.Rows[i]["Time"]);
                records[i] = record;
            }

            return records;
        }

    }
}
