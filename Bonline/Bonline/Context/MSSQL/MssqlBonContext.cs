using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
 public class MssqlBonContext : Database.DB, IBonContext
 {
  public void Insert(Bon bon)
  {
   using (SqlConnection conn = new SqlConnection(ConnectionString))
   {
    conn.Open();
    string query = "INSERT INTO bon (description, date, location) VALUES (@description, @date, @location)";
    SqlCommand cmd = new SqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@description", bon.Description);
    cmd.Parameters.AddWithValue("@date", bon.Date);
    cmd.Parameters.AddWithValue("@location", bon.Loc);
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