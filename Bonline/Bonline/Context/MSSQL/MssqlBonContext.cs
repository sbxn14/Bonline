using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
 public class MssqlBonContext : IBonContext
 {
  public void Insert(Bon bon)
  {
   try
   {
    using (SqlConnection conn = new SqlConnection(DB.ConnectionString))
    {
	string query = "INSERT INTO bon (Boodschappen, Datum, LocatieID, ) VALUES (@Boodschappen, @Datum, @LocatieID)";
	SqlCommand cmd = new SqlCommand(query);
	cmd.Parameters.AddWithValue("@Boodschappen", bon.Description);
	cmd.Parameters.AddWithValue("@Datum", bon.Date);
	cmd.Parameters.AddWithValue("@LocatieId", bon.Loc);
	DB.RunNonQuery(cmd);
    }
   }
   catch (Exception e)
   {
    Console.WriteLine(e);
    throw;
   }
  }

  public List<Bon> Select()
  {
   return DB.RunQuery(new Bon());
  }
 }
}