using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Net.Leobreda.Mock
{
    public static class Verbose
    {

		private static string _folder { get; set; }
		private static bool _enabled { get; set; }

		private static uint _size { get; set; }
		private static uint _amount { get; set; }


		static Verbose()
		{
			string config = System.IO.Directory.GetCurrentDirectory();
#if DEBUG
			config = config.Replace(@"bin\Debug\netcoreapp2.0", "");
#endif

			var builder = new ConfigurationBuilder().SetBasePath(config)
			.AddJsonFile("appsettings.json");

			IConfigurationRoot _configurationRoot = builder.Build();

			_enabled = Convert.ToBoolean(_configurationRoot["Net.Leobreda.Mock.Verbose:enabled"].ToString());
			_folder = _configurationRoot["Net.Leobreda.Mock.Verbose:folder"].ToString();
			_size = Convert.ToUInt32(_configurationRoot["Net.Leobreda.Mock.Verbose:size"].ToString());
			_amount = Convert.ToUInt32(_configurationRoot["Net.Leobreda.Mock.Verbose:amount"].ToString());

		}



		public static void Write(string title, object input, object output = null)
		{
			if(!_enabled)
				return;

			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic.Add("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			dic.Add("title", title);
			dic.Add("input", input);
			dic.Add("output", output);

			string json = JsonConvert.SerializeObject(dic);

			using (StreamWriter w = System.IO.File.AppendText(File))
			{
				w.WriteLine(json);
				//w.Close();
			}
		}

		public static string File
		{
			get
			{

				string file = "verbose";

				if (System.IO.File.Exists(_folder + file + ".txt"))
					if ((new FileInfo(_folder + file + ".txt").Length / 1024) > (_size ))
						System.IO.File.Move(_folder + file + ".txt",
							_folder + file + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");

				return _folder + file + ".txt";
			}
		}
	}
}
