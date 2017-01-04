using MetaData.Game.RaideroftheLostArk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersWPF.Models
{
    public class RaiderRoundMetaDataInfoUIModel : BaseModel
    {
        public RaiderRoundMetaDataInfoUIModel(RaiderRoundMetaDataInfo parent)
        {
            this.ParentObject = parent;
        }

        private RaiderRoundMetaDataInfo _parentObject;

        public RaiderRoundMetaDataInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;

                NotifyPropertyChange("ID");
                NotifyPropertyChange("State");
                NotifyPropertyChange("StateText");
                NotifyPropertyChange("StartTimeText");
                NotifyPropertyChange("CountDownMinutes");
                NotifyPropertyChange("CountDownSeconds");
                NotifyPropertyChange("AwardPoolSumStones");
                NotifyPropertyChange("JoinedPlayerCount");
                NotifyPropertyChange("WinnerUserName");
                NotifyPropertyChange("WinStones");
                NotifyPropertyChange("EndTimeText");
                NotifyPropertyChange("BetJoinButtonVisible");
                NotifyPropertyChange("WinnerUserNameVisible");
            }
        }

        public int ID
        {
            get { return this._parentObject.ID; }
        }

        /// <summary>
        /// Not Null
        /// </summary>
        public RaiderRoundState State
        {
            get
            {
                return this._parentObject.State;
            }
        }

        public string StateText
        {
            get
            {
                string text = "已结束";
                switch (this.State)
                {
                    case RaiderRoundState.Waiting:
                        text = "等待下注";
                        break;
                    case RaiderRoundState.Started:
                        text = "已开始";
                        break;
                    case RaiderRoundState.Finished:
                        text = "已结束";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        public string StartTimeText
        {
            get
            {
                if (this._parentObject.StartTime == null)
                {
                    return "";
                }
                return this._parentObject.StartTime.ToDateTime().ToString();
            }
        }

        public int CountDownMinutes
        {
            get
            {
                int mintues = 5;
                if (this._parentObject.CountDownTotalSecond != 0)
                {
                    mintues = (int)this._parentObject.CountDownTotalSecond / 60;
                }

                return mintues;
            }
        }

        public int CountDownSeconds
        {
            get
            {
                int seconds = 0;
                if (this._parentObject.CountDownTotalSecond != 0)
                {
                    seconds = (int)this._parentObject.CountDownTotalSecond % 60;
                }

                return seconds;
            }
        }

        public int AwardPoolSumStones
        {
            get
            {
                return this._parentObject.AwardPoolSumStones;
            }
        }

        public int JoinedPlayerCount
        {
            get { return this._parentObject.JoinedPlayerCount; }
        }

        public string WinnerUserName
        {
            get
            {
                if (this._parentObject.WinnerUserName == null)
                {
                    return "";
                }
                return this._parentObject.WinnerUserName;
            }
        }

        public int WinStones
        {
            get
            {
                return this._parentObject.WinStones;
            }
        }

        public string EndTimeText
        {
            get
            {
                if (this._parentObject.EndTime == null)
                {
                    return "";
                }

                return this._parentObject.EndTime.ToDateTime().ToString();
            }
        }

        public Visibility BetJoinButtonVisible
        {
            get
            {
                if (this._parentObject.State != RaiderRoundState.Finished)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility WinnerUserNameVisible
        {
            get
            {
                if (this._parentObject.State == RaiderRoundState.Finished)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }
    }
}
