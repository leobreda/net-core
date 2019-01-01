using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MockSQL
{
	public class Program
	{
		public static IConfigurationRoot Configuration { get; set; }

		static string print = "id: {0}\nfullname: {1}\ndatebirth: {2}";


		static void Main(string[] args)
		{

			Console.WriteLine("Open README.md file BEFORE running this program, and comment this line!");


			Program program = new Program();

			if(Net.Leobreda.Mock.Sql.Configuration.Enabled)
				Console.WriteLine("This Program is using MOCK files");
			else
				Console.WriteLine("This Program is using MySQL database");
			

		
			List<People> list = new List<People>();
			People people = null;

			int id = 0;
			Console.WriteLine("====================\nInsert()");
			id = program.Insert();

			Console.WriteLine("====================\nSelect()");
			program.Select(id);

			Console.WriteLine("====================\nSelectAll()");
			program.SelectAll();

			Console.WriteLine("====================\nUpdate()");
			program.Update(id);

			Console.WriteLine("====================\nDelete()");
			program.Delete(id);

			Console.WriteLine("Press ENTER to exit program");
			Console.Read();
		}


		public int Insert()
		{
			People people = new People();
			people.fullname = "Leonardo Breda";
			people.datebirth = DateTime.Parse("1985-01-01");

			people.Insert();

			string console = string.Format(print,
				people.id, people.fullname, people.datebirth.ToString("dd/MM/yyyy"));

			Console.WriteLine(console);

			Task.Delay(1000).Wait();

			return people.id;

		}

		public void Select(int id)
		{
			People people = new People();
			people.id = id;

			people.Select();

			string console = string.Format(print,
				people.id, people.fullname, people.datebirth.ToString("dd/MM/yyyy"));

			Console.WriteLine(console);
			Task.Delay(1000).Wait();
		}

		public void SelectAll()
		{
			People people = new People();

			List<People> list = people.ListAll();


			string console = null;

			foreach (People _people in list)
			{

				console = string.Format(print,
				_people.id, _people.fullname, _people.datebirth.ToString("dd/MM/yyyy"));

				Console.WriteLine(console);
			}
			Task.Delay(1000).Wait();
		}

		public void Update(int id)
		{
			People people = new People();
			people.id = id;
			people.Select();

			string console = string.Format(print,
				people.id, people.fullname, people.datebirth.ToString("dd/MM/yyyy"));

			Console.WriteLine("Before:");
			Console.WriteLine(console);


			people.datebirth = DateTime.Now.AddDays(-300);
			int count = people.Update();

			console = string.Format(print,
				people.id, people.fullname, people.datebirth.ToString("dd/MM/yyyy"));

			Console.WriteLine("After:");
			Console.WriteLine(console);


			Console.WriteLine("Registers updated: " + count);
			Task.Delay(1000).Wait();
		}

		
		public void Delete(int id)
		{

			People people = new People();
			people.id = id;


			/// Using Sql. Delete() return 1 row deleted.
			int count = people.Delete();

			Console.WriteLine("Registers deleted: " + count);
			Task.Delay(1000).Wait();
		}

	}

}

