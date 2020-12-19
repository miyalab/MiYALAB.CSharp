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
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace MiYALAB.CSharp.Image
{
    //--------------------------------------------------------------------------------
    // 画像処理用構造体
    //--------------------------------------------------------------------------------
    /// <summary>
    /// 輝度データ
    /// </summary>
    public struct RGB
    {
        /// <summary>
        /// 輝度データ
        /// </summary>
        /// <param name="_R"></param>
        /// <param name="_G"></param>
        /// <param name="_B"></param>
        public RGB(byte _R, byte _G, byte _B)
        {
            R = _R;
            G = _G;
            B = _B;
        }
        /// <summary>
        /// 輝度データ
        /// </summary>
        /// <param name="_R"></param>
        /// <param name="_G"></param>
        /// <param name="_B"></param>
        public RGB(int _R, int _G, int _B)
        {
            R = (byte)_R;
            G = (byte)_G;
            B = (byte)_B;
        }
        /// <summary>
        /// 輝度
        /// </summary>
        public byte R, G, B;
    }

    /// <summary>
    /// 座標データ
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// 座標データ
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public Point(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        /// <summary>
        /// 座標
        /// </summary>
        public int X, Y;
    }

    /// <summary>
    /// 画像サイズデータ
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// 画像サイズデータ
        /// </summary>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        public Size(int _width, int _height)
        {
            Width = _width;
            Height = _height;
        }
        /// <summary>
        /// サイズ
        /// </summary>
        public int Width, Height;
    }

    /// <summary>
    /// 色の輝度データ
    /// </summary>
    public struct Color
    {
        /// <summary>
        /// 黒色(0, 0, 0)
        /// </summary>
        public static readonly RGB Black = new RGB(0, 0, 0);
        /// <summary>
        /// 白色(255, 255, 255)
        /// </summary>
        public static readonly RGB White = new RGB(255, 255, 255);
        /// <summary>
        /// 赤色(255, 0, 0)
        /// </summary>
        public static readonly RGB Red = new RGB(255, 0, 0);
        /// <summary>
        /// 緑色(0, 255, 0)
        /// </summary>
        public static readonly RGB Green = new RGB(0, 255, 0);
        /// <summary>
        /// 青色(0, 0, 255)
        /// </summary>
        public static readonly RGB Blue = new RGB(0, 0, 255);
    }

    /// <summary>
    /// グレースケール変換法
    /// </summary>
    public struct GrayScaleMethod
    {
        /// <summary>
        /// v = (R+G+B)/3
        /// </summary>
        public const int Average = 0;
        /// <summary>
        /// v = 0.299R + 0.587G + 0.114B
        /// </summary>
        public const int BT601 = 1;
        /// <summary>
        /// v = 0.2126R + 0.7152G + 0.0722B
        /// </summary>
        public const int BT709 = 2;
        /// <summary>
        /// v = 0.25R + 0.50G + 0.25B
        /// </summary>
        public const int YCgCo = 3;
        /// <summary>
        /// v = max(R, G, B)
        /// </summary>
        public const int Max = 4;
        /// <summary>
        /// v = min(R, G, B)
        /// </summary>
        public const int Min = 5;
    }

    /// <summary>
    /// 画像処理クラス
    /// </summary>
    public partial class ImageProcessor
    {
        //--------------------------------------------------------------------------------
        // Bitmap関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Bitmapをbyte[]に変換する．
        /// </summary>
        /// <param name="bmp">変換元の32bitARGB Bitmap</param>
        /// <returns>1 pixel = 4 byte (+3:A, +2:R, +1:G, +0:B) に変換したbyte配列</returns>
        public static byte[] BitmapToByteArray(Bitmap bmp)
        {
            Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // 32bppArgbフォーマットで値を格納
            int bytes = bmp.Width * bmp.Height * 4;
            byte[] rgbValues = new byte[bytes];

            // Bitmapをbyte[]へコピー
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            bmp.UnlockBits(bmpData);
            return rgbValues;
        }

        public static Bitmap ByteArrayToBitmap(byte[] rgbValues, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // byte[]をBitmapへコピー
            Marshal.Copy(rgbValues, 0, bmpData.Scan0, rgbValues.Length);

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        /// <summary>
        /// byte配列をBitmapに変換する．
        /// </summary>
        /// <param name="rgbValues">1 pixel = 4 byte (+3:A, +2:R, +1:G, +0:B) に変換されたたbyte配列</param>
        /// <param name="size">変換後のbitmapのサイズ</param>
        /// <returns>Bitmap</returns>
        public static Bitmap ByteArrayToBitmap(byte[] rgbValues, Size size)
        {
            return ByteArrayToBitmap(rgbValues, size.Width, size.Height);
        }

        //--------------------------------------------------------------------------------
        // トリミング関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータをトリミングします．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="posX">トリミング左上座標x</param>
        /// <param name="posY">トリミング左上座標</param>
        /// <param name="bmpWidth">画像幅</param>
        /// <param name="bmpHeight">画像高さ</param>
        /// <param name="trimWidth">トリミング幅</param>
        /// <param name="trimHeight">トリミング高さ</param>
        /// <returns>トリミング画像のbyte配列</returns>
        public static byte[] Trim(byte[] rgbValues, int bmpWidth, int bmpHeight, int posX, int posY, int trimWidth, int trimHeight)
        {
            int x, y;
            int retWork, rgbWork;
            byte[] ret = new byte[4 * trimWidth * trimHeight];

            for (y = 0; y < trimHeight; y++)
            {
                for (x = 0; x < trimWidth; x++)
                {
                    retWork = 4 * (trimWidth * y + x);
                    rgbWork = 4 * (bmpWidth * (y + posY) + (x + posX));
                    ret[retWork] = rgbValues[rgbWork];
                    ret[retWork + 1] = rgbValues[rgbWork + 1];
                    ret[retWork + 2] = rgbValues[rgbWork + 2];
                    ret[retWork + 3] = rgbValues[rgbWork + 3];
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータをトリミングします．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="pos">トリミング左上座標</param>
        /// <param name="bmpSize">画像サイズ</param>
        /// <param name="trimSize">トリミングサイズ</param>
        /// <returns>トリミング画像のbyte配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Trim(byte[] rgbValues, Size bmpSize, Point pos, Size trimSize)
        {
            return Trim(rgbValues, bmpSize.Width, bmpSize.Height, pos.X, pos.Y, trimSize.Width, trimSize.Height);
        }

        /// <summary>
        /// bitmapデータをトリミングします．
        /// </summary>
        /// <param name="bmp">bitmap画像</param>
        /// <param name="posX">トリミング左上座標x</param>
        /// <param name="posY">トリミング左上座標y</param>
        /// <param name="trimWidth">トリミング幅</param>
        /// <param name="trimHeight">トリミング高さ</param>
        /// <returns>トリミング画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap Trim(Bitmap bmp, int posX, int posY, int trimWidth, int trimHeight)
        {
            return ByteArrayToBitmap(
                Trim(BitmapToByteArray(bmp),  bmp.Width, bmp.Height, posX, posY, trimWidth, trimHeight),
                trimWidth,
                trimHeight);
        }

        /// <summary>
        /// byte配列のbitmapデータをトリミングします．
        /// </summary>
        /// <param name="bmp">bitmap画像</param>
        /// <param name="pos">トリミング左上座標</param>
        /// <param name="trimSize">トリミングサイズ</param>
        /// <returns>トリミング画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap Trim(Bitmap bmp, Point pos, Size trimSize)
        {
            return ByteArrayToBitmap(
                Trim(BitmapToByteArray(bmp), bmp.Width, bmp.Height, pos.X, pos.Y, trimSize.Width, trimSize.Height),
                trimSize.Width,
                trimSize.Height);
        }

        //--------------------------------------------------------------------------------
        // グレースケール変換関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータをグレースケール化する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <param name="method">変換法</param>
        /// <returns>グレースケール画像のbyte配列</returns>
        public static byte[] GrayscaleConverter(byte[] rgbValues, int method)
        {
            byte[] ret = new byte[rgbValues.Length];
            double work = 0;

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                if (method == GrayScaleMethod.Average) work = (double)(rgbValues[i] + rgbValues[i + 1] + rgbValues[i + 2]) / 3;
                else if (method == GrayScaleMethod.BT601) work = (double)0.299 * rgbValues[i + 2] + 0.587 * rgbValues[i + 1] + 0.114 * rgbValues[i];
                else if (method == GrayScaleMethod.BT709) work = (double)0.2126 * rgbValues[i + 2] + 0.7152 * rgbValues[i + 1] + 0.0722 * rgbValues[i];
                else if (method == GrayScaleMethod.YCgCo) work = (double)0.25 * rgbValues[i + 2] + 0.5 * rgbValues[i + 1] + 0.25 * rgbValues[i];
                else if (method == GrayScaleMethod.Max) work = Math.Max(Math.Max(rgbValues[i], rgbValues[i + 1]), rgbValues[i + 2]);
                else if (method == GrayScaleMethod.Min) work = Math.Min(Math.Min(rgbValues[i], rgbValues[i + 1]), rgbValues[i + 2]);

                ret[i] = ret[i + 1] = ret[i + 2] = (byte)work;
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// bitmapデータをグレースケール化する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="method">変換法</param>
        /// <returns>グレースケール画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap GrayscaleConverter(Bitmap bmp, int method)
        {
            return ByteArrayToBitmap(
                GrayscaleConverter(BitmapToByteArray(bmp), method),
                bmp.Width,
                bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // 色反転関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータを色反転する．
        /// </summary>
        /// <param name="rgbValues">byte配列に変換されたbitmap</param>
        /// <returns>色反転画像のbyte配列</returns>
        public static byte[] Not(byte[] rgbValues)
        {
            byte[] ret = new byte[rgbValues.Length];

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                ret[i] = (byte)Math.Abs(255 - rgbValues[i]);
                ret[i + 1] = (byte)Math.Abs(255 - rgbValues[i + 1]);
                ret[i + 2] = (byte)Math.Abs(255 - rgbValues[i + 2]);
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// bitmapデータを色反転する．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <returns>色反転画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap Not(Bitmap bmp)
        {
            return ByteArrayToBitmap(
                Not(BitmapToByteArray(bmp)),
                bmp.Width,
                bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // 色論理積関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータの色論理積の計算をします．
        /// </summary>
        /// <param name="rgbValues1">byte配列のbitmap</param>
        /// <param name="rgbValues2">byte配列のbitmap</param>
        /// <returns>論理積画像のbyte配列</returns>
        public static byte[] And(byte[] rgbValues1, byte[] rgbValues2)
        {
            byte[] ret = new byte[rgbValues1.Length];

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                ret[i] = Math.Min(rgbValues1[i], rgbValues2[i]);
                ret[i + 1] = Math.Min(rgbValues1[i + 1], rgbValues2[i + 1]);
                ret[i + 2] = Math.Min(rgbValues1[i + 2], rgbValues2[i + 2]);
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// bitmapデータの色論理積の計算をします．
        /// </summary>
        /// <param name="bmp1">bitmap</param>
        /// <param name="bmp2">bitmap</param>
        /// <returns>論理積画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap And(Bitmap bmp1, Bitmap bmp2)
        {
            return ByteArrayToBitmap(
                And(BitmapToByteArray(bmp1), BitmapToByteArray(bmp2)),
                bmp1.Width,
                bmp1.Height);
        }

        //--------------------------------------------------------------------------------
        // 色論理和関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータの色論理和の計算をします．
        /// </summary>
        /// <param name="rgbValues1">byte配列のbitmap</param>
        /// <param name="rgbValues2">byte配列のbitmap</param>
        /// <returns>論理和画像のbyte配列</returns>
        public static byte[] Or(byte[] rgbValues1, byte[] rgbValues2)
        {
            byte[] ret = new byte[rgbValues1.Length];

            for (int i = 0; i < rgbValues1.Length; i += 4)
            {
                ret[i] = Math.Max(rgbValues1[i], rgbValues2[i]);
                ret[i + 1] = Math.Max(rgbValues1[i + 1], rgbValues2[i + 1]);
                ret[i + 2] = Math.Max(rgbValues1[i + 2], rgbValues2[i + 2]);
                ret[i + 3] = 255;
            }

            return ret;
        }

        /// <summary>
        /// bitmapデータの色論理和の計算をします．
        /// </summary>
        /// <param name="bmp1">bitmap</param>
        /// <param name="bmp2">bitmap</param>
        /// <returns>論理和画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap Or(Bitmap bmp1, Bitmap bmp2)
        {
            return ByteArrayToBitmap(
                Or(BitmapToByteArray(bmp1), BitmapToByteArray(bmp2)),
                bmp1.Width,
                bmp1.Height);
        }

        //--------------------------------------------------------------------------------
        // ポインタの描画処理関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータに四角のポインタを描画します．
        /// </summary>
        /// <param name="rgbValues">byte配列のbitmap</param>
        /// <param name="bmpWidth">Bitmapデータの幅</param>
        /// <param name="bmpHeight">Bitmapデータの高さ</param>
        /// <param name="R">ポインタの色(R)</param>
        /// <param name="G">ポインタの色(G)</param>
        /// <param name="B">ポインタの色(B)</param>
        /// <param name="posX">ポインタの座標(X)</param>
        /// <param name="posY">ポインタの座標(Y)</param>
        /// <param name="width">ポインタの幅</param>
        /// <param name="height">ポインタの高さ</param>
        /// <returns>四角のポイント描画画像のbyte配列</returns>
        public static byte[] PointBox(byte[] rgbValues, int bmpWidth, int bmpHeight,
            byte R, byte G, byte B, int posX, int posY, int width, int height)
        {
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            int wWork = posX + width / 2;
            int wbWork = posX - width / 2;
            int hWork = posY + height / 2;
            int x, y = posY - height / 2;

            for ( ; y <= hWork; y++)
            {
                for (x = wbWork; x <= wWork; x++)
                {
                    if (x < 0 || bmpWidth <= x || y < 0 || bmpHeight <= y) continue;
                    
                    retWork = 4 * (bmpWidth * y + x);
                    ret[retWork] = B;
                    ret[retWork + 1] = G;
                    ret[retWork + 2] = R;
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータに四角のポインタを描画します．
        /// </summary>
        /// <param name="rgbValues">byte配列のbitmap</param>
        /// <param name="bmpSize">Bitmapデータのサイズ</param>
        /// <param name="color">ポインタの色</param>
        /// <param name="pos">ポインタの座標</param>
        /// <param name="size">ポインタのサイズ</param>
        /// <returns>四角のポイント描画画像のbyte配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] PointBox(byte[] rgbValues, Size bmpSize, RGB color, Point pos, Size size)
        {
            return PointBox(rgbValues, bmpSize.Width, bmpSize.Height, color.R, color.G, color.B, pos.X, pos.Y, size.Width, size.Height);
        }

        /// <summary>
        /// bitmapデータに四角のポインタを描画します．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="R">ポインタの色(R)</param>
        /// <param name="G">ポインタの色(G)</param>
        /// <param name="B">ポインタの色(B)</param>
        /// <param name="posX">ポインタの座標(X)</param>
        /// <param name="posY">ポインタの座標(Y)</param>
        /// <param name="width">ポインタの幅</param>
        /// <param name="height">ポインタの高さ</param>
        /// <returns>四角のポイント描画画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap PointBox(Bitmap bmp, byte R, byte G, byte B, int posX, int posY, int width, int height)
        {
            return ByteArrayToBitmap(
                PointBox(BitmapToByteArray(bmp), bmp.Width, bmp.Height, R, G, B, posX, posY, width, height),
                bmp.Width,
                bmp.Height);
        }

        /// <summary>
        /// bitmapデータに四角のポインタを描画します．
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <param name="color">ポインタの色</param>
        /// <param name="pos">ポインタの座標</param>
        /// <param name="size">ポインタのサイズ</param>
        /// <returns>四角のポイント描画画像</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bitmap PointBox(Bitmap bmp, RGB color, Point pos, Size size)
        {
            return ByteArrayToBitmap(
                PointBox(BitmapToByteArray(bmp), bmp.Width, bmp.Height, color.R, color.G, color.B, pos.X, pos.Y, size.Width, size.Height),
                bmp.Width,
                bmp.Height);
        }

        //--------------------------------------------------------------------------------
        // ボックスラインの描画処理関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のbitmapデータに四角のラインを描画します．
        /// </summary>
        /// <param name="rgbValues">byte配列のbitmap</param>
        /// <param name="bmpWidth">Bitmapデータの幅</param>
        /// <param name="bmpHeight">Bitmapデータの高さ</param>
        /// <param name="R">ラインの色(R)</param>
        /// <param name="G">ラインの色(G)</param>
        /// <param name="B">ラインの色(B)</param>
        /// <param name="posX">ラインの座標(X)</param>
        /// <param name="posY">ラインの座標(Y)</param>
        /// <param name="width">ラインの幅</param>
        /// <param name="height">ラインの高さ</param>
        /// <returns>四角のライン描画画像のbyte配列</returns>
        public static byte[] DrawBoxLine(byte[] rgbValues, int bmpWidth, int bmpHeight,
            byte R, byte G, byte B, int posX, int posY, int width, int height)
        {
            int p;
            int pWork = width + posX;

            int retWork, retWork2;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for(p = posX; p < pWork; p++)
            {
                retWork = 4 * (bmpWidth * posY + p);
                retWork2 = 4 * (bmpWidth * (posY + height - 1) + p);
                ret[retWork] = ret[retWork2] = B;
                ret[retWork + 1] = ret[retWork2 + 1] = G;
                ret[retWork + 2] = ret[retWork2 + 2] = R;
            }

            pWork = height + posY;
            for (p = posY; p < pWork; p++)
            {
                retWork = 4 * (bmpWidth * p + posX);
                retWork2 = 4 * (bmpWidth * p + posX + width - 1);
                ret[retWork] = ret[retWork2] = B;
                ret[retWork + 1] = ret[retWork2 + 1] = G;
                ret[retWork + 2] = ret[retWork2 + 2] = R;
            }

            return ret;
        }

        /// <summary>
        /// byte配列のbitmapデータに四角のラインを描画します．
        /// </summary>
        /// <param name="rgbValues">byte配列のbitmap</param>
        /// <param name="bmpSize">Bitmapデータのサイズ</param>
        /// <param name="color">ラインの色</param>
        /// <param name="pos">ラインの座標</param>
        /// <param name="size">ラインのサイズ</param>
        /// <returns>四角のライン描画画像のbyte配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] DrawBoxLine(byte[] rgbValues, Size bmpSize, RGB color, Point pos, Size size)
        {
            return DrawBoxLine(rgbValues, bmpSize.Width, bmpSize.Height, color.R, color.G, color.B, pos.X, pos.Y, size.Width, size.Height);
        }

        /// <summary>
        /// byte配列のbitmapデータに四角のラインを描画します．
        /// </summary>
        /// <param name="rgbValues">byte配列のbitmap</param>
        /// <param name="bmpSize">Bitmapデータのサイズ</param>
        /// <param name="color">ラインの色</param>
        /// <param name="posUL">ラインの左上座標</param>
        /// <param name="posDR">ラインの右下座標</param>
        /// <returns>四角のライン描画画像のbyte配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] DrawBoxLine(byte[] rgbValues, Size bmpSize, RGB color, Point posUL, Point posDR)
        {
            return DrawBoxLine(rgbValues, bmpSize.Width, bmpSize.Height,
                color.R, color.G, color.B,
                posUL.X, posUL.Y,
                posDR.X - posUL.X + 1, posDR.Y - posUL.Y + 1);
        }

        //--------------------------------------------------------------------------------
        // 四角の描画処理関連
        //--------------------------------------------------------------------------------
        public static byte[] DrawBox(byte[] rgbValues, int bmpWidth, int bmpHeight,
            byte R, byte G, byte B, int posX, int posY, int width, int height)
        {
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            int x, y;
            int xWork = width + posX;
            int yWork = height + posY;
            
            for(y = posY; y < yWork; y++)
            {
                for(x = posX; x < xWork; x++)
                {
                    retWork = 4 * (bmpWidth * y + x);
                    ret[retWork] = B;
                    ret[retWork + 1] = G;
                    ret[retWork + 2] = R;
                    ret[retWork + 3] = 255;
                }
            }

            return ret;
        }
    }
}