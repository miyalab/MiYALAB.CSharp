using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MiYALAB.CSharp.Monitor;
using MiYALAB.CSharp.Device;

namespace LibCheck
{
	class gmm : GraphicMonitor
	{
		protected override void pictureBox_Click(object sender, EventArgs e)
		{
			
		}
	}

	public partial class FormMain : Form
	{
		GraphicMonitor gm = new GraphicMonitor(0, 0);
		gmm gmmm = new gmm();
		public FormMain()
		{
			InitializeComponent();
			gmmm.Text = "gmm";
			gmmm.Show();
		}

		private void buttonCheck_Click(object sender, EventArgs e)
		{
			foreach(var device in CaptureDeviceProcessor.GetDeviceList())
            {
				textBoxLog.AppendText(device.name + Environment.NewLine);
				textBoxLog.AppendText(device.pnpClass + Environment.NewLine);
				textBoxLog.AppendText(device.deviceId + Environment.NewLine);
			}

			foreach(var device in SerialDeviceProcessor.GetDeviceList())
            {
				textBoxLog.AppendText(device.name + Environment.NewLine);
				textBoxLog.AppendText(device.pnpClass + Environment.NewLine);
				textBoxLog.AppendText(device.deviceId + Environment.NewLine);
			}
		}
	}
}
