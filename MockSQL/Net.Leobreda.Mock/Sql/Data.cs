using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Data.Common;
using System.Data;
using Newtonsoft.Json;

namespace Net.Leobreda.Mock.Sql
{
	public static class Data
	{
		public static DataTable ExecuteReader(string commandText)
		{
			DataTable dt;

			string file = string.Empty;


			string json = File.ReadAllText(Configuration.Folder + "ExecuteReader.json");

			List<Mock> list = JsonConvert.DeserializeObject<List<Mock>>(json);

			foreach (Mock mock in list)
			{
				if (!string.IsNullOrEmpty(file))
					break;

				foreach (string _command in mock.commandText)
				{
					if (!string.IsNullOrEmpty(file))
						break;

					if (commandText.ToLower().Trim().Equals(_command.ToLower().Trim()))
					{
						file = mock.file;
						break;
					}
					else
					{
						if (!_command.IndexOf('?').Equals(-1))
						{
							bool find = true;
							string[] _arrCommand = _command.Split('?');
							foreach (string position in _arrCommand)
							{
								if (commandText.Trim().IndexOf(position.Trim()) == -1)
								{
									find = false;
								}
							}

							if (find)
							{
								file = mock.file;
								break;
							}
						}

					}


				}


			}


			FileStream fs = File.Open(Configuration.Folder + file, FileMode.Open, FileAccess.Read);

			Verbose.Write("Net.Leobreda.Mock.Sql.Data.GetDataTable()", "File", Configuration.Folder + file);

			string line = null;


			List<KeyValuePair<string, string>> columns = new List<KeyValuePair<string, string>>();
			List<string> type = new List<string>();

			Regex CSVParser = new Regex(";(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

			using (var sr = new StreamReader(fs))
			{
				line = sr.ReadLine();

				string[] fields = CSVParser.Split(line);

				line = sr.ReadLine();
				string[] types = CSVParser.Split(line);


				dt = new DataTable();


				for (int i = 0; i < fields.Length; i++)
					Util.AddColumn(dt, fields[i], types[i]);


				int column = 0;

				while ((line = sr.ReadLine()) != null)
				{
					fields = CSVParser.Split(line);

					column = 0;
					DataRow dr = dt.NewRow();
					foreach (string field in fields)
					{

						dr[column] = Util.GetObject(types[column], field);

						column++;
					}
					dt.Rows.Add(dr);
				}
			}

			Verbose.Write("Net.Leobreda.Mock.Sql.Data.GetDataTable()", commandText, dt);


			return dt;
		}

		public static object ExecuteScalar(string commandText)
		{
			string file = string.Empty;

			object _return = null;

			string json = File.ReadAllText(Configuration.Folder + "ExecuteScalar.json");

			List<Mock> list = JsonConvert.DeserializeObject<List<Mock>>(json);

			foreach (Mock mock in list)
			{
				foreach (string _command in mock.commandText)
				{
					if (commandText.ToLower().Trim().Equals(_command.ToLower().Trim()))
					{
						_return = Util.GetObject(mock.type, mock.value);
					}
					else
					{
						if (!_command.IndexOf('?').Equals(-1))
						{
							bool find = true;
							string[] _arrCommand = _command.Split('?');
							foreach (string position in _arrCommand)
							{
								if (commandText.Trim().IndexOf(position.Trim()) == -1)
								{
									find = false;
								}
							}

							if (find)
							{
								_return = Util.GetObject(mock.type, mock.value);
							}
						}
					}
				}
			}

			Verbose.Write("Net.Leobreda.Mock.Sql.Data.ExecuteScalar()", commandText, _return);

			return _return;
		}

		public static int ExecuteNonQuery(string commandText)
		{
			string file = string.Empty;

			int _return = 0;

			string json = File.ReadAllText(Configuration.Folder + "ExecuteNonQuery.json");

			List<Mock> list = JsonConvert.DeserializeObject<List<Mock>>(json);

			foreach (Mock mock in list)
			{
				foreach (string _command in mock.commandText)
				{
					if (commandText.ToLower().Trim().Equals(_command.ToLower().Trim()))
					{
						_return = (int)Util.GetObject("int", mock.value);
					}
					else
					{
						if (!_command.IndexOf('?').Equals(-1))
						{
							bool find = true;
							string[] _arrCommand = _command.Split('?');
							foreach (string position in _arrCommand)
							{
								if (commandText.Trim().IndexOf(position.Trim()) == -1)
								{
									find = false;
								}
							}

							if (find)
							{
								_return = (int)Util.GetObject("int", mock.value);
							}
						}
					}
				}
			}

			Verbose.Write("Net.Leobreda.Mock.Sql.Data.ExecuteNonQuery()", commandText, _return);

			return _return;
		}

	}



}
