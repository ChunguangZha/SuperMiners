using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    [DataContract]
    public class OperResultObject
    {
        [DataMember]
        public int OperResultCode = OperResult.RESULTCODE_FALSE;

        [DataMember]
        public string Message;
    }

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
        /// 客户端软件版本过时
        /// </summary>
        public const int RESULTCODE_CLIENT_VERSION_OLD = 3;

        /// <summary>
        /// 注册用户时_用户名已存在
        /// </summary>
        public const int RESULTCODE_REGISTER_USERNAME_EXIST = 100;

        /// <summary>
        /// 注册用户时_用户名过短
        /// </summary>
        public const int RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT = 101;

        /// <summary>
        /// 注册用户时_昵称已存在
        /// </summary>
        public const int RESULTCODE_REGISTER_NICKNAME_EXIST = 102;

        /// <summary>
        /// 注册用户时_支付宝已存在
        /// </summary>
        public const int RESULTCODE_REGISTER_ALIPAY_EXIST = 103;

        /// <summary>
        /// 注册用户时_支付宝实名已存在
        /// </summary>
        public const int RESULTCODE_REGISTER_ALIPAYREALNAME_EXIST = 104;

        /// <summary>
        /// 注册用户时_身份证号已存在
        /// </summary>
        public const int RESULTCODE_REGISTER_IDCARDNO_EXIST = 105;

        /// <summary>
        /// 微信注册时，当前openid已经注册过迅灵账户。
        /// </summary>
        public const int RESULTCODE_WEXIN_REGISTER_OPENID_EXIST = 106;

        /// <summary>
        /// 用户不存在
        /// </summary>
        public const int RESULTCODE_USER_NOT_EXIST = 300;
        
        public const int RESULTCODE_USER_OFFLINE = 301;

        public const int RESULTCODE_USER_CANNOT_UPDATEALIPAY = 302;

        public const int RESULTCODE_USER_IS_LOCKED = 303;

        /// <summary>
        /// 玩家矿工数超限
        /// </summary>
        public const int RESULTCODE_USER_MINERSCOUNT_OUT = 304;

        /// <summary>
        /// 登录时，用户名或密码不正确
        /// </summary>
        public const int RESULTCODE_USERNAME_PASSWORD_ERROR = 305;

        /// <summary>
        /// 您当前登录账户为测试玩家，要求同一账户只能在一台电脑登录，且一台电脑只能登录一个账户
        /// </summary>
        public const int RESULTCODE_USERLOGIN_ISTESTUSER_LOGINLIMIT = 306;

        /// <summary>
        /// 微信绑定时，该迅灵用户已经被其它微信用户绑定
        /// </summary>
        public const int RESULTCODE_WEIXIN_USERBINDEDBYOTHER = 350;

        /// <summary>
        /// 余额不足
        /// </summary>
        public const int RESULTCODE_LACK_OF_BALANCE = 500;
        /// <summary>
        /// 不满足提现要求，不能提现
        /// </summary>
        public const int RESULTCODE_CANOT_WITHDRAWRMB = 501;

        /// <summary>
        /// 为玩家支付灵币提现时，提现灵币和冻结灵币不一致
        /// </summary>
        public const int RESULTCODE_WITHDRAW_FREEZING_RMB_ERROR = 502;

        /// <summary>
        /// 为玩家支付灵币提现时，提现状态错误
        /// </summary>
        public const int RESULTCODE_WITHDRAW_RECORD_STATE_ERROR = 503;

        /// <summary>
        /// 为玩家支付灵币提现时，该订单已经被处理
        /// </summary>
        public const int RESULTCODE_WITHDRAW_ORDER_BEHANDLED = 504;

        /// <summary>
        /// 玩家两次提现时间间隔不足
        /// </summary>
        public const int RESULTCODE_WITHDRAW_INTERVAL_LESS = 505;

        /// <summary>
        /// 收取矿石时，没有可收取的矿石
        /// </summary>
        public const int RESULTCODE_GATHERSTONE_NOSTONES = 506;

        /// <summary>
        /// 购买矿山时，矿山已满，无法购买。
        /// </summary>
        public const int RESULTCODE_BUYMINE_MINEISFULL = 507;

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
        /// 订单没有被锁定
        /// </summary>
        public const int RESULTCODE_ORDER_NOT_BE_LOCKED = 603;

        /// <summary>
        /// 申诉订单时，如果交易已经成功，返回该值
        /// </summary>
        public const int RESULTCODE_ORDER_BUY_SUCCEED = 604;

        /// <summary>
        /// 矿石订单当前不是异常状态，用于异常处理。
        /// </summary>
        public const int RESULTCODE_ORDER_ISNOT_EXCEPTION = 605;

        public const int RESULTCODE_ORDER_BE_LOCKED_BY_OTHER = 606;

        /// <summary>
        /// 可销售的矿石不足
        /// </summary>
        public const int RESULTCODE_ORDER_SELLABLE_STONE_LACK = 610;

        /// <summary>
        /// 矿石订单卖家冻结矿石数不对
        /// </summary>
        public const int RESULTCODE_ORDER_SELLER_FREEZINGSTONECOUNT_ERROR = 611;

        /// <summary>
        /// 矿石出售券不足
        /// </summary>
        public const int RESULTCODE_ORDER_SELLSTONEQUAN_LACK = 612;

        /// <summary>
        /// 支付订单时，支付金额不足
        /// </summary>
        public const int RESULTCODE_ORDER_PAYMONEY_LESS = 660;

        /// <summary>
        /// 娱乐功能中，获奖信息不存在
        /// </summary>
        public const int RESULTCODE_GAME_WINAWARDRECORD_NOT_EXIST = 700;

        /// <summary>
        /// 娱乐——夺宝奇兵，每轮游戏已结束
        /// </summary>
        public const int RESULTCODE_GAME_RAIDER_ROUNDFINISHED = 701;

        /// <summary>
        /// 娱乐——夺宝奇兵，正在等待第二位玩家加入，才能开始游戏
        /// </summary>
        public const int RESULTCODE_GAME_RAIDER_WAITINGSECONDPLAYERJOIN_TOSTART = 702;

        /// <summary>
        /// 股票市场， 撤单失败，开市期间无法撤单
        /// </summary>
        public const int RESULTCODE_STACK_CANCELORDER_FAILED_MARKETISOPENING = 800;

        /// <summary>
        /// 股票市场，取消订单失败，当日总手数错误
        /// </summary>
        public const int RESULTCODE_STACK_CANCELORDER_FAILED_TOTALHANDCOUNTERROR = 801;

        /// <summary>
        /// 股票市场， 挂单失败，闭市期间无法挂单
        /// </summary>
        public const int RESULTCODE_STACK_DELEGATEORDER_FAILED_MARKETISCLOSED = 802;

        /// <summary>
        /// 股票市场，挂单价超出涨跌停范围
        /// </summary>
        public const int RESULTCODE_STACK_PRICE_OUTOFRANGE = 803;



        
        private static Dictionary<int, string> _resultCode_Msg = new Dictionary<int, string>();

        static OperResult()
        {
            _resultCode_Msg.Add(OperResult.RESULTCODE_EXCEPTION, "操作异常");
            _resultCode_Msg.Add(OperResult.RESULTCODE_FALSE, "操作失败");
            _resultCode_Msg.Add(RESULTCODE_CLIENT_VERSION_OLD, "客户端软件版本过时，请更新");
            _resultCode_Msg.Add(OperResult.RESULTCODE_LACK_OF_BALANCE, "余额不足");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_BE_LOCKED, "订单已经被锁定");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_NOT_BE_LOCKED, "订单没有被锁定");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER, "订单不属于当前玩家");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_NOT_EXIST, "订单不存在");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_SELLABLE_STONE_LACK, "可出售矿石不足");
            _resultCode_Msg.Add(OperResult.RESULTCODE_PARAM_INVALID, "参数无效");
            _resultCode_Msg.Add(OperResult.RESULTCODE_TRUE, "成功");
            _resultCode_Msg.Add(OperResult.RESULTCODE_USER_NOT_EXIST, "玩家不存在");
            _resultCode_Msg.Add(OperResult.RESULTCODE_USER_OFFLINE, "玩家不在线");
            _resultCode_Msg.Add(OperResult.RESULTCODE_CANOT_WITHDRAWRMB, "只有贡献值大于50的玩家可以提现，且提现金额必须大于5元人民币");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_BUY_SUCCEED, "矿石交易已经交易成功");
            _resultCode_Msg.Add(RESULTCODE_ORDER_ISNOT_EXCEPTION, "该订单当前不是异常状态");
            _resultCode_Msg.Add(RESULTCODE_GAME_WINAWARDRECORD_NOT_EXIST, "没有找到您的中奖信息");
            _resultCode_Msg.Add(RESULTCODE_REGISTER_NICKNAME_EXIST, "用户昵称已经存在");
            _resultCode_Msg.Add(RESULTCODE_WITHDRAW_FREEZING_RMB_ERROR, "提现灵币和冻结灵币不一致");
            _resultCode_Msg.Add(RESULTCODE_WITHDRAW_RECORD_STATE_ERROR, "提现状态错误");
            _resultCode_Msg.Add(RESULTCODE_WITHDRAW_ORDER_BEHANDLED, "该提现订单已经被处理，请刷新后再试");
            _resultCode_Msg.Add(RESULTCODE_REGISTER_ALIPAY_EXIST, "该支付宝信息已经被其它人使用");
            _resultCode_Msg.Add(RESULTCODE_REGISTER_ALIPAYREALNAME_EXIST, "该支付宝实名已经被其它人使用");
            _resultCode_Msg.Add(RESULTCODE_USER_CANNOT_UPDATEALIPAY, "您已经绑定过支付宝信息，无法再修改，如想修改请联系客服");
            _resultCode_Msg.Add(RESULTCODE_REGISTER_IDCARDNO_EXIST, "该身份证号已经被其它人使用");
            _resultCode_Msg.Add(RESULTCODE_WEIXIN_USERBINDEDBYOTHER, "该迅灵用户已经被其它微信用户绑定");
            _resultCode_Msg.Add(RESULTCODE_USER_IS_LOCKED, "该账户已经被管理员锁定");
            _resultCode_Msg.Add(RESULTCODE_WEXIN_REGISTER_OPENID_EXIST, "当前微信号已经注册过迅灵账户，无法再注册");
            _resultCode_Msg.Add(RESULTCODE_ORDER_PAYMONEY_LESS, "支付金额不足");
            _resultCode_Msg.Add(RESULTCODE_ORDER_BE_LOCKED_BY_OTHER, "订单被抢了");
            _resultCode_Msg.Add(RESULTCODE_ORDER_SELLER_FREEZINGSTONECOUNT_ERROR, "卖家冻结矿石数错误");
            _resultCode_Msg.Add(RESULTCODE_USER_MINERSCOUNT_OUT, "矿工数不能超过5000");
            _resultCode_Msg.Add(RESULTCODE_ORDER_SELLSTONEQUAN_LACK, "矿石出售券不足");
            _resultCode_Msg.Add(RESULTCODE_WITHDRAW_INTERVAL_LESS, "跟上次提现时间间隔不足");
            _resultCode_Msg.Add(RESULTCODE_USERNAME_PASSWORD_ERROR, "用户名或密码错误");
            _resultCode_Msg.Add(RESULTCODE_USERLOGIN_ISTESTUSER_LOGINLIMIT, "您当前登录账户为测试玩家，要求同一账户只能在一台电脑登录，且一台电脑只能登录一个账户");
            _resultCode_Msg.Add(RESULTCODE_GATHERSTONE_NOSTONES, "没有可收取的矿石");
            _resultCode_Msg.Add(RESULTCODE_STACK_CANCELORDER_FAILED_MARKETISOPENING, "开市期间无法撤单");
            _resultCode_Msg.Add(RESULTCODE_STACK_CANCELORDER_FAILED_TOTALHANDCOUNTERROR, "总手数不对");
            _resultCode_Msg.Add(RESULTCODE_STACK_DELEGATEORDER_FAILED_MARKETISCLOSED, "尚未开市");
            _resultCode_Msg.Add(RESULTCODE_STACK_PRICE_OUTOFRANGE, "挂单价不能超出涨跌停范围");
            _resultCode_Msg.Add(RESULTCODE_GAME_RAIDER_ROUNDFINISHED, "本轮游戏已结束，请参加下一轮");
            _resultCode_Msg.Add(RESULTCODE_GAME_RAIDER_WAITINGSECONDPLAYERJOIN_TOSTART, "正在等待第二位玩家加入才能开始");
            _resultCode_Msg.Add(RESULTCODE_BUYMINE_MINEISFULL, "已无矿脉储量，无法继续勘探");
        }

        public static string GetMsg(int resultCode)
        {
            if (_resultCode_Msg.ContainsKey(resultCode))
            {
                return _resultCode_Msg[resultCode];
            }

            return "";
        }
    }
}