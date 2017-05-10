using MetaData.Shopping;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperMinersWPF.Models
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
        }

        public DiamondsShoppingItemType Type
        {
            get { return this._parentObject.Type; }
        }

        public string ItemTypeText
        {
            get
            {
                string text = "";
                switch (this.Type)
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
        }

        public SellState SellState
        {
            get { return this._parentObject.SellState; }
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
        }

        public byte[] IconBuffer
        {
            get { return this._parentObject.IconBuffer; }
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
