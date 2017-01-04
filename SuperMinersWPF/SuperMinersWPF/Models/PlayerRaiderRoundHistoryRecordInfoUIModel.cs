using MetaData.Game.RaideroftheLostArk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class PlayerRaiderRoundHistoryRecordInfoUIModel : BaseModel
    {
        public PlayerRaiderRoundHistoryRecordInfoUIModel(PlayerRaiderRoundHistoryRecordInfo parent)
        {
            this.ParentObject = parent;
        }

        private PlayerRaiderRoundHistoryRecordInfo _parentObject;

        public PlayerRaiderRoundHistoryRecordInfo ParentObject
        {
            get { return _parentObject; }
            set{
                this._parentObject = value;

                NotifyPropertyChange("ID");
                NotifyPropertyChange("State");
                NotifyPropertyChange("StateText");
                NotifyPropertyChange("StartTimeText");
                NotifyPropertyChange("AwardPoolSumStones");
                NotifyPropertyChange("JoinedPlayerCount");
                NotifyPropertyChange("WinnerUserName");
                NotifyPropertyChange("WinStones");
                NotifyPropertyChange("EndTimeText");
                NotifyPropertyChange("BetJoinStoneCount");
            }
        }

        public int ID
        {
            get { return this._parentObject.RoundInfo.ID; }
        }

        /// <summary>
        /// Not Null
        /// </summary>
        public RaiderRoundState State
        {
            get
            {
                return this._parentObject.RoundInfo.State;
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
                if (this._parentObject.RoundInfo.StartTime == null)
                {
                    return "";
                }
                return this._parentObject.RoundInfo.StartTime.ToDateTime().ToString();
            }
        }

        public int AwardPoolSumStones
        {
            get
            {
                return this._parentObject.RoundInfo.AwardPoolSumStones;
            }
        }

        public int JoinedPlayerCount
        {
            get { return this._parentObject.RoundInfo.JoinedPlayerCount; }
        }

        public string WinnerUserName
        {
            get
            {
                if (this._parentObject.RoundInfo.WinnerUserName == null)
                {
                    return "";
                }

                string userName = this._parentObject.RoundInfo.WinnerUserName;
                //return userName.Substring(0, 1) + "***" + userName.Substring(userName.Length - 1, 1);
                return userName;
            }
        }

        public int WinStones
        {
            get
            {
                return this._parentObject.RoundInfo.WinStones;
            }
        }

        public string EndTimeText
        {
            get
            {
                if (this._parentObject.RoundInfo.EndTime == null)
                {
                    return "";
                }

                return this._parentObject.RoundInfo.EndTime.ToDateTime().ToString();
            }
        }


        public int BetJoinStoneCount
        {
            get
            {
                return this._parentObject.BetJoinStoneCount;
            }
        }
    }
}
