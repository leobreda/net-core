using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Leobreda.Mock.Sql
{
	partial class Mock
	{
		public string description { get; set; }
		public List<string> commandText { get; set; }
		public string file { get; set; }
		public string type { get; set; }
		public string value { get; set; }


		public Mock()
		{
			this.description = string.Empty;
			this.commandText = new List<string>();
			this.file = string.Empty;
		}
	}
}
