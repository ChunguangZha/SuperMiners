using MetaData.Game.GambleStone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region GambleStoneBetIn

        public event EventHandler<WebInvokeEventArgs<GambleStonePlayerBetInResult>> GambleStoneBetInCompleted;
        public void GambleStoneBetIn(GambleStoneItemColor color, int stoneCount, int gravelCount, object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<GambleStonePlayerBetInResult>(this._context, "GambleStoneBetIn", this.GambleStoneBetInCompleted, userState, GlobalData.Token, color, stoneCount, gravelCount);
            }
        }

        #endregion

        #region GetGambleStoneRoundInning

        public event EventHandler<WebInvokeEventArgs<GambleStoneRound_InningInfo>> GetGambleStoneRoundInningCompleted;
        public void GetGambleStoneRoundInning(object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<GambleStoneRound_InningInfo>(this._context, "GetGambleStoneRoundInning", this.GetGambleStoneRoundInningCompleted, userState, GlobalData.Token);
            }
        }

        #endregion

        #region Callback

        public event Action<GambleStoneRoundInfo, GambleStoneInningInfo, GambleStonePlayerBetRecord> OnGambleStoneWinNotify;

        public void RaiseOnGambleStoneWinNotify(GambleStoneRoundInfo roundInfo, GambleStoneInningInfo inningInfo, GambleStonePlayerBetRecord maxWinner)
        {
            Action<GambleStoneRoundInfo, GambleStoneInningInfo, GambleStonePlayerBetRecord> handler = this.OnGambleStoneWinNotify;
            if (null != handler)
            {
                handler(roundInfo, inningInfo, maxWinner);
            }
        }


        #endregion

    }
}
