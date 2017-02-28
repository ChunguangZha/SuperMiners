using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    /// <summary>
    /// 注册用户奖励
    /// </summary>
    [DataContract]
    public class RegisterUserConfig
    {
        /// <summary>
        /// 同一IP地址，可以注册用户数。
        /// </summary>
        [DataMember]
        public int UserCountCreateByOneIP = 5;

        /// <summary>
        /// 给新注册用户赠送贡献值
        /// </summary>
        [DataMember]
        public decimal GiveToNewUserExp = 1;

        /// <summary>
        /// 给新注册用户赠送金币数
        /// </summary>
        [DataMember]
        public decimal GiveToNewUserGoldCoin = 0;

        /// <summary>
        /// 给新注册用户赠送矿山数
        /// </summary>
        [DataMember]
        public decimal GiveToNewUserMines = 0m;

        /// <summary>
        /// 给新注册用户赠送矿工数
        /// </summary>
        [DataMember]
        public int GiveToNewUserMiners = 0;

        /// <summary>
        /// 给新注册用户赠送矿石数
        /// </summary>
        [DataMember]
        public decimal GiveToNewUserStones = 0m;

        /// <summary>
        /// 第一次支付宝充值金币，奖励金币倍数
        /// </summary>
        [DataMember]
        public float FirstAlipayRechargeGoldCoinAwardMultiple = 0f;

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("注册奖励：");
            if (GiveToNewUserExp > 0)
            {
                strBuilder.Append(string.Format("贡献值-[{0}],", this.GiveToNewUserExp));
            }
            if (GiveToNewUserGoldCoin > 0)
            {
                strBuilder.Append(string.Format("金币-[{0}],", this.GiveToNewUserGoldCoin));
            }
            if (GiveToNewUserMines > 0)
            {
                strBuilder.Append(string.Format("矿山-[{0}],", this.GiveToNewUserMines));
            }
            if (GiveToNewUserMiners > 0)
            {
                strBuilder.Append(string.Format("矿工-[{0}],", this.GiveToNewUserMiners));
            }
            if (GiveToNewUserStones > 0)
            {
                strBuilder.Append(string.Format("矿石-[{0}],", this.GiveToNewUserStones));
            }

            return strBuilder.ToString(0, strBuilder.Length - 1);
        }
    }
}
