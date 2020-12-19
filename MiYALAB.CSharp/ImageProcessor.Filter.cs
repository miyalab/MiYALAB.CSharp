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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MiYALAB.CSharp.Image
{
    /// <summary>
    /// フィルタカーネル
    /// </summary>
    public struct FilterKernel
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
        /// ガウシアンフィルタ3x3
        /// </summary>
        public static readonly int[,] Gaussian3 =
        {
            {1, 2, 1},
            {2, 4, 2},
            {1, 2, 1}
        };
        /// <summary>
        /// ガウシアンフィルタ5x5
        /// </summary>
        public static readonly int[,] Gaussian5 =
        {
            {1, 4,  6,  4,  1 },
            {4, 16, 24, 16, 4 },
            {6, 24, 36, 24, 6 },
            {4, 16, 24, 16, 4 },
            {1, 4,  6,  4,  1 }
        };
        /// <summary>
        /// ガウシアンフィルタ7x7
        /// </summary>
        public static readonly int[,] Gaussian7 =
        {
            {1,  6,   15,  20,  15,  6,   1 },
            {6,  36,  90,  120, 90,  36,  6 },
            {15, 90,  225, 300, 225, 90,  15 },
            {20, 120, 300, 400, 300, 120, 20 },
            {15, 90,  225, 300, 225, 90,  15 },
            {6,  36,  90,  120, 90,  36,  6 },
            {1,  6,   15,  20,  15,  6,   1 }
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
    /// 画像処理クラス
    /// </summary>
    public partial class ImageProcessor
    {
        //--------------------------------------------------------------------------------
        // フィルタ関連
        //--------------------------------------------------------------------------------
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
            int x, y;
            double work;
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 1; y < height - 1; y++)
            {
                for (x = 1; x < width - 1; x++)
                {
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

                    retWork = 4 * (width * y + x);
                    ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = (byte)work;
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
            int x, y;
            int work;
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 1; y < height - 1; y++)
            {
                for (x = 1; x < width - 1; x++)
                {
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

                    work = (int)(work * weight);

                    retWork = 4 * (width * y + x);
                    ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = (byte)work;
                }
            }

            return ret;
        }

        //--------------------------------------------------------------------------------
        // ガウシアンフィルタ関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// byte配列のグレースケールBitmapデータのガウシアンフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="width">Bitmapデータの幅</param>
        /// <param name="height">Bitmapデータの高さ</param>
        /// <param name="kernelSize">kernelの大きさ(3 or 5 or 7)</param>
        /// <returns>ガウシアンフィルタ後の画像のbyte配列</returns>
        public static byte[] FilterGaussian(byte[] rgbValues, int width, int height, int kernelSize)
        {
            int[,] kernel;
            if (kernelSize == 3) kernel = FilterKernel.Gaussian3;
            else if (kernelSize == 5) kernel = FilterKernel.Gaussian5;
            else if (kernelSize == 7) kernel = FilterKernel.Gaussian7;
            else { return null; }

            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            //Array.Copy(rgbValues, ret, rgbValues.Length);
            
            int xyWork = kernelSize / 2;
            int xb = width - xyWork;
            int yb = height - xyWork;
            int x, y;
            int lx, ly;
            double work;
            double kernelM = Math.Pow(2, 2 * kernelSize - 2);
            
            for(y = xyWork; y < yb; y++)
            {
                for(x = xyWork; x < xb; x++)
                {
                    work = 0;
                    for(ly = 0; ly < kernelSize; ly++)
                    {
                        for (lx = 0; lx < kernelSize; lx++)
                        {
                            work += rgbValues[4 * (width * (y + ly - xyWork) + (x + lx - xyWork))] * kernel[ly, lx];
                        }
                    }
                    work /= kernelM;

                    retWork = 4 * (width * y + x);
                    ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = (byte)work;
                    ret[retWork + 3] = 255;
                }
            }

            return ret;
        }

        //--------------------------------------------------------------------------------
        // Prewittフィルタ関連
        //--------------------------------------------------------------------------------
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
            int x, y;
            int workX, workY;
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 1; y < height - 1; y++)
            {
                for (x = 1; x < width - 1; x++)
                {
                    workX =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FilterKernel.PrewittX[0, 0]
                        + rgbValues[4 * (width * (y) + (x - 1))] * FilterKernel.PrewittX[0, 1]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FilterKernel.PrewittX[0, 2]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FilterKernel.PrewittX[2, 0]
                        + rgbValues[4 * (width * (y) + (x + 1))] * FilterKernel.PrewittX[2, 1]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FilterKernel.PrewittX[2, 2];
                    workY =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FilterKernel.PrewittY[0, 0]
                        + rgbValues[4 * (width * (y - 1) + (x))] * FilterKernel.PrewittY[1, 0]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FilterKernel.PrewittY[2, 0]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FilterKernel.PrewittY[0, 2]
                        + rgbValues[4 * (width * (y + 1) + (x))] * FilterKernel.PrewittY[1, 2]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FilterKernel.PrewittY[2, 2];

                    workX = (Math.Abs(workX) + Math.Abs(workY));
                    workX = (int)(workX * weight);

                    retWork = 4 * (width * y + x);
                    ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = (byte)workX;
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のグレースケールBitmapデータのPrewittフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <param name="weight">計算重み</param>
        /// <returns>Prewittフィルタ後の画像のbyte配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] FilterPrewitt(byte[] rgbValues, Size size, double weight)
        {
            return FilterPrewitt(rgbValues, size.Width, size.Height, weight);
        }

        //--------------------------------------------------------------------------------
        // Sobelフィルタ関連
        //--------------------------------------------------------------------------------
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
            int x, y;
            int workX, workY;
            int retWork;
            byte[] ret = new byte[rgbValues.Length];
            Array.Copy(rgbValues, ret, rgbValues.Length);

            for (y = 1; y < height - 1; y++)
            {
                for (x = 1; x < width - 1; x++)
                {
                    workX =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FilterKernel.SobelX[0, 0]
                        + rgbValues[4 * (width * (y) + (x - 1))] * FilterKernel.SobelX[0, 1]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FilterKernel.SobelX[0, 2]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FilterKernel.SobelX[2, 0]
                        + rgbValues[4 * (width * (y) + (x + 1))] * FilterKernel.SobelX[2, 1]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FilterKernel.SobelX[2, 2];
                    workY =
                          rgbValues[4 * (width * (y - 1) + (x - 1))] * FilterKernel.SobelY[0, 0]
                        + rgbValues[4 * (width * (y - 1) + (x))] * FilterKernel.SobelY[1, 0]
                        + rgbValues[4 * (width * (y - 1) + (x + 1))] * FilterKernel.SobelY[2, 0]
                        + rgbValues[4 * (width * (y + 1) + (x - 1))] * FilterKernel.SobelY[0, 2]
                        + rgbValues[4 * (width * (y + 1) + (x))] * FilterKernel.SobelY[1, 2]
                        + rgbValues[4 * (width * (y + 1) + (x + 1))] * FilterKernel.SobelY[2, 2];

                    workX = (Math.Abs(workX) + Math.Abs(workY));
                    workX = (int)(workX * weight);

                    retWork = 4 * (width * y + x);
                    ret[retWork] = ret[retWork + 1] = ret[retWork + 2] = (byte)workX;
                }
            }

            return ret;
        }

        /// <summary>
        /// byte配列のグレースケールBitmapデータのSobelフィルタ処理を行います．
        /// </summary>
        /// <param name="rgbValues">byte配列のグレースケールBitmapデータ</param>
        /// <param name="size">Bitmapデータのサイズ</param>
        /// <param name="weight">計算重み</param>
        /// <returns>Sobelフィルタ後の画像のbyte配列</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] FilterSobel(byte[] rgbValues, Size size, double weight)
        {
            return FilterSobel(rgbValues, size.Width, size.Height, weight);
        }

        //--------------------------------------------------------------------------------
        // Cannyフィルタ関連
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Canny法によるエッジ検出
        /// </summary>
        /// <param name="rgbValues">微分画像</param>
        /// <param name="width">bitmapデータの幅</param>
        /// <param name="height">bitmapデータの高さ</param>
        /// <param name="thresholdMin">canny最小閾値</param>
        /// <param name="thresholdMax">canny最大閾値</param>
        /// <returns></returns>
        public static byte[] Canny(byte[] rgbValues, int width, int height, int thresholdMin, int thresholdMax)
        {
            int work;
            int x, y;
            
            byte[] maxOver = new byte[rgbValues.Length];
            byte[] minOver = new byte[rgbValues.Length];

            if (thresholdMax < thresholdMin) 
                (thresholdMax, thresholdMin) = (thresholdMin, thresholdMax);

            for(y = 0; y < height; y++)
            {
                for(x = 0; x < width; x++)
                {
                    work = 4 * (width * y + x);

                    // 最小閾値以上
                    if (rgbValues[work] >= thresholdMin)
                    {
                        minOver[work] = minOver[work + 1] = minOver[work + 2] = 255;
                    }
                    // 最大閾値以上
                    if (rgbValues[work] >= thresholdMax)
                    {
                        maxOver[work] = maxOver[work + 1] = maxOver[work + 2] = 255;
                    }
                    maxOver[work + 3] = minOver[work + 3] = 255;
                }
            }

            bool flag;
            int i;
            Label[] labels = Labeling(minOver, width, height);
            for(i = 1; i < labels.Length; i++)
            {
                flag = false;
                for(y = labels[i].Pos.Y; y < labels[i].Size.Height; y++)
                {
                    for(x = labels[i].Pos.X; x < labels[i].Size.Width; x++)
                    {
                        work = 4 * (width * y + x);

                        if (labels[i].rgbValues[work] == maxOver[work])
                        {
                            labels[0].rgbValues = Or(labels[0].rgbValues, labels[i].rgbValues);
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true) break;
                }
            }

            return labels[0].rgbValues;
        }
    }
}
