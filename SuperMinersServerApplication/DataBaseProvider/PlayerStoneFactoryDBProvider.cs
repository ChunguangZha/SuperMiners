using MetaData;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class PlayerStoneFactoryDBProvider
    {
        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(int userID)
        {
            return null;
        }

        public StoneFactoryOutputRMBRecord[] GetProfitRecords(int userID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return null;
        }

        public bool OpenFactory(int userID, CustomerMySqlTransaction myTrans)
        {
            //需先检查数据库中是否存在工厂信息，如果没有则添加
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="stoneStackCount">矿石股数（一万矿石为一股）</param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public bool JoinInStone(int userID, int stoneStackCount, CustomerMySqlTransaction myTrans)
        {

        }

        public bool WithdrawStone(int userID, 

        public bool AddNewStackChangeRecord(int userID, int stoneStackCount, CustomerMySqlTransaction myTrans)
        {
            StoneFactoryStackChangeRecord record = new StoneFactoryStackChangeRecord()
            {
                UserID = userID,
                JoinStoneStackCount = stoneStackCount,
                Time = new MyDateTime()
            };

        }
    }
}
