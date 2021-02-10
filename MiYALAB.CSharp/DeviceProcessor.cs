using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace MiYALAB.CSharp.Device
{
	/// <summary>
	/// デバイスデータ
	/// </summary>
	public struct DeviceProperty
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
		/// ハードウェアID
		/// </summary>
		public string deviceId;
	}

	/// <summary>
	/// キャプチャデバイスクラス
	/// </summary>
	public class CaptureDeviceProcessor
	{
		/// <summary>
		/// キャプチャデバイスリスト取得
		/// </summary>
		/// <returns>キャプチャデバイスリスト</returns>
		public static DeviceProperty[] GetDeviceList()
		{
			DeviceProperty deviceWork;
			List<DeviceProperty> devices = new List<DeviceProperty>();

			foreach(ManagementObject device in new ManagementClass("Win32_PnPEntity").GetInstances().Cast<ManagementObject>())
			{
				deviceWork.pnpClass = (string)device.GetPropertyValue("PNPClass");
				if (deviceWork.pnpClass == "Image" || deviceWork.pnpClass == "Camera")
				{
					deviceWork.name = (string)device.GetPropertyValue("Name");
					deviceWork.deviceId = (string)device.GetPropertyValue("DeviceID");
					devices.Add(deviceWork);
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
		public static DeviceProperty[] GetDeviceList()
		{
			DeviceProperty deviceWork;
			List<DeviceProperty> devices = new List<DeviceProperty>();

			foreach (ManagementObject device in new ManagementClass("Win32_PnPEntity").GetInstances().Cast<ManagementObject>())
			{
				deviceWork.pnpClass = (string)device.GetPropertyValue("PNPClass");
				if (deviceWork.pnpClass == "Ports")
				{
					deviceWork.name = (string)device.GetPropertyValue("Name");
					deviceWork.deviceId = (string)device.GetPropertyValue("DeviceID");
					devices.Add(deviceWork);
				}
			}

			return devices.ToArray();
		}
	}
}
