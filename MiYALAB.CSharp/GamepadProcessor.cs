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
using System.Runtime.InteropServices;

namespace MiYALAB.CSharp
{
    namespace Gamepad
    {
        /// <summary>
        /// ゲームパッドコントロール用クラス
        /// </summary>
        public class GamepadProcessor
        {
            /// <summary></summary>
            public const int MM_JOY1MOVE = 0x3A0;
            /// <summary></summary>
            public const int MM_JOY2MOVE = 0x3A1;
            /// <summary></summary>
            public const int MM_JOY1ZMOVE = 0x3A2;
            /// <summary></summary>
            public const int MM_JOY2ZMOVE = 0x3A3;
            /// <summary></summary>
            public const int MM_JOY1BUTTONDOWN = 0x3B5;
            /// <summary></summary>
            public const int MM_JOY2BUTTONDOWN = 0x3B6;
            /// <summary></summary>
            public const int MM_JOY1BUTTONUP = 0x3B7;
            /// <summary></summary>
            public const int MM_JOY2BUTTONUP = 0x3B8;

            /// <summary></summary>
            public const int MMSYSERR_BADDEVICEID = 2;
            /// <summary></summary>
            public const int MMSYSERR_NODRIVER = 6;
            /// <summary></summary>
            public const int MMSYSERR_INVALPARAM = 11;

            /// <summary></summary>
            public const int JOY_ERR_NONE = 0;
            /// <summary></summary>
            public const int JOY_ERR_PARMS = 165;
            /// <summary></summary>
            public const int JOY_ERR_NOCANDO = 166;
            /// <summary></summary>
            public const int JOY_ERR_UNPLUGGED = 167;
            
            /// <summary></summary>
            public const int JOY_RETURN_X = 0x001;
            /// <summary></summary>
            public const int JOY_RETURN_Y = 0x002;
            /// <summary></summary>
            public const int JOY_RETURN_Z = 0x004;
            /// <summary></summary>
            public const int JOY_RETURN_R = 0x008;
            /// <summary></summary>
            public const int JOY_RETURN_U = 0x010;
            /// <summary></summary>
            public const int JOY_RETURN_V = 0x020;
            /// <summary></summary>
            public const int JOY_RETURN_POV = 0x040;
            /// <summary></summary>
            public const int JOY_RETURN_BUTTONS = 0x080;
            /// <summary></summary>
            public const int JOY_RETURN_ALL = 0x0FF;

            /// <summary></summary>
            public const int JOY_RETURN_RAWDATA = 0x100;
            /// <summary></summary>
            public const int JOY_RETURN_POVCTS = 0x200;
            /// <summary></summary>
            public const int JOY_RETURN_CENTERED = 0x400;

            /// <summary></summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct JOYINFOEX
            {
                /// <summary></summary>
                public int dwSize;
                /// <summary></summary>
                public int dwFlags;
                /// <summary></summary>
                public int dwXpos;
                /// <summary></summary>
                public int dwYpos;
                /// <summary></summary>
                public int dwZpos;
                /// <summary></summary>
                public int dwRpos;
                /// <summary></summary>
                public int dwUpos;
                /// <summary></summary>
                public int dwVpos;
                /// <summary></summary>
                public int dwButtons;
                /// <summary></summary>
                public int dwButtonNumber;
                /// <summary></summary>
                public int dwPOV;
                /// <summary></summary>
                public int dwReserved1;
                /// <summary></summary>
                public int dwReserved2;
            }

            /// <summary></summary>
            public static class GetInfo
            {
                /// <summary>
                /// 最大接続ゲームパッド数取得
                /// </summary>
                /// <returns>最大ゲームパッド数</returns>
                [DllImport("winmm.dll")]
                public static extern int joyGetNumDevs();

                /// <summary>
                /// ゲームパッド入力パラメータ更新
                /// </summary>
                /// <param name="uJoyID"></param>
                /// <param name="pji"></param>
                /// <returns>0：接続, otherwise：エラー</returns>
                [DllImport("winmm.dll")]
                public static extern int joyGetPosEx(int uJoyID, ref JOYINFOEX pji);

                /// <summary>
                /// 
                /// </summary>
                /// <param name="hwnd"></param>
                /// <param name="uJoyID"></param>
                /// <param name="uPeriod"></param>
                /// <param name="fChanged"></param>
                /// <returns></returns>
                [DllImport("winmm.dll")]
                public static extern int joySetCapture(IntPtr hwnd, int uJoyID, int uPeriod, int fChanged);

                /// <summary>
                /// 
                /// </summary>
                /// <param name="uJoyID"></param>
                /// <returns></returns>
                [DllImport("winmm.dll")]
                public static extern int joyReleaseCapture(int uJoyID);
            }
        }

        /// <summary>
        /// GamepadProcessor用 DualShock4 シンボル値
        /// </summary>
        public class DUALSHOCK4
        {
            /// <summary>
            /// □ボタン
            /// </summary>
            public const int SQUARE = 0x00000001;
            /// <summary>
            /// ×ボタン
            /// </summary>
            public const int CROSS = 0x00000002;
            /// <summary>
            /// 〇ボタン
            /// </summary>
            public const int CIRCLE = 0x00000004;
            /// <summary>
            /// △ボタン
            /// </summary>
            public const int TRIANGLE = 0x00000008;
            /// <summary>
            /// L1ボタン
            /// </summary>
            public const int L1 = 0x00000010;
            /// <summary>
            /// R1ボタン
            /// </summary>
            public const int R1 = 0x00000020;
            /// <summary>
            /// L2ボタン
            /// </summary>
            public const int L2 = 0x00000040;
            /// <summary>
            /// R2ボタン
            /// </summary>
            public const int R2 = 0x00000080;
            /// <summary>
            /// SHAREボタン
            /// </summary>
            public const int SHARE = 0x00000100;
            /// <summary>
            /// OPTIONSボタン
            /// </summary>
            public const int OPTIONS = 0x00000200;
            /// <summary>
            /// L3ボタン
            /// </summary>
            public const int L3 = 0x00000400;
            /// <summary>
            /// R3ボタン
            /// </summary>
            public const int R3 = 0x00000800;
            /// <summary>
            /// PSボタン
            /// </summary>
            public const int PS = 0x00001000;
            /// <summary>
            /// PADボタン
            /// </summary>
            public const int PAD = 0x00002000;

        }
    }
}
