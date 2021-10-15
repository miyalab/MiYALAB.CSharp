/*
 * MIT License
 * 
 * Copyright (c) 2020-2021 MiYA LAB(K.Miyauchi)
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

using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MiYALAB.CSharp.File
{
	/// <summary>
	/// return values of file control function
	/// </summary>
	public enum FileState
	{
		/// <summary>
		/// OK
		/// </summary>
		OK = 0x0000,
		/// <summary>
		/// NOT EXIST
		/// </summary>
		NOT_EXIST = 0x0001,
		/// <summary>
		/// OPEN ERROR
		/// </summary>
		OPEN_ERROR = 0x0002,
		/// <summary>
		/// CLOSE ERROR
		/// </summary>
		CLOSE_ERROR = 0x0004,
		/// <summary>
		/// CREATE
		/// </summary>
		CREATE = 0x0008,
		/// <summary>
		/// NOT FOUND FILE
		/// </summary>
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
			return Save(fileName, param); ;
		}

		/// <summary>
		/// setting load
		/// </summary>
		public FileState Load()
		{
			if (fileName == "") return FileState.NONE_FILENAME;
			if (Load(fileName, ref __param) != FileState.OK)
			{
				Save(fileName, param);
				return FileState.CREATE;
			}
			return FileState.OK;
		}

		/// <summary>
		/// setting save
		/// </summary>
		public static FileState Save(string filename, objectType savedata)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(objectType));
			StreamWriter streamWriter = new StreamWriter(filename, false, new UTF8Encoding(false));
			serializer.Serialize(streamWriter, savedata);
			streamWriter.Close();
			return FileState.OK;
		}

		/// <summary>
		/// setting load
		/// </summary>
		public static FileState Load(string filename, ref objectType loaddata)
		{
			if (!System.IO.File.Exists(filename))
			{
				return FileState.NOT_EXIST;
			}
			XmlSerializer serializer = new XmlSerializer(typeof(objectType));
			StreamReader streamReader = new StreamReader(filename, new UTF8Encoding(false));
			loaddata = (objectType)serializer.Deserialize(streamReader);
			streamReader.Close();
			return FileState.OK;
		}
	}
}
