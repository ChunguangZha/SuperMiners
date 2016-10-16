using MetaData.AgentUser;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class AgentAwardRecordDBProvider
    {
        public bool AddAgentAwardRecord(AgentAwardRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string sqlText = "insert into agentawardrecord " +
                    " (`AgentID`, `AgentUserName`, `PlayerID`, `PlayerUserName`, `PlayerInchargeRMB`, `AgentAwardRMB`, `PlayerInchargeContent`, `Time`) " +
                    " values (@AgentID, @AgentUserName, @PlayerID, @PlayerUserName, @PlayerInchargeRMB, @AgentAwardRMB, @PlayerInchargeContent, @Time)";
                mycmd = myTrans.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@AgentID", record.AgentID);
                mycmd.Parameters.AddWithValue("@AgentUserName", DESEncrypt.EncryptDES(record.AgentUserName));
                mycmd.Parameters.AddWithValue("@PlayerID", record.PlayerID);
                mycmd.Parameters.AddWithValue("@PlayerUserName", DESEncrypt.EncryptDES(record.PlayerUserName));
                mycmd.Parameters.AddWithValue("@PlayerInchargeRMB", record.PlayerInchargeRMB);
                mycmd.Parameters.AddWithValue("@AgentAwardRMB", record.AgentAwardRMB);
                mycmd.Parameters.AddWithValue("@PlayerInchargeContent", record.PlayerInchargeContent);
                mycmd.Parameters.AddWithValue("@Time", record.Time.ToDateTime());
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public AgentAwardRecord[] GetAgentAwardRecords(int agentID)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                string sqlText = "select * from agentawardrecord where AgentID = @AgentID";

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@AgentID", agentID);

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                adapter.Dispose();
                return MetaDBAdapter<AgentAwardRecord>.GetAgentAwardRecordFromDataTable(table);
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
