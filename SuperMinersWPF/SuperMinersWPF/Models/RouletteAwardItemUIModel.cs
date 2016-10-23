using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SuperMinersWPF.Models
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
            set { _parentObject = value; }
        }

        public int ID
        {
            get { return this.ParentObject.ID; }
        }

        private SolidColorBrush _background = new SolidColorBrush(Color.FromArgb(255, 255, 220, 21));

        public SolidColorBrush Background
        {
            get { return this._background; }
            set
            {
                this._background = value;
                NotifyPropertyChange("Background");
            }
        }

        public BitmapImage Icon
        {
            get
            {
                string imageFile = "";
                switch (RouletteAwardType)
                {
                    case RouletteAwardType.None:
                        imageFile = "again.png";
                        break;
                    case RouletteAwardType.Stone:
                        imageFile = "stone.png";
                        break;
                    case RouletteAwardType.GoldCoin:
                        imageFile = "goldcoin.png";
                        break;
                    case RouletteAwardType.Exp:
                        imageFile = "exp.png";
                        break;
                    case RouletteAwardType.StoneReserve:
                        imageFile = "";
                        break;
                    case RouletteAwardType.Huafei:
                        imageFile = "phonefees.png";
                        break;
                    case RouletteAwardType.IQiyiOneMonth:
                        imageFile = "iqiyi.png";
                        break;
                    case RouletteAwardType.LeTV:
                        imageFile = "letv.png";
                        break;
                    case RouletteAwardType.Xunlei:
                        imageFile = "xunlei.png";
                        break;
                    case RouletteAwardType.Junnet:
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrEmpty(imageFile))
                {
                    return null;
                }
                return new BitmapImage(new Uri("pack://application:,,,/Resources/" + imageFile));
            }
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
        /// 是否为实物奖品，除了系统内部的都称为实物
        /// </summary>
        public bool IsRealAward
        {
            get { return this.ParentObject.IsRealAward; }
            set
            {
                this.ParentObject.IsRealAward = value;
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

    }
}
