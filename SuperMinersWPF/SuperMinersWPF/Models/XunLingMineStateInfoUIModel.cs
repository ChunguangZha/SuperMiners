using MetaData.ActionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class XunLingMineStateInfoUIModel : BaseModel
    {
        public XunLingMineStateInfoUIModel(XunLingMineStateInfo parent)
        {
            this.ParentObject = parent;
        }

        private XunLingMineStateInfo _parentObject;

        public XunLingMineStateInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("AllPlayerCount");
                NotifyPropertyChange("AllMinersCount");
                NotifyPropertyChange("AllStonesReserves");
                NotifyPropertyChange("AllProducedStonesCount");
                NotifyPropertyChange("AllStockOfStones");
                NotifyPropertyChange("AllStonesCount");
                NotifyPropertyChange("SurplusBuyableStoneCount");
            }
        }


        public int AllPlayerCount
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.AllPlayerCount;
            }
        }

        public int AllMinersCount
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.AllMinersCount;
            }
        }

        public int AllStonesReserves
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.AllStonesReserves;
            }
        }

        public decimal AllProducedStonesCount
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.AllProducedStonesCount;
            }
        }

        public decimal AllStockOfStones
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.AllStockOfStones;
            }
        }

        /// <summary>
        /// 所有矿石量（包括库存量和未开采量）
        /// </summary>
        public decimal AllStonesCount
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.AllStonesCount;
            }
        }

        /// <summary>
        /// 剩余可勘探矿石量
        /// </summary>
        public string SurplusBuyableStoneCount
        {
            get
            {
                if (this._parentObject == null)
                {
                    return "";
                }

                if (GlobalData.CurrentUser.ExpLevel == 0)
                {
                    return "VIP可见";
                }
                int surplusValue = GlobalData.GameConfig.LimitStoneCount - (int)AllStonesCount;
                if (surplusValue < 0)
                {
                    return "已溢出";
                }
                return surplusValue.ToString() + "矿石";
            }
        }
    }
}
