using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SuperMinersWPF.Models
{
    class TopListInfoUIModel
    {
        private TopListInfo _parentObject;

        public TopListInfoUIModel(int index, TopListInfo parent)
        {
            Index = index + 1;
            this._parentObject = parent;
        }

        public int Index
        {
            get;
            private set;
        }

        public ImageSource IndexImage
        {
            get
            {
                if (this.Index == 1)
                {
                    return new BitmapImage(new Uri("/Resources/1.png", UriKind.Relative));
                }
                if (this.Index == 2)
                {
                    return new BitmapImage(new Uri("/Resources/2.png", UriKind.Relative));
                }
                if (this.Index == 3)
                {
                    return new BitmapImage(new Uri("/Resources/3.png", UriKind.Relative));
                }

                return null;
            }
        }

        public Brush Foreground
        {
            get
            {
                if (this.Index == 1)
                {
                    return new SolidColorBrush(Colors.White);
                }
                if (this.Index == 2)
                {
                    return new SolidColorBrush(Colors.White);
                }
                if (this.Index == 3)
                {
                    return new SolidColorBrush(Colors.White);
                }

                return new SolidColorBrush(Colors.Gray);
            }
        }

        public string UserName
        {
            get
            {
                return this._parentObject.UserName;
            }
        }

        public string NickName
        {
            get
            {
                return this._parentObject.NickName;
            }
        }

        public decimal Value
        {
            get
            {
                return this._parentObject.Value;
            }
        }
    }
}
