using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Model
{
    [DataContract]
    public class PlayerInfoLoginWrap
    {
        [DataMember]
        public bool isOnline
        {
            get;
            set;
        }

        //[DataMember]
        //public string LoginIP
        //{
        //    get;
        //    set;
        //}

        [DataMember]
        public PlayerSimpleInfo SimpleInfo = new PlayerSimpleInfo();

        [DataMember]
        public PlayerFortuneInfo FortuneInfo = new PlayerFortuneInfo();

        [DataMember]
        public PlayerLockedInfo LockedInfo = null;
    }

}
