using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Actions
{
    public class MineTradeAction : BaseAction
    {
        public override string NavMenu
        {
            get { return "交易系统\\" + MenuHeader; }
        }

        public override string MenuHeader
        {
            get { return "矿山交易"; }
        }
    }
}
