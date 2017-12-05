using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
	public class MssqlOrganisatieContext : IOrganisatieContext
	{
		public void Insert(Organisatie org)
		{
			string name = org.Name;

			string query = "INSERT INTO organisatie (name) VALUES (@name)";
			SqlCommand cmd = new SqlCommand(query);

			cmd.Parameters.AddWithValue("@name", name);
			Db.RunNonQuery(cmd);
		}


		public List<Organisatie> Select()
		{
			throw new NotImplementedException();
		}

		public void Update(int id, string NieuwNaam)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}


	}
}