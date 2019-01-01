using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Net.Leobreda.Mock.Sql;
using Net.Leobreda.Mock;
using Microsoft.Extensions.Configuration;

namespace Net.Leobreda.Database
{
	public class MySql
	{
		private static string connectionString { get { return Configuration.ConnectionStringMySql; } }
		private static MySqlConnection _connection { get; set; }

		static MySql() { }

		public static void StartConnection()
		{
			if (_connection == null)
				_connection = new MySqlConnection(connectionString);

			if (_connection.State.Equals(ConnectionState.Closed))
				_connection.Open();
		}

		public static void CloseConnection()
		{
			if (_connection == null)
				return;

			if (_connection.State.Equals(ConnectionState.Open))
				_connection.Close();
		}


		public static DbDataReader ExecuteReader(string strSQL)
		{
			if (Net.Leobreda.Mock.Sql.Configuration.Enabled)
			{
				DataTable dt = Data.ExecuteReader(strSQL);
				return dt.CreateDataReader();
			}

			DataTable dataTable = null;
			using (DbConnection cn = new MySqlConnection(connectionString))
			{
				DbDataAdapter sqlDataAdapter = new MySqlDataAdapter(strSQL, (MySqlConnection)cn);
				dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
			}
			Verbose.Write("Sql.getDataReader()", strSQL, dataTable);


			return dataTable.CreateDataReader();

		}


		public static int ExecuteNonQuery(string strSQL, bool connected = false)
		{
			if (Net.Leobreda.Mock.Sql.Configuration.Enabled)
			{
				return Data.ExecuteNonQuery(strSQL);
			}

			int _return = 0;

			if (!connected)
				MySql.StartConnection();

			using (MySqlCommand cmd = new MySqlCommand(strSQL))
			{
				cmd.Connection = _connection;
				_return = cmd.ExecuteNonQuery();
			}

			if (!connected)
				MySql.CloseConnection();

			Verbose.Write("Sql.ExecuteNonQuery(string)", strSQL);

			return _return;
		}

		public static object ExecuteScalar(string strSQL, bool connected = false)
		{
			if (Net.Leobreda.Mock.Sql.Configuration.Enabled)
			{
				return Data.ExecuteScalar(strSQL);
			}

			if (!connected)
				MySql.StartConnection();


			object obj = null;
			using (MySqlCommand cmd = new MySqlCommand(strSQL))
			{
				cmd.Connection = _connection;

				obj = (object)cmd.ExecuteScalar();
			}

			if (!connected)
				MySql.CloseConnection();

			Verbose.Write("Sql.ExecuteScalar()", obj);

			return obj;
		}
	}
}
