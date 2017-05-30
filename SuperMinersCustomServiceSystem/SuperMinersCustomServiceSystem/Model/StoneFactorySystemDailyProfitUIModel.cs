using MetaData.StoneFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class StoneFactorySystemDailyProfitUIModel : BaseModel
    {
        public StoneFactorySystemDailyProfitUIModel(StoneFactorySystemDailyProfit parent)
        {
            this.ParentObject = parent;
        }

        private StoneFactorySystemDailyProfit _parentObject;

        public StoneFactorySystemDailyProfit ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("ID");
                NotifyPropertyChange("ProfitRate");
                NotifyPropertyChange("DayText");
            }
        }

        public int ID
        {
            get { return this._parentObject.ID; }
        }

        /// <summary>
        /// 收益率
        /// </summary>
        public decimal ProfitRate
        {
            get { return this._parentObject.profitRate; }
        }

        public string DayText
        {
            get { return this._parentObject.Day.ToString(); }
        }

    }
}
