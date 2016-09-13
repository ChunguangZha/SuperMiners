using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public partial class ServerClient
    {

        public event EventHandler<WebInvokeEventArgs<GoldCoinRechargeRecord[]>> GetFinishedGoldCoinRechargeRecordListCompleted;
        public void GetFinishedGoldCoinRechargeRecordList(string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<GoldCoinRechargeRecord[]>(this._context, "GetFinishedGoldCoinRechargeRecordList", this.GetFinishedGoldCoinRechargeRecordListCompleted, GlobalData.Token, playerUserName, orderNumber, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<MinersBuyRecord[]>> GetBuyMinerFinishedRecordListCompleted;
        public void GetBuyMinerFinishedRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<MinersBuyRecord[]>(this._context, "GetBuyMinerFinishedRecordList", this.GetBuyMinerFinishedRecordListCompleted, GlobalData.Token, playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<MinesBuyRecord[]>> GetBuyMineFinishedRecordListCompleted;
        public void GetBuyMineFinishedRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<MinesBuyRecord[]>(this._context, "GetBuyMineFinishedRecordList", this.GetBuyMineFinishedRecordListCompleted, GlobalData.Token, playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

    }
}
