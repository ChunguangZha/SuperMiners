using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public float Value
        {
            get
            {
                return this._parentObject.Value;
            }
        }
    }
}
