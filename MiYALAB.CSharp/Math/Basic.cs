﻿/*
 * MIT License
 * 
 * Copyright (c) 2020-2022 MiYA LAB(K.Miyauchi)
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
    /// 数学基本関数
    /// </summary>
    public class MathBasic
    {
        /// <summary>
        /// 度数法 -> 弧度法
        /// </summary>
        /// <param name="deg">angle[deg]</param>
        /// <returns></returns>
        public static double DegToRad(double deg)
        {
            return deg * Math.PI / 180;
        }

        /// <summary>
        /// 弧度法 -> 度数法
        /// </summary>
        /// <param name="rad">angle[rad]</param>
        /// <returns></returns>
        public static double RadToDeg(double rad)
        {
            return rad * 180 / Math.PI;
        }

        /// <summary>
        /// 拡張arctan関数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>-π~π[rad]</returns>
        public static double AtanEx(double x, double y)
        {
            double ret = Math.Atan(y / x);
            
            // 第2象限
            if(x < 0 && y > 0)
            {
                ret += Math.PI;
            }
            // 第3象限
            if(x < 0 && y < 0)
            {
                ret -= Math.PI;
            }

            return ret;
        }
    }
}
