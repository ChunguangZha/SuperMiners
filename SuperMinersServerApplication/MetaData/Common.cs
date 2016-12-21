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
    public class MyDateTime : IComparable, IComparer<MyDateTime>
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

        public DateTime dTime = DateTime.Now;

        public MyDateTime()
        {
            dTime = DateTime.Now;
            Year = dTime.Year;
            Month = dTime.Month;
            Day = dTime.Day;
            Hour = dTime.Hour;
            Minute = dTime.Minute;
            Second = dTime.Second;
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

        public static MyDateTime FromString(string timeString)
        {
            try
            {
                if (string.IsNullOrEmpty(timeString))
                {
                    return new MyDateTime();
                }

                DateTime dtime = DateTime.Now;
                DateTime.TryParse(timeString, out dtime);
                return FromDateTime(dtime);
            }
            catch
            {
                return new MyDateTime();
            }
        }

        public override string ToString()
        {
            return this.ToDateTime().ToString();
        }

        public int CompareTo(object obj)
        {
            MyDateTime other = obj as MyDateTime;
            if (other == null)
            {
                return -1;
            }

            DateTime thisTime = this.ToDateTime();
            DateTime otherTime = other.ToDateTime();
            return thisTime.CompareTo(otherTime);
        }

        public int Compare(MyDateTime x, MyDateTime y)
        {
            DateTime xTime = x.ToDateTime();
            DateTime yTime = y.ToDateTime();
            return xTime.CompareTo(yTime);
        }
    }
}
