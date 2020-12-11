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
        class GamepadProcessor
        {
            public const int MM_JOY1MOVE = 0x3A0;
            public const int MM_JOY2MOVE = 0x3A1;
            public const int MM_JOY1ZMOVE = 0x3A2;
            public const int MM_JOY2ZMOVE = 0x3A3;
            public const int MM_JOY1BUTTONDOWN = 0x3B5;
            public const int MM_JOY2BUTTONDOWN = 0x3B6;
            public const int MM_JOY1BUTTONUP = 0x3B7;
            public const int MM_JOY2BUTTONUP = 0x3B8;

            public const int MMSYSERR_BADDEVICEID = 2;
            public const int MMSYSERR_NODRIVER = 6;
            public const int MMSYSERR_INVALPARAM = 11;

            public const int JOY_ERR_NONE = 0;
            public const int JOY_ERR_PARMS = 165;
            public const int JOY_ERR_NOCANDO = 166;
            public const int JOY_ERR_UNPLUGGED = 167;

            public const int JOY_RETURN_X = 0x001;
            public const int JOY_RETURN_Y = 0x002;
            public const int JOY_RETURN_Z = 0x004;
            public const int JOY_RETURN_R = 0x008;
            public const int JOY_RETURN_U = 0x010;
            public const int JOY_RETURN_V = 0x020;
            public const int JOY_RETURN_POV = 0x040;
            public const int JOY_RETURN_BUTTONS = 0x080;
            public const int JOY_RETURN_ALL = 0x0FF;

            public const int JOY_RETURN_RAWDATA = 0x100;
            public const int JOY_RETURN_POVCTS = 0x200;
            public const int JOY_RETURN_CENTERED = 0x400;

            [StructLayout(LayoutKind.Sequential)]
            public struct JOYINFOEX
            {
                public int dwSize;
                public int dwFlags;
                public int dwXpos;
                public int dwYpos;
                public int dwZpos;
                public int dwRpos;
                public int dwUpos;
                public int dwVpos;
                public int dwButtons;
                public int dwButtonNumber;
                public int dwPOV;
                public int dwReserved1;
                public int dwReserved2;
            }

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

                [DllImport("winmm.dll")]
                public static extern int joySetCapture(IntPtr hwnd, int uJoyID, int uPeriod, int fChanged);

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
