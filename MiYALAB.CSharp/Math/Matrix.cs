/*
 * MIT License
 * 
 * Copyright (c) 2020-2021 MiYA LAB(K.Miyauchi)
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiYALAB.CSharp.Mathematics
{
    /// <summary>
    /// マルチスレッド処理化 行列計算クラス
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// LUP分解クラス
        /// </summary>
        private class LUP
        {
            public Matrix mat;
            public int[] perm;
            public int toggle;
        }

        /// <summary>
        /// 行列データ
        /// </summary>
        private double[][] data;

        //----------------------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------------------
        /// <summary>
        /// マルチスレッド処理化 行列計算クラスのコンストラクタ
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public Matrix(int height, int width)
        {
            // 配列の初期化
            data = new double[height][];
            for (int i = 0; i < height; i++)
            {
                data[i] = new double[width];
            }
        }

        //----------------------------------------------------------------------------------
        // indexer
        //----------------------------------------------------------------------------------
        /// <summary>
        /// 行列の高さのインデクサ
        /// </summary>
        public int height
        {
            get { return data.Length; }
        }
        /// <summary>
        /// 行列の行数のインデクサ
        /// </summary>
        public int rows
        {
            get { return height; }
        }
        /// <summary>
        /// 行列の幅のインデクサ
        /// </summary>
        public int width
        {
            get { return data[0].Length; }
        }
        /// <summary>
        /// 行列の列数のインデクサ
        /// </summary>
        public int colums
        {
            get { return width; }
        }
        /// <summary>
        /// 行列の要素のインデクサ
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public double this[int y, int x]
        {
            set
            {
                if (0 <= y && y < height && 0 <= x && x < width) data[y][x] = value;
                else throw new Exception("It is out of the index range.");
            }
            get
            {
                if (0 <= y && y < height && 0 <= x && x < width) return data[y][x];
                else throw new Exception("It is out of the index range.");
            }
        }
        /// <summary>
        /// 行列の行ポインタのインデクサ
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public double[] this[int row]
        {
            set
            {
                if (0 <= row && row < height) data[row] = value;
                else throw new Exception("It is out of the index range.");
            }
            get
            {
                if (0 <= row && row < height) return data[row];
                else throw new Exception("It is out of the index range.");
            }
        }

        //----------------------------------------------------------------------------------
        // class method
        //----------------------------------------------------------------------------------
        /// <summary>
        /// 行列の文字列化メソッド
        /// </summary>
        /// <returns></returns>
        override public string ToString()
        {
            string ret = "";

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ret += this[y, x].ToString() + " ";
                }
                ret += Environment.NewLine;
            }

            return ret;
        }
        /// <summary>
        /// 行列の複製メソッド
        /// </summary>
        /// <returns></returns>
        public Matrix Clone()
        {
            return Clone(this);
        }
        /// <summary>
        /// 行列の逆行列計算メソッド
        /// </summary>
        /// <returns></returns>
        public Matrix Inverse()
        {
            return Inverse(this);
        }
        /// <summary>
        /// 行列の行列式計算メソッド
        /// </summary>
        /// <returns></returns>
        public double Det()
        {
            return Det(this);
        }

        //----------------------------------------------------------------------------------
        // static class method
        //----------------------------------------------------------------------------------
        /// <summary>
        /// 行列の複製メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Clone(Matrix mat)
        {
            Matrix ret = new Matrix(mat.height, mat.width);

            Parallel.For(0, mat.height, i =>
            {
                for (int j = 0; j < mat.width; j++)
                {
                    ret[i, j] = mat[i, j];
                }
            });
            
            return ret;
        }
        /// <summary>
        /// 行列の加算メソッド
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix Add(Matrix mat1, Matrix mat2)
        {
            if (mat1.width == mat2.width && mat1.height == mat2.height)
            {
                Matrix ret = new Matrix(mat1.height, mat1.width);

                Parallel.For(0, mat1.height, i =>
                {
                    for (int j = 0; j < mat1.width; j++)
                    {
                        ret[i, j] = mat1[i, j] + mat2[i, j];
                    }
                });
                return ret;
            }
            else
            {
                throw new Exception("Input a matrix of the same size.");
            }
        }
        /// <summary>
        /// 行列の減算メソッド
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix Sub(Matrix mat1, Matrix mat2)
        {
            if (mat1.width == mat2.width && mat1.height == mat2.height)
            {
                Matrix ret = new Matrix(mat1.height, mat1.width);

                Parallel.For(0, mat1.height, i =>
                {
                    for (int j = 0; j < mat1.width; j++)
                    {
                        ret[i, j] = mat1[i, j] - mat2[i, j];
                    }
                });
                return ret;
            }
            else
            {
                throw new Exception("Input a matrix of the same size.");
            }
        }
        /// <summary>
        /// 行列の乗算メソッド
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix Mult(Matrix mat1, Matrix mat2)
        {
            if (mat1.width == mat2.height)
            {
                Matrix ret = new Matrix(mat1.height, mat2.width);

                Parallel.For(0, mat1.height, i =>
                {
                    for (int j = 0; j < mat2.width; j++)
                    {
                        for (int k = 0; k < mat1.width; k++)
                        {
                            ret[i, j] += mat1[i, k] * mat2[k, j];
                        }
                    }
                });

                return ret;
            }
            else
            {
                throw new Exception("Input a matrix with the same width of mat1 and height of mat2");
            }
        }
        /// <summary>
        /// 行列のスカラ積メソッド
        /// </summary>
        /// <param name="k"></param>
        /// <param name="mat1"></param>
        /// <returns></returns>
        public static Matrix Mult(double k, Matrix mat1)
        {
            Matrix ret = new Matrix(mat1.height, mat1.width);

            Parallel.For(0, mat1.height, i =>
            {
                for (int j = 0; j < mat1.width; j++)
                {
                    ret[i, j] = mat1[i, j] * k;
                }
            });

            return ret;
        }
        /// <summary>
        /// 行列のスカラ積メソッド
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Matrix Mult(Matrix mat1, double k)
        {
            return Mult(k, mat1);
        }
        /// <summary>
        /// 行列の転置行列計算メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Transpose(Matrix mat)
        {
            Matrix ret = new Matrix(mat.width, mat.height);
            Parallel.For(0, mat.height, i =>
            {
                for(int j = 0; j < mat.width; j++)
                {
                    ret[j, i] = mat[i, j];
                }
            });

            return ret;
        }
        /// <summary>
        /// 行列のLUP分解メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        private static LUP DecomposeLUP(Matrix mat)
        {
            LUP ret = new LUP();

            ret.toggle = 1;
            ret.mat = mat.Clone();
            ret.perm = new int[mat.height];
            Parallel.For(0, mat.height, i =>{ ret.perm[i] = i; });

            for(int j = 0; j < mat.height - 1; j++)
            {
                double colMax = Math.Abs(ret.mat[j][j]); // largest val in col j
                int pRow = j;
                for (int i = j + 1; i < mat.height; ++i)
                {
                    if (ret.mat[i][j] > colMax)
                    {
                        colMax = ret.mat[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) // swap rows
                {
                    double[] rowPtr = ret.mat[pRow];
                    ret.mat[pRow] = ret.mat[j];
                    ret.mat[j] = rowPtr;
                    int tmp = ret.perm[pRow]; // and swap perm info
                    ret.perm[pRow] = ret.perm[j];
                    ret.perm[j] = tmp;
                    ret.toggle = -ret.toggle; // row-swap toggle
                }
                if (Math.Abs(ret.mat[j][j]) < 1.0E-20) return null; // consider a throw
                for (int i = j + 1; i < mat.height; ++i)
                {
                    ret.mat[i][j] /= ret.mat[j][j];
                    for (int k = j + 1; k < mat.height; k++) ret.mat[i][k] -= ret.mat[i][j] * ret.mat[j][k];
                }
            }
            return ret;
        }
        /// <summary>
        /// 行列の逆行列計算の補助計算メソッド
        /// </summary>
        /// <param name="luMat"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static double[] HelperSolve(Matrix luMat, double[] b)
        {
            // solve luMatrix * x = b
            int n = luMat.height;
            double[] x = new double[n];
            b.CopyTo(x, 0);
            Parallel.For(1, n, i => 
            {
                double sum = x[i];
                for (int j = 0; j < i; j++) sum -= luMat[i][j] * x[j];
                x[i] = sum;
            });
            x[n - 1] /= luMat[n - 1][n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j) sum -= luMat[i][j] * x[j];
                x[i] = sum / luMat[i][i];
            }
            return x;
        }
        /// <summary>
        /// 行列の逆行列計算メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Inverse(Matrix mat)
        {
            int n = mat.height;
            Matrix ret = mat.Clone();

            LUP lum = DecomposeLUP(mat);
            if (lum == null) throw new Exception("Unable to compute inverse");
            double[] b = new double[n];
            
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == lum.perm[j]) b[j] = 1.0;
                    else b[j] = 0.0;
                }
                double[] x = HelperSolve(lum.mat, b);
                for (int j = 0; j < n; ++j) ret[j][i] = x[j];
            }
            return ret;
        }
        /// <summary>
        /// 行列の行列式計算メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static double Det(Matrix mat)
        {
            LUP lum = DecomposeLUP(mat);
            if (lum == null) throw new Exception("Unable to compute MatrixDeterminant");
            double ret = lum.toggle;
            Parallel.For(0, lum.mat.height, i => { ret *= lum.mat[i][i]; });
            return ret;
        }
        /// <summary>
        /// 行列データを二次元配列変換メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static double[][] MatrixToDouble2D(Matrix mat) 
        {
            return (double[][])mat.data.Clone();
        }
        /// <summary>
        /// 二次元配列を行列変換メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Double2DToMatrix(double[][] mat)
        {
            Matrix ret = new Matrix(mat.Length, mat[0].Length);
            ret.data = mat;
            return ret;
        }
        /// <summary>
        /// 二次元配列を行列変換メソッド
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Matrix Double2DToMatrix(double[,] mat)
        {
            Matrix ret = new Matrix(mat.GetLength(0), mat.GetLength(1));

            Parallel.For(0, ret.height, i =>
            {
                for(int j=0; j<ret.width; j++)
                {
                    ret[i, j] = mat[i, j];
                }
            });

            return ret;
        }

        //----------------------------------------------------------------------------------
        // Overload of operator
        //----------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            return Add(mat1, mat2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            return Sub(mat1, mat2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            return Mult(mat1, mat2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="mat1"></param>
        /// <returns></returns>
        public static Matrix operator *(double k, Matrix mat1)
        {
            return Mult(k, mat1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix mat1, double k)
        {
            return Mult(mat1, k);
        }
    }
}
