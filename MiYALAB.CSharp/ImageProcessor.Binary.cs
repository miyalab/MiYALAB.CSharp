/*
 * MIT License
 * 
 * Copyright (c) 2020 MiYA LAB(K.Miyauchi)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace MiYALAB.CSharp.Image
{
    /// <summary>
    /// 画像処理クラス
    /// </summary>
    public partial class ImageProcessor
    {
        //--------------------------------------------------------------------------------
        // 二値化処理関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="rThreshold">最大赤色閾値</param>
        /// <param name="gThreshold">最大緑色閾値</param>
        /// <param name="bThreshold">最大青色閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        public static byte[] BinaryNotConverter(byte[] rgbValues, int rThreshold, int gThreshold, int bThreshold)
        {
            byte[] ret = new byte[rgbValues.Length];

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (rgbValues[i] <= bThreshold && rgbValues[i + 1] <= gThreshold && rgbValues[i + 2] <= rThreshold)
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 255;
                }
                else
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 0;
                }
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="rThresholdMax">最大赤色閾値</param>
        /// <param name="gThresholdMax">最大緑色閾値</param>
        /// <param name="bThresholdMax">最大青色閾値</param>
        /// <param name="rThresholdMin">最小赤色閾値</param>
        /// <param name="gThresholdMin">最小緑色閾値</param>
        /// <param name="bThresholdMin">最小青色閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        public static byte[] BinaryNotConverter(byte[] rgbValues, int rThresholdMax, int gThresholdMax, int bThresholdMax, int rThresholdMin, int gThresholdMin, int bThresholdMin)
        {
            byte[] ret = new byte[rgbValues.Length];

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (bThresholdMin <= rgbValues[i] && rgbValues[i] <= bThresholdMax &&
                    gThresholdMin <= rgbValues[i + 1] && rgbValues[i + 1] <= gThresholdMax &&
                    rThresholdMin <= rgbValues[i + 2] && rgbValues[i + 2] <= rThresholdMax)
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 255;
                }
                else
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 0;
                }
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <param name="thresholdMin">最小閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        public static byte[] BinaryNotConverter(byte[] rgbValues, RGB thresholdMax, RGB thresholdMin)
        {
            return BinaryConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B, thresholdMin.R, thresholdMin.G, thresholdMin.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        public static byte[] BinaryNotConverter(byte[] rgbValues, RGB thresholdMax)
        {
            return BinaryConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="ThresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        public static byte[] BinaryNotConverter(byte[] rgbValues, int ThresholdMax)
        {
            return BinaryConverter(rgbValues, ThresholdMax, ThresholdMax, ThresholdMax);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="rThreshold">赤色閾値</param>
        /// <param name="gThreshold">緑色閾値</param>
        /// <param name="bThreshold">青色閾値</param>
        /// <returns>二値化画像(白：閾値内, 黒：閾値外)</returns>
        public static Bitmap BinaryNotConverter(Bitmap bmp, int rThreshold, int gThreshold, int bThreshold)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp), rThreshold, gThreshold, bThreshold),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="rThresholdMax">最大赤色閾値</param>
        /// <param name="gThresholdMax">最大緑色閾値</param>
        /// <param name="bThresholdMax">最大青色閾値</param>
        /// <param name="rThresholdMin">最小赤色閾値</param>
        /// <param name="gThresholdMin">最小緑色閾値</param>
        /// <param name="bThresholdMin">最小青色閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        public static Bitmap BinaryNotConverter(Bitmap bmp, int rThresholdMax, int gThresholdMax, int bThresholdMax, int rThresholdMin, int gThresholdMin, int bThresholdMin)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp), rThresholdMax, gThresholdMax, bThresholdMax, rThresholdMin, gThresholdMin, bThresholdMin),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <param name="thresholdMin">最小閾値</param>
        /// <returns>二値化画像(白：閾値内, 黒：閾値外)</returns>
        public static Bitmap BinaryNotConverter(Bitmap bmp, RGB thresholdMax, RGB thresholdMin)
        {
            return ByteArrayToBitmap(BinaryConverter(
                BitmapToByteArray(bmp),
                thresholdMax.R, thresholdMax.G, thresholdMax.B,
                thresholdMin.R, thresholdMin.G, thresholdMin.B),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <returns>二値化画像(白：閾値内, 黒：閾値外)</returns>
        public static Bitmap BinaryNotConverter(Bitmap bmp, RGB thresholdMax)
        {
            return ByteArrayToBitmap(BinaryConverter(
                BitmapToByteArray(bmp),
                thresholdMax.R, thresholdMax.G, thresholdMax.B),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="ThresholdMax">最大閾値</param>
        /// <returns>二値化画像(白：閾値内, 黒：閾値外)</returns>
        public static Bitmap BinaryNotConverter(Bitmap bmp, int ThresholdMax)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp), ThresholdMax, ThresholdMax, ThresholdMax),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="rThreshold">最大赤色閾値</param>
        /// <param name="gThreshold">最大緑色閾値</param>
        /// <param name="bThreshold">最大青色閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        public static byte[] BinaryConverter(byte[] rgbValues, int rThreshold, int gThreshold, int bThreshold)
        {
            byte[] ret = new byte[rgbValues.Length];
            
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (rgbValues[i] <= bThreshold && rgbValues[i + 1] <= gThreshold && rgbValues[i + 2] <= rThreshold)
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 0;
                }
                else
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 255;
                }
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="rThresholdMax">最大赤色閾値</param>
        /// <param name="gThresholdMax">最大緑色閾値</param>
        /// <param name="bThresholdMax">最大青色閾値</param>
        /// <param name="rThresholdMin">最小赤色閾値</param>
        /// <param name="gThresholdMin">最小緑色閾値</param>
        /// <param name="bThresholdMin">最小青色閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        public static byte[] BinaryConverter(byte[] rgbValues, int rThresholdMax, int gThresholdMax, int bThresholdMax, int rThresholdMin, int gThresholdMin, int bThresholdMin)
        {
            byte[] ret = new byte[rgbValues.Length];

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (bThresholdMin <= rgbValues[i] && rgbValues[i] <= bThresholdMax &&
                    gThresholdMin <= rgbValues[i + 1] && rgbValues[i + 1] <= gThresholdMax &&
                    rThresholdMin <= rgbValues[i + 2] && rgbValues[i + 2] <= rThresholdMax)
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 0;
                }
                else
                {
                    ret[i] = ret[i + 1] = ret[i + 2] = 255;
                }
                ret[i + 3] = 255;
            }

            return rgbValues;
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <param name="thresholdMin">最小閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        public static byte[] BinaryConverter(byte[] rgbValues, RGB thresholdMax, RGB thresholdMin)
        {
            return BinaryNotConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B, thresholdMin.R, thresholdMin.G, thresholdMin.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        public static byte[] BinaryConverter(byte[] rgbValues, RGB thresholdMax)
        {
            return BinaryNotConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="ThresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        public static byte[] BinaryConverter(byte[] rgbValues, int ThresholdMax)
        {
            return BinaryNotConverter(rgbValues, ThresholdMax, ThresholdMax, ThresholdMax);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="rThreshold">赤色閾値</param>
        /// <param name="gThreshold">緑色閾値</param>
        /// <param name="bThreshold">青色閾値</param>
        /// <returns>二値化画像(白：閾値外, 黒：閾値内)</returns>
        public static Bitmap BinaryConverter(Bitmap bmp, int rThreshold, int gThreshold, int bThreshold)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp), rThreshold, gThreshold, bThreshold),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="rThresholdMax">最大赤色閾値</param>
        /// <param name="gThresholdMax">最大緑色閾値</param>
        /// <param name="bThresholdMax">最大青色閾値</param>
        /// <param name="rThresholdMin">最小赤色閾値</param>
        /// <param name="gThresholdMin">最小緑色閾値</param>
        /// <param name="bThresholdMin">最小青色閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        public static Bitmap BinaryConverter(Bitmap bmp, int rThresholdMax, int gThresholdMax, int bThresholdMax, int rThresholdMin, int gThresholdMin, int bThresholdMin)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp),
                rThresholdMax, gThresholdMax, bThresholdMax,
                rThresholdMin, gThresholdMin, bThresholdMin),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <param name="thresholdMin">最小閾値</param>
        /// <returns>二値化画像(白：閾値外, 黒：閾値内)</returns>
        public static Bitmap BinaryConverter(Bitmap bmp, RGB thresholdMax, RGB thresholdMin)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp),
                thresholdMax.R, thresholdMax.G, thresholdMax.B,
                thresholdMin.R, thresholdMin.G, thresholdMin.B),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <returns>二値化画像(白：閾値外, 黒：閾値内)</returns>
        public static Bitmap BinaryConverter(Bitmap bmp, RGB thresholdMax)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp),
                thresholdMax.R, thresholdMax.G, thresholdMax.B),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータを二値化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="ThresholdMax">最大閾値</param>
        /// <returns>二値化画像(白：閾値外, 黒：閾値内)</returns>
        public static Bitmap BinaryConverter(Bitmap bmp, int ThresholdMax)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp), ThresholdMax, ThresholdMax, ThresholdMax),
                bmp.Width,
                bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // 二値化面積計算関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列の二値化Bitmapデータの白色面積を求めます．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <returns>白色面積値</returns>
        public static int WhiteArea(byte[] rgbValues)
        {
            int Area = 0;
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (rgbValues[i] != 0) Area++;
            }

            return Area;
        }

        /// <summary>
        /// 二値化Bitmapデータの白色面積を求めます．
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>白色面積値</returns>
        public static int WhiteArea(Bitmap bmp)
        {
            return WhiteArea(BitmapToByteArray(bmp));
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの黒色面積を求めます．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <returns>黒色面積値</returns>
        public static int BlackArea(byte[] rgbValues)
        {
            int Area = 0;
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (rgbValues[i] == 0) Area++;
            }
            return Area;
        }

        /// <summary>
        /// 二値化Bitmapデータの黒色面積を求めます．
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>黒色面積値</returns>
        public static int BlackArea(Bitmap bmp)
        {
            return BlackArea(BitmapToByteArray(bmp));
        }

        //--------------------------------------------------------------------------------
        // 二値化重心計算関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// 黒色重心座標を求めます．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <returns>黒色重心座標</returns>
        public static Point BlackCentroid(byte[] rgbValues, int width, int height)
        {
            int x, y;
            int Area = 0;
            int dx = 0;
            int dy = 0;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 0)
                    {
                        Area++;
                        dx += x;
                        dy += y;
                    }
                }
            }

            if (Area == 0) return new Point(0, 0);
            return new Point(dx / Area, dy / Area);
        }

        /// <summary>
        /// 黒色重心座標を求めます．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <returns>黒色重心座標</returns>
        public static Point BlackCentroid(byte[] rgbValues, Size size)
        {
            return BlackCentroid(rgbValues, size.Width, size.Width);
        }

        /// <summary>
        /// 黒色重心座標を求めます．
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>黒色重心座標</returns>
        public static Point BlackCentroid(Bitmap bmp)
        {
            return BlackCentroid(BitmapToByteArray(bmp), bmp.Width, bmp.Height);
        }

        /// <summary>
        /// 白色重心座標を求めます．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <returns>白色重心座標</returns>
        public static Point WhiteCentroid(byte[] rgbValues, int width, int height)
        {
            int x, y;
            int Area = 0;
            int dx = 0;
            int dy = 0;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 255)
                    {
                        Area++;
                        dx += x;
                        dy += y;
                    }
                }
            }
            if (Area == 0) return new Point(0, 0);
            return new Point(dx / Area, dy / Area);
        }

        /// <summary>
        /// 白色重心座標を求めます．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <returns>白色重心座標</returns>
        public static Point WhiteCentroid(byte[] rgbValues, Size size)
        {
            return WhiteCentroid(rgbValues, size.Width, size.Height);
        }

        /// <summary>
        /// 白色重心座標を求めます．
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>白色重心座標</returns>
        public static Point WhiteCentroid(Bitmap bmp)
        {
            return WhiteCentroid(BitmapToByteArray(bmp), bmp.Width, bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // 収縮関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列の二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <returns>白色収縮画像のbyte配列</returns>
        public static byte[] Erode(byte[] rgbValues, int width, int height)
        {
            int x, y;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 0)
                    {
                        if (x >= 1)
                        {
                            ret[4 * (width * y + x - 1)] =
                            ret[4 * (width * y + x - 1) + 1] =
                            ret[4 * (width * y + x - 1) + 2] = 0;
                        }
                        if (x < width - 1)
                        {
                            ret[4 * (width * y + x + 1)] =
                            ret[4 * (width * y + x + 1) + 1] =
                            ret[4 * (width * y + x + 1) + 2] = 0;
                        }
                        if (y >= 1)
                        {
                            ret[4 * (width * (y - 1) + x)] =
                            ret[4 * (width * (y - 1) + x) + 1] =
                            ret[4 * (width * (y - 1) + x) + 2] = 0;

                        }
                        if (y < height - 1)
                        {
                            ret[4 * (width * (y + 1) + x)] =
                            ret[4 * (width * (y + 1) + x) + 1] =
                            ret[4 * (width * (y + 1) + x) + 2] = 0;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <returns>白色収縮画像のbyte配列</returns>
        public static byte[] Erode(byte[] rgbValues, Size size)
        {
            return Erode(rgbValues, size.Width, size.Height);
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="border">膨張幅</param>
        /// <returns>白色収縮画像のbyte配列</returns>
        public static byte[] Erode(byte[] rgbValues, int width, int height, int border)
        {
            int x, y, lx, ly;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 0)
                    {
                        for (ly = y - border; ly <= y + border; ly++)
                        {
                            if (ly < 0 || height - 1 < ly) continue;
                            for (lx = x - border; lx <= x + border; lx++)
                            {
                                if (lx < 0 || width - 1 < lx) continue;
                                    ret[4 * (width * ly + lx)] = 
                                    ret[4 * (width * ly + lx) + 1] = 
                                    ret[4 * (width * ly + lx) + 2] = 0;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <param name="border">膨張幅</param>
        /// <returns>白色収縮画像のbyte配列</returns>
        public static byte[] Erode(byte[] rgbValues, Size size, int border)
        {
            return Erode(rgbValues, size.Width, size.Height, border);
        }

        /// <summary>
        /// 二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="bmp">二値化Bitmapデータ</param>
        /// <returns>白色収縮画像</returns>
        public static Bitmap Erode(Bitmap bmp)
        {
            return ByteArrayToBitmap(
                Erode(BitmapToByteArray(bmp), bmp.Width, bmp.Height),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// 二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="bmp">二値化Bitmapデータ</param>
        /// <param name="border">膨張幅</param>
        /// <returns>白色収縮画像</returns>
        public static Bitmap Erode(Bitmap bmp, int border)
        {
            return ByteArrayToBitmap(
                Erode(BitmapToByteArray(bmp), bmp.Width, bmp.Height, border),
                bmp.Width,
                bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // 膨張関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列の二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <returns>白色膨張画像のbyte配列</returns>
        public static byte[] Dilate(byte[] rgbValues, int width, int height)
        {
            int x, y;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 255)
                    {
                        if (x >= 1)
                        {
                            ret[4 * (width * y + x - 1)] =
                            ret[4 * (width * y + x - 1) + 1] =
                            ret[4 * (width * y + x - 1) + 2] = 255;
                        }
                        if (x < width - 1)
                        {
                            ret[4 * (width * y + x + 1)] =
                            ret[4 * (width * y + x + 1) + 1] =
                            ret[4 * (width * y + x + 1) + 2] = 255;
                        }
                        if (y >= 1)
                        {
                            ret[4 * (width * (y - 1) + x)] =
                            ret[4 * (width * (y - 1) + x) + 1] =
                            ret[4 * (width * (y - 1) + x) + 2] = 255;

                        }
                        if (y < height - 1)
                        {
                            ret[4 * (width * (y + 1) + x)] =
                            ret[4 * (width * (y + 1) + x) + 1] =
                            ret[4 * (width * (y + 1) + x) + 2] = 255;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <returns>白色膨張画像のbyte配列</returns>
        public static byte[] Dilate(byte[] rgbValues, Size size)
        {
            return Dilate(rgbValues, size.Width, size.Height);
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="border">膨張幅</param>
        /// <returns>白色膨張画像のbyte配列</returns>
        public static byte[] Dilate(byte[] rgbValues, int width, int height, int border)
        {
            int x, y, lx, ly;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 255)
                    {
                        for(ly = y - border; ly <= y + border; ly++)
                        {
                            if (ly < 0 || height - 1 < ly) continue;
                            for(lx = x - border; lx <= x + border; lx++)
                            {
                                if (lx < 0 || width - 1 < lx) continue;
                                ret[4 * (width * ly + lx)] = 
                                    ret[4 * (width * ly + lx)+1] = 
                                    ret[4 * (width * ly + lx)+2] = 255;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列の二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列の二値化Bitmapデータ</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <param name="border">膨張幅</param>
        /// <returns>白色膨張画像のbyte配列</returns>
        public static byte[] Dilate(byte[] rgbValues, Size size, int border)
        {
            return Dilate(rgbValues, size.Width, size.Height, border);
        }

        /// <summary>
        /// 二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="bmp">二値化Bitmapデータ</param>
        /// <returns>白色膨張画像</returns>
        public static Bitmap Dilate(Bitmap bmp)
        {
            return ByteArrayToBitmap(
                Dilate(BitmapToByteArray(bmp), bmp.Width, bmp.Height),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// 二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="bmp">二値化Bitmapデータ</param>
        /// <param name="border">膨張幅</param>
        /// <returns>白色膨張画像</returns>
        public static Bitmap Dilate(Bitmap bmp, int border)
        {
            return ByteArrayToBitmap(
                Dilate(BitmapToByteArray(bmp), bmp.Width, bmp.Height, border),
                bmp.Width,
                bmp.Height);
        }
    }
}
