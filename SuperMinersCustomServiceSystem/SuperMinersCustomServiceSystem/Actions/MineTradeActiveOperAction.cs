using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Actions
{
    public class MineTradeActiveOperAction : BaseAction
    {
        public override string NavMenu
        {
            get { return "交易系统\\矿山交易\\实时交易"; }
        }

        public override string MenuHeader
        {
            get { return "实时交易"; }
        }
    }
}
