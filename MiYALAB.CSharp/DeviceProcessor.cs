using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiYALAB.CSharp.Device
{
    /// <summary>
    /// デバイス情報データ
    /// </summary>
    public struct DeviceInfo
    {
		/// <summary>
		/// ハードウェア名
		/// </summary>
        public string name;
		/// <summary>
		/// pnpクラス
		/// </summary>
        public string pnpClass;
        /// <summary>
        /// デバイスID
        /// </summary>
        public string deviceId;
    }

    /// <summary>
    /// キャプチャデバイスクラス
    /// </summary>
    public class CaptureDeviceProcessor
    {
        /// <summary>
        /// キャプチャデバイスリストの取得
        /// </summary>
        /// <returns>キャプチャデバイスリスト</returns>
        public static DeviceInfo[] GetDeviceList()
        {
            DeviceInfo work;
            List<DeviceInfo> devices = new List<DeviceInfo>();

            foreach(ManagementBaseObject pnp in new ManagementClass("Win32_PnPEntity").GetInstances())
            {
                work.pnpClass = (string)pnp.GetPropertyValue("PNPClass");
                if (work.pnpClass == "Camera" || work.pnpClass == "Image")
                {
                    work.name = (string)pnp.GetPropertyValue("name");
                    work.deviceId = (string)pnp.GetPropertyValue("DeviceID");

                    devices.Add(work);
                }
            }

            return devices.ToArray();
        }
    }

    /// <summary>
    /// シリアル通信デバイスクラス
    /// </summary>
    public class SerialDeviceProcessor
    {
        /// <summary>
        /// シリアル通信デバイスリスト取得
        /// </summary>
        /// <returns>シリアル通信デバイスリスト</returns>
        public static DeviceInfo[] GetDeviceList()
        {
            DeviceInfo work;
            List<DeviceInfo> devices = new List<DeviceInfo>();

            foreach (ManagementBaseObject pnp in new ManagementClass("Win32_PnPEntity").GetInstances())
            {
                work.pnpClass = (string)pnp.GetPropertyValue("PNPClass");
                if (work.pnpClass == "Ports")
                {
                    work.name = (string)pnp.GetPropertyValue("name");
                    work.deviceId = (string)pnp.GetPropertyValue("DeviceID");

                    devices.Add(work);
                }
            }

            return devices.ToArray();
        }
    }
}
