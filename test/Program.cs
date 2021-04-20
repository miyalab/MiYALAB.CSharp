using System;
using System.IO;
using System.Management;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_USBControllerDevice");

                foreach (var queryObj in searcher.Get())
                {
                    sb.AppendLine("-----------------------------------");
                    sb.AppendLine("Win32_USBController instance : " + queryObj["Dependent"].ToString().Split('\"')[1]);
                    sb.AppendLine("-----------------------------------");

                    var obj = new ManagementObject(queryObj["Dependent"].ToString());
                    foreach (var propertie in obj.Properties)
                    {
                        if (propertie.Value != null)
                        {
                            if (propertie.Value is string[])
                            {
                                string[] strings = (string[])propertie.Value;
                                sb.AppendLine(propertie.Name + ":");

                                foreach (string str in strings)
                                {
                                    sb.AppendLine(str);
                                }
                            }
                            else
                            {
                                sb.AppendLine(propertie.Name + "=" + propertie.Value);
                            }
                        }
                    }

                }

            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            File.WriteAllText("result.txt", sb.ToString());

            Console.WriteLine("Done.");

            Console.Read();
        }
    }
}