using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Actions
{
    public class GoldCoinRechargeAction : BaseAction
    {
        public override string NavMenu
        {
            get { return "交易系统\\" + MenuHeader; }
        }

        public override string MenuHeader
        {
            get { return "金币充值"; }
        }
    }
}
