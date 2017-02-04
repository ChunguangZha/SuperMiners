using MetaData.Game.GambleStone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class GambleStonePlayerBetRecordUIModel : BaseModel
    {
        public GambleStonePlayerBetRecordUIModel(GambleStonePlayerBetRecord parent)
        {
            this.ParentObject = parent;
        }

        private GambleStonePlayerBetRecord _parentObject;

        public GambleStonePlayerBetRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                RefreshUI();
            }
        }

        public void RefreshUI()
        {
            NotifyPropertyChange("UserID");
            NotifyPropertyChange("UserName");
            NotifyPropertyChange("TimeText");
            NotifyPropertyChange("RoundID");
            NotifyPropertyChange("InningID");
            NotifyPropertyChange("BetRedStone");
            NotifyPropertyChange("BetGreenStone");
            NotifyPropertyChange("BetBlueStone");
            NotifyPropertyChange("BetPurpleStone");
            NotifyPropertyChange("BetRedStoneText");
            NotifyPropertyChange("BetGreenStoneText");
            NotifyPropertyChange("BetBlueStoneText");
            NotifyPropertyChange("BetPurpleStoneText");
            NotifyPropertyChange("WinnedStone");
        }

        public void Clear()
        {
            this._parentObject.BetRedStone = 0;
            this._parentObject.BetGreenStone = 0;
            this._parentObject.BetBlueStone = 0;
            this._parentObject.BetPurpleStone = 0;

            NotifyPropertyChange("BetRedStoneText");
            NotifyPropertyChange("BetGreenStoneText");
            NotifyPropertyChange("BetBlueStoneText");
            NotifyPropertyChange("BetPurpleStoneText");
        }

        public int UserID
        {
            get
            {
                return this._parentObject.UserID;
            }
        }

        /// <summary>
        /// 非数据库字段
        /// </summary>
        public string UserName
        {
            get
            {
                return this._parentObject.UserName;
            }
        }

        public string TimeText
        {
            get
            {
                if (this._parentObject.Time == null)
                {
                    return "";
                }
                return this._parentObject.Time.ToDateTime().ToString();
            }
        }

        public int RoundID
        {
            get
            {
                return this._parentObject.RoundID;
            }
        }

        public string InningID
        {
            get
            {
                return this._parentObject.InningID;
            }
        }

        public int BetRedStone
        {
            get
            {
                return this._parentObject.BetRedStone;
            }
        }

        public int BetGreenStone
        {
            get
            {
                return this._parentObject.BetGreenStone;
            }
        }

        public int BetBlueStone
        {
            get
            {
                return this._parentObject.BetBlueStone;
            }
        }

        public int BetPurpleStone
        {
            get
            {
                return this._parentObject.BetPurpleStone;
            }
        }

        public string BetRedStoneText
        {
            get
            {
                return this._parentObject.BetRedStone.ToString();
            }
        }

        public string BetGreenStoneText
        {
            get
            {
                return this._parentObject.BetGreenStone.ToString();
            }
        }

        public string BetBlueStoneText
        {
            get
            {
                return this._parentObject.BetBlueStone.ToString();
            }
        }

        public string BetPurpleStoneText
        {
            get
            {
                return this._parentObject.BetPurpleStone.ToString();
            }
        }

        public int WinnedStone
        {
            get
            {
                return this._parentObject.WinnedStone;
            }
        }

    }
}
