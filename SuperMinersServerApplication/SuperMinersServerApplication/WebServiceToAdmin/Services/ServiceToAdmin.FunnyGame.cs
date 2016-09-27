using MetaData;
using MetaData.Game.Roulette;
using SuperMinersServerApplication.Controller.Game;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    public partial class ServiceToAdmin : IServiceToAdmin
    {

        public MetaData.Game.Roulette.RouletteAwardItem[] GetAwardItems(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return RouletteAwardController.Instance.GetAwardItems();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.GetAwardItems Exception.", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool SetAwardItems(string token, MetaData.Game.Roulette.RouletteAwardItem[] items)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    //TODO: 不应该删除已有的奖项信息，否则中奖记录也要丢失，需十一之后处理
                    return RouletteAwardController.Instance.SetAwardItems(items);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.SetAwardItems Exception.", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.Roulette.RouletteWinnerRecord[] GetNotPayWinAwardRecords(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return RouletteAwardController.Instance.GetNotPayWinAwardRecords();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.GetNotPayWinAwardRecords Exception.", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public RouletteWinnerRecord[] GetAllPayWinAwardRecords(string token, string UserName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return RouletteAwardController.Instance.GetAllPayWinAwardRecords(UserName, RouletteAwardItemID, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.GetAllPayWinAwardRecords Exception.", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int PayAward(string token, string adminUserName, string playerUserName, int recordID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return RouletteAwardController.Instance.PayAward(adminUserName, playerUserName, recordID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.PayAward Exception. adminUserName:" + adminUserName + ", playerUserName:" + playerUserName + ", recordID:" + recordID, exc);
                    return OperResult.RESULTCODE_FALSE;
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
