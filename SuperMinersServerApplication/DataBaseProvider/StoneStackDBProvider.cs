using MetaData.Game.StoneStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class StoneStackDBProvider
    {
        public StoneStackDailyRecordInfo GetLastStoneStackDailyRecordInfo()
        {
            return null;
        }

        public StoneDelegateSellOrderInfo[] GetAllWaitingStoneDelegateSellOrderInfo()
        {
            return null;
        }

        public StoneDelegateBuyOrderInfo[] GetAllWaitingStoneDelegateBuyOrderInfo()
        {
            return null;
        }

        public bool SaveFinishedStoneDelegateSellOrderInfo(StoneDelegateSellOrderInfo sellOrder)
        {
            return true;
        }

        public bool SaveWaitingStoneDelegateSellOrderInfo(StoneDelegateSellOrderInfo sellOrder)
        {
            return true;
        }

        public StoneDelegateSellOrderInfo[] GetAllStoneDelegateSellOrderInfoByPlayer(int userID)
        {
            return null;
        }

        public bool SaveFinishedStoneDelegateBuyOrderInfo(StoneDelegateBuyOrderInfo buyOrder)
        {
            return true;
        }

        public bool SaveWaitingStoneDelegateBuyOrderInfo(StoneDelegateBuyOrderInfo buyOrder)
        {
            return true;
        }

        public StoneDelegateBuyOrderInfo[] GetAllStoneDelegateBuyOrderInfoByPlayer(int userID)
        {
            return null;
        }

        public bool SaveStoneStackDailyRecordInfo(StoneStackDailyRecordInfo dailyInfo)
        {
            return true;
        }
    }
}
