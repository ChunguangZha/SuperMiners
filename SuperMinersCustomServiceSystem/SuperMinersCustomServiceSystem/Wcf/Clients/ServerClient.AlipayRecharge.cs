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
        public event EventHandler<WebInvokeEventArgs<AlipayRechargeRecord[]>> GetAllExceptionAlipayRechargeRecordsCompleted;
        public void GetAllExceptionAlipayRechargeRecords()
        {
            this._invoker.Invoke<AlipayRechargeRecord[]>(this._context, "GetAllExceptionAlipayRechargeRecords", this.GetAllExceptionAlipayRechargeRecordsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<int>> HandleExceptionAlipayRechargeRecordCompleted;
        public void HandleExceptionAlipayRechargeRecord(AlipayRechargeRecord exceptionRecord, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "HandleExceptionAlipayRechargeRecord", this.HandleExceptionAlipayRechargeRecordCompleted, userState, GlobalData.Token, exceptionRecord);
        }

    }
}
