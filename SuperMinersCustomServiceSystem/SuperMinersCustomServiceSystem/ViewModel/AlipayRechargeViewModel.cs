using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class AlipayRechargeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "支付宝充值";
            }
        }

        private ObservableCollection<AlipayRechargeRecordUIModel> _listExceptionAlipayRecords = new ObservableCollection<AlipayRechargeRecordUIModel>();

        public ObservableCollection<AlipayRechargeRecordUIModel> ListExceptionAlipayRecords
        {
            get
            {
                return _listExceptionAlipayRecords;
            }
        }

        public AlipayRechargeViewModel()
        {

        }
    }
}
