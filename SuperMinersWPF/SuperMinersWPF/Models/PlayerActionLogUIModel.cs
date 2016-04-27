using MetaData.ActionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class PlayerActionLogUIModel
    {
        PlayerActionLog _parentObject;

        public PlayerActionLogUIModel(PlayerActionLog parent)
        {
            this._parentObject = parent;
        }

        public DateTime Time
        {
            get
            {
                return this._parentObject.Time;
            }
        }

        public string UserName
        {
            get
            {
                return this._parentObject.UserName;
            }
        }

        public string LogMessage
        {
            get
            {
                return "";
            }
        }
    }
}
