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
   using (SqlConnection conn = new SqlConnection(DB.ConnectionString))
   {
    conn.Open();
    string query = "INSERT INTO bon (Boodschappen, Datum, LocatieID, ) VALUES (@Boodschappen, @Datum, @LocatieID)";
    SqlCommand cmd = new SqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@Boodschappen", bon.Description);
    cmd.Parameters.AddWithValue("@Datum", bon.Date);
    cmd.Parameters.AddWithValue("@LocatieId", bon.Loc);
    cmd.ExecuteNonQuery();
    conn.Close();
   }
  }

  public List<Bon> Select()
  {
   return DB.RunQuery(new Bon());
  }
 }
}