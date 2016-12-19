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
        public NoticeInfo()
        {
            CreateNewFileName();
        }

        private bool _isChecked;

        public bool Checked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Checked"));
                }
            }
        }


        [DataMember]
        public string Title { get; set; }

        public DateTime Time { get; set; }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        public void CreateNewFileName()
        {
            _fileName = Time.ToLongDateString() + Time.Hour.ToString() + Time.Minute.ToString() + Time.Second.ToString() + " " + Title + (new Random().Next(999)).ToString();
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
