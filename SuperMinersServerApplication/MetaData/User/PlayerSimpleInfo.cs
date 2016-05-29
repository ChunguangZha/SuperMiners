using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    /// <summary>
    /// 玩家基本信息
    /// </summary>
    [DataContract]
    public class PlayerSimpleInfo
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 支付宝账户
        /// </summary>
        [DataMember]
        public string Alipay { get; set; }

        /// <summary>
        /// 支付宝账户真实姓名
        /// </summary>
        [DataMember]
        public string AlipayRealName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string QQ { get; set; }

        /// <summary>
        /// 用户注册时的IP
        /// </summary>
        [DataMember]
        public string RegisterIP { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        [DataMember]
        public string InvitationCode { get; set; }

        #region RegisterTime

        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }
        [DataMember]
        public string RegisterTimeString
        {
            get
            {
                if (this.RegisterTime == null)
                {
                    return "";
                }
                return this.RegisterTime.ToString();
            }
            set
            {
                try
                {
                    RegisterTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    RegisterTime = null;
                }
            }
        }

        #endregion

        /// <summary>
        /// 上一次登录时间，用20150101表示无效日期
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        [DataMember]
        public string LastLoginTimeString
        {
            get
            {
                if (this.LastLoginTime == null)
                {
                    return "";
                }
                return this.LastLoginTime.ToString();
            }
            set
            {
                try
                {
                    LastLoginTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    LastLoginTime = null;
                }
            }
        }

        /// <summary>
        /// 上一次登出时间，用20150101表示无效日期
        /// </summary>
        public DateTime? LastLogOutTime { get; set; }
        [DataMember]
        public string LastLogOutTimeString
        {
            get
            {
                if (LastLogOutTime == null)
                {
                    return "";
                }
                return this.LastLogOutTime.ToString();
            }
            set
            {
                try
                {
                    LastLogOutTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    LastLogOutTime = null;
                }
            }
        }

        /// <summary>
        /// 推荐人
        /// </summary>
        [DataMember]
        public string ReferrerUserName { get; set; }

        [DataMember]
        public bool LockedLogin { get; set; }

        public DateTime? LockedLoginTime { get; set; }

        [DataMember]
        public string LockedLoginTimeString
        {
            get
            {
                if (LockedLoginTime == null)
                {
                    return "";
                }
                return this.LockedLoginTime.ToString();
            }
            set
            {
                try
                {
                    LockedLoginTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    LockedLoginTime = null;
                }
            }
        }
    }
}
