using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
 public class MssqlAccountContext : Database.DB, IAccountContext
 {

  public void Insert(Account account)
  {
   using (SqlConnection conn = new SqlConnection(ConnectionString))
   {
    conn.Open();
    string query = "INSERT INTO dbo.Account (Administrator, Inactief, Email, Password) VALUES (@Administrator, @Inactief, @Email, @Password, @admin)";
    SqlCommand cmd = new SqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@Administrator", account.Admin);
    cmd.Parameters.AddWithValue("@Inactief", account.Inactief);
    cmd.Parameters.AddWithValue("@email", account.Email);
    cmd.Parameters.AddWithValue("@password", account.Password);
    cmd.ExecuteNonQuery();
    conn.Close();
   }
  }

  public List<Account> Select()
  {
   return DB.RunQuery(new Account());
  }

  public void Update(Account account, string NewPassword)
  {

  }

  public void Delete(Account account)
  {

  }
 }
}
