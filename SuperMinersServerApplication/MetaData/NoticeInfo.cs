using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    [DataContract]
    public class NoticeInfo : INotifyPropertyChanged
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }


        [DataMember]
        public string Title { get; set; }

        public DateTime Time { get; set; }

        public string FileName
        {
            get
            {
                string fileName = Time.ToLongDateString() + Time.Hour.ToString() +  Time.Minute.ToString() + Time.Second.ToString() + " "  + Title + (new Random().Next(999)).ToString();
                return fileName;
            }
        }

        [DataMember]
        public string TimeString
        {
            get
            {
                if (this.Time == null)
                {
                    return "";
                }
                return this.Time.ToString();
            }
            set
            {
                try
                {
                    Time = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    Time = DateTime.MinValue;
                }
            }
        }

        [DataMember]
        public string Content { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
