using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class UserReferrerTreeItem
    {
        /// <summary>
        /// 小于0表示上线，0表示自己，大于0表示下线。
        /// </summary>
        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string RegisterIP { get; set; }

        #region RegisterTime

        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
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
                    RegisterTime = Common.INVALIDTIME;
                }
            }
        }

        #endregion

    }
}
