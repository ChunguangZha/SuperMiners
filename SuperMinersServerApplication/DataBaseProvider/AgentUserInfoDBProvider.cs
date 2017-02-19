using MetaData.AgentUser;
using MetaData.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class AgentUserInfoDBProvider
    {
        public bool AddAgentUser(AgentUserInfo info, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string sqlText = "insert into agentuserinfo " +
                    " (`UserID`,`TotalAwardRMB`,`InvitationURL`) " +
                    " values (@UserID,@TotalAwardRMB,@InvitationURL)";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", info.Player.SimpleInfo.UserID);
                mycmd.Parameters.AddWithValue("@TotalAwardRMB", info.TotalAwardRMB);
                mycmd.Parameters.AddWithValue("@InvitationURL", info.InvitationURL);
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

        public AgentUserInfo GetAgentUserInfo(PlayerInfo player)
        {
            if (player.SimpleInfo.GroupType != PlayerGroupType.AgentPlayer)
            {
                return null;
            }

            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlTextA = "select * from agentuserinfo where UserID = @UserID; ";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@UserID", player.SimpleInfo.UserID);
                myconn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                DataTable tableAgents = new DataTable();
                adapter.Fill(tableAgents);
                if (tableAgents.Rows.Count == 0)
                {
                    return null;
                }

                AgentUserInfo agent = new AgentUserInfo();
                agent.ID = Convert.ToInt32(tableAgents.Rows[0]["id"]);
                agent.TotalAwardRMB = Convert.ToDecimal(tableAgents.Rows[0]["TotalAwardRMB"]);
                agent.InvitationURL = Convert.ToString(tableAgents.Rows[0]["InvitationURL"]);
                agent.Player = player;
                tableAgents.Clear();
                tableAgents.Dispose();
                adapter.Dispose();

                return agent;
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

        public AgentUserInfo[] GetAllAgentUsers()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlTextA = "select * from agentuserinfo; ";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                myconn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                DataTable tableAgents = new DataTable();
                adapter.Fill(tableAgents);
                if (tableAgents.Rows.Count == 0)
                {
                    return null;
                }

                DataTable tablePlayers = new DataTable();
                string sqlTextB = "select a.*, c.UserName as ReferrerUserName, b.* , g.Gravel, g.FirstGetGravelTime from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId left join playergravelinfo g on a.id = g.UserID  where a.GroupType = @GroupType";
                mycmd = new MySqlCommand(sqlTextB, myconn);
                mycmd.Parameters.AddWithValue("@GroupType", (int)PlayerGroupType.AgentPlayer);
                adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(tablePlayers);
                var players = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(tablePlayers);
                if (players == null)
                {
                    return null;
                }

                var lists = MetaDBAdapter<AgentUserInfo>.GetAgentUserInfoFromDataTable(tableAgents, players);
                tablePlayers.Clear();
                tablePlayers.Dispose();
                adapter.Dispose();

                return lists;
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
