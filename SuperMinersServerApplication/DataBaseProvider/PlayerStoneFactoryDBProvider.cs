using MetaData;
using MetaData.StoneFactory;
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

        public StoneFactoryProfitRMBChangedRecord[] GetProfitRecords(int userID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return null;
        }

        public StoneFactoryStackChangeRecord[] GetFactoryStackChangedRecord(int userID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
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
        /// <param name="isJoinIn">true表示投入；false表示提取</param>
        /// <param name="stoneStackCount"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public bool AddNewStackChangeRecord(int userID, bool isJoinIn, int stoneStackCount, CustomerMySqlTransaction myTrans)
        {
            StoneFactoryStackChangeRecord record = new StoneFactoryStackChangeRecord()
            {
                UserID = userID,
                JoinStoneStackCount = isJoinIn ? stoneStackCount : -stoneStackCount,
                Time = new MyDateTime()
            };

            return false;
        }

        public bool AddMiners(int userID, int minersCount, CustomerMySqlTransaction myTrans)
        {
            StoneFactoryOneGroupSlave slave = new StoneFactoryOneGroupSlave()
            {
                UserID = userID,
                ChargeTime = new MyDateTime(),
                JoinInSlaveCount = minersCount,
                isLive = true,
                LifeDays = 2,
                LiveSlaveCount = minersCount
            };

            return false;
        }

        public bool AddFoods(int userID, int foodsCount, CustomerMySqlTransaction myTrans)
        {
            //直接修改字段值
            return false;
        }

        public bool AddProfitRMBChangedRecord(int userID, int operRMB, FactoryProfitOperType operType, CustomerMySqlTransaction myTrans)
        {
            StoneFactoryProfitRMBChangedRecord record = new StoneFactoryProfitRMBChangedRecord()
            {
                UserID = userID,
                OperRMB = operType == FactoryProfitOperType.WithdrawRMB? -operRMB : operRMB,
                OperTime = new MyDateTime(),
                ProfitType = operType
            };

            return false;
        }

    }
}
