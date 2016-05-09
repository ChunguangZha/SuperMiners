//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;

//namespace MetaData.User
//{
//    /// <summary>
//    /// TODO: 需要建表啊！！数据库结构
//    /// </summary>
//    [DataContract]
//    public class PlayerTempOutputStoneInfo
//    {
//        [DataMember]
//        public string UserName { get; set; }

//        public DateTime? StartTime = null;
//        [DataMember]
//        public string StartTimeString
//        {
//            get
//            {
//                if (this.StartTime == null)
//                {
//                    return "";
//                }
//                return this.StartTime.ToString();
//            }
//            set
//            {
//                try
//                {
//                    StartTime = DateTime.Parse(value);
//                }
//                catch (Exception)
//                {
//                    StartTime = null;
//                }
//            }
//        }
        
//        [DataMember]
//        public float TempOutput;
//    }
//}
