using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
 public class MssqlLocatieContext : ILocatieContext
 {
  public void Insert(Locatie loc)
  {
   string name = loc.Name;
   string address = loc.Address;
   Organisatie org = loc.Org;


   string query = "INSERT INTO locatie (name, address, organisatie) VALUES (@name, @address, @organisatie)";
   SqlCommand cmd = new SqlCommand(query);

   cmd.Parameters.AddWithValue("@name", name);
   cmd.Parameters.AddWithValue("@address", address);
   cmd.Parameters.AddWithValue("@organisatie", org);
   Db.RunNonQuery(cmd);
  }

	 public List<Locatie> Select()
	 {
		 throw new NotImplementedException();
	 }

	 public void Delete(int id)
	 {
		 throw new NotImplementedException();
	 }
 }
}