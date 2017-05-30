using MetaData;
using MetaData.StoneFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
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
            set { _parentObject = value; }
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        /// <summary>
        /// 收益率
        /// </summary>
        public decimal profitRate
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public MyDateTime Day
        {
            get
            {
                return this._parentObject.Day;
            }
        }

        public string DayText
        {
            get
            {
                return this.Day.Month.ToString() + "月" + this.Day.Day.ToString() + "日";
            }
        }
    }
}
