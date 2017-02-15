using MetaData;
using MetaData.Game.Roulette;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GameRouletteDBProvider
    {
        public bool AddRouletteAwardItem(RouletteAwardItem item)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "insert into rouletteawarditem " +
                    " (`AwardName`, `AwardNumber`, `RouletteAwardType`, `ValueMoneyYuan`, `IsLargeAward`, `WinProbability`, `IconBuffer`) " +
                    " values (@AwardName, @AwardNumber, @RouletteAwardType, @ValueMoneyYuan, @IsLargeAward, @WinProbability, @IconBuffer)";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@AwardName", item.AwardName);
                mycmd.Parameters.AddWithValue("@AwardNumber", item.AwardNumber);
                mycmd.Parameters.AddWithValue("@RouletteAwardType", (int)item.RouletteAwardType);
                mycmd.Parameters.AddWithValue("@ValueMoneyYuan", item.ValueMoneyYuan);
                mycmd.Parameters.AddWithValue("@IsLargeAward", item.IsLargeAward);
                mycmd.Parameters.AddWithValue("@WinProbability", item.WinProbability);
                mycmd.Parameters.AddWithValue("@IconBuffer", item.IconBuffer);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool UpdateRouletteAwardItem(RouletteAwardItem item)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "update rouletteawarditem " +
                    " set `AwardName` = @AwardName, `AwardNumber` = @AwardNumber, `RouletteAwardType` = @RouletteAwardType, `ValueMoneyYuan` = @ValueMoneyYuan, `IsLargeAward` = @IsLargeAward, `WinProbability` = @WinProbability, `IconBuffer` = @IconBuffer " +
                    " where id = @id ;";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@AwardName", item.AwardName);
                mycmd.Parameters.AddWithValue("@AwardNumber", item.AwardNumber);
                mycmd.Parameters.AddWithValue("@RouletteAwardType", (int)item.RouletteAwardType);
                mycmd.Parameters.AddWithValue("@ValueMoneyYuan", item.ValueMoneyYuan);
                mycmd.Parameters.AddWithValue("@IsLargeAward", item.IsLargeAward);
                mycmd.Parameters.AddWithValue("@WinProbability", item.WinProbability);
                mycmd.Parameters.AddWithValue("@IconBuffer", item.IconBuffer);
                mycmd.Parameters.AddWithValue("@id", item.ID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool DeleteAwardItem(RouletteAwardItem item)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlText = "delete rouletteawarditem where id = @id ;";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@id", item.ID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        private bool UpdateRouletteAwardItem_WinProbability(RouletteAwardItem[] items, CustomerMySqlTransaction trans)
        {
            foreach (var item in items)
            {
                string sqlInsertText = "update rouletteawarditem " +
                    " set `WinProbability` = @WinProbability where id = @id; ";

                MySqlCommand mycmd = trans.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@id", item.ID);
                mycmd.Parameters.AddWithValue("@AwardName", item.AwardName);
                mycmd.Parameters.AddWithValue("@AwardNumber", item.AwardNumber);
                mycmd.Parameters.AddWithValue("@RouletteAwardType", (int)item.RouletteAwardType);
                mycmd.Parameters.AddWithValue("@ValueMoneyYuan", item.ValueMoneyYuan);
                mycmd.Parameters.AddWithValue("@IsLargeAward", item.IsLargeAward);
                mycmd.Parameters.AddWithValue("@WinProbability", item.WinProbability);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
            }

            return true;
        }

        private bool UpdateCurrentAwardItemList(RouletteAwardItem[] items, CustomerMySqlTransaction trans)
        {
            string sqlDeleteText = "delete from currentrouletteawarditemlist;";
            MySqlCommand mycmd = trans.CreateCommand();
            mycmd.CommandText = sqlDeleteText;
            mycmd.ExecuteNonQuery();
            mycmd.Dispose();

            for (int i=0;i<items.Length;i++)
            {
                var item = items[i];
                string sqlInsertText = "insert into currentrouletteawarditemlist " +
                    " (`Index`, `AwarditemID`) " +
                    " values (@Index, @AwarditemID)";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@Index", i);
                mycmd.Parameters.AddWithValue("@AwarditemID", item.ID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
            }

            return true;
        }

        /// <summary>
        /// 保存从所有奖项中选出的12个
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool SaveCurrentRouletteAwardItemsList(RouletteAwardItem[] items)
        {
            CustomerMySqlTransaction trans = null;
            try
            {
                trans = MyDBHelper.Instance.CreateTrans();
                UpdateRouletteAwardItem_WinProbability(items, trans);
                UpdateCurrentAwardItemList(items, trans);

                trans.Commit();
                return true;
            }
            catch (Exception exc)
            {
                trans.Rollback();
                throw exc;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        public RouletteAwardItem[] GetAllRouletteAwardItems()
        {
            RouletteAwardItem[] items = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select * from rouletteawarditem";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                items = MetaDBAdapter<RouletteAwardItem>.GetRouletteAwardItemFromDataTable(table);
                return items;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public RouletteAwardItem[] GetCurrentRouletteAwardItemsList()
        {
            RouletteAwardItem[] items = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select c.`Index`, c.`AwarditemID`, r.* from currentrouletteawarditemlist c left join rouletteawarditem r on c.`AwarditemID` = r.`id` ;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                items = MetaDBAdapter<RouletteAwardItem>.GetRouletteAwardItemFromDataTable(table);
                return items;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public RouletteRoundInfo[] GetAllRouletteRoundInfo()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select * from rouletteroundinfo ;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                var items = MetaDBAdapter<RouletteRoundInfo>.GetRouletteRoundInfoFromDataTable(table);
                return items;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public RouletteRoundInfo GetLastRouletteRoundInfo()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select * from rouletteroundinfo order by id desc limit 1 ;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                var items = MetaDBAdapter<RouletteRoundInfo>.GetRouletteRoundInfoFromDataTable(table);
                if (items == null ||items.Length == 0)
                {
                    return null;
                }

                return items[0];
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool SaveRouletteRoundInfo(RouletteRoundInfo info)
        {
            bool isAdd = false;
            var lastInfo = GetLastRouletteRoundInfo();
            if (lastInfo == null)
            {
                isAdd = true;
            }
            else if (lastInfo.Finished)
            {
                isAdd = true;
            }
            else
            {
                isAdd = false;
            }

            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string sqlInsertText = "";

                if (isAdd)
                {
                    sqlInsertText = "insert into rouletteroundinfo " +
                        " (`AwardPoolSumStone`, `WinAwardSumYuan`, `StartTime`, `MustWinAwardItemID`, `Finished`) " +
                        " values (@AwardPoolSumStone, @WinAwardSumYuan, @StartTime, @MustWinAwardItemID, @Finished )";
                }
                else
                {
                    sqlInsertText = "update rouletteroundinfo " +
                        " set `AwardPoolSumStone` = @AwardPoolSumStone, `WinAwardSumYuan` = @WinAwardSumYuan, `StartTime` = @StartTime, `MustWinAwardItemID` = @MustWinAwardItemID, `Finished` = @Finished " +
                        " where id = @id ;";
                    mycmd.Parameters.AddWithValue("@id", lastInfo.ID);
                }
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@AwardPoolSumStone", info.AwardPoolSumStone);
                mycmd.Parameters.AddWithValue("@WinAwardSumYuan", info.WinAwardSumYuan);
                mycmd.Parameters.AddWithValue("@StartTime", info.StartTime);
                mycmd.Parameters.AddWithValue("@MustWinAwardItemID", info.MustWinAwardItemID);
                mycmd.Parameters.AddWithValue("@Finished", info.Finished);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool AddRouletteWinnerRecord(RouletteWinnerRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "insert into roulettewinnerrecord " +
                    " (`UserID`, `AwardItemID`, `WinTime`, `IsGot`, `GotTime`, `IsPay`, `PayTime`, `GotInfo1`, `GotInfo2`) " +
                    " values (@UserID, @AwardItemID, @WinTime, @IsGot, @GotTime, @IsPay, @PayTime, @GotInfo1, @GotInfo2)";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@AwardItemID", record.AwardItem.ID);
                mycmd.Parameters.AddWithValue("@WinTime", record.WinTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@IsGot", record.IsGot);
                if (record.GotTime == null)
                {
                    mycmd.Parameters.AddWithValue("@GotTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@GotTime", record.GotTime.ToDateTime());
                }
                mycmd.Parameters.AddWithValue("@IsPay", record.IsPay);
                if (record.PayTime == null)
                {
                    mycmd.Parameters.AddWithValue("@PayTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@PayTime", record.PayTime.ToDateTime());
                }
                mycmd.Parameters.AddWithValue("@GotInfo1", DESEncrypt.EncryptDES(record.GotInfo1));
                mycmd.Parameters.AddWithValue("@GotInfo2", DESEncrypt.EncryptDES(record.GotInfo2));

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public RouletteWinnerRecord GetPayWinAwardRecord(int UserID, int RouletteAwardItemID, DateTime WinTime)
        {
            RouletteWinnerRecord record = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                DataTable table = new DataTable();

                string sqlTextB = "select  r.* from roulettewinnerrecord r " +
                    " where  r.UserID = @UserID and r.AwardItemID = @AwardItemID order by r.id desc limit 1;";// and r.WinTime >= @WinTime";

                mycmd = myconn.CreateCommand();
                mycmd.Parameters.AddWithValue("@UserID", UserID);
                mycmd.Parameters.AddWithValue("@AwardItemID", RouletteAwardItemID);
                //mycmd.Parameters.AddWithValue("@WinTime", WinTime);


                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextB +
                                    " ) ttt " +
                                    "  left join  superminers.playersimpleinfo s  on ttt.UserID = s.id ";

                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var records = MetaDBAdapter<RouletteWinnerRecord>.GetRouletteWinnerRecordFromDataTable(table);
                if (records != null && records.Length != 0)
                {
                    record = records[records.Length - 1];
                    if (record.RouletteAwardItemID != RouletteAwardItemID)
                    {
                        return null;
                    }
                }

                return record;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool SetWinnerRecordGot(RouletteWinnerRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "update roulettewinnerrecord " +
                    " set `IsGot` = @IsGot, `GotTime` = @GotTime, `GotInfo1` = @GotInfo1, `GotInfo2` = @GotInfo2 where `id` = @ID ";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@IsGot", record.IsGot);
                mycmd.Parameters.AddWithValue("@GotTime", record.GotTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@GotInfo1", DESEncrypt.EncryptDES(record.GotInfo1));
                mycmd.Parameters.AddWithValue("@GotInfo2", DESEncrypt.EncryptDES(record.GotInfo2));
                mycmd.Parameters.AddWithValue("@ID", record.RecordID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool SetWinnerRecordPay(RouletteWinnerRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "update roulettewinnerrecord " +
                    " set `IsPay` = @IsPay, `PayTime` = @PayTime where `id` = @ID ";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@IsPay", record.IsPay);
                mycmd.Parameters.AddWithValue("@PayTime", record.PayTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@ID", record.RecordID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        /// <summary>
        /// 没有填充AwardItem属性
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RouletteAwardItemID"></param>
        /// <param name="ContainsNone"></param>
        /// <param name="BeginWinTime"></param>
        /// <param name="EndWinTime"></param>
        /// <param name="IsGot">-1表示null;0表示false;1表示true</param>
        /// <param name="IsPay">-1表示null;0表示false;1表示true</param>
        /// <param name="pageItemCount"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public RouletteWinnerRecord[] GetAllPayWinAwardRecords(string UserName, int RouletteAwardItemID, bool ContainsNone, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex, RouletteAwardItem noneAwardItem)
        {
            RouletteWinnerRecord[] records = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                string sqlTextA = "select  r.*, m.AwardName, m.RouletteAwardType " +
                                    " from roulettewinnerrecord r " + 
                                    " left join rouletteawarditem m on r.AwardItemID = m.id ";

                StringBuilder builder = new StringBuilder();

                //builder.Append(" r.AwardItemID != @AwardItemID ");
                //mycmd.Parameters.AddWithValue("@AwardItemID", noneAwardItem.ID);

                if (!string.IsNullOrEmpty(UserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" r.UserID = ( select id from  superminers.playersimpleinfo where UserName = @UserName ) ");
                    string encryptUserName = DESEncrypt.EncryptDES(UserName);
                    mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                }

                if (RouletteAwardItemID >= 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" r.AwardItemID = @AwardItemID ");
                    mycmd.Parameters.AddWithValue("@AwardItemID", RouletteAwardItemID);
                }
                else
                {
                    //如果外部传入中奖类型，则不判断为NONE的情况
                    if (!ContainsNone)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append(" and ");
                        }
                        builder.Append(" m.RouletteAwardType != @RouletteAwardType ");
                        mycmd.Parameters.AddWithValue("@RouletteAwardType", RouletteAwardType.None);
                    }
                }
                if (BeginWinTime != null && !BeginWinTime.IsNull && EndWinTime != null && !EndWinTime.IsNull)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginWinTime = BeginWinTime.ToDateTime();
                    DateTime endWinTime = EndWinTime.ToDateTime();
                    if (beginWinTime >= endWinTime)
                    {
                        return null;
                    }
                    builder.Append(" r.WinTime >= @beginWinTime and r.WinTime < @endWinTime ");
                    mycmd.Parameters.AddWithValue("@beginWinTime", beginWinTime);
                    mycmd.Parameters.AddWithValue("@endWinTime", endWinTime);
                }
                if (IsGot >= 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" r.IsGot = @IsGot ");
                    mycmd.Parameters.AddWithValue("@IsGot", IsGot != 0);
                }
                if (IsPay >= 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" r.IsPay = @IsPay ");
                    mycmd.Parameters.AddWithValue("@IsPay", IsPay != 0);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by r.id desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join  superminers.playersimpleinfo s  on ttt.UserID = s.id ";

                mycmd.CommandText = sqlAllText;
                myconn.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                records = MetaDBAdapter<RouletteWinnerRecord>.GetRouletteWinnerRecordFromDataTable(table);
                return records;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        /// <summary>
        /// 没有填充AwardItem属性
        /// </summary>
        /// <returns></returns>
        public RouletteWinnerRecord[] GetNotPayWinAwardRecords()
        {
            RouletteWinnerRecord[] records = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select  r.*, m.AwardName, m.RouletteAwardType " +
                                    " from roulettewinnerrecord r " +
                                    " left join rouletteawarditem m on r.AwardItemID = m.id " +
                                    " where m.RouletteAwardType = @RouletteAwardType and r.IsPay = @IsPay ";

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlText +
                                    " ) ttt " +
                                    "  left join  superminers.playersimpleinfo s  on ttt.UserID = s.id ";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlAllText;
                mycmd.Parameters.AddWithValue("@RouletteAwardType", RouletteAwardType.RealAward);
                mycmd.Parameters.AddWithValue("@IsPay", false);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                records = MetaDBAdapter<RouletteWinnerRecord>.GetRouletteWinnerRecordFromDataTable(table);
                return records;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }
    }
}
