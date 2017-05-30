using MetaData.StoneFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public partial class ServerClient
    {
        public event EventHandler<WebInvokeEventArgs<StoneFactorySystemDailyProfit[]>> GetStoneFactorySystemDailyProfitListCompleted;
        public void GetStoneFactorySystemDailyProfitList(int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<StoneFactorySystemDailyProfit[]>(this._context, "GetStoneFactorySystemDailyProfitList", this.GetStoneFactorySystemDailyProfitListCompleted, GlobalData.Token, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerStoneFactoryAccountInfo[]>> GetAllPlayerStoneFactoryAccountInfosCompleted;
        public void GetAllPlayerStoneFactoryAccountInfos()
        {
            this._invoker.Invoke<PlayerStoneFactoryAccountInfo[]>(this._context, "GetAllPlayerStoneFactoryAccountInfos", this.GetAllPlayerStoneFactoryAccountInfosCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<int>> AdminSetStoneFactoryProfitRateCompleted;
        public void AdminSetStoneFactoryProfitRate(decimal profitRate)
        {
            this._invoker.Invoke<int>(this._context, "AdminSetStoneFactoryProfitRate", this.AdminSetStoneFactoryProfitRateCompleted, GlobalData.Token, profitRate);
        }

        public event EventHandler<WebInvokeEventArgs<int>> GetSumLastDayValidStoneStackCompleted;
        public void GetSumLastDayValidStoneStack()
        {
            this._invoker.Invoke<int>(this._context, "GetSumLastDayValidStoneStack", this.GetSumLastDayValidStoneStackCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<StoneFactorySystemDailyProfit>> GetYesterdayFactoryProfitRateCompleted;
        public void GetYesterdayFactoryProfitRate()
        {
            this._invoker.Invoke<StoneFactorySystemDailyProfit>(this._context, "GetYesterdayFactoryProfitRate", this.GetYesterdayFactoryProfitRateCompleted, GlobalData.Token);
        }

    }
}
