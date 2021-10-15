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
using System.Linq;
using System.Runtime.CompilerServices;

namespace MiYALAB.CSharp.Image
{
    /// <summary>
    /// ラベルデータ構造
    /// </summary>
    public struct Label
    {
        /// <summary>
        /// ラベルデータ構造
        /// </summary>
        /// <param name="_posUL">左上座標</param>
        /// <param name="_posDR">右下座標</param>
        /// <param name="_size">画像サイズ</param>
        /// <param name="_area">面積</param>
        /// <param name="_centroid">重心座標</param>
        /// <param name="_rgbValues">bitmapのbyte配列</param>
        /// <param name="_bmp">bitmap</param>
        public Label(Point _posUL, Point _posDR, Size _size, int _area, Point _centroid, byte[] _rgbValues, Bitmap _bmp)
        {
            Pos = _posUL;
            PosDR = _posDR;
            Size = _size;
            Area = _area;
            Centroid = _centroid;
            rgbValues = _rgbValues;
            bmp = _bmp;
        }

        /// <summary>
        /// 左上座標
        /// </summary>
        public Point Pos;
        /// <summary>
        /// 右下座標
        /// </summary>
        public Point PosDR;
        /// <summary>
        /// サイズ
        /// </summary>
        public Size Size;
        /// <summary>
        /// 面積
        /// </summary>
        public int Area;
        /// <summary>
        /// 重心座標
        /// </summary>
        public Point Centroid;
        /// <summary>
        /// bitmapデータのbyte配列
        /// </summary>
        public byte[] rgbValues;
        /// <summary>
        /// bitmapデータ
        /// </summary>
        public Bitmap bmp;
    }
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] BinaryNotConverter(byte[] rgbValues, RGB thresholdMax, RGB thresholdMin)
        {
            return BinaryNotConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B, thresholdMin.R, thresholdMin.G, thresholdMin.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] BinaryNotConverter(byte[] rgbValues, RGB thresholdMax)
        {
            return BinaryNotConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="ThresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] BinaryNotConverter(byte[] rgbValues, int ThresholdMax)
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
        /// <returns>二値化画像(白：閾値内, 黒：閾値外)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryNotConverter(Bitmap bmp, int rThreshold, int gThreshold, int bThreshold)
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
        /// <returns>二値化画像のbyte配列(白：閾値内, 黒：閾値外)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryNotConverter(Bitmap bmp, int rThresholdMax, int gThresholdMax, int bThresholdMax, int rThresholdMin, int gThresholdMin, int bThresholdMin)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp), rThresholdMax, gThresholdMax, bThresholdMax, rThresholdMin, gThresholdMin, bThresholdMin),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryNotConverter(Bitmap bmp, RGB thresholdMax, RGB thresholdMin)
        {
            return ByteArrayToBitmap(BinaryNotConverter(
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryNotConverter(Bitmap bmp, RGB thresholdMax)
        {
            return ByteArrayToBitmap(BinaryNotConverter(
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryNotConverter(Bitmap bmp, int ThresholdMax)
        {
            return ByteArrayToBitmap(
                BinaryNotConverter(BitmapToByteArray(bmp), ThresholdMax, ThresholdMax, ThresholdMax),
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

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <param name="thresholdMin">最小閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] BinaryConverter(byte[] rgbValues, RGB thresholdMax, RGB thresholdMin)
        {
            return BinaryConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B, thresholdMin.R, thresholdMin.G, thresholdMin.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="thresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] BinaryConverter(byte[] rgbValues, RGB thresholdMax)
        {
            return BinaryConverter(rgbValues, thresholdMax.R, thresholdMax.G, thresholdMax.B);
        }

        /// <summary>
        /// byte配列のbitmapデータを二値化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="ThresholdMax">最大閾値</param>
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] BinaryConverter(byte[] rgbValues, int ThresholdMax)
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
        /// <returns>二値化画像(白：閾値外, 黒：閾値内)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryConverter(Bitmap bmp, int rThreshold, int gThreshold, int bThreshold)
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
        /// <returns>二値化画像のbyte配列(白：閾値外, 黒：閾値内)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryConverter(Bitmap bmp, int rThresholdMax, int gThresholdMax, int bThresholdMax, int rThresholdMin, int gThresholdMin, int bThresholdMin)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryConverter(Bitmap bmp, RGB thresholdMax, RGB thresholdMin)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryConverter(Bitmap bmp, RGB thresholdMax)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap BinaryConverter(Bitmap bmp, int ThresholdMax)
        {
            return ByteArrayToBitmap(
                BinaryConverter(BitmapToByteArray(bmp), ThresholdMax, ThresholdMax, ThresholdMax),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point BlackCentroid(byte[] rgbValues, Size size)
        {
            return BlackCentroid(rgbValues, size.Width, size.Width);
        }

        /// <summary>
        /// 黒色重心座標を求めます．
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>黒色重心座標</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point WhiteCentroid(byte[] rgbValues, Size size)
        {
            return WhiteCentroid(rgbValues, size.Width, size.Height);
        }

        /// <summary>
        /// 白色重心座標を求めます．
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>白色重心座標</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            int retWork;
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
                            retWork = 4 * (width * y + x - 1);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 0;
                        }
                        if (x < width - 1)
                        {
                            retWork = 4 * (width * y + x + 1);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 0;
                        }
                        if (y >= 1)
                        {
                            retWork = 4 * (width * (y - 1) + x);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 0;

                        }
                        if (y < height - 1)
                        {
                            retWork = 4 * (width * (y + 1) + x);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 0;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            int lxWork, lyWork;
            int x, y, lx, ly;
            int bWidth;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 0)
                    {
                        lyWork = y + border;
                        for (ly = y - border; ly <= lyWork; ly++)
                        {
                            if (ly < 0 || height - 1 < ly) continue;

                            bWidth = border - Math.Abs(y - ly);
                            lxWork = x + bWidth;
                            
                            for (lx = x - bWidth; lx <= lxWork; lx++)
                            {
                                if (lx < 0 || width - 1 < lx) continue;

                                retWork = 4 * (width * ly + lx);
                                ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 0;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Erode(byte[] rgbValues, Size size, int border)
        {
            return Erode(rgbValues, size.Width, size.Height, border);
        }

        /// <summary>
        /// 二値化Bitmapデータの白色収縮処理を行います．
        /// </summary>
        /// <param name="bmp">二値化Bitmapデータ</param>
        /// <returns>白色収縮画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            int retWork;
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
                            retWork = 4 * (width * y + x - 1);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 255;
                        }
                        if (x < width - 1)
                        {
                            retWork = 4 * (width * y + x + 1);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 255;
                        }
                        if (y >= 1)
                        {
                            retWork = 4 * (width * (y - 1) + x);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 255;

                        }
                        if (y < height - 1)
                        {
                            retWork = 4 * (width * (y + 1) + x);
                            ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 255;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            int lxWork, lyWork;
            int x, y, lx, ly;
            int bWidth;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 255)
                    {
                        lyWork = y + border;
                        for (ly = y - border; ly <= lyWork; ly++)
                        {
                            if (ly < 0 || height - 1 < ly) continue;

                            bWidth = border - Math.Abs(y - ly);
                            lxWork = x + bWidth;

                            for (lx = x - bWidth; lx <= lxWork; lx++)
                            {
                                if (lx < 0 || width - 1 < lx) continue;

                                retWork = 4 * (width * ly + lx);
                                ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = 255;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Dilate(byte[] rgbValues, Size size, int border)
        {
            return Dilate(rgbValues, size.Width, size.Height, border);
        }

        /// <summary>
        /// 二値化Bitmapデータの白色膨張処理を行います．
        /// </summary>
        /// <param name="bmp">二値化Bitmapデータ</param>
        /// <returns>白色膨張画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap Dilate(Bitmap bmp, int border)
        {
            return ByteArrayToBitmap(
                Dilate(BitmapToByteArray(bmp), bmp.Width, bmp.Height, border),
                bmp.Width,
                bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // ラベリング関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// ラベリング処理(ラスタスキャン方式)
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <returns>ラベルデータ配列</returns>
        public static Label[] Labeling(byte[] rgbValues, int width, int height)
        {
            int x, y;
            int num = 1;
            int[] labelNum = new int[rgbValues.Length / 4];
            List<int> depth = new List<int>() { 0 };
            byte[] _rgbValues = new byte[rgbValues.Length];
            Array.Copy(rgbValues, _rgbValues, rgbValues.Length);

            // 単純ラベル割り当て
            int check;
            int[] work = new int[9] { 65536, 65536, 65536, 65536, 65536, 65536, 65536, 65536, 65536 };
            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (rgbValues[4 * (width * y + x)] == 255)
                    {
                        work[0] = work[1] = work[2] = work[3] = 65536;

                        // 注目画素左上
                        if (0 < x && 0 < y && labelNum[width * (y - 1) + (x - 1)] != 0)
                            work[0] = labelNum[width * (y - 1) + (x - 1)];

                        // 注目画素右上
                        if (x < width - 1 && 0 < y && labelNum[width * (y - 1) + (x + 1)] != 0)
                            work[1] = labelNum[width * (y - 1) + (x + 1)];

                        // 注目画素上
                        if (0 < y && labelNum[width * (y - 1) + (x)] != 0)
                            work[2] = labelNum[width * (y - 1) + (x)];

                        // 注目画素左
                        if (0 < x && labelNum[width * (y) + (x - 1)] != 0)
                            work[3] = labelNum[width * (y) + (x - 1)];

                        check = work.Min();

                        // 新しいラベル番号割り当て
                        if (check == 65536)
                        {
                            depth.Add(num);
                            labelNum[width * y + x] = num;

                            num++;
                        }

                        // 最小ラベル番号割り当て 最接近ラベルデータ更新
                        else
                        {
                            labelNum[width * y + x] = check;

                            if (work[0] != 65536)
                            {
                                depth[labelNum[width * (y - 1) + (x - 1)]] = Math.Min(check, depth[labelNum[width * (y - 1) + (x - 1)]]);
                            }
                            if (work[1] != 65536)
                            {
                                depth[labelNum[width * (y - 1) + (x + 1)]] = Math.Min(check, depth[labelNum[width * (y - 1) + (x + 1)]]);
                            }
                            if (work[2] != 65536)
                            {
                                depth[labelNum[width * (y - 1) + (x)]] = Math.Min(check, depth[labelNum[width * (y - 1) + (x)]]);
                            }
                            if (work[3] != 65536)
                            {
                                depth[labelNum[width * (y) + (x - 1)]] = Math.Min(check, depth[labelNum[width * (y) + (x - 1)]]);
                            }
                        }

                        // 黒色に変換
                        _rgbValues[4 * (width * y + x)] = 0;
                        _rgbValues[4 * (width * y + x) + 1] = 0;
                        _rgbValues[4 * (width * y + x) + 2] = 0;
                    }
                }
            }

            // 最接近ラベル修正
            for (x = depth.Count - 1; 0 < x; x--)
            {
                check = x;

                while (check != depth[check])
                {
                    check = depth[check];
                    depth[x] = check;
                }
            }

            // 最接近ラベル再探索(2列間隔捜査)
            for (y = 0; y < height; y += 2)
            {
                for (x = 0; x < width; x++)
                {
                    if (labelNum[width * y + x] != 0)
                    {
                        work[0] = work[1] = work[2] = work[3] = work[5] = work[6] = work[7] = work[8] = 65536;
                        work[4] = depth[labelNum[width * y + x]];

                        // 注目画素左上
                        if (0 < x && 0 < y && labelNum[width * (y - 1) + (x - 1)] != 0)
                            work[0] = depth[labelNum[width * (y - 1) + (x - 1)]];

                        // 注目画素右上
                        if (x < width - 1 && 0 < y && labelNum[width * (y - 1) + (x + 1)] != 0)
                            work[1] = depth[labelNum[width * (y - 1) + (x + 1)]];

                        // 注目画素上
                        if (0 < y && labelNum[width * (y - 1) + (x)] != 0)
                            work[2] = depth[labelNum[width * (y - 1) + (x)]];

                        // 注目画素左
                        if (0 < x && labelNum[width * (y) + (x - 1)] != 0)
                            work[3] = depth[labelNum[width * (y) + (x - 1)]];

                        // 注目画素左下
                        if (0 < x && y < height - 1 && labelNum[width * (y + 1) + (x - 1)] != 0)
                            work[5] = depth[labelNum[width * (y + 1) + (x - 1)]];

                        // 注目画素右下
                        if (x < width - 1 && y < height - 1 && labelNum[width * (y + 1) + (x + 1)] != 0)
                            work[6] = depth[labelNum[width * (y + 1) + (x + 1)]];

                        // 注目画素下
                        if (y < height - 1 && labelNum[width * (y + 1) + (x)] != 0)
                            work[7] = depth[labelNum[width * (y + 1) + (x)]];

                        // 注目画素右
                        if (x < width - 1 && labelNum[width * (y) + (x + 1)] != 0)
                            work[8] = depth[labelNum[width * (y) + (x + 1)]];

                        check = work.Min();
                        depth[labelNum[width * y + x]] = check;

                        if (work[0] != 65536)
                        {
                            depth[labelNum[width * (y - 1) + (x - 1)]] = Math.Min(check, depth[labelNum[width * (y - 1) + (x - 1)]]);
                        }
                        if (work[1] != 65536)
                        {
                            depth[labelNum[width * (y - 1) + (x + 1)]] = Math.Min(check, depth[labelNum[width * (y - 1) + (x + 1)]]);
                        }
                        if (work[2] != 65536)
                        {
                            depth[labelNum[width * (y - 1) + (x)]] = Math.Min(check, depth[labelNum[width * (y - 1) + (x)]]);
                        }
                        if (work[3] != 65536)
                        {
                            depth[labelNum[width * (y) + (x - 1)]] = Math.Min(check, depth[labelNum[width * (y) + (x - 1)]]);
                        }
                        if (work[5] != 65536)
                        {
                            depth[labelNum[width * (y + 1) + (x - 1)]] = Math.Min(check, depth[labelNum[width * (y + 1) + (x - 1)]]);
                        }
                        if (work[6] != 65536)
                        {
                            depth[labelNum[width * (y + 1) + (x + 1)]] = Math.Min(check, depth[labelNum[width * (y + 1) + (x + 1)]]);
                        }
                        if (work[7] != 65536)
                        {
                            depth[labelNum[width * (y + 1) + (x)]] = Math.Min(check, depth[labelNum[width * (y + 1) + (x)]]);
                        }
                        if (work[8] != 65536)
                        {
                            depth[labelNum[width * (y) + (x + 1)]] = Math.Min(check, depth[labelNum[width * (y) + (x + 1)]]);
                        }
                    }
                }
            }

            // 最接近ラベル修正
            for (x = depth.Count - 1; 0 < x; x--)
            {
                check = x;

                while (check != depth[check])
                {
                    check = depth[check];
                    depth[x] = check;
                }
            }

            // ラベルデータ数算出
            IDictionary<int, int> listNum = new Dictionary<int, int>();
            listNum.Add(0, 0);
            for (x = 1; x < depth.Count; x++)
            {
                if (x == depth[x])
                {
                    listNum.Add(x, listNum.Count);
                }
            }

            // ラベル初期化
            Label init = new Label(
                new Point(width, height),
                new Point(0, 0),
                new Size(0, 0),
                0,
                new Point(0, 0),
                null,
                null);
            Label[] labels = Enumerable.Repeat<Label>(init, listNum.Count).ToArray();

            // ラベルデータ配列のrgbValues初期化
            for (x = 0; x < listNum.Count; x++)
            {
                labels[x].rgbValues = new byte[_rgbValues.Length];
                Array.Copy(_rgbValues, labels[x].rgbValues, _rgbValues.Length);
            }

            // ラベル番号再割り当て
            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if (labelNum[width * y + x] != 0)
                    {
                        work[1] = width * y + x;
                        work[0] = labelNum[work[1]] = depth[labelNum[work[1]]];
                        work[0] = listNum[work[0]];
                        work[1] *= 4;
                        labels[work[0]].rgbValues[work[1]] = 
                            labels[work[0]].rgbValues[work[1] + 1] = 
                            labels[work[0]].rgbValues[work[1] + 2] = 255;
                        labels[work[0]].Area += 1;
                        labels[work[0]].Centroid.X += x;
                        labels[work[0]].Centroid.Y += y;
                        if (labels[work[0]].Pos.X > x) labels[work[0]].Pos.X = x;
                        if (labels[work[0]].Pos.Y > y) labels[work[0]].Pos.Y = y;
                        if (labels[work[0]].PosDR.X < x) labels[work[0]].PosDR.X = x;
                        if (labels[work[0]].PosDR.Y < y) labels[work[0]].PosDR.Y = y;
                    }
                }
            }

            // ラベルのパラメータ計算
            for (x = 1; x < listNum.Count; x++)
            {
                labels[x].Centroid.X /= labels[x].Area;
                labels[x].Centroid.Y /= labels[x].Area;
                labels[x].Size.Width = labels[x].PosDR.X - labels[x].Pos.X + 1;
                labels[x].Size.Height = labels[x].PosDR.Y - labels[x].Pos.Y + 1;
            }

            return labels;
        }

        /// <summary>
        /// ラベリング処理(ラスタスキャン方式)
        /// </summary>
        /// <param name="rgbValues">byte配列に変換された二値化bitmap</param>
        /// <param name="size">bitmapデータのサイズ</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Label[] Labeling(byte[] rgbValues, Size size)
        {
            return Labeling(rgbValues, size.Width, size.Height);
        }

        /// <summary>
        /// ラベリング処理(ラスタスキャン方式)
        /// </summary>
        /// <param name="bmp">二値化bitmap</param>
        /// <returns>ラベルデータ配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Label[] Labeling(Bitmap bmp)
        {
            Label[] labels = Labeling(BitmapToByteArray(bmp), bmp.Width, bmp.Height);

            for(int i = 0; i < labels.Length; i++)
            {
                labels[i].bmp = ByteArrayToBitmap(labels[i].rgbValues, bmp.Width, bmp.Height);
            }

            return labels;
        }
    }
}
