using DataBaseProvider;
using MetaData;
using MetaData.AgentUser;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Trade
{
    public class AgentAwardController
    {
        #region Single

        private static AgentAwardController _instance = new AgentAwardController();

        internal static AgentAwardController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        public bool PlayerRechargeRMB(PlayerInfo player, AgentAwardType awardType, decimal playerInchargeRMB, CustomerMySqlTransaction trans)
        {
            if (player.SimpleInfo.IsAgentReferred || player.FortuneInfo.Exp < 50
                || player.SimpleInfo.AgentUserID == 0 
                || player.SimpleInfo.AgentReferredLevel == 0 || player.SimpleInfo.AgentReferredLevel > 2)
            {
                return false;
            }

            var agent = GetReferredAgent(player);
            if (agent == null)
            {
                return false;
            }

            AgentAwardRecord record = new AgentAwardRecord();
            record.AgentID = agent.ID;
            record.AgentUserName = agent.Player.SimpleInfo.UserName;
            record.PlayerID = player.SimpleInfo.UserID;
            record.PlayerUserName = player.SimpleInfo.UserName;
            record.Time = MyDateTime.FromDateTime(DateTime.Now);
            switch (awardType)
            {
                case AgentAwardType.PlayerAgentExp:
                    record.PlayerInchargeRMB = 0;
                    record.AgentAwardRMB = 30 * GlobalConfig.GameConfig.Yuan_RMB;
                    record.PlayerInchargeContent = "玩家贡献值达到50奖励";
                    break;
                case AgentAwardType.PlayerInchargeGoldCoin:
                    record.PlayerInchargeRMB = playerInchargeRMB;
                    if (player.SimpleInfo.AgentReferredLevel == 1)
                    {
                        record.AgentAwardRMB = 0.1m * playerInchargeRMB;
                    }
                    else if (player.SimpleInfo.AgentReferredLevel == 2)
                    {
                        record.AgentAwardRMB = 0.05m * playerInchargeRMB;
                    }
                    record.PlayerInchargeContent = "玩家用充值金币奖励";
                    break;
                case AgentAwardType.PlayerInchargeMine:
                    record.PlayerInchargeRMB = playerInchargeRMB;
                    if (player.SimpleInfo.AgentReferredLevel == 1)
                    {
                        record.AgentAwardRMB = 0.1m * playerInchargeRMB;
                    }
                    else if (player.SimpleInfo.AgentReferredLevel == 2)
                    {
                        record.AgentAwardRMB = 0.05m * playerInchargeRMB;
                    }
                    record.PlayerInchargeContent = "玩家购买矿山奖励";
                    break;
                default:
                    break;
            }

            if (record.AgentAwardRMB > 1)
            {
                DBProvider.AgentAwardRecordDBProvider.AddAgentAwardRecord(record, trans);
            }

            return true;
        }

        private AgentUserInfo GetReferredAgent(PlayerInfo player)
        {
            PlayerInfo AgentUser = DBProvider.UserDBProvider.GetPlayerByUserID(player.SimpleInfo.AgentUserID);
            if (AgentUser == null)
            {
                return null;
            }

            AgentUserInfo agent = DBProvider.AgentUserInfoDBProvider.GetAgentUserInfo(AgentUser);
            return agent;
        }
    }

    public enum AgentAwardType
    {
        PlayerAgentExp,
        PlayerInchargeGoldCoin,
        PlayerInchargeMine,
    }
}
