using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    public class PlayerLoginInfo
    {
        public int ID { get; set; }

        public string LoginIP { get; set; }

        public string LoginMac { get; set; }

        public DateTime LoginTime { get; set; }

        public int UserID { get; set; }
    }
}
