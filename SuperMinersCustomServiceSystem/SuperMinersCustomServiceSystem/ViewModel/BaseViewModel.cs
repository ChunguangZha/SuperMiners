using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public abstract string MenuHeader { get; }

        private int _activeItemsCount;

        public int ActiveItemsCount
        {
            get { return _activeItemsCount; }
            set
            {
                _activeItemsCount = value;
                NotifyPropertyChanged("ActiveItemsCountString");
                NotifyPropertyChanged("ActiveItemsCountVisibility");
            }
        }

        public string ActiveItemsCountString
        {
            get
            {
                if (ActiveItemsCount < 100)
                {
                    return ActiveItemsCount.ToString();
                }

                return "99+";
            }
        }

        public Visibility ActiveItemsCountVisibility
        {
            get
            {
                if (ActiveItemsCount > 0)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }

        internal void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
