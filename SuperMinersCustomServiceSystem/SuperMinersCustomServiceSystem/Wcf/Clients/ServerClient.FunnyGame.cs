using MetaData;
using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public partial class ServerClient
    {
        public event EventHandler<WebInvokeEventArgs<RouletteAwardItem[]>> GetAllAwardItemsCompleted;
        public void GetAllAwardItems()
        {
            this._invoker.Invoke<RouletteAwardItem[]>(this._context, "GetAllAwardItems", this.GetAllAwardItemsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<int>> AddAwardItemCompleted;
        public void AddAwardItem(RouletteAwardItem item)
        {
            this._invoker.Invoke<int>(this._context, "AddAwardItem", this.AddAwardItemCompleted, GlobalData.Token, item);
        }

        public event EventHandler<WebInvokeEventArgs<int>> UpdateAwardItemCompleted;
        public void UpdateAwardItem(RouletteAwardItem item)
        {
            this._invoker.Invoke<int>(this._context, "UpdateAwardItem", this.UpdateAwardItemCompleted, GlobalData.Token, item);
        }

        public event EventHandler<WebInvokeEventArgs<int>> DeleteAwardItemCompleted;
        public void DeleteAwardItem(RouletteAwardItem item)
        {
            this._invoker.Invoke<int>(this._context, "DeleteAwardItem", this.DeleteAwardItemCompleted, GlobalData.Token, item);
        }

        public event EventHandler<WebInvokeEventArgs<RouletteAwardItem[]>> GetCurrentAwardItemsCompleted;
        public void GetCurrentAwardItems()
        {
            this._invoker.Invoke<RouletteAwardItem[]>(this._context, "GetCurrentAwardItems", this.GetCurrentAwardItemsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> SetCurrentAwardItemsCompleted;
        public void SetCurrentAwardItems(RouletteAwardItem[] items)
        {
            this._invoker.Invoke<bool>(this._context, "SetCurrentAwardItems", this.SetCurrentAwardItemsCompleted, GlobalData.Token, items);
        }

        public event EventHandler<WebInvokeEventArgs<RouletteWinnerRecord[]>> GetNotPayWinAwardRecordsCompleted;
        public void GetNotPayWinAwardRecords()
        {
            this._invoker.Invoke<RouletteWinnerRecord[]>(this._context, "GetNotPayWinAwardRecords", this.GetNotPayWinAwardRecordsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<RouletteWinnerRecord[]>> GetAllPayWinAwardRecordsCompleted;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RouletteAwardItemID"></param>
        /// <param name="BeginWinTime"></param>
        /// <param name="EndWinTime"></param>
        /// <param name="IsGot">-1表示null;0表示false;1表示true</param>
        /// <param name="IsPay">-1表示null;0表示false;1表示true</param>
        /// <param name="pageItemCount"></param>
        /// <param name="pageIndex"></param>
        public void GetAllPayWinAwardRecords(string UserName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<RouletteWinnerRecord[]>(this._context, "GetAllPayWinAwardRecords", this.GetAllPayWinAwardRecordsCompleted, GlobalData.Token, UserName, RouletteAwardItemID, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<int>> PayAwardCompleted;
        public void PayAward(string adminUserName, string playerUserName, int recordID)
        {
            this._invoker.Invoke<int>(this._context, "PayAward", this.PayAwardCompleted, GlobalData.Token, adminUserName, playerUserName, recordID);
        }

        public event EventHandler<WebInvokeEventArgs<RouletteRoundInfo[]>> GetAllRouletteRoundInfoCompleted;
        public void GetAllRouletteRoundInfo()
        {
            this._invoker.Invoke<RouletteRoundInfo[]>(this._context, "GetAllRouletteRoundInfo", this.GetAllRouletteRoundInfoCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> SaveRouletteLargeWinMultipleCompleted;
        public void SaveRouletteLargeWinMultiple(decimal multiple)
        {
            this._invoker.Invoke<bool>(this._context, "SaveRouletteLargeWinMultiple", this.SaveRouletteLargeWinMultipleCompleted, GlobalData.Token, multiple);
        }

        #region Callback

        public event Action<RouletteWinnerRecord> OnSomebodyWinRouletteAward;

        public void RaiseOnSomebodyWinRouletteAward(RouletteWinnerRecord record)
        {
            Action<RouletteWinnerRecord> handler = this.OnSomebodyWinRouletteAward;
            if (null != handler)
            {
                handler(record);
            }
        }

        #endregion
    }
}
