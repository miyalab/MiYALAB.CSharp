﻿//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.IO.Ports;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace MiYALAB.CSharp.Device
//{
//    public class WinSerialPort : IDisposable
//    {
//        #region Win32 API宣言 

//        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
//        private static extern IntPtr CreateFile(
//         string lpFileName,                          // ファイル名 
//         DesiredAccess dwDesiredAccess,              // アクセスモード 
//         ShareMode dwShareMode,                      // 共有モード 
//         int lpSecurityAttributes,                   // セキュリティ記述子 
//         CreationDisposition dwCreationDisposition,  // 作成方法 
//         FlagsAndAttributes dwFlagsAndAttributes,    // ファイル属性 
//         IntPtr hTemplateFile                        // テンプレートファイルのハンドル 
//         );

//        [DllImport("kernel32.dll", SetLastError = true)]
//        public static extern bool CloseHandle(IntPtr hObject);

//        [DllImport("kernel32.dll")]
//        static extern bool GetCommState(IntPtr hFile, ref DCB lpDCB);

//        [DllImport("kernel32.dll")]
//        static extern bool SetCommState(IntPtr hFile, ref DCB lpDCB);

//        [DllImport("kernel32.dll", SetLastError = true)]
//        //static extern bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);
//        static extern bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, int lpOverlapped);

//        [DllImport("kernel32.dll")]
//        //static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, [In] ref NativeOverlapped lpOverlapped);
//        static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, int lpOverlapped);

//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern bool GetCommTimeouts(IntPtr hFile, ref COMMTIMEOUTS lpCommTimeouts);

//        [DllImport("kernel32.dll", SetLastError = true)]
//        static extern bool SetCommTimeouts(IntPtr hFile, [In] ref COMMTIMEOUTS lpCommTimeouts);

//        [DllImport("kernel32.dll")]
//        static extern bool PurgeComm(IntPtr hFile, Purge dwFlags);
//        #endregion
//        #region 列挙体 

//        private enum DesiredAccess : uint
//        {
//            GENERIC_READ = 0x80000000,
//            GENERIC_WRITE = 0x40000000,
//            GENERIC_EXECUTE = 0x20000000
//        }

//        private enum ShareMode : uint
//        {
//            FILE_SHARE_READ = 0x00000001,
//            FILE_SHARE_WRITE = 0x00000002,
//            FILE_SHARE_DELETE = 0x00000004
//        }

//        private enum CreationDisposition : uint
//        {
//            CREATE_NEW = 1,
//            CREATE_ALWAYS = 2,
//            OPEN_EXISTING = 3,
//            OPEN_ALWAYS = 4,
//            TRUNCATE_EXISTING = 5
//        }

//        private enum FlagsAndAttributes : uint
//        {
//            FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
//            FILE_ATTRIBUTE_ENCRYPTED = 0x00004000,
//            FILE_ATTRIBUTE_HIDDEN = 0x00000002,
//            FILE_ATTRIBUTE_NORMAL = 0x00000080,
//            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
//            FILE_ATTRIBUTE_OFFLINE = 0x00001000,
//            FILE_ATTRIBUTE_READONLY = 0x00000001,
//            FILE_ATTRIBUTE_SYSTEM = 0x00000004,
//            FILE_ATTRIBUTE_TEMPORARY = 0x00000100
//        }

//        public enum DtrControl : int
//        {
//            /// <summary>
//            /// Disables the DTR line when the device is opened and leaves it disabled.
//            /// </summary>
//            Disable = 0,

//            /// <summary>
//            /// Enables the DTR line when the device is opened and leaves it on.
//            /// </summary>
//            Enable = 1,

//            /// <summary>
//            /// Enables DTR handshaking. If handshaking is enabled, it is an error for the application to adjust the line by 
//            /// using the EscapeCommFunction function.
//            /// </summary>
//            Handshake = 2
//        }

//        public enum RtsControl : int
//        {
//            /// <summary>
//            /// Disables the RTS line when the device is opened and leaves it disabled.
//            /// </summary>
//            Disable = 0,

//            /// <summary>
//            /// Enables the RTS line when the device is opened and leaves it on.
//            /// </summary>
//            Enable = 1,

