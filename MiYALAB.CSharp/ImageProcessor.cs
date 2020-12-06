using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiYALAB.CSharp.Image
{
    public class ImageProcessor
    {
        /// <summary>
        /// Bitmapをbyte[]に変換する．
        /// </summary>
        /// <param name="bmp">変換元の32bitARGB Bitmap</param>
        /// <returns>1 pixel = 4 byte (+3:A, +2:R, +1:G, +0:B) に変換したbyte配列</returns>
        public byte[] BitmapToByteArray(Bitmap bmp)
        {
            Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // Bitmapの先頭アドレスを取得
            IntPtr ptr = bmpData.Scan0;

            // 32bppArgbフォーマットで値を格納
            int bytes = bmp.Width * bmp.Height * 4;
            byte[] rgbValues = new byte[bytes];

            // Bitmapをbyte[]へコピー
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            bmp.UnlockBits(bmpData);
            return rgbValues;
        }

        /// <summary>
        /// byte配列をBitmapに変換する．
        /// </summary>
        /// <param name="rgbValues">1 pixel = 4 byte (+3:A, +2:R, +1:G, +0:B) に変換されたたbyte配列</param>
        /// <param name="width">変換後のbitmapの幅</param>
        /// <param name="height">変換後のbitmapの高さ</param>
        /// <returns>変換先のBitmap</returns>
        public Bitmap ByteArrayToBitmap(byte[] rgbValues, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);

            Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // Bitmapの先頭アドレスを取得
            IntPtr ptr = bmpData.Scan0;

            // Bitmapへコピー
            Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="Threshold">閾値</param>
        /// <returns></returns>
        public byte[] BinaryConverter(byte[] rgbValues, int Threshold)
        {
            for(int i = 0; i < rgbValues.Length; i += 4)
            {
                if(rgbValues[i]<Threshold && rgbValues[i+1] < Threshold && rgbValues[i+2] < Threshold)
                {
                    rgbValues[i] = 0;
                    rgbValues[i + 1] = 0;
                    rgbValues[i + 2] = 0;
                }
                else
                {
                    rgbValues[i] = 255;
                    rgbValues[i + 1] = 255;
                    rgbValues[i + 2] = 255;
                }
            }

            return rgbValues;
        }
    }
}
