using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public class Common
    {
        public static readonly DateTime INVALIDTIME = new DateTime(2000, 1, 1);

    }

    [DataContract]
    public class MyDateTime
    {
        [DataMember]
        public bool IsNull;

        [DataMember]
        public int Year;

        [DataMember]
        public int Month;

        [DataMember]
        public int Day;

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        public static MyDateTime FromDateTime(DateTime time)
        {
            return new MyDateTime()
            {
                IsNull = false,
                Year = time.Year,
                Month = time.Month,
                Day = time.Day
            };
        }
    }
}
