using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class ExpChangeRecordUIModel
    {
        public ExpChangeRecordUIModel(ExpChangeRecord parent)
        {
            this.ParentObject = parent;
        }

        private ExpChangeRecord _parentObject;

        public ExpChangeRecord ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public int UserID
        {
            get
            {
                return _parentObject.UserID;
            }
        }

        public string UserName
        {
            get
            {
                return _parentObject.UserName;
            }
        }

        public decimal AddExp
        {
            get
            {
                return _parentObject.AddExp;
            }
        }

        public decimal NewExp
        {
            get
            {
                return _parentObject.NewExp;
            }
        }

        public DateTime Time
        {
            get
            {
                return _parentObject.Time.ToDateTime();
            }
        }

        public string OperContent
        {
            get
            {
                return _parentObject.OperContent;
            }
        }

    }
}
