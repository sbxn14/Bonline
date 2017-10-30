using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bonline.Models
{
 public class Account
 {
  public int ID { get; set; }
  public string Email { get; set; }
  public string Wachtwoord { get; set; }
  public string Query { get; set; }

  public Account()
  {
   Query = "Select * from account";
  }

  public void Parse(SqlDataReader reader)
  {
   ID = reader.GetInt32(reader.GetOrdinal("ID"));
   Email = reader.GetString(reader.GetOrdinal("email"));
   Wachtwoord = reader.GetString(reader.GetOrdinal("email"));
  }

 }
}