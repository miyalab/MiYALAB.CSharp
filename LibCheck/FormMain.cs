using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MiYALAB.CSharp.Device;

namespace LibCheck
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void buttonCheck_Click(object sender, EventArgs e)
		{
			foreach(Device device in SerialDeviceProcessor.GetDeviceList())
			{
				textBoxLog.AppendText(device.name + Environment.NewLine);
				textBoxLog.AppendText(device.pnpClass + Environment.NewLine);
				textBoxLog.AppendText(device.deviceId + Environment.NewLine);
				textBoxLog.AppendText(Environment.NewLine);

			}
		}
	}
}
