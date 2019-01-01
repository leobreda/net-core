using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Net.Leobreda.Database
{
	public static class Configuration
	{
		private static string _connectionStringMySql { get; set; }
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

			_connectionStringMySql = _configurationRoot["ConnectionStrings:Mysql"].ToString();
		

		}
	
		public static string ConnectionStringMySql { get { return _connectionStringMySql; } }

	}
}
