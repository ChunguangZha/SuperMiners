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
        public bool SaveRouletteAwardItems(RouletteAwardItem[] items)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlDeleteText = "delete from rouletteawarditem;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlDeleteText;
                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                foreach (var item in items)
                {
                    string sqlInsertText = "insert into rouletteawarditem " +
                        " (`AwardName`, `AwardNumber`, `RouletteAwardType`, `ValueMoneyYuan`, `IsLargeAward`, `IsRealAward`, `WinProbability`) " +
                        " values (@AwardName, @AwardNumber, @RouletteAwardType, @ValueMoneyYuan, @IsLargeAward, @IsRealAward, @WinProbability)";

                    mycmd = myconn.CreateCommand();
                    mycmd.CommandText = sqlInsertText;
                    mycmd.Parameters.AddWithValue("@AwardName", item.AwardName);
                    mycmd.Parameters.AddWithValue("@AwardNumber", item.AwardNumber);
                    mycmd.Parameters.AddWithValue("@RouletteAwardType", (int)item.RouletteAwardType);
                    mycmd.Parameters.AddWithValue("@ValueMoneyYuan", item.ValueMoneyYuan);
                    mycmd.Parameters.AddWithValue("@IsLargeAward", item.IsLargeAward);
                    mycmd.Parameters.AddWithValue("@IsRealAward", item.IsRealAward);
                    mycmd.Parameters.AddWithValue("@WinProbability", item.WinProbability);

                    mycmd.ExecuteNonQuery();
                    mycmd.Dispose();
                }

                mycmd = null;

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

        public RouletteAwardItem[] GetRouletteAwardItems()
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
                mycmd.Parameters.AddWithValue("@WinTime", record.WinTime);
                mycmd.Parameters.AddWithValue("@IsGot", record.IsGot);
                if (record.GotTime == null)
                {
                    mycmd.Parameters.AddWithValue("@GotTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@GotTime", record.GotTime);
                }
                mycmd.Parameters.AddWithValue("@IsPay", record.IsPay);
                if (record.PayTime == null)
                {
                    mycmd.Parameters.AddWithValue("@PayTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);
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

        public RouletteWinnerRecord GetPayWinAwardRecord(string UserName, int RouletteAwardItemID, DateTime WinTime)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlTextA = "select  r.*, s.UserName, s.NickName from roulettewinnerrecord r left join playersimpleinfo s on r.UserID = s.id  " +
                    " where s.UserName = @UserName and r.AwardItemID = @AwardItemID and r.WinTime >= @WinTime";

                mycmd = myconn.CreateCommand();
                string encryptUserName = DESEncrypt.EncryptDES(UserName);
                mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                mycmd.Parameters.AddWithValue("@AwardItemID", RouletteAwardItemID);
                mycmd.Parameters.AddWithValue("@WinTime", WinTime);
                mycmd.CommandText = sqlTextA;
                myconn.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                var records = MetaDBAdapter<RouletteWinnerRecord>.GetRouletteWinnerRecordFromDataTable(table);
                if (records != null && records.Length == 1)
                {
                    return records[0];
                }

                return null;
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
                mycmd.Parameters.AddWithValue("@GotTime", record.GotTime);
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
                mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);
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
        /// <param name="BeginWinTime"></param>
        /// <param name="EndWinTime"></param>
        /// <param name="IsGot">-1表示null;0表示false;1表示true</param>
        /// <param name="IsPay">-1表示null;0表示false;1表示true</param>
        /// <param name="pageItemCount"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public RouletteWinnerRecord[] GetAllPayWinAwardRecords(string UserName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex, RouletteAwardItem noneAwardItem)
        {
            RouletteWinnerRecord[] records = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                string sqlTextA = "select  r.*, s.UserName, s.NickName as UserNickName from roulettewinnerrecord r left join playersimpleinfo s on r.UserID = s.id  ";

                StringBuilder builder = new StringBuilder();

                builder.Append(" r.AwardItemID != @AwardItemID ");
                mycmd.Parameters.AddWithValue("@AwardItemID", noneAwardItem.ID);

                if (!string.IsNullOrEmpty(UserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.UserName = @UserName ");
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

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;

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
                string sqlText = "select  r.*, s.UserName, s.NickName as UserNickName from roulettewinnerrecord r left join playersimpleinfo s on r.UserID = s.id where r.IsPay = @IsPay";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
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
