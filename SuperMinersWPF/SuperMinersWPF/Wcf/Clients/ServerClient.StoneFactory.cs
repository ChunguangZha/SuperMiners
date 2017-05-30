using MetaData;
using MetaData.StoneFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        public event EventHandler<WebInvokeEventArgs<StoneFactorySystemDailyProfit[]>> GetStoneFactorySystemDailyProfitListCompleted;
        public void GetStoneFactorySystemDailyProfitList(int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<StoneFactorySystemDailyProfit[]>(this._context, "GetStoneFactorySystemDailyProfitList", this.GetStoneFactorySystemDailyProfitListCompleted, GlobalData.Token, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerStoneFactoryAccountInfo>> GetPlayerStoneFactoryAccountInfoCompleted;
        public void GetPlayerStoneFactoryAccountInfo(int userID)
        {
            this._invoker.Invoke<PlayerStoneFactoryAccountInfo>(this._context, "GetPlayerStoneFactoryAccountInfo", this.GetPlayerStoneFactoryAccountInfoCompleted, GlobalData.Token, userID);
        }

        public event EventHandler<WebInvokeEventArgs<StoneFactoryProfitRMBChangedRecord[]>> GetStoneFactoryProfitRMBChangedRecordListCompleted;
        public void GetStoneFactoryProfitRMBChangedRecordList(int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<StoneFactoryProfitRMBChangedRecord[]>(this._context, "GetStoneFactoryProfitRMBChangedRecordList", this.GetStoneFactoryProfitRMBChangedRecordListCompleted, GlobalData.Token, userID, beginTime, endTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<int>> AddStoneToFactoryCompleted;
        public void AddStoneToFactory(int stoneStackCount)
        {
            this._invoker.Invoke<int>(this._context, "AddStoneToFactory", this.AddStoneToFactoryCompleted, GlobalData.Token, GlobalData.CurrentUser.UserID, GlobalData.CurrentUser.UserName, stoneStackCount);
        }

        public event EventHandler<WebInvokeEventArgs<int>> AddMinersToFactoryCompleted;
        public void AddMinersToFactory(int minersGroupCount)
        {
            this._invoker.Invoke<int>(this._context, "AddMinersToFactory", this.AddMinersToFactoryCompleted, GlobalData.Token, GlobalData.CurrentUser.UserID, GlobalData.CurrentUser.UserName, minersGroupCount);
        }

        public event EventHandler<WebInvokeEventArgs<int>> WithdrawOutputRMBFromFactoryCompleted;
        public void WithdrawOutputRMBFromFactory(decimal withdrawRMBCount)
        {
            this._invoker.Invoke<int>(this._context, "WithdrawOutputRMBFromFactory", this.WithdrawOutputRMBFromFactoryCompleted, GlobalData.Token, GlobalData.CurrentUser.UserID, GlobalData.CurrentUser.UserName, withdrawRMBCount);
        }

        public event EventHandler<WebInvokeEventArgs<int>> WithdrawStoneFromFactoryCompleted;
        public void WithdrawStoneFromFactory(int stoneStackCount)
        {
            this._invoker.Invoke<int>(this._context, "WithdrawStoneFromFactory", this.WithdrawStoneFromFactoryCompleted, GlobalData.Token, GlobalData.CurrentUser.UserID, GlobalData.CurrentUser.UserName, stoneStackCount);
        }

        public event EventHandler<WebInvokeEventArgs<int>> FeedSlaveCompleted;
        public void FeedSlave()
        {
            this._invoker.Invoke<int>(this._context, "FeedSlave", this.FeedSlaveCompleted, GlobalData.Token, GlobalData.CurrentUser.UserID);
        }

    }
}
