using MetaData.StoneFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperMinersWPF.Models
{
    public class StoneFactoryProfitRMBChangedRecordUIModel : BaseModel
    {
        public StoneFactoryProfitRMBChangedRecordUIModel(StoneFactoryProfitRMBChangedRecord parent)
        {
            this.ParentObject = parent;
        }

        private StoneFactoryProfitRMBChangedRecord _parentObject;

        public StoneFactoryProfitRMBChangedRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("ID");
                NotifyPropertyChange("UserID");
                NotifyPropertyChange("OperRMB");
                NotifyPropertyChange("OperRMBText");
                NotifyPropertyChange("ProfitType");
                NotifyPropertyChange("ProfitTypeText");
                NotifyPropertyChange("OperTimeText");
                NotifyPropertyChange("Background");
            }
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public int UserID
        {
            get
            {
                return this._parentObject.UserID;
            }
        }

        /// <summary>
        /// 操作灵币，即包括加工厂生产出的收益入账、推荐注册的三级内玩家收获奖励入账，也包括提现出账（出账时值为负数）
        /// </summary>
        public decimal OperRMB
        {
            get
            {
                return this._parentObject.OperRMB;
            }
        }

        public string OperRMBText
        {
            get
            {
                if (OperRMB > 0)
                {
                    return "+" + OperRMB.ToString("0.00");
                }
                else if (OperRMB < 0)
                {
                    return OperRMB.ToString("0.00");
                }
                else
                {
                    return OperRMB.ToString();
                }
            }
        }

        public FactoryProfitOperType ProfitType
        {
            get
            {
                return this._parentObject.ProfitType;
            }
        }

        public string ProfitTypeText
        {
            get
            {
                string text = "";
                switch (this.ProfitType)
                {
                    case FactoryProfitOperType.FactoryOutput:
                        text = "工厂收益";
                        break;
                    case FactoryProfitOperType.WithdrawRMB:
                        text = "提现";
                        break;
                    case FactoryProfitOperType.Level1ReferenceAward:
                        text = "一级返利";
                        break;
                    case FactoryProfitOperType.Level2ReferenceAward:
                        text = "二级返利";
                        break;
                    case FactoryProfitOperType.Level3ReferenceAward:
                        text = "三级返利";
                        break;
                    default:
                        break;
                }
                return text;
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperTimeText
        {
            get
            {
                return this._parentObject.OperTime.ToString();
            }
        }

        private SolidColorBrush _withdrawRMBBackground = new SolidColorBrush(Color.FromArgb((byte)0xFF, (byte)0x90, (byte)0xE5, (byte)0xDF));

        public SolidColorBrush Background
        {
            get
            {
                return this.ProfitType == FactoryProfitOperType.WithdrawRMB ? _withdrawRMBBackground : null;
            }
        }

    }
}
