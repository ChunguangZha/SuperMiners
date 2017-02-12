using MetaData.Game.GambleStone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersWPF.Models
{
    public class GambleStoneInningInfoUIModel : BaseModel
    {
        public GambleStoneInningInfoUIModel(GambleStoneInningInfo parent)
        {
            this.ParentObject = parent;
        }

        private GambleStoneInningInfo _parenObject;

        public GambleStoneInningInfo ParentObject
        {
            get { return _parenObject; }
            set
            {
                _parenObject = value;
                RefreshUI();
            }
        }

        public void RefreshUI()
        {
            NotifyPropertyChange("ID");
            NotifyPropertyChange("InningIndex");
            NotifyPropertyChange("RoundID");
            NotifyPropertyChange("CountDownSeconds");
            NotifyPropertyChange("CountDownSecondsText");
            NotifyPropertyChange("SrcStoneVisible");
            NotifyPropertyChange("BitWinnedColorVisible");
            NotifyPropertyChange("BetInButtonEnable");
            NotifyPropertyChange("BetRedStone");
            NotifyPropertyChange("BetGreenStone");
            NotifyPropertyChange("BetBlueStone");
            NotifyPropertyChange("BetPurpleStone");
            NotifyPropertyChange("BetRedStoneText");
            NotifyPropertyChange("BetGreenStoneText");
            NotifyPropertyChange("BetBlueStoneText");
            NotifyPropertyChange("BetPurpleStoneText");
            NotifyPropertyChange("WinnedTimes");
            NotifyPropertyChange("WinnedOutStone");
            NotifyPropertyChange("WinnedColor");
        }

        public string ID
        {
            get
            {
                return this._parenObject.ID;
            }
        }

        /// <summary>
        /// 第N局（1-64）
        /// </summary>
        public int InningIndex
        {
            get
            {
                return this._parenObject.InningIndex;
            }
        }

        public int RoundID
        {
            get
            {
                return this._parenObject.RoundID;
            }
        }

        public int CountDownSeconds
        {
            get
            {
                return this._parenObject.CountDownSeconds;
            }
        }

        public string CountDownSecondsText
        {
            get
            {
                if (this.CountDownSeconds <= 0)
                {
                    return "";
                }

                string text = "";

                switch (this._parenObject.State)
                {
                    case GambleStoneInningStatusType.Readying:
                        text = this.CountDownSeconds + " 秒后开始";
                        break;
                    case GambleStoneInningStatusType.BetInWaiting:
                        text = this.CountDownSeconds + " 秒后切开";
                        break;
                    case GambleStoneInningStatusType.Opening:
                        text = "开奖 " + this.CountDownSeconds;
                        break;
                    case GambleStoneInningStatusType.Finished:
                        text = "此局结束";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        public Visibility SrcStoneVisible
        {
            get
            {
                switch (this._parenObject.State)
                {
                    case GambleStoneInningStatusType.Readying:
                        return Visibility.Visible;
                    case GambleStoneInningStatusType.BetInWaiting:
                        return Visibility.Visible;
                    case GambleStoneInningStatusType.Opening:
                        return Visibility.Collapsed;
                    case GambleStoneInningStatusType.Finished:
                        return Visibility.Collapsed;
                    default:
                        return Visibility.Visible;
                }
            }
        }

        public Visibility BitWinnedColorVisible
        {
            get
            {
                switch (this._parenObject.State)
                {
                    case GambleStoneInningStatusType.Readying:
                        return Visibility.Collapsed;
                    case GambleStoneInningStatusType.BetInWaiting:
                        return Visibility.Collapsed;
                    case GambleStoneInningStatusType.Opening:
                        return Visibility.Visible;
                    case GambleStoneInningStatusType.Finished:
                        return Visibility.Visible;
                    default:
                        return Visibility.Collapsed;
                }
            }
        }

        public bool BetInButtonEnable
        {
            get
            {
                return this._parenObject.State == GambleStoneInningStatusType.BetInWaiting && this.CountDownSeconds > 0;
            }
        }

        public int BetRedStone
        {
            get
            {
                return this._parenObject.BetRedStone;
            }
        }

        public string BetRedStoneText
        {
            get
            {
                return this._parenObject.BetRedStone.ToString();
            }
        }

        public int BetGreenStone
        {
            get
            {
                return this._parenObject.BetGreenStone;
            }
        }

        public string BetGreenStoneText
        {
            get
            {
                return this._parenObject.BetGreenStone.ToString();
            }
        }

        public int BetBlueStone
        {
            get
            {
                return this._parenObject.BetBlueStone;
            }
        }

        public string BetBlueStoneText
        {
            get
            {
                return this._parenObject.BetBlueStone.ToString();
            }
        }

        public int BetPurpleStone
        {
            get
            {
                return this._parenObject.BetPurpleStone;
            }
        }

        public string BetPurpleStoneText
        {
            get
            {
                return this._parenObject.BetPurpleStone.ToString();
            }
        }

        public GambleStoneItemColor WinnedColor
        {
            get
            {
                return this._parenObject.WinnedColor;
            }
        }

        public int WinnedTimes
        {
            get
            {
                return this._parenObject.WinnedTimes;
            }
        }

        public int WinnedOutStone
        {
            get
            {
                return this._parenObject.WinnedOutStone;
            }
        }


    }
}
