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
        public string hardwareId;
    }

    /// <summary>
    /// キャプチャデバイスデータ取得
    /// </summary>
    public class CaptureDeviceProcessor
    {
        public static List<DeviceInfo> GetCaptureDeviceList()
        {
            DeviceInfo work;
            List<DeviceInfo> devices = new List<DeviceInfo>();
            ManagementClass pnpEntity = new ManagementClass("Win32_PnPEntity");

            foreach(var pnp in pnpEntity.GetInstances())
            {
                if ((string)pnp.GetPropertyValue("PNPClass") == "Camera")
                {
                    work.name = pnp.GetPropertyValue("name").ToString();
                    work.pnpClass = "Camera";
                    work.hardwareId = pnp.GetPropertyValue("HardwareID").ToString();

                    devices.Add(work);
                }
            }

            return devices;
        }
    }

    /// <summary>
    /// シリアル通信デバイスデータ取得
    /// </summary>
    public class SerialDeviceProcessor
    {
        public static IEnumerable<string> GetSerialDeviceList()
        {
            ManagementClass pnpEntity = new ManagementClass("Win32_PnPEntity");
            Regex comRegex = new Regex(@"\(COM[1-9][0-9]?[0-9]?\)");               // COMとつくものを探索

            return pnpEntity
                .GetInstances()                                                     // 一覧を取得
                .Cast<ManagementObject>()
                .Select(managementObj => managementObj.GetPropertyValue("Name"))    // デバイス名取得
                .Where(nameObj => nameObj != null)                                  // nullのものは除外
                .Select(nameObj => nameObj.ToString())                              // 文字列に変換
                .Where(name => comRegex.IsMatch(name));                             // 正規表現でフィルタリング
        }
    }
}
