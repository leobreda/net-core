using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Net.Leobreda.Mock.Sql
{
    partial class Util
    {
		public static object GetObject(string dataType, string value)
		{
			switch (dataType)
			{
				case "string":
				default:
					return value;
					break;

				case "int":
					return Convert.ToInt32(value);
					break;

				case "bigint":
				case "long":
					return Convert.ToInt64(value);
					break;

				case "double":
					return Convert.ToDouble(value);
					break;

				case "decimal":
					return Convert.ToDecimal(value.Replace(".", ","));
					break;

				case "date":
				case "datetime":
					return Convert.ToDateTime(value);
					break;
			}

		}

		public static void AddColumn(DataTable dataTable, string columnName, string typeName)
		{
			switch (typeName)
			{
				case "string":
				default:
					dataTable.Columns.Add(columnName, typeof(string));
					break;

				case "int":
					dataTable.Columns.Add(columnName, typeof(int));
					break;

				case "bigint":
				case "long":
					dataTable.Columns.Add(columnName, typeof(long));
					break;

				case "double":
					dataTable.Columns.Add(columnName, typeof(double));
					break;

				case "decimal":
					dataTable.Columns.Add(columnName, typeof(decimal));
					break;

				case "date":
				case "datetime":
					dataTable.Columns.Add(columnName, typeof(DateTime));
					break;
			}


		}
	}
}
