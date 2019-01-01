using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Net.Leobreda.Database;
using MockSQL;
using System.Collections.Generic;

namespace UnitTest
{
	[TestClass]
	public class UnitTest
	{
		public static IConfigurationRoot Configuration { get; set; }


		public UnitTest()
		{
			/// Info
			/// 
			/// Activate Mock on Net.Leobreda.Mock.Sql:enabled in appsettings.json file
			/// 
			///		Net.Leobreda.Mock.Sql:enabled = true	Using Mock files
			///		Net.Leobreda.Mock.Sql:enabled = false	Using SQL
			///
			/// Net.Leobreda.Mock.Sql:folder contains Mock folder:
			///		ExecuteReader.json = Call ExecuteReader(). Return DataReader
			///		ExecuteScalar.json = Call ExecuteScalar(). Return @@identity
			///		ExecuteNonQuery.json = Call ExecuteNonQuery(). Return affected rows
		}

		[TestMethod]
		public void Insert()
		{
			People people = new People();
			people.fullname = "Leonardo Breda";
			people.datebirth = DateTime.Parse("1985-01-01");

			people.Insert();

			/// If you are using Sql, ExecuteScalar return AUTO_INCREMENT
			/// If you are using Mock, ExecuteScalar return 9999.

			Assert.AreEqual(9999, people.id);
		}


		[TestMethod]
		public void ListAllPeople()
		{
			People people = new People();

			List<People> list = people.ListAll();

			/// If you are using Sql, DataReader returns all rows on PEOPLE table.
			/// If you are using Mock, DataReader returns all rows on people.csv file.

			Assert.AreEqual(2, list.Count);

			// In SQL, Leonardo Breda was born in 1985-01-01
			// In people.csv, Leonardo Breda was born in 2018-02-03

			foreach (People _people in list)
			{
				if (_people.fullname.Equals("Leonardo Breda"))
				{
					Assert.AreEqual(2018, _people.datebirth.Year);
					Assert.AreEqual(02, _people.datebirth.Month);
					Assert.AreEqual(03, _people.datebirth.Day);
				}
			}

		}

		[TestMethod]
		public void Update()
		{
			People people = new People();

			people.ListAll();

			List<People> list = people.ListAll();

			int count = 0;

			foreach (People _people in list)
			{
				if(_people.fullname.Equals("Leonardo Breda"))
				{
					//force update datebirth
					_people.datebirth = DateTime.Now.AddDays(-700);

					// get rows updated
					count = _people.Update();
				}

			}

			/// If you are using Sql, ExecuteNonQuery return 1 row updated.
			/// If you are using Mock, ExecuteNonQuery return 10 rows updated.
			
			Assert.AreEqual(10, count);
		}

		[TestMethod]
		public void Delete()
		{

			People people = new People();

			people.ListAll();

			List<People> list = people.ListAll();

			int count = 0;

			foreach (People _people in list)
			{
				if (_people.fullname.Equals("Leonardo Breda"))
				{
					// get rows deleted
					count = _people.Delete();
				}

			}

			/// If you are using Sql, ExecuteNonQuery return 1 row deleted.
			/// If you are using Mock, ExecuteNonQuery return 3 rows deleted.
			
			Assert.AreEqual(3, count);

		}

	}
}
