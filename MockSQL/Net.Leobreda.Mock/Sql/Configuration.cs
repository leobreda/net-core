using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Extensions.Configuration;

namespace Net.Leobreda.Mock.Sql
{
	public static class Configuration
	{
		private static string _folder { get; set; }
		private static bool _enabled { get; set; }
		
		static Configuration()
		{
			string config = System.IO.Directory.GetCurrentDirectory();
#if DEBUG
			config = config.Replace(@"bin\Debug\netcoreapp2.0", "");
#endif

			var builder = new ConfigurationBuilder().SetBasePath(config)
			.AddJsonFile("appsettings.json");

			IConfigurationRoot _configurationRoot = builder.Build();

			_enabled = Convert.ToBoolean(_configurationRoot["Net.Leobreda.Mock.Sql:enabled"].ToString());
			_folder = _configurationRoot["Net.Leobreda.Mock.Sql:folder"].ToString();

		}

		public static string Folder { get { return _folder; } }

		public static bool Enabled { get { return _enabled; } }

	}
}
