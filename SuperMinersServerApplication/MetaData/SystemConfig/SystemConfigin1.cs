using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    [DataContract]
    public class SystemConfigin1
    {
        [DataMember]
        public GameConfig GameConfig;

        [DataMember]
        public AwardReferrerConfig[] AwardReferrerConfigList;

        [DataMember]
        public RegisterUserConfig RegisterUserConfig;

        [DataMember]
        public RouletteConfig RouletteConfig;
    }
}