//            /// <summary>
//            /// Enables RTS handshaking. The driver raises the RTS line when the "type-ahead" (input) buffer 
//            /// is less than one-half full and lowers the RTS line when the buffer is more than 
//            /// three-quarters full. If handshaking is enabled, it is an error for the application to 
//            /// adjust the line by using the EscapeCommFunction function.
//            /// </summary>
//            Handshake = 2,

//            /// <summary>
//            /// Specifies that the RTS line will be high if bytes are available for transmission. After 
//            /// all buffered bytes have been sent, the RTS line will be low.
//            /// </summary>
//            Toggle = 3
//        }

//        public enum Purge : uint
//        {
//            TxAbort = 1,
//            ExAbort = 2,
//            TxClear = 4,
//            RxClear = 8,
//        }
//        #endregion
//        #region "構造体"
//        struct COMMTIMEOUTS
//        {
//            public UInt32 ReadIntervalTimeout;
//            public UInt32 ReadTotalTimeoutMultiplier;
//            public UInt32 ReadTotalTimeoutConstant;
//            public UInt32 WriteTotalTimeoutMultiplier;
//            public UInt32 WriteTotalTimeoutConstant;
//        }
//        #endregion
//        #region "DCB構造体"
//        [StructLayout(LayoutKind.Sequential)]
//        internal struct DCB
//        {
//            internal uint DCBLength;
//            internal uint BaudRate;
//            private BitVector32 Flags;

//            private ushort wReserved;        // not currently used 
//            internal ushort XonLim;           // transmit XON threshold 
//            internal ushort XoffLim;          // transmit XOFF threshold             

//            internal byte ByteSize;
//            internal Parity Parity;
//            internal StopBits StopBits;

//            internal sbyte XonChar;          // Tx and Rx XON character 
//            internal sbyte XoffChar;         // Tx and Rx XOFF character 
//            internal sbyte ErrorChar;        // error replacement character 
//            internal sbyte EofChar;          // end of input character 
//            internal sbyte EvtChar;          // received event character 
//            private ushort wReserved1;       // reserved; do not use     

//            private static readonly int fBinary;
//            private static readonly int fParity;
//            private static readonly int fOutxCtsFlow;
//            private static readonly int fOutxDsrFlow;
//            private static readonly BitVector32.Section fDtrControl;
//            private static readonly int fDsrSensitivity;
//            private static readonly int fTXContinueOnXoff;
//            private static readonly int fOutX;
//            private static readonly int fInX;
//            private static readonly int fErrorChar;
//            private static readonly int fNull;
//            private static readonly BitVector32.Section fRtsControl;
//            private static readonly int fAbortOnError;

//            static DCB()
//            {
//                // Create Boolean Mask
//                int previousMask;
//                fBinary = BitVector32.CreateMask();
//                fParity = BitVector32.CreateMask(fBinary);
//                fOutxCtsFlow = BitVector32.CreateMask(fParity);
//                fOutxDsrFlow = BitVector32.CreateMask(fOutxCtsFlow);
//                previousMask = BitVector32.CreateMask(fOutxDsrFlow);
//                previousMask = BitVector32.CreateMask(previousMask);
//                fDsrSensitivity = BitVector32.CreateMask(previousMask);
//                fTXContinueOnXoff = BitVector32.CreateMask(fDsrSensitivity);
//                fOutX = BitVector32.CreateMask(fTXContinueOnXoff);
//                fInX = BitVector32.CreateMask(fOutX);
//                fErrorChar = BitVector32.CreateMask(fInX);
//                fNull = BitVector32.CreateMask(fErrorChar);
//                previousMask = BitVector32.CreateMask(fNull);
//                previousMask = BitVector32.CreateMask(previousMask);
//                fAbortOnError = BitVector32.CreateMask(previousMask);

//                // Create section Mask
//                BitVector32.Section previousSection;
//                previousSection = BitVector32.CreateSection(1);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                fDtrControl = BitVector32.CreateSection(2, previousSection);
//                previousSection = BitVector32.CreateSection(1, fDtrControl);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                previousSection = BitVector32.CreateSection(1, previousSection);
//                fRtsControl = BitVector32.CreateSection(3, previousSection);
//                previousSection = BitVector32.CreateSection(1, fRtsControl);
//            }

//            public bool Binary
//            {
//                get { return Flags[fBinary]; }
//                set { Flags[fBinary] = value; }
//            }

//            public bool CheckParity
//            {
//                get { return Flags[fParity]; }
//                set { Flags[fParity] = value; }
//            }

