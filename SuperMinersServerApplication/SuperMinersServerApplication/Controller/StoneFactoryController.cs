using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    /// <summary>
    /// 10000矿石为一股投入到加工厂（存储必须1万的倍数），加工厂凝练每天分配给相应比例的灵币，
    /// 18天后可以转入灵币账户进行提现或消费操作，凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，
    /// 加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
    /// </summary>
    public class StoneFactoryController
    {
        //凝练按天计算，每天0点计算前一天数值，为了简化计算，当前投入的矿石，矿工，食物，都是以第二天0时开始计算。
        //每天23：50分到次到1点禁止充值矿石、矿工、食物。


    }
}
