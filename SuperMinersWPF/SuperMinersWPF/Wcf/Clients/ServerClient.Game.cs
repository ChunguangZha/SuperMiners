using MetaData.Trade;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region BuyMiner

        public event EventHandler<WebInvokeEventArgs<int>> BuyMinerCompleted;
        public void BuyMiner(int minersCount, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "BuyMiner", this.BuyMinerCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, minersCount);
        }

        #endregion

        #region BuyMine

        public event EventHandler<WebInvokeEventArgs<TradeOperResult>> BuyMineCompleted;
        public void BuyMine(int minesCount, int payType)
        {
            this._invoker.Invoke<TradeOperResult>(this._context, "BuyMine", this.BuyMineCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName, minesCount, payType);
        }

        #endregion

        #region GatherStones

        public event EventHandler<WebInvokeEventArgs<int>> GatherStonesCompleted;
        public void GatherStones(float stones)
        {
            this._invoker.Invoke<int>(this._context, "GatherStones", this.GatherStonesCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName, stones);
        }

        #endregion

        #region GoldCoinRecharge

        public event EventHandler<WebInvokeEventArgs<TradeOperResult>> GoldCoinRechargeCompleted;
        public void GoldCoinRecharge(int goldCoinCount, int payType)
        {
            this._invoker.Invoke<TradeOperResult>(this._context, "GoldCoinRecharge", this.GoldCoinRechargeCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName, goldCoinCount, payType);
        }

        #endregion

        #region GetExpTopList

        public event EventHandler<WebInvokeEventArgs<TopListInfo[]>> GetExpTopListCompleted;
        public void GetExpTopList()
        {
            this._invoker.Invoke<TopListInfo[]>(this._context, "GetExpTopList", this.GetExpTopListCompleted, GlobalData.Token);
        }

        #endregion

        #region GetStoneTopList

        public event EventHandler<WebInvokeEventArgs<TopListInfo[]>> GetStoneTopListCompleted;
        public void GetStoneTopList()
        {
            this._invoker.Invoke<TopListInfo[]>(this._context, "GetStoneTopList", this.GetStoneTopListCompleted, GlobalData.Token);
        }

        #endregion

        #region GetMinerTopList

        public event EventHandler<WebInvokeEventArgs<TopListInfo[]>> GetMinerTopListCompleted;
        public void GetMinerTopList()
        {
            this._invoker.Invoke<TopListInfo[]>(this._context, "GetMinerTopList", this.GetMinerTopListCompleted, GlobalData.Token);
        }

        #endregion

        #region GetGoldCoinTopList

        public event EventHandler<WebInvokeEventArgs<TopListInfo[]>> GetGoldCoinTopListCompleted;
        public void GetGoldCoinTopList()
        {
            this._invoker.Invoke<TopListInfo[]>(this._context, "GetGoldCoinTopList", this.GetGoldCoinTopListCompleted, GlobalData.Token);
        }

        #endregion

        #region GetReferrerTopList

        public event EventHandler<WebInvokeEventArgs<TopListInfo[]>> GetReferrerTopListCompleted;
        public void GetReferrerTopList()
        {
            this._invoker.Invoke<TopListInfo[]>(this._context, "GetReferrerTopList", this.GetReferrerTopListCompleted, GlobalData.Token);
        }

        #endregion
    }
}
