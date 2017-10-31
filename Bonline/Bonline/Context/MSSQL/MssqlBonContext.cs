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
  public void Insert(Bon b)
  {
   string description = b.Description;
   DateTime date = b.Date;
   Locatie loc = b.Loc;

   string query = "INSERT INTO bon (description, date, location) VALUES (@description, @date, @location)";
   SqlCommand cmd = new SqlCommand(query);

   cmd.Parameters.AddWithValue("@description", description);
   cmd.Parameters.AddWithValue("@date", date);
   cmd.Parameters.AddWithValue("@location", loc);
   DB.RunNonQuery(cmd);
  }

  public List<Bon> Select()
  {
   throw new NotImplementedException();
  }
 }
}