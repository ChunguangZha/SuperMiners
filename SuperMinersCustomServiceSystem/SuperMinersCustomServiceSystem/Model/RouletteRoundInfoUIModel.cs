using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class RouletteRoundInfoUIModel : BaseModel
    {
        public RouletteRoundInfoUIModel(RouletteRoundInfo parent)
        {
            this.ParentObject = parent;
        }

        private RouletteRoundInfo _parentObject;

        public RouletteRoundInfo ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public int ID
        {
            get { return this._parentObject.ID; }
        }

        /// <summary>
        /// 奖池累计矿石
        /// </summary>
        public int AwardPoolSumStone
        {
            get { return this._parentObject.AwardPoolSumStone; }
        }

        /// <summary>
        /// 本轮累计中出价值人民币元
        /// </summary>
        public decimal WinAwardSumYuan
        {
            get { return this._parentObject.WinAwardSumYuan; }
        }

        public decimal CurrentRoundProfitYuan
        {
            get
            {
                return this.AwardPoolSumStone / GlobalData.GameConfig.Stones_RMB / GlobalData.GameConfig.Yuan_RMB - this.WinAwardSumYuan;
            }
        }

        public DateTime StartTime
        {
            get { return this._parentObject.StartTime; }
        }

        public int MustWinAwardItemID
        {
            get { return this._parentObject.MustWinAwardItemID; }
        }

        public bool Finished
        {
            get { return this._parentObject.Finished; }
        }
    }
}
