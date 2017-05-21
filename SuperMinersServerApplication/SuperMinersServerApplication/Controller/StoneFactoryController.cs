using DataBaseProvider;
using MetaData;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    /// <summary>
    /// 10000矿石为一股投入到加工厂（存储必须1万的倍数），加工厂凝练每天分配给相应比例的灵币，
    /// 18天后可以转入灵币账户进行提现或消费操作，凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，
    /// 加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
    /// </summary>
    public class StoneFactoryController
    {
        //凝练按天计算，每天0点计算前一天数值，为了简化计算，当前投入的矿石，矿工，食物，都是以第二天0时开始计算。
        //每天23：50分到次到1点禁止充值矿石、矿工、食物。

        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(int userID)
        {
            return DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
        }

        public StoneFactoryOutputRMBRecord[] GetProfitRecords(int userID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.PlayerStoneFactoryDBProvider.GetProfitRecords(userID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public int OpenFactory(int userID, string userName)
        {
            //工厂开启状态。开启一次 1000积分。72小时 没有存入矿石和苦力 就 在关闭
            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                int operResult = PlayerController.Instance.OpenFactory(userID, userName, myTrans);
                if (operResult == OperResult.RESULTCODE_TRUE)
                {
                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.OpenFactory(userID, myTrans);
                    return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                }
                return operResult;
            }, exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });

            return result;
        }

        public int AddStone(int userID, int stoneCount)
        {
            //1万矿石可以投入一股，30天后，可撤回到玩家矿石账户

            return OperResult.RESULTCODE_FALSE;
        }

        public int RemoveStone(int userID, int stoneCount)
        {

            return OperResult.RESULTCODE_FALSE;
        }

        public int AddMiners(int userID, int minersCount)
        {

            return OperResult.RESULTCODE_FALSE;
        }

        public int AddFoods(int userID, int foodsCount)
        {

            return OperResult.RESULTCODE_FALSE;
        }

        public int WithdrawOutputRMB(int userID, int withdrawRMBCount)
        {

            return OperResult.RESULTCODE_FALSE;
        }

    }
}
