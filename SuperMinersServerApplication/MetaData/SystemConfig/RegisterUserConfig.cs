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
        public float GiveToNewUserExp = 1;

        /// <summary>
        /// 给新注册用户赠送金币数
        /// </summary>
        [DataMember]
        public float GiveToNewUserGoldCoin = 5;

        /// <summary>
        /// 给新注册用户赠送矿山数
        /// </summary>
        [DataMember]
        public int GiveToNewUserMines = 0;

        /// <summary>
        /// 给新注册用户赠送矿工数
        /// </summary>
        [DataMember]
        public int GiveToNewUserMiners = 1;

        /// <summary>
        /// 给新注册用户赠送矿石数
        /// </summary>
        [DataMember]
        public float GiveToNewUserStones = 0.05f;

    }
}
