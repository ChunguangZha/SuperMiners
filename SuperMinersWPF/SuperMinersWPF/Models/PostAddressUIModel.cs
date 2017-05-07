using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class PostAddressUIModel : BaseModel
    {
        public PostAddressUIModel(PostAddress parent)
        {
            this.ParentObject = parent;
        }

        private PostAddress _parentObject;

        public PostAddress ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("Province");
                NotifyPropertyChange("City");
                NotifyPropertyChange("County");
                NotifyPropertyChange("DetailAddress");
                NotifyPropertyChange("ReceiverName");
                NotifyPropertyChange("PhoneNumber");
            }
        }

        public string Province
        {
            get
            {
                return this._parentObject.Province;
            }
            set
            {
                this._parentObject.Province = value;
                NotifyPropertyChange("Province");
            }
        }

        public string City
        {
            get
            {
                return this._parentObject.City;
            }
            set
            {
                this._parentObject.City = value;
                NotifyPropertyChange("City");
            }
        }

        public string County
        {
            get
            {
                return this._parentObject.County;
            }
            set
            {
                this._parentObject.County = value;
                NotifyPropertyChange("County");
            }
        }

        public string DetailAddress
        {
            get
            {
                return this._parentObject.DetailAddress;
            }
            set
            {
                this._parentObject.DetailAddress = value;
                NotifyPropertyChange("DetailAddress");
            }
        }

        public string ReceiverName
        {
            get
            {
                return this._parentObject.ReceiverName;
            }
            set
            {
                this._parentObject.ReceiverName = value;
                NotifyPropertyChange("ReceiverName");
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this._parentObject.PhoneNumber;
            }
            set
            {
                this._parentObject.PhoneNumber = value;
                NotifyPropertyChange("PhoneNumber");
            }
        }

    }
}
