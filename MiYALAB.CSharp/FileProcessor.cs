using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MiYALAB.CSharp.FileProcessor
{
	/// <summary>
	/// return values of file control function
	/// </summary>
	public enum FileState
	{
		OK = 0x0000,
		NOT_EXIST = 0x0001,
		OPEN_ERROR = 0x0002,
		CLOSE_ERROR = 0x0004,
		CREATE = 0x0008,
		NONE_FILENAME = 0x0010
	}

	/// <summary>
	/// class for setting file processing
	/// </summary>
	/// <typeparam name="objectType"></typeparam>
	public class SettingProcessor<objectType>
	{
		/// <summary>
		/// Constructor of setting processor class
		/// </summary>
		/// <param name="filename"></param>
		public SettingProcessor(string filename)
		{
			fileName = filename;
		}

		/// <summary>setting parameter</summary>
		public objectType __param;
		/// <summary>setting parameter</summary>
		public objectType param
		{
			set { __param = value; }
			get { return __param; }
		}

		/// <summary>setting filename</summary>
		private string fileName;
		/// <summary>setting filename</summary>
		public string FileName { get; set; }

		/// <summary>
		/// setting save
		/// </summary>
		public FileState Save()
		{
			if (fileName == "") return FileState.NONE_FILENAME;
			return Save<objectType>(fileName, param); ;
		}

		/// <summary>
		/// setting load
		/// </summary>
		public FileState Load()
		{
			if (fileName == "") return FileState.NONE_FILENAME;
			if (Load<objectType>(fileName, ref __param) != FileState.OK)
			{
				Save<objectType>(fileName, param);
				return FileState.CREATE;
			}
			return FileState.OK;
		}

		/// <summary>
		/// setting save
		/// </summary>
		public static FileState Save<type>(string filename, type savedata)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(type));
			StreamWriter streamWriter = new StreamWriter(filename, false, new UTF8Encoding(false));
			serializer.Serialize(streamWriter, savedata);
			streamWriter.Close();
			return FileState.OK;
		}

		/// <summary>
		/// setting load
		/// </summary>
		public static FileState Load<type>(string filename, ref type loaddata)
		{
			if (!File.Exists(filename))
			{
				return FileState.NOT_EXIST;
			}
			XmlSerializer serializer = new XmlSerializer(typeof(type));
			StreamReader streamReader = new StreamReader(filename, new UTF8Encoding(false));
			loaddata = (type)serializer.Deserialize(streamReader);
			streamReader.Close();
			return FileState.OK;
		}
	}
}
