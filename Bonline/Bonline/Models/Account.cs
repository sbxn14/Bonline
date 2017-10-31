using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Bonline.Database;

namespace Bonline.Models
{
 public class Account : IQuery
 {
  public int Id { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public bool Admin { get; set; }
  public string Query { get; set; }


  public Account()
  {
   Query = "Select * from dbo.Accounts";
  }

  public void Parse(SqlDataReader reader)
  {
   Id = reader.GetInt32(reader.GetOrdinal("Id"));
   Email = reader.GetString(reader.GetOrdinal("Emailaddress"));
   Password = reader.GetString(reader.GetOrdinal("Password"));
   Admin = reader.GetBoolean(reader.GetOrdinal("Admin"));
  }

 }
}