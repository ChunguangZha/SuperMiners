using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    public class AdminInfo
    {
        /// <summary>
        /// maxlength=15
        /// </summary>
        public string UserName;

        /// <summary>
        /// minlength= 6, maxlength=15
        /// </summary>
        public string LoginPassword;

        /// <summary>
        /// minlength= 6, maxlength=15
        /// </summary>
        public string ActionPassword;

        public string[] Macs;
    }
}
