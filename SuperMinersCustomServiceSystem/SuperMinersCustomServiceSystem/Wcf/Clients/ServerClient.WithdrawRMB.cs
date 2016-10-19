using MetaData;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public partial class ServerClient
    {
        public event Action<WithdrawRMBRecord> OnSomebodyWithdrawRMB;

        public void RaiseOnSomebodyWithdrawRMB(WithdrawRMBRecord record)
        {
            Action<WithdrawRMBRecord> handler = this.OnSomebodyWithdrawRMB;
            if (null != handler)
            {
                handler(record);
            }
        }

        public event EventHandler<WebInvokeEventArgs<int>> PayWithdrawRMBRecordCompleted;
        public void PayWithdrawRMBRecord(WithdrawRMBRecord record)
        {
            this._invoker.Invoke<int>(this._context, "PayWithdrawRMBRecord", this.PayWithdrawRMBRecordCompleted, GlobalData.Token, record);
        }

        public event EventHandler<WebInvokeEventArgs<WithdrawRMBRecord[]>> GetWithdrawRMBRecordListCompleted;
        public void GetWithdrawRMBRecordList(int state, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex, object userState)
        {
            this._invoker.InvokeUserState<WithdrawRMBRecord[]>(this._context, "GetWithdrawRMBRecordList", this.GetWithdrawRMBRecordListCompleted, userState, GlobalData.Token, state, playerUserName, beginCreateTime, endCreateTime, adminUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);
        }

    }
}
