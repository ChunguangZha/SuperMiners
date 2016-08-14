using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Utility
{
    public static class ResultCodeMsg
    {
        private static Dictionary<int, string> _resultCode_Msg = new Dictionary<int, string>();

        static ResultCodeMsg()
        {
            _resultCode_Msg.Add(OperResult.RESULTCODE_EXCEPTION, "操作异常");
            _resultCode_Msg.Add(OperResult.RESULTCODE_FALSE, "操作失败");
            _resultCode_Msg.Add(OperResult.RESULTCODE_LACK_OF_BALANCE, "余额不足");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_BE_LOCKED, "订单已经被锁定");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER, "订单不属于当前玩家");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_NOT_EXIST, "订单不存在");
            _resultCode_Msg.Add(OperResult.RESULTCODE_ORDER_SELLABLE_STONE_LACK, "可出售矿石不足");
            _resultCode_Msg.Add(OperResult.RESULTCODE_PARAM_INVALID, "参数无效");
            _resultCode_Msg.Add(OperResult.RESULTCODE_TRUE, "成功");
            _resultCode_Msg.Add(OperResult.RESULTCODE_USER_NOT_EXIST, "玩家不存在");
            _resultCode_Msg.Add(OperResult.RESULTCODE_USER_OFFLINE, "玩家不在线");
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
