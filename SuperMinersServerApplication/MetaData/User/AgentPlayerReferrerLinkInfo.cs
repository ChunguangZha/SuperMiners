using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    public class AgentPlayerReferrerLinkInfo
    {
        public int UserID { get; set; }

        /// <summary>
        /// 长度为300
        /// </summary>
        public string ReferrerLink { get; set; }
    }
}
