using MetaData.Game.Roulette;
using System;
#if Client
using SuperMinersWPF.Models;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MetaData;

namespace SuperMinersCustomServiceSystem.Model
{
    public class RouletteWinnerRecordUIModel : BaseModel
    {
        public RouletteWinnerRecordUIModel(RouletteWinnerRecord parent)
        {
            ParentObject = parent;
        }

        private RouletteWinnerRecord _parentObject;

        public RouletteWinnerRecord ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public int RecordID
        {
            get { return this.ParentObject.RecordID; }
        }

        public int UserID
        {
            get { return this.ParentObject.UserID; }
        }

        public string UserName
        {
            get { return this.ParentObject.UserName; }
        }

        public string UserNickName
        {
            get { return this.ParentObject.UserNickName; }
        }

        public int RouletteAwardItemID
        {
            get { return this.ParentObject.RouletteAwardItemID; }
        }

        public RouletteAwardItem AwardItem
        {
            get { return this.ParentObject.AwardItem; }
        }

        public string AwardItemName
        {
            get { return this.AwardItem.AwardName; }
        }

        public DateTime WinTime
        {
            get { return this.ParentObject.WinTime.ToDateTime(); }
        }

        /// <summary>
        /// 是否已领取
        /// </summary>
        public bool IsGot
        {
            get { return this.ParentObject.IsGot; }
        }

        /// <summary>
        /// 允许为null
        /// </summary>
        public DateTime? GotTime
        {
            get
            {
                if (this.ParentObject.GotTime == null)
                {
                    return null;
                }
                return this.ParentObject.GotTime.ToDateTime();
            }
        }

        /// <summary>
        /// 是否已支付
        /// </summary>
        public bool IsPay
        {
            get { return this.ParentObject.IsPay; }
        }

        public Visibility PayButtonVisibility
        {
            get { return IsPay ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility GetButtonVisibility
        {
            get
            {
                if (this.AwardItem != null && this.AwardItem.RouletteAwardType == RouletteAwardType.RealAward && !this.IsGot)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 允许为null
        /// </summary>
        public DateTime? PayTime
        {
            get
            {
                if (this.ParentObject.PayTime == null)
                {
                    return null;
                }
                return this.ParentObject.PayTime.ToDateTime();
            }
        }

        public string GotInfo1
        {
            get { return this.ParentObject.GotInfo1; }
        }

        public string GotInfo2
        {
            get { return this.ParentObject.GotInfo2; }
        }

        public void SetRecordPay()
        {
            this.ParentObject.IsPay = true;
            this.ParentObject.PayTime = MyDateTime.FromDateTime(DateTime.Now);
            NotifyPropertyChange("IsPay");
            NotifyPropertyChange("PayTime");

        }

        public override bool Equals(object obj)
        {
            RouletteWinnerRecordUIModel other = obj as RouletteWinnerRecordUIModel;
            if (other == null)
            {
                return false;
            }

            return this.RecordID == other.RecordID;
        }
    }
}
