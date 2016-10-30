using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            set
            {
                _parentObject = value;

                this._icon = GetIconSource(this._parentObject.IconBuffer);
            }
        }

        public static BitmapSource GetIconSource(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }

            IntPtr ptr = IntPtr.Zero;
            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(new MemoryStream(buffer));
                ptr = bmp.GetHbitmap();

                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                      ptr, IntPtr.Zero, Int32Rect.Empty,
                      BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    DeleteObject(ptr);
                }
            }
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

        private BitmapSource _icon = null;

        public BitmapSource Icon
        {
            get
            {
                return _icon;
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

        ///// <summary>
        ///// 是否为实物奖品，除了系统内部的都称为实物
        ///// </summary>
        //public bool IsRealAward
        //{
        //    get { return this.ParentObject.IsRealAward; }
        //    set
        //    {
        //        this.ParentObject.IsRealAward = value;
        //    }
        //}

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

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

    }
}
