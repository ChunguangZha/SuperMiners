using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SuperMinersCustomServiceSystem.Actions
{
    public abstract class BaseAction : INotifyPropertyChanged
    {
        public abstract string NavMenu { get; }

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

        /// <summary>
        /// 可以为null
        /// </summary>
        public UserControl UserControl { get; private set; }
        private TreeViewItem _tvItem;

        //public BaseAction(UserControl usercontrol, TreeViewItem tv)
        //{
        //    this.UserControl = usercontrol;
        //    this._tvItem = tv;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
