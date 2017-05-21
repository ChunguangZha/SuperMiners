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
    }
}
