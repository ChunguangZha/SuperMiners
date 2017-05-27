using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperMinersServerApplication.UIModel
{
    public class IncomeMoneyAccountUIModel : BaseUIModel
    {
        public IncomeMoneyAccountUIModel()
        {
            IsChanged = false;
        }

        public bool IsChanged { get; set; }

        private string _incomeMoneyAlipay = "";

        public string IncomeMoneyAlipay
        {
            get { return this._incomeMoneyAlipay; }
            set
            {
                if (value != this._incomeMoneyAlipay)
                {
                    this._incomeMoneyAlipay = value;
                    IsChanged = true;
                    NotifyPropertyChanged("IncomeMoneyAlipay");
                }
            }
        }

        private string _incomeMoneyAlipayRealName = "";

        public string IncomeMoneyAlipayRealName
        {
            get { return this._incomeMoneyAlipayRealName; }
            set
            {
                if (value != this._incomeMoneyAlipayRealName)
                {
                    this._incomeMoneyAlipayRealName = value;
                    IsChanged = true;
                    NotifyPropertyChanged("IncomeMoneyAlipayRealName");
                }
            }
        }

        /// <summary>
        /// 收款二维码图片序列化后
        /// </summary>
        public byte[] _alipay2DCode = null;

        public BitmapImage Alipay2DCodeImage
        {
            get
            {
                if (this._alipay2DCode == null)
                {
                    return null;
                }

                return ByteArrayToBitmapImage(this._alipay2DCode);
            }
            set
            {
                IsChanged = true;
                if (value == null)
                {
                    this._alipay2DCode = null;
                }
                else
                {
                    this._alipay2DCode = BitmapImageToByteArray(value);
                }
            }
        }


        private BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;

            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }

            return bmp;
        }

        private byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] byteArray = null;

            try
            {
                Stream sMarket = bmp.StreamSource;

                if (sMarket != null && sMarket.Length > 0)
                {
                    //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。   
                    sMarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(sMarket))
                    {
                        byteArray = br.ReadBytes((int)sMarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling   
            }

            return byteArray;
        }

        //public static IncomeMoneyAccountUIModel CreateFromDBObject(IncomeMoneyAccount parent)
        //{
        //    if (parent == null)
        //    {
        //        return new IncomeMoneyAccountUIModel();
        //    }
        //    IncomeMoneyAccountUIModel uiConfig = new IncomeMoneyAccountUIModel()
        //    {
        //        _alipay2DCode = parent.Alipay2DCode,
        //        IncomeMoneyAlipay = parent.IncomeMoneyAlipay,
        //        IncomeMoneyAlipayRealName = parent.IncomeMoneyAlipayRealName
        //    };

        //    return uiConfig;
        //}

        //public IncomeMoneyAccount ToDBObject()
        //{
        //    IncomeMoneyAccount dbConfig = new IncomeMoneyAccount()
        //    {
        //        Alipay2DCode = this._alipay2DCode,
        //        IncomeMoneyAlipay = this.IncomeMoneyAlipay,
        //        IncomeMoneyAlipayRealName = this.IncomeMoneyAlipayRealName
        //    };

        //    return dbConfig;
        //}
    }
}
