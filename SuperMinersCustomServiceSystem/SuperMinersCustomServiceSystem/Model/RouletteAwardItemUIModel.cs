using MetaData.Game.Roulette;
using System;
#if Client
using SuperMinersWPF.Models;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace SuperMinersCustomServiceSystem.Model
{
    public class RouletteAwardItemUIModel : BaseModel
    {
        public RouletteAwardItemUIModel(RouletteAwardItem parent)
        {
            this.ParentObject = parent;
        }

        private RouletteAwardItem _parentObject;

        public RouletteAwardItem ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                this._icon = null;
                if (this._parentObject.IconBuffer != null)
                {
                    this._icon = new BitmapImage();
                    this._icon.StreamSource = new MemoryStream(this._parentObject.IconBuffer);
                }

                NotifyPropertyChange("ID");
                NotifyPropertyChange("AwardName");
                NotifyPropertyChange("AwardNumber");
                NotifyPropertyChange("RouletteAwardType");
                NotifyPropertyChange("ValueMoneyYuan");
                NotifyPropertyChange("IsLargeAward");
                NotifyPropertyChange("WinProbability");
                NotifyPropertyChange("Icon");
            }
        }

        public int ID
        {
            get { return this.ParentObject.ID; }
        }

        public string AwardName
        {
            get { return this.ParentObject.AwardName; }
            set
            {
                this.ParentObject.AwardName = value;
            }
        }

        public int AwardNumber
        {
            get { return this.ParentObject.AwardNumber; }
            set
            {
                this.ParentObject.AwardNumber = value;
            }
        }

        public RouletteAwardType RouletteAwardType
        {
            get { return this.ParentObject.RouletteAwardType; }
            set
            {
                this.ParentObject.RouletteAwardType = value;
            }
        }

        /// <summary>
        /// 价值人民币
        /// </summary>
        public float ValueMoneyYuan
        {
            get { return this.ParentObject.ValueMoneyYuan; }
            set
            {
                this.ParentObject.ValueMoneyYuan = value;
            }
        }

        public bool IsLargeAward
        {
            get { return this.ParentObject.IsLargeAward; }
            set
            {
                this.ParentObject.IsLargeAward = value;
            }
        }

        /// <summary>
        /// 中奖概率倍数，整数值，计算时将所有中中奖概率加一起求百分比
        /// </summary>
        public float WinProbability
        {
            get { return this.ParentObject.WinProbability; }
            set
            {
                this.ParentObject.WinProbability = value;
            }
        }

        private BitmapImage _icon = null;

        public BitmapImage Icon
        {
            get
            {
                return _icon;
            }
        }

    }
}
