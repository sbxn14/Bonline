using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
 public class MssqlAccountContext : IAccountContext
 {

  public void Insert(Account account)
  {
   string email = account.Email;
   string password = account.Password;
   bool admin = account.Admin;

   string query = "INSERT INTO account (email, password, admin) VALUES (@email, @password, @admin)";
   SqlCommand cmd = new SqlCommand(query);

   cmd.Parameters.AddWithValue("@email", email);
   cmd.Parameters.AddWithValue("@password", password);
   cmd.Parameters.AddWithValue("@admin", admin);
   DB.RunNonQuery(cmd);
  }

  public List<Account> Select()
  {
   return DB.RunQuery(new Account());
  }

  public void Update(int Id, string NewPassword)
  {

  }

  public void Delete(int Id)
  {
  }
 }
}
