using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Actions
{
    public class MinerTradeHistoryAction : BaseAction
    {
        public override string NavMenu
        {
            get { return "交易系统\\矿工交易\\交易历史"; }
        }

        public override string MenuHeader
        {
            get { return "交易记录"; }
        }
    }
}
