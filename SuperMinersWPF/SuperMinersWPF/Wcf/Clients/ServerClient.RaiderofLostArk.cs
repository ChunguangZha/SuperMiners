using MetaData.Game.RaideroftheLostArk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region GetPlayerRaiderRoundHistoryRecordInfo

        public event EventHandler<WebInvokeEventArgs<PlayerRaiderRoundHistoryRecordInfo[]>> GetPlayerRaiderRoundHistoryRecordInfoCompleted;
        public void GetPlayerRaiderRoundHistoryRecordInfo(int pageItemCount, int pageIndex, object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<PlayerRaiderRoundHistoryRecordInfo[]>(this._context, "GetPlayerRaiderRoundHistoryRecordInfo", this.GetPlayerRaiderRoundHistoryRecordInfoCompleted, userState, GlobalData.Token, pageItemCount, pageIndex);
            }
        }

        #endregion

        #region GetHistoryRaiderRoundRecords

        public event EventHandler<WebInvokeEventArgs<RaiderRoundMetaDataInfo[]>> GetHistoryRaiderRoundRecordsCompleted;
        public void GetHistoryRaiderRoundRecords(int pageItemCount, int pageIndex, object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<RaiderRoundMetaDataInfo[]>(this._context, "GetHistoryRaiderRoundRecords", this.GetHistoryRaiderRoundRecordsCompleted, userState, GlobalData.Token, pageItemCount, pageIndex);
            }
        }

        #endregion

        #region GetCurrentRaiderRoundInfo

        public event EventHandler<WebInvokeEventArgs<RaiderRoundMetaDataInfo>> GetCurrentRaiderRoundInfoCompleted;
        public void GetCurrentRaiderRoundInfo(object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<RaiderRoundMetaDataInfo>(this._context, "GetCurrentRaiderRoundInfo", this.GetCurrentRaiderRoundInfoCompleted, userState, GlobalData.Token);
            }
        }

        #endregion

        #region JoinRaider

        public event EventHandler<WebInvokeEventArgs<int>> JoinRaiderCompleted;
        public void JoinRaider(int roundID, int betStoneCount, object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<int>(this._context, "JoinRaider", this.JoinRaiderCompleted, userState, GlobalData.Token, roundID, betStoneCount);
            }
        }

        #endregion

        #region GetPlayerselfBetInfo

        public event EventHandler<WebInvokeEventArgs<RaiderPlayerBetInfo[]>> GetPlayerselfBetInfoCompleted;
        public void GetPlayerselfBetInfo(int roundID, int pageItemCount, int pageIndex, object userState)
        {
            if (this._invoker != null)
            {
                this._invoker.InvokeUserState<RaiderPlayerBetInfo[]>(this._context, "GetPlayerselfBetInfo", this.GetPlayerselfBetInfoCompleted, userState, GlobalData.Token, roundID, pageItemCount, pageIndex);
            }
        }

        #endregion

        #region Callback

        public event Action<RaiderRoundMetaDataInfo> OnPlayerJoinRaiderSucceed;

        public void RaiseOnPlayerJoinRaiderSucceed(RaiderRoundMetaDataInfo roundInfo)
        {
            Action<RaiderRoundMetaDataInfo> handler = this.OnPlayerJoinRaiderSucceed;
            if (null != handler)
            {
                handler(roundInfo);
            }
        }

        public event Action<RaiderRoundMetaDataInfo> OnPlayerWinedRaiderNotify;

        public void RaiseOnPlayerWinedRaiderNotify(RaiderRoundMetaDataInfo record)
        {
            Action<RaiderRoundMetaDataInfo> handler = this.OnPlayerWinedRaiderNotify;
            if (null != handler)
            {
                handler(record);
            }
        }


        #endregion

    }
}
