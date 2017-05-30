using MetaData.Shopping;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SuperMinersCustomServiceSystem.Model
{
    public class DiamondShoppingItemUIModel : BaseModel
    {
        public DiamondShoppingItemUIModel(DiamondShoppingItem parent)
        {
            this.ParentObject = parent;
        }

        private DiamondShoppingItem _parentObject;

        public DiamondShoppingItem ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                this._icon = MyImageConverter.GetIconSource(this._parentObject.IconBuffer);

                NotifyPropertyChange("ID");
                NotifyPropertyChange("Name");
                NotifyPropertyChange("Type");
                NotifyPropertyChange("ItemTypeText");
                NotifyPropertyChange("Remark");
                NotifyPropertyChange("SellState");
                NotifyPropertyChange("SellStateText");
                NotifyPropertyChange("ValueDiamonds");
                NotifyPropertyChange("StocksCount");
                NotifyPropertyChange("DetailText");
                NotifyPropertyChange("DetailImageNames");
                NotifyPropertyChange("Icon");
            }
        }

        public int ID
        {
            get { return this._parentObject.ID; }
        }

        public string Name
        {
            get { return this._parentObject.Name; }
            set
            {
                this._parentObject.Name = value;
                NotifyPropertyChange("Name");
            }
        }

        public DiamondsShoppingItemType ItemType
        {
            get { return this._parentObject.ItemType; }
            set
            {
                this._parentObject.ItemType = value;
                NotifyPropertyChange("ItemType");
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
                    case DiamondsShoppingItemType.LiveThing:
                        text = "生活用品";
                        break;
                    case DiamondsShoppingItemType.Digital:
                        text = "数码产品";
                        break;
                    case DiamondsShoppingItemType.Food:
                        text = "食品专区";
                        break;
                    case DiamondsShoppingItemType.HomeAppliances:
                        text = "家用电器";
                        break;
                    case DiamondsShoppingItemType.PhoneFee:
                        text = "话费充值";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        /// <summary>
        /// MaxLength:2000
        /// </summary>
        public string Remark
        {
            get { return this._parentObject.Remark; }
            set
            {
                this._parentObject.Remark = value;
                NotifyPropertyChange("Remark");
            }
        }

        public SellState SellState
        {
            get { return this._parentObject.SellState; }
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

        public decimal ValueDiamonds
        {
            get { return this._parentObject.ValueDiamonds; }
            set
            {
                this._parentObject.ValueDiamonds = value;
                NotifyPropertyChange("ValueDiamonds");
                NotifyPropertyChange("ValueRMBYuan");
            }
        }

        public decimal ValueRMBYuan
        {
            get { return Math.Round(this._parentObject.ValueDiamonds * 0.375M, 2); }
        }

        public int StocksCount
        {
            get { return this._parentObject.Stocks; }
            set
            {
                this._parentObject.Stocks = value;
                NotifyPropertyChange("StocksCount");
            }
        }

        public byte[] IconBuffer
        {
            get { return this._parentObject.IconBuffer; }
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

        public string DetailText
        {
            get { return this._parentObject.DetailText; }
            set
            {
                this._parentObject.DetailText = value;
                NotifyPropertyChange("DetailText");
            }
        }

        public string[] DetailImageNames
        {
            get { return this._parentObject.DetailImageNames; }
            set
            {
                this._parentObject.DetailImageNames = value;
                NotifyPropertyChange("DetailImageNames");
            }
        }

    }
}
