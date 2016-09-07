using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Actions
{
    public class WithdrawRMBActiveOperAction : BaseAction
    {
        public override string NavMenu
        {
            get { return "交易系统\\灵币提现\\实时交易"; }
        }

        public override string MenuHeader
        {
            get { return "实时交易"; }
        }
    }
}
