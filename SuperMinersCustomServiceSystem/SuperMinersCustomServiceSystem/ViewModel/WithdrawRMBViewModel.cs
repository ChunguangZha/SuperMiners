using MetaData;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class WithdrawRMBViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "灵币提现";
            }
        }

        public WithdrawRMBViewModel()
        {
            GlobalData.Client.OnSomebodyWithdrawRMB += Client_OnSomebodyWithdrawRMB;
        }

        public void AsyncPayWithdrawRMBRecord(WithdrawRMBRecord record)
        {

        }

        public void AsyncGetWithdrawRMBRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {

        }

        void Client_OnSomebodyWithdrawRMB(WithdrawRMBRecord record)
        {
            throw new NotImplementedException();
        }

    }
}
