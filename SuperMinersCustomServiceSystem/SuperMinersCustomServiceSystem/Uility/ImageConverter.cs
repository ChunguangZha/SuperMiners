using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SuperMinersCustomServiceSystem.Uility
{
    public class MyImageConverter
    {
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

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

    }
}
