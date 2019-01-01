using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace MockSQL
{
	public class People
	{
		public int id { get; set; }
		public string fullname { get; set; }
		public DateTime datebirth { get; set; }

		public People()
		{
			this.id = default(int);
			this.fullname = default(string);
			this.datebirth = DateTime.MinValue;
		}


		public void Insert()
		{
			/// If you are using Sql, ExecuteScalar return AUTO_INCREMENT
			/// If you are using Mock, ExecuteScalar return 9999.
			/// See ExecuteScalar.json file, to get return value.

			string sql = string.Format("INSERT INTO PEOPLE (FULLNAME, DATEBIRTH) " +
				"VALUES ('{0}','{1}');SELECT @@identity;",
				this.fullname,
				this.datebirth.ToString("yyyy-MM-dd"));

			this.id = Convert.ToInt32(Net.Leobreda.Database.MySql.ExecuteScalar(sql));

		}

		public List<People> ListAll()
		{
			List<People> list = new List<People>();
			People people = null;
			
			/// If you are using Sql, returns all rows in PEOPLE sql table
			/// If you are using Mock, return all rows n people.csv file
			/// To identify which csv file will be used, see as/is SQL statement on ExecuteReader.json file.

			DbDataReader dr = Net.Leobreda.Database.MySql
				.ExecuteReader("SELECT ID, FULLNAME, DATEBIRTH FROM PEOPLE ORDER BY ID");

			while (dr.Read())
			{
				people = new People();
				people.id = Convert.ToInt32(dr["ID"].ToString());
				people.fullname = dr["FULLNAME"].ToString();
				people.datebirth = Convert.ToDateTime(dr["DATEBIRTH"].ToString());

				list.Add(people);
			}

			return list;
		}

		public void Select()
		{
			/// If you are using Mock, person.csv will be converted to a DataReader.
			/// To identify which csv file will be used, see as/is SQL statement on ExecuteReader.json file.

			
			string sql = string.Format("SELECT ID, FULLNAME, DATEBIRTH FROM PEOPLE WHERE ID={0}",
				this.id);

			DbDataReader dr = Net.Leobreda.Database.MySql.ExecuteReader(sql);

			while (dr.Read())
			{
				
				this.id = Convert.ToInt32(dr["ID"].ToString());
				this.fullname = dr["FULLNAME"].ToString();
				this.datebirth = Convert.ToDateTime(dr["DATEBIRTH"].ToString());
			}
			
		}

		public int Update()
		{
			/// If you are using Sql, ExecuteNonQuery return 1 row updated.
			/// If you are using Mock, ExecuteNonQuery return 10 rows updated.
			/// See ExecuteNonQuery.json file, to get return value.

			string sql = string.Format("UPDATE PEOPLE SET FULLNAME='{0}', DATEBIRTH='{1}' " +
				"WHERE ID={2}", this.fullname, this.datebirth.ToString("yyyy-MM-dd"), this.id);

			return Net.Leobreda.Database.MySql.ExecuteNonQuery(sql);

		}

		public int Delete()
		{
			/// If you are using Sql, ExecuteNonQuery return 1 row deleted.
			/// If you are using Mock, ExecuteNonQuery return 3 rows deleted.
			/// See ExecuteNonQuery.json file, to get return value.
			/// 
			string sql = string.Format("DELETE FROM PEOPLE WHERE ID={0}",
				this.id);

			return Net.Leobreda.Database.MySql.ExecuteNonQuery(sql);

		}


	}
}
