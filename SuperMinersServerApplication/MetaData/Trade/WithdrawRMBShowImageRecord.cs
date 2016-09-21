using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class WithdrawRMBShowImageRecord
    {
        

        /// <summary>
        /// NOT NULL
        /// </summary>
        [DataMember]
        public string PlayerUserName = "";

        /// <summary>
        /// NOT NULL
        /// </summary>
        [DataMember]
        public MyDateTime ShowImageTime;

        /// <summary>
        /// NOT NULL
        /// </summary>
        [DataMember]
        public byte[] ImageSource;

        /// <summary>
        /// 小于0为无效。
        /// </summary>
        [DataMember]
        public int WithdrawRMBRecordID = -1;

        [DataMember]
        public int AwardGoldCoin;

        /// <summary>
        /// NULL
        /// </summary>
        [DataMember]
        public string AdminUserName;

        /// <summary>
        /// NULL
        /// </summary>
        [DataMember]
        public MyDateTime ConfirmTime;
    }
}
