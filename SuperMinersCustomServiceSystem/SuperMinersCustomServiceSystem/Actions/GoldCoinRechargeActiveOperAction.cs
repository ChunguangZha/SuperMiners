using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Actions
{
    public class GoldCoinRechargeActiveOperAction : BaseAction
    {
        public override string NavMenu
        {
            get { return "交易系统\\金币充值\\" + MenuHeader; }
        }

        public override string MenuHeader
        {
            get { return "实时交易"; }
        }
    }
}