//            public bool OutxCtsFlow
//            {
//                get { return Flags[fOutxCtsFlow]; }
//                set { Flags[fOutxCtsFlow] = value; }
//            }

//            public bool OutxDsrFlow
//            {
//                get { return Flags[fOutxDsrFlow]; }
//                set { Flags[fOutxDsrFlow] = value; }
//            }

//            public DtrControl DtrControl
//            {
//                get { return (DtrControl)Flags[fDtrControl]; }
//                set { Flags[fDtrControl] = (int)value; }
//            }

//            public bool DsrSensitivity
//            {
//                get { return Flags[fDsrSensitivity]; }
//                set { Flags[fDsrSensitivity] = value; }
//            }

//            public bool TxContinueOnXoff
//            {
//                get { return Flags[fTXContinueOnXoff]; }
//                set { Flags[fTXContinueOnXoff] = value; }
//            }

//            public bool OutX
//            {
//                get { return Flags[fOutX]; }
//                set { Flags[fOutX] = value; }
//            }

//            public bool InX
//            {
//                get { return Flags[fInX]; }
//                set { Flags[fInX] = value; }
//            }

//            public bool ReplaceErrorChar
//            {
//                get { return Flags[fErrorChar]; }
//                set { Flags[fErrorChar] = value; }
//            }

//            public bool Null
//            {
//                get { return Flags[fNull]; }
//                set { Flags[fNull] = value; }
//            }

//            public RtsControl RtsControl
//            {
//                get { return (RtsControl)Flags[fRtsControl]; }
//                set { Flags[fRtsControl] = (int)value; }
//            }

//            public bool AbortOnError
//            {
//                get { return Flags[fAbortOnError]; }
//                set { Flags[fAbortOnError] = value; }
//            }
//        }
//        #endregion

//        IntPtr port { get; set; }
//        string portName { get; }
//        uint baudRate { get; }
//        Parity parity { get; }
//        byte dataBits { get; }
//        StopBits stopBits { get; }
//        byte[] recvBuffer { get; } = new byte[1];

//        public WinSerialPort(string portName, uint baudRate, Parity parity, byte dataBits, StopBits stopBits)
//        {
//            this.portName = portName;
//            this.baudRate = baudRate;
//            this.parity = parity;
//            this.dataBits = dataBits;
//            this.stopBits = stopBits;
//        }

//        public bool Open()
//        {
//            port = CreateFile(portName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, 0, 0, CreationDisposition.OPEN_EXISTING, FlagsAndAttributes.FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);

//            //ポートオープン
//            if (0 >= port.ToInt32())
//                return false;

//            //通信設定
//            var dcb = new DCB();
//            if (!GetCommState(port, ref dcb))
//                return false;
//            dcb.RtsControl = RtsControl.Enable;
//            dcb.DtrControl = DtrControl.Enable;
//            dcb.BaudRate = 250 * 1000;
//            dcb.ByteSize = 8;
//            dcb.Parity = parity;
//            dcb.StopBits = stopBits;
//            dcb.Binary = true;
//            dcb.CheckParity = true;
//            if (!SetCommState(port, ref dcb))
//                return false;

//            //タイムアウト設定
//            var Commtimeouts = new COMMTIMEOUTS();
//            if (!GetCommTimeouts(port, ref Commtimeouts))
//                return false;

//            Commtimeouts.ReadIntervalTimeout = 100;

//            if (!SetCommTimeouts(port, ref Commtimeouts))
//                return false;

//            return true;
//        }

//        public void Close()
//        {
//            CloseHandle(port);
//            port = IntPtr.Zero;
//        }

//        public void Write(byte[] buffer, uint offset, uint size)
//        {
//            uint writen = 0;
//            WriteFile(port, buffer, (uint)size, out writen, 0);
//        }

//        public int ReadByte()
//        {
//            uint recved = 0;
//            if (ReadFile(port, recvBuffer, 1, out recved, 0))
//            {
//                if (0 < recved)
//                {
//                    return recvBuffer[0];
//                }
//            }
//            return -1;
//        }

//        public void DiscardInBuffer()
//        {
//            PurgeComm(port, Purge.RxClear);
//        }

//        public void DiscardOutBuffer()
//        {
//            PurgeComm(port, Purge.TxClear);
//        }

//        public void Dispose()
//        {
//            if (port != IntPtr.Zero)
//            {
//                Close();
//            }
//        }
//    }
//}
