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
        public event EventHandler<WebInvokeEventArgs<AlipayRechargeRecord[]>> GetAllExceptionAlipayRechargeRecordsCompleted;
        public void GetAllExceptionAlipayRechargeRecords()
        {
            this._invoker.Invoke<AlipayRechargeRecord[]>(this._context, "GetAllExceptionAlipayRechargeRecords", this.GetAllExceptionAlipayRechargeRecordsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<AlipayRechargeRecord[]>> GetAllAlipayRechargeRecordsCompleted;
        public void GetAllAlipayRechargeRecords(string orderNumber, string alipayOrderNumber, string payEmail, string playerUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<AlipayRechargeRecord[]>(this._context, "GetAllAlipayRechargeRecords", this.GetAllAlipayRechargeRecordsCompleted, GlobalData.Token, orderNumber, alipayOrderNumber, payEmail, playerUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<AlipayRechargeRecord>> SearchExceptionAlipayRechargeRecordCompleted;
        public void SearchExceptionAlipayRechargeRecord(string orderNumber)
        {
            this._invoker.Invoke<AlipayRechargeRecord>(this._context, "SearchExceptionAlipayRechargeRecord", this.SearchExceptionAlipayRechargeRecordCompleted, GlobalData.Token, orderNumber);
        }

    }
}
