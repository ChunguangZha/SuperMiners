using MetaData;
using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region WithdrawRMB

        public event EventHandler<WebInvokeEventArgs<RouletteAwardItem[]>> GetAwardItemsCompleted;
        public void GetAwardItems(object userState)
        {
            this._invoker.InvokeUserState<RouletteAwardItem[]>(this._context, "GetAwardItems", this.GetAwardItemsCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName);
        }

        #endregion

        #region StartRoulette

        public event EventHandler<WebInvokeEventArgs<RouletteWinAwardResult>> StartRouletteCompleted;
        public void StartRoulette(object userState)
        {
            this._invoker.InvokeUserState<RouletteWinAwardResult>(this._context, "StartRoulette", this.StartRouletteCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName);
        }

        #endregion

        #region FinishRoulette

        public event EventHandler<WebInvokeEventArgs<RouletteWinnerRecord>> FinishRouletteCompleted;
        public void FinishRoulette(int winAwardIndex, object userState)
        {
            this._invoker.InvokeUserState<RouletteWinnerRecord>(this._context, "FinishRoulette", this.FinishRouletteCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, winAwardIndex);
        }

        #endregion

        #region TakeRouletteAward

        public event EventHandler<WebInvokeEventArgs<RouletteAwardItem[]>> TakeRouletteAwardCompleted;
        public void TakeRouletteAward(int recordID, string info1, string info2, object userState)
        {
            this._invoker.InvokeUserState<RouletteAwardItem[]>(this._context, "TakeRouletteAward", this.TakeRouletteAwardCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, recordID, info1, info2);
        }

        #endregion

        #region GetAllWinAwardRecords

        public event EventHandler<WebInvokeEventArgs<RouletteWinnerRecord[]>> GetAllWinAwardRecordsCompleted;
        public void GetAllWinAwardRecords(int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex, object userState)
        {
            this._invoker.InvokeUserState<RouletteWinnerRecord[]>(this._context, "GetAllWinAwardRecords", this.GetAllWinAwardRecordsCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, RouletteAwardItemID, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex);
        }

        #endregion

        #region Callback

        public event Action<string> OnGameRouletteWinNotify;

        public void RaiseOnGameRouletteWinNotify(string awardInfo)
        {
            Action<string> handler = this.OnGameRouletteWinNotify;
            if (null != handler)
            {
                handler(awardInfo);
            }
        }

        public event Action<RouletteWinnerRecord> OnGameRouletteWinRealAwardPaySucceed;

        public void RaiseOnGameRouletteWinRealAwardPaySucceed(RouletteWinnerRecord record)
        {
            Action<RouletteWinnerRecord> handler = this.OnGameRouletteWinRealAwardPaySucceed;
            if (null != handler)
            {
                handler(record);
            }
        }


        #endregion

    }
}
