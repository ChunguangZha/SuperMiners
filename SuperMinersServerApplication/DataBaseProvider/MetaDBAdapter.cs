using MetaData;
using MetaData.ActionLog;
using MetaData.AgentUser;
using MetaData.Game.GambleStone;
using MetaData.Game.RaideroftheLostArk;
using MetaData.Game.Roulette;
using MetaData.Game.StoneStack;
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

        internal static AgentUserInfo[] GetAgentUserInfoFromDataTable(DataTable dt, PlayerInfo[] players)
        {
            List<AgentUserInfo> listAgents = new List<AgentUserInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                int userID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                var player = players.FirstOrDefault(p => p.SimpleInfo.UserID == userID);
                if (player != null)
                {
                    AgentUserInfo agent = new AgentUserInfo();
                    agent.ID = id;
                    agent.Player = player;
                    agent.TotalAwardRMB = Convert.ToDecimal(dt.Rows[i]["TotalAwardRMB"]);
                    agent.InvitationURL = Convert.ToString(dt.Rows[i]["InvitationURL"]);
                    listAgents.Add(agent);
                }
            }

            return listAgents.ToArray();
        }

        internal static AgentAwardRecord[] GetAgentAwardRecordFromDataTable(DataTable dt)
        {
            AgentAwardRecord[] records = new AgentAwardRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AgentAwardRecord record = new AgentAwardRecord();
                record.AgentAwardRMB = Convert.ToDecimal(dt.Rows[i]["AgentAwardRMB"]);
                record.AgentID = Convert.ToInt32(dt.Rows[i]["AgentID"]);
                record.AgentUserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["AgentUserName"]));
                record.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                record.PlayerID = Convert.ToInt32(dt.Rows[i]["PlayerID"]);
                record.PlayerInchargeContent = Convert.ToString(dt.Rows[i]["PlayerInchargeContent"]);
                record.PlayerInchargeRMB = Convert.ToDecimal(dt.Rows[i]["PlayerInchargeRMB"]);
                record.PlayerUserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["PlayerUserName"]));
                record.Time = MyDateTime.FromDateTime(Convert.ToDateTime(dt.Rows[i]["Time"]));

                records[i] = record;
            }

            return records;
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
                player.SimpleInfo.GroupType = (PlayerGroupType)Convert.ToInt32(dt.Rows[i]["GroupType"]);
                player.SimpleInfo.IsAgentReferred = Convert.ToBoolean(dt.Rows[i]["IsAgentReferred"]);
                player.SimpleInfo.AgentReferredLevel = Convert.ToInt32(dt.Rows[i]["AgentReferredLevel"]);
                player.SimpleInfo.AgentUserID = Convert.ToInt32(dt.Rows[i]["AgentUserID"]);
                player.SimpleInfo.Alipay = DESEncrypt.DecryptDES(encryptedAlipay);
                player.SimpleInfo.AlipayRealName = DESEncrypt.DecryptDES(encryptedAlipayRealName);
                player.SimpleInfo.IDCardNo = dt.Rows[i]["IDCardNo"] == DBNull.Value ? "" : dt.Rows[i]["IDCardNo"].ToString();
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
                if (dt.Rows[i]["LastLoginIP"] != DBNull.Value)
                {
                    player.SimpleInfo.LastLoginIP = dt.Rows[i]["LastLoginIP"].ToString();
                }
                if (dt.Rows[i]["LastLoginMac"] != DBNull.Value)
                {
                    player.SimpleInfo.LastLoginMac = dt.Rows[i]["LastLoginMac"].ToString();
                }

                player.FortuneInfo.UserName = player.SimpleInfo.UserName;
                player.FortuneInfo.Exp = Convert.ToDecimal(dt.Rows[i]["Exp"]);
                player.FortuneInfo.CreditValue = Convert.ToInt64(dt.Rows[i]["CreditValue"]);
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
                player.FortuneInfo.StoneSellQuan = Convert.ToInt32(dt.Rows[i]["StoneSellQuan"]);

                player.GravelInfo = new PlayerGravelInfo();
                if (dt.Rows[i]["Gravel"] != DBNull.Value)
                {
                    player.GravelInfo.Gravel = Convert.ToInt32(dt.Rows[i]["Gravel"]);
                }
                if (dt.Rows[i]["FirstGetGravelTime"] != DBNull.Value)
                {
                    player.GravelInfo.FirstGetGravelTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["FirstGetGravelTime"]));
                }

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
                admin.GroupType = (AdminGroupType)Convert.ToInt32(dt.Rows[i]["GroupType"]);
                string macs = dt.Rows[i]["Mac"].ToString();
                admin.Macs = macs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                admins[i] = admin;
            }

            return admins;
        }

        internal static PlayerLastSellStoneRecord[] GetPlayerLastSellStoneRecord(DataTable dt)
        {
            PlayerLastSellStoneRecord[] records = new PlayerLastSellStoneRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PlayerLastSellStoneRecord record = new PlayerLastSellStoneRecord();
                record.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                record.SellStoneOrderNumber = dt.Rows[i]["SellStoneOrderNumber"].ToString();
                record.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);

                records[i] = record;
            }

            return records;
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
                record.Time = MyDateTime.FromDateTime(Convert.ToDateTime(dt.Rows[i]["Time"]));
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
                order.StonesOrder.SellerCreditValue = Convert.ToInt64(dt.Rows[i]["SellerCreditValue"]);
                order.StonesOrder.SellerExpValue = Convert.ToInt32(dt.Rows[i]["SellerExpValue"]);
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
                if (DBNull.Value != dt.Rows[i]["SellerCreditValue"])
                {
                    order.StonesOrder.SellerCreditValue = Convert.ToInt64(dt.Rows[i]["SellerCreditValue"]);
                }
                if (DBNull.Value != dt.Rows[i]["SellerExpValue"])
                {
                    order.StonesOrder.SellerExpValue = Convert.ToInt32(dt.Rows[i]["SellerExpValue"]);
                }
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

        internal static RouletteRoundInfo[] GetRouletteRoundInfoFromDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            RouletteRoundInfo[] items = new RouletteRoundInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RouletteRoundInfo item = new RouletteRoundInfo();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.AwardPoolSumStone = Convert.ToInt32(dt.Rows[i]["AwardPoolSumStone"]);
                item.WinAwardSumYuan = Convert.ToDecimal(dt.Rows[i]["WinAwardSumYuan"]);
                item.StartTime = Convert.ToDateTime(dt.Rows[i]["StartTime"]);
                item.MustWinAwardItemID = Convert.ToInt32(dt.Rows[i]["MustWinAwardItemID"]);
                item.Finished = Convert.ToBoolean(dt.Rows[i]["Finished"]);

                items[i] = item;
            }

            return items;
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
                item.RouletteAwardType = (RouletteAwardType)Convert.ToInt32(dt.Rows[i]["RouletteAwardType"]);
                item.ValueMoneyYuan = Convert.ToSingle(dt.Rows[i]["ValueMoneyYuan"]);
                item.WinProbability = Convert.ToSingle(dt.Rows[i]["WinProbability"]);
                if (dt.Rows[i]["IconBuffer"] != DBNull.Value)
                {
                    item.IconBuffer = (byte[])dt.Rows[i]["IconBuffer"];
                }

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
                record.WinTime = MyDateTime.FromDateTime(Convert.ToDateTime(dt.Rows[i]["WinTime"]));
                record.IsGot = Convert.ToBoolean(dt.Rows[i]["IsGot"]);
                if (dt.Rows[i]["GotTime"] != DBNull.Value)
                {
                    record.GotTime = MyDateTime.FromDateTime(Convert.ToDateTime(dt.Rows[i]["GotTime"]));
                }
                record.IsPay = Convert.ToBoolean(dt.Rows[i]["IsPay"]);
                if (dt.Rows[i]["PayTime"] != DBNull.Value)
                {
                    record.PayTime = MyDateTime.FromDateTime(Convert.ToDateTime(dt.Rows[i]["PayTime"]));
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
                if (dt.Rows[i]["SellerCreditValue"] != DBNull.Value)
                {
                    order.SellerCreditValue = Convert.ToInt64(dt.Rows[i]["SellerCreditValue"]);
                }
                if (dt.Rows[i]["SellerExpValue"] != DBNull.Value)
                {
                    order.SellerExpValue = Convert.ToInt32(dt.Rows[i]["SellerExpValue"]);
                }
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

        internal static WaitToReferAwardRecord[] GetWaitToAwardExpRecordListFromDataTable(DataTable dt)
        {
            WaitToReferAwardRecord[] records = new WaitToReferAwardRecord[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                records[i] = new WaitToReferAwardRecord()
                {
                    ID = Convert.ToInt32(dt.Rows[i]["id"]),
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
                string message = "";
                if (dt.Rows[i]["Message"] != DBNull.Value)
                {
                    message = dt.Rows[i]["Message"].ToString();
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
                    State = (RMBWithdrawState)Convert.ToInt32(dt.Rows[i]["RMBWithdrawState"]),
                    AdminUserName = adminUserName,
                    AlipayOrderNumber = alipayOrderNumber,
                    PayTime = payTime,
                    Message = message,
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

        internal static PlayerLockedInfo[] GetPlayerLockedInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            PlayerLockedInfo[] items = new PlayerLockedInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                PlayerLockedInfo item = new PlayerLockedInfo();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                item.LockedLogin = Convert.ToBoolean(dt.Rows[i]["LockedLogin"]);
                item.LockedLoginTime = MyDateTime.FromDateTime(Convert.ToDateTime(dt.Rows[i]["LockedLoginTime"]));
                item.ExpireDays = Convert.ToInt32(dt.Rows[i]["ExpireDays"]);

                items[i] = item;
            }

            return items;
        }

        internal static StoneStackDailyRecordInfo[] GetStoneStackDailyRecordInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            StoneStackDailyRecordInfo[] items = new StoneStackDailyRecordInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                StoneStackDailyRecordInfo item = new StoneStackDailyRecordInfo();
                item.Day = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["Day"]));
                item.OpenPrice = Convert.ToDecimal(dt.Rows[i]["OpenPrice"]);
                item.ClosePrice = Convert.ToDecimal(dt.Rows[i]["ClosePrice"]);
                item.MinTradeSucceedPrice = Convert.ToDecimal(dt.Rows[i]["MinTradeSucceedPrice"]);
                item.MaxTradeSucceedPrice = Convert.ToDecimal(dt.Rows[i]["MaxTradeSucceedPrice"]);
                item.TradeSucceedStoneHandSum = Convert.ToInt32(dt.Rows[i]["TradeSucceedStoneHandSum"]);
                item.TradeSucceedRMBSum = Convert.ToDecimal(dt.Rows[i]["TradeSucceedRMBSum"]);
                item.DelegateSellStoneSum = Convert.ToInt32(dt.Rows[i]["DelegateSellStoneSum"]);
                item.DelegateBuyStoneSum = Convert.ToInt32(dt.Rows[i]["DelegateBuyStoneSum"]);

                items[i] = item;
            }

            return items;
        }

        internal static StoneDelegateSellOrderInfo[] GetStoneDelegateSellOrderInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            StoneDelegateSellOrderInfo[] items = new StoneDelegateSellOrderInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                StoneDelegateSellOrderInfo item = new StoneDelegateSellOrderInfo();
                item.OrderNumber = dt.Rows[i]["OrderNumber"] as string;
                item.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                item.UserName = DESEncrypt.DecryptDES(dt.Rows[i]["UserName"].ToString());
                item.SellUnit = new StackTradeUnit()
                {
                    Price = Convert.ToDecimal(dt.Rows[i]["Price"]),
                    TradeStoneHandCount = Convert.ToInt32(dt.Rows[i]["TradeStoneHandCount"])
                };
                item.FinishedStoneTradeHandCount = Convert.ToInt32(dt.Rows[i]["FinishedStoneTradeHandCount"]);
                item.SellState = (StoneDelegateSellState)Convert.ToInt32(dt.Rows[i]["SellState"]);
                item.DelegateTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["DelegateTime"]));

                if (dt.Rows[i]["FinishedTime"] != DBNull.Value)
                {
                    item.FinishedTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["FinishedTime"]));
                }
                item.IsSubOrder = Convert.ToBoolean(dt.Rows[i]["IsSubOrder"]);
                if (dt.Rows[i]["ParentOrderNumber"] != DBNull.Value)
                {
                    item.ParentOrderNumber = dt.Rows[i]["ParentOrderNumber"].ToString();
                }

                items[i] = item;
            }

            return items;
        }
        
        internal static StoneDelegateBuyOrderInfo[] GetStoneDelegateBuyOrderInfoFromDataTable(DataTable dt, bool isFinishedOrder)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            StoneDelegateBuyOrderInfo[] items = new StoneDelegateBuyOrderInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                StoneDelegateBuyOrderInfo item = new StoneDelegateBuyOrderInfo();
                item.OrderNumber = dt.Rows[i]["OrderNumber"] as string;
                item.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                item.UserName = DESEncrypt.DecryptDES(dt.Rows[i]["UserName"].ToString());
                item.PayType = (PayType)Convert.ToInt32(dt.Rows[i]["PayType"]);
                item.BuyUnit = new StackTradeUnit()
                {
                    Price = Convert.ToDecimal(dt.Rows[i]["Price"]),
                    TradeStoneHandCount = Convert.ToInt32(dt.Rows[i]["TradeStoneHandCount"])
                };
                item.FinishedStoneTradeHandCount = Convert.ToInt32(dt.Rows[i]["FinishedStoneTradeHandCount"]);
                item.BuyState = (StoneDelegateBuyState)Convert.ToInt32(dt.Rows[i]["BuyState"]);
                item.DelegateTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["DelegateTime"]));

                item.IsSubOrder = Convert.ToBoolean(dt.Rows[i]["IsSubOrder"]);
                if (dt.Rows[i]["ParentOrderNumber"] != DBNull.Value)
                {
                    item.ParentOrderNumber = dt.Rows[i]["ParentOrderNumber"].ToString();
                }
                if (dt.Rows[i]["FinishedTime"] != DBNull.Value)
                {
                    item.FinishedTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["FinishedTime"]));
                }
                if (dt.Rows[i]["AwardGoldCoin"] != DBNull.Value)
                {
                    item.AwardGoldCoin = Convert.ToDecimal(dt.Rows[i]["AwardGoldCoin"]);
                }

                if (!isFinishedOrder)
                {
                    if (dt.Rows[i]["AlipayLink"] != DBNull.Value)
                    {
                        item.AlipayLink = dt.Rows[i]["AlipayLink"].ToString();
                    }
                }

                items[i] = item;
            }

            return items;
        }

        internal static PlayerRaiderRoundHistoryRecordInfo[] GetPlayerRaiderRoundHistoryRecordInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            PlayerRaiderRoundHistoryRecordInfo[] items = new PlayerRaiderRoundHistoryRecordInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                PlayerRaiderRoundHistoryRecordInfo item = new PlayerRaiderRoundHistoryRecordInfo();
                item.BetJoinStoneCount = Convert.ToInt32(dt.Rows[i]["AllBetStones"]);
                item.RoundInfo = new RaiderRoundMetaDataInfo();
                item.RoundInfo.ID = Convert.ToInt32(dt.Rows[i]["RaiderRoundID"]);
                item.RoundInfo.State = (RaiderRoundState)Convert.ToInt32(dt.Rows[i]["State"]);
                if (dt.Rows[i]["StartTime"] != DBNull.Value)
                {
                    item.RoundInfo.StartTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["StartTime"]));
                }
                if (dt.Rows[i]["AwardPoolSumStones"] != DBNull.Value)
                {
                    item.RoundInfo.AwardPoolSumStones = Convert.ToInt32(dt.Rows[i]["AwardPoolSumStones"]);
                }
                if (dt.Rows[i]["JoinedPlayerCount"] != DBNull.Value)
                {
                    item.RoundInfo.JoinedPlayerCount = Convert.ToInt32(dt.Rows[i]["JoinedPlayerCount"]);
                }
                if (dt.Rows[i]["WinnerUserName"] != DBNull.Value)
                {
                    item.RoundInfo.WinnerUserName = DESEncrypt.DecryptDES(dt.Rows[i]["WinnerUserName"].ToString());
                }
                if (dt.Rows[i]["WinStones"] != DBNull.Value)
                {
                    item.RoundInfo.WinStones = Convert.ToInt32(dt.Rows[i]["WinStones"]);
                }
                if (dt.Rows[i]["EndTime"] != DBNull.Value)
                {
                    item.RoundInfo.EndTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["EndTime"]));
                }

                items[i] = item;
            }

            return items;
        }

        internal static RaiderRoundMetaDataInfo[] GetRaiderRoundMetaDataInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            RaiderRoundMetaDataInfo[] items = new RaiderRoundMetaDataInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                RaiderRoundMetaDataInfo item = new RaiderRoundMetaDataInfo();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.State = (RaiderRoundState)Convert.ToInt32(dt.Rows[i]["State"]);
                if (dt.Rows[i]["StartTime"] != DBNull.Value)
                {
                    item.StartTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["StartTime"]));
                }
                if (dt.Rows[i]["AwardPoolSumStones"] != DBNull.Value)
                {
                    item.AwardPoolSumStones = Convert.ToInt32(dt.Rows[i]["AwardPoolSumStones"]);
                }
                if (dt.Rows[i]["JoinedPlayerCount"] != DBNull.Value)
                {
                    item.JoinedPlayerCount = Convert.ToInt32(dt.Rows[i]["JoinedPlayerCount"]);
                }
                if (dt.Rows[i]["WinnerUserName"] != DBNull.Value)
                {
                    item.WinnerUserName = DESEncrypt.DecryptDES(dt.Rows[i]["WinnerUserName"].ToString());
                }
                if (dt.Rows[i]["WinStones"] != DBNull.Value)
                {
                    item.WinStones = Convert.ToInt32(dt.Rows[i]["WinStones"]);
                }
                if (dt.Rows[i]["EndTime"] != DBNull.Value)
                {
                    item.EndTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["EndTime"]));
                }

                items[i] = item;
            }

            return items;
        }

        internal static PlayerBetInfo[] GetPlayerBetInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            PlayerBetInfo[] items = new PlayerBetInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                PlayerBetInfo item = new PlayerBetInfo();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.RaiderRoundID = Convert.ToInt32(dt.Rows[i]["RaiderRoundID"]);
                item.UserName = DESEncrypt.DecryptDES(dt.Rows[i]["UserName"].ToString());
                item.BetStones = Convert.ToInt32(dt.Rows[i]["BetStones"]);
                item.Time = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["Time"]));

                items[i] = item;
            }

            return items;
        }

        internal static XunLingMineStateInfo[] GetXunLingMineStateInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            XunLingMineStateInfo[] items = new XunLingMineStateInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                XunLingMineStateInfo item = new XunLingMineStateInfo();
                item.AllPlayerCount = Convert.ToInt32(dt.Rows[i]["AllPlayerCount"]);
                item.AllMinersCount = Convert.ToInt32(dt.Rows[i]["AllMinersCount"]);
                item.AllStonesReserves = Convert.ToInt32(dt.Rows[i]["AllStonesReserves"]);
                item.AllProducedStonesCount = Convert.ToDecimal(dt.Rows[i]["AllProducedStonesCount"]);
                item.AllStockOfStones = Convert.ToDecimal(dt.Rows[i]["AllStockOfStones"]);
                item.AllStonesCount = Convert.ToDecimal(dt.Rows[i]["AllStonesCount"]);

                items[i] = item;
            }

            return items;
        }

        internal static PlayerGravelRequsetRecordInfo[] GetPlayerGravelRequsetRecordInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            PlayerGravelRequsetRecordInfo[] items = new PlayerGravelRequsetRecordInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                PlayerGravelRequsetRecordInfo item = new PlayerGravelRequsetRecordInfo();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                item.UserName = DESEncrypt.DecryptDES(Convert.ToString(dt.Rows[i]["UserName"]));
                item.RequestDate = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["RequestDate"]));
                item.IsResponsed = Convert.ToBoolean(dt.Rows[i]["IsResponsed"]);
                if (dt.Rows[i]["ResponseDate"] != DBNull.Value)
                {
                    item.ResponseDate = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["ResponseDate"]));
                }
                item.Gravel = Convert.ToInt32(dt.Rows[i]["Gravel"]);
                item.IsGoted = Convert.ToBoolean(dt.Rows[i]["IsGoted"]);

                items[i] = item;
            }

            return items;
        }

        internal static GambleStoneRoundInfo[] GetGambleStoneRoundInfoFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            GambleStoneRoundInfo[] items = new GambleStoneRoundInfo[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                GambleStoneRoundInfo item = new GambleStoneRoundInfo();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.StartTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["StartTime"]));
                item.FinishedInningCount = Convert.ToInt32(dt.Rows[i]["FinishedInningCount"]);
                if (dt.Rows[i]["EndTime"] != DBNull.Value)
                {
                    item.EndTime = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["EndTime"]));
                }
                item.CurrentWinRedCount = Convert.ToInt32(dt.Rows[i]["CurrentWinRedCount"]);
                item.CurrentWinGreenCount = Convert.ToInt32(dt.Rows[i]["CurrentWinGreenCount"]);
                item.CurrentWinBlueCount = Convert.ToInt32(dt.Rows[i]["CurrentWinBlueCount"]);
                item.CurrentWinPurpleCount = Convert.ToInt32(dt.Rows[i]["CurrentWinPurpleCount"]);
                item.LastWinRedCount = Convert.ToInt32(dt.Rows[i]["LastWinRedCount"]);
                item.LastWinGreenCount = Convert.ToInt32(dt.Rows[i]["LastWinGreenCount"]);
                item.LastWinBlueCount = Convert.ToInt32(dt.Rows[i]["LastWinBlueCount"]);
                item.LastWinPurpleCount = Convert.ToInt32(dt.Rows[i]["LastWinPurpleCount"]);
                item.AllBetInStone = Convert.ToInt32(dt.Rows[i]["AllBetInStone"]);
                item.AllWinnedOutStone = Convert.ToInt32(dt.Rows[i]["AllWinnedOutStone"]);
                if (dt.Rows[i]["WinColorItems"] != DBNull.Value)
                {
                    byte[] buffer = (byte[])dt.Rows[i]["WinColorItems"];
                    int index = 0;
                    item.WinColorItems = BytesConverter.GetByteArrayFromBytes(buffer, index, out index);
                    if (item.WinColorItems == null || item.WinColorItems.Length == 0)
                    {
                        item.WinColorItems = new byte[64];
                    }
                }
                item.TableName = Convert.ToString(dt.Rows[i]["TableName"]);

                items[i] = item;
            }

            return items;
        }

        internal static GambleStoneDailyScheme[] GetGambleStoneDailySchemeFromDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            GambleStoneDailyScheme[] items = new GambleStoneDailyScheme[dt.Rows.Count];
            for (int i = 0; i < items.Length; i++)
            {
                GambleStoneDailyScheme item = new GambleStoneDailyScheme();
                item.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                item.Date = new MyDateTime(Convert.ToDateTime(dt.Rows[i]["Date"]));
                item.ProfitStoneObjective = Convert.ToInt32(dt.Rows[i]["ProfitStoneObjective"]);
                item.AllBetInStone = Convert.ToInt32(dt.Rows[i]["AllBetInStone"]);
                item.AllWinnedOutStone = Convert.ToInt32(dt.Rows[i]["AllWinnedOutStone"]);

                items[i] = item;
            }

            return items;
        }
    }
}
