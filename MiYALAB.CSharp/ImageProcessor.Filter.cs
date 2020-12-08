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
        // フィルタ関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// フィルタカーネル
        /// </summary>
        public static class FILTER_KERNEL
        {
            /// <summary>
            /// 平均フィルタ
            /// </summary>
            public static readonly double[,] Average =
            {
                {(double)1/9,(double)1/9,(double)1/9 },
                {(double)1/9,(double)1/9,(double)1/9 },
                {(double)1/9,(double)1/9,(double)1/9 }
            };
            /// <summary>
            /// ガウシアンフィルタ
            /// </summary>
            public static readonly double[,] Gaussian =
            {
                {(double)1/16,  (double)1/8, (double)1/16},
                {(double)1/ 8,  (double)1/4, (double)1/ 8},
                {(double)1/16,  (double)1/8, (double)1/16}
            };
            /// <summary>
            /// X方向Prewittフィルタ
            /// </summary>
            public static readonly int[,] PrewittX =
            {
                {-1, 0, 1},
                {-1, 0, 1},
                {-1, 0, 1}
            };
            /// <summary>
            /// Y方向Prewittフィルタ
            /// </summary>
            public static readonly int[,] PrewittY =
            {
                {-1,-1, -1},
                { 0, 0,  0},
                { 1, 1,  1}
            };
            /// <summary>
            /// X方向Sobelフィルタ
            /// </summary>
            public static readonly int[,] SobelX =
            {
                {-1, 0, 1},
                {-2, 0, 2},
                {-1, 0, 1}
            };
            /// <summary>
            /// Y方向Sobelフィルタ
            /// </summary>
            public static readonly int[,] SobelY =
            {
                {-1,-2, -1},
                { 0, 0,  0},
                { 1, 2,  1}
            };
            /// <summary>
            /// ラプラシアンフィルタ
            /// </summary>
            public static readonly int[,] Laplacian =
            {
                {0,  1, 0},
                {1, -4, 1},
                {0,  1, 0}
            };
        }

        /// <summary>
        /// byte配列のグレースケールBitmapデータのフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="kernel">フィルタ[y,x]</param>
        /// <param name="weight">計算重み</param>
        /// <returns>フィルタ後の画像のbyte配列</returns>
        public static byte[] Filter(byte[] rgbValues, int width, int height, double[,] kernel, double weight)
        {
            byte[] ret = (byte[])rgbValues.Clone();

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double work;
                    work =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * kernel[0, 0]
                        + rgbValues[4 * (width * (y) + (x - 1))] * kernel[0, 1]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * kernel[0, 2]
                        + rgbValues[4 * (width * (y - 1) + (x))] * kernel[1, 0]
                        + rgbValues[4 * (width * (y) + (x))] * kernel[1, 1]
                        + rgbValues[4 * (width * (y + 1) + (x))] * kernel[1, 2]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * kernel[2, 0]
                        + rgbValues[4 * (width * (y) + (x + 1))] * kernel[2, 1]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * kernel[2, 2];

                    work *= weight;

                    ret[4 * (width * y + x)] =
                            ret[4 * (width * y + x) + 1] =
                            ret[4 * (width * y + x) + 2] = (byte)work;
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のグレースケールBitmapデータのフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="kernel">フィルタ[y,x]</param>
        /// <param name="weight">計算重み</param>
        /// <returns>フィルタ後の画像のbyte配列</returns> 
        public static byte[] Filter(byte[] rgbValues, int width, int height, int[,] kernel, double weight)
        {
            byte[] ret = (byte[])rgbValues.Clone();

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double work;
                    work =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * kernel[0, 0]
                        + rgbValues[4 * (width * (y) + (x - 1))] * kernel[0, 1]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * kernel[0, 2]
                        + rgbValues[4 * (width * (y - 1) + (x))] * kernel[1, 0]
                        + rgbValues[4 * (width * (y) + (x))] * kernel[1, 1]
                        + rgbValues[4 * (width * (y + 1) + (x))] * kernel[1, 2]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * kernel[2, 0]
                        + rgbValues[4 * (width * (y) + (x + 1))] * kernel[2, 1]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * kernel[2, 2];

                    work *= weight;

                    ret[4 * (width * y + x)] =
                            ret[4 * (width * y + x) + 1] =
                            ret[4 * (width * y + x) + 2] = (byte)work;
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のグレースケールBitmapデータのPrewittフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="weight">計算重み</param>
        /// <returns>Prewittフィルタ後の画像のbyte配列</returns>
        public static byte[] FilterPrewitt(byte[] rgbValues, int width, int height, double weight)
        {
            byte[] ret = (byte[])rgbValues.Clone();

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double workX, workY;
                    workX =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FILTER_KERNEL.PrewittX[0, 0]
                        + rgbValues[4 * (width * (y) + (x - 1))] * FILTER_KERNEL.PrewittX[0, 1]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FILTER_KERNEL.PrewittX[0, 2]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FILTER_KERNEL.PrewittX[2, 0]
                        + rgbValues[4 * (width * (y) + (x + 1))] * FILTER_KERNEL.PrewittX[2, 1]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FILTER_KERNEL.PrewittX[2, 2];
                    workY =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FILTER_KERNEL.PrewittY[0, 0]
                        + rgbValues[4 * (width * (y - 1) + (x))] * FILTER_KERNEL.PrewittY[1, 0]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FILTER_KERNEL.PrewittY[2, 0]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FILTER_KERNEL.PrewittY[0, 2]
                        + rgbValues[4 * (width * (y + 1) + (x))] * FILTER_KERNEL.PrewittY[1, 2]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FILTER_KERNEL.PrewittY[2, 2];

                    workX = (Math.Abs(workX) + Math.Abs(workY));
                    workX *= weight;

                    ret[4 * (width * y + x)] =
                            ret[4 * (width * y + x) + 1] =
                            ret[4 * (width * y + x) + 2] = (byte)workX;
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のグレースケールBitmapデータのSobelフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="weight">計算重み</param>
        /// <returns>Sobelフィルタ後の画像のbyte配列</returns>
        public static byte[] FilterSobel(byte[] rgbValues, int width, int height, double weight)
        {
            byte[] ret = (byte[])rgbValues.Clone();

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double workX, workY;
                    workX =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FILTER_KERNEL.SobelX[0, 0]
                        + rgbValues[4 * (width * (y) + (x - 1))] * FILTER_KERNEL.SobelX[0, 1]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FILTER_KERNEL.SobelX[0, 2]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FILTER_KERNEL.SobelX[2, 0]
                        + rgbValues[4 * (width * (y) + (x + 1))] * FILTER_KERNEL.SobelX[2, 1]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FILTER_KERNEL.SobelX[2, 2];
                    workY =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FILTER_KERNEL.SobelY[0, 0]
                        + rgbValues[4 * (width * (y - 1) + (x))] * FILTER_KERNEL.SobelY[1, 0]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FILTER_KERNEL.SobelY[2, 0]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FILTER_KERNEL.SobelY[0, 2]
                        + rgbValues[4 * (width * (y + 1) + (x))] * FILTER_KERNEL.SobelY[1, 2]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FILTER_KERNEL.SobelY[2, 2];

                    workX = (Math.Abs(workX) + Math.Abs(workY));
                    workX *= weight;

                    ret[4 * (width * y + x)] =
                            ret[4 * (width * y + x) + 1] =
                            ret[4 * (width * y + x) + 2] = (byte)workX;
                }
            }
            
            return ret;
        }

    }
}
