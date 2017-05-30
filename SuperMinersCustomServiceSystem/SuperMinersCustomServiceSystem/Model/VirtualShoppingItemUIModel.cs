using MetaData.Shopping;
#if Client
using SuperMinersWPF.Models;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using SuperMinersCustomServiceSystem.Uility;

namespace SuperMinersCustomServiceSystem.Model
{
    public class VirtualShoppingItemUIModel : BaseModel
    {
        public VirtualShoppingItemUIModel(VirtualShoppingItem parent)
        {
            ParentObject = parent;
        }

        private VirtualShoppingItem _parentObject;

        public VirtualShoppingItem ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                this._icon = MyImageConverter.GetIconSource(this._parentObject.IconBuffer);

                NotifyPropertyChange("ID");
                NotifyPropertyChange("Name");
                NotifyPropertyChange("Remark");
                NotifyPropertyChange("ItemType");
                NotifyPropertyChange("ItemTypeIndex");
                NotifyPropertyChange("ItemTypeText");
                NotifyPropertyChange("SellState");
                NotifyPropertyChange("SellStateText");
                NotifyPropertyChange("PlayerMaxBuyableCount");
                NotifyPropertyChange("ValueShoppingCredits");
                NotifyPropertyChange("GainExp");
                NotifyPropertyChange("GainRMB");
                NotifyPropertyChange("GainGoldCoin");
                NotifyPropertyChange("GainMine_StoneReserves");
                NotifyPropertyChange("GainMiner");
                NotifyPropertyChange("GainStone");
                NotifyPropertyChange("GainDiamond");
                NotifyPropertyChange("GainShoppingCredits");
                NotifyPropertyChange("GainGravel");
                NotifyPropertyChange("Icon");
            }
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        /// <summary>
        /// MaxLength:100
        /// </summary>
        public string Name
        {
            get
            {
                return this._parentObject.Name;
            }
            set
            {
                this._parentObject.Name = value;
                NotifyPropertyChange("Name");
            }
        }

        /// <summary>
        /// MaxLength:2000
        /// </summary>
        public string Remark
        {
            get
            {
                return this._parentObject.Remark;
            }
            set
            {
                this._parentObject.Remark = value;
                NotifyPropertyChange("Remark");
            }
        }

        public int ItemTypeIndex
        {
            get { return (int)this.ItemType; }
            set
            {
                this.ItemType = (VirtualShoppingItemType)value;
            }
        }

        public VirtualShoppingItemType ItemType
        {
            get
            {
                return this._parentObject.ItemType;
            }
            set
            {
                this._parentObject.ItemType = value;
                NotifyPropertyChange("ItemType");
                NotifyPropertyChange("ItemTypeIndex");
                NotifyPropertyChange("ItemTypeText");
            }
        }

        public string ItemTypeText
        {
            get
            {
                string text = "";
                switch (this.ItemType)
                {
                    case VirtualShoppingItemType.Normal:
                        text = "普通商品";
                        break;
                    case VirtualShoppingItemType.FactoryOpenTool:
                        text = "开启工厂";
                        break;
                    case VirtualShoppingItemType.FactorySlaveFoods30Days:
                        text = "苦力30天食物";
                        break;
                    default:
                        break;
                }
                return text;
            }
        }

        public SellState SellState
        {
            get
            {
                return this._parentObject.SellState;
            }
            set
            {
                this._parentObject.SellState = value;
                NotifyPropertyChange("SellState");
                NotifyPropertyChange("SellStateText");
            }
        }

        public string SellStateText
        {
            get
            {
                string text = "";
                switch (SellState)
                {
                    case SellState.OnSell:
                        text = "上架中";
                        break;
                    case SellState.OffSell:
                        text = "已下架";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        /// <summary>
        /// 玩家最多可以购买该商品次数
        /// </summary>
        public int PlayerMaxBuyableCount
        {
            get
            {
                return this._parentObject.PlayerMaxBuyableCount;
            }
            set
            {
                this._parentObject.PlayerMaxBuyableCount = value;
                NotifyPropertyChange("PlayerMaxBuyableCount");
            }
        }

        /// <summary>
        /// 价值积分
        /// </summary>
        public decimal ValueShoppingCredits
        {
            get
            {
                return this._parentObject.ValueShoppingCredits;
            }
            set
            {
                this._parentObject.ValueShoppingCredits = value;
                NotifyPropertyChange("ValueShoppingCredits");
            }
        }

        public decimal GainExp
        {
            get
            {
                return this._parentObject.GainExp;
            }
            set
            {
                this._parentObject.GainExp = value;
                NotifyPropertyChange("GainExp");
            }
        }

        public decimal GainRMB
        {
            get
            {
                return this._parentObject.GainRMB;
            }
            set
            {
                this._parentObject.GainRMB = value;
                NotifyPropertyChange("GainRMB");
            }
        }

        public decimal GainGoldCoin
        {
            get
            {
                return this._parentObject.GainGoldCoin;
            }
            set
            {
                this._parentObject.GainGoldCoin = value;
                NotifyPropertyChange("GainGoldCoin");
            }
        }

        public decimal GainMine_StoneReserves
        {
            get
            {
                return this._parentObject.GainMine_StoneReserves;
            }
            set
            {
                this._parentObject.GainMine_StoneReserves = value;
                NotifyPropertyChange("GainMine_StoneReserves");
            }
        }

        public decimal GainMiner
        {
            get
            {
                return this._parentObject.GainMiner;
            }
            set
            {
                this._parentObject.GainMiner = value;
                NotifyPropertyChange("GainMiner");
            }
        }

        public decimal GainStone
        {
            get
            {
                return this._parentObject.GainStone;
            }
            set
            {
                this._parentObject.GainStone = value;
                NotifyPropertyChange("GainStone");
            }
        }

        public decimal GainDiamond
        {
            get
            {
                return this._parentObject.GainDiamond;
            }
            set
            {
                this._parentObject.GainDiamond = value;
                NotifyPropertyChange("GainDiamond");
            }
        }

        public decimal GainShoppingCredits
        {
            get
            {
                return this._parentObject.GainShoppingCredits;
            }
            set
            {
                this._parentObject.GainShoppingCredits = value;
                NotifyPropertyChange("GainShoppingCredits");
            }
        }

        public decimal GainGravel
        {
            get
            {
                return this._parentObject.GainGravel;
            }
            set
            {
                this._parentObject.GainGravel = value;
                NotifyPropertyChange("GainGravel");
            }
        }

        public byte[] IconBuffer
        {
            get
            {
                return this._parentObject.IconBuffer;
            }
            set
            {
                this._parentObject.IconBuffer = value;
            }
        }

        public void SetIcon(byte[] buffer)
        {
            this._parentObject.IconBuffer = buffer;
            this._icon = MyImageConverter.GetIconSource(buffer);
        }

        private BitmapSource _icon = null;

        public BitmapSource Icon
        {
            get
            {
                return _icon;
            }
        }


    }
}
