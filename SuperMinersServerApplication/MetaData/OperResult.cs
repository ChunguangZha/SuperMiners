using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public class OperResult
    {
        public const int RESULTCODE_EXCEPTION = -1;

        public const int RESULTCODE_TRUE = 0;

        public const int RESULTCODE_FALSE = 1;

        /// <summary>
        /// 参数无效
        /// </summary>
        public const int RESULTCODE_PARAM_INVALID = 2;

        /// <summary>
        /// 注册用户时_用户名已存在
        /// </summary>
        public const int RESULTCODE_REGISTER_USERNAME_EXIST = 100;

        /// <summary>
        /// 注册用户时_用户名过短
        /// </summary>
        public const int RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT = 101;

        /// <summary>
        /// 用户不存在
        /// </summary>
        public const int RESULTCODE_USER_NOT_EXIST = 300;

        public const int RESULTCODE_USER_OFFLINE = 301;

        /// <summary>
        /// 余额不足
        /// </summary>
        public const int RESULTCODE_LACK_OF_BALANCE = 500;

        /// <summary>
        /// 订单不存在
        /// </summary>
        public const int RESULTCODE_ORDER_NOT_EXIST = 600;

        /// <summary>
        /// 订单不属于当前玩家
        /// </summary>
        public const int RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER = 601;

        /// <summary>
        /// 订单被锁定
        /// </summary>
        public const int RESULTCODE_ORDER_BE_LOCKED = 602;

        /// <summary>
        /// 可销售的矿石不足
        /// </summary>
        public const int RESULTCODE_ORDER_SELLABLE_STONE_LACK = 603;
    }
}
