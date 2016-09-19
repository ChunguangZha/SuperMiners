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
        public bool IsNull = true;

        [DataMember]
        public int Year;

        [DataMember]
        public int Month;

        [DataMember]
        public int Day;

        [DataMember]
        public int Hour;

        [DataMember]
        public int Minute;

        [DataMember]
        public int Second;

        public MyDateTime()
        {
            DateTime timenow = DateTime.Now;
            Year = timenow.Year;
            Month = timenow.Month;
            Day = timenow.Day;
            Hour = timenow.Hour;
            Minute = timenow.Minute;
            Second = timenow.Second;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day, Hour, Minute, Second);
        }

        public static MyDateTime FromDateTime(DateTime time)
        {
            return new MyDateTime()
            {
                IsNull = false,
                Year = time.Year,
                Month = time.Month,
                Day = time.Day,
                Hour = time.Hour,
                Minute = time.Minute,
                Second = time.Second
            };
        }
    }
}
