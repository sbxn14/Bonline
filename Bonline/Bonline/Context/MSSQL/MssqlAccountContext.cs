﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
 public class MssqlAccountContext : IAccountContext
 {

  public void Insert(Account account)
  {
   try
   {
    using (SqlConnection conn = new SqlConnection(DB.ConnectionString))
    {
	//changed query: waardes komen overeen met de db
	string query = "INSERT INTO dbo.Account (Administrator, Inactief, Email, Wachtwoord) VALUES (@Administrator, @Inactief, @Email, @Wachtwoord)";
	SqlCommand cmd = new SqlCommand(query);
	cmd.Parameters.AddWithValue("@Administrator", account.Admin);
	cmd.Parameters.AddWithValue("@Inactief", account.Inactief);
	cmd.Parameters.AddWithValue("@Email", account.Email);
	cmd.Parameters.AddWithValue("@Wachtwoord", account.Password);
	Database.DB.RunNonQuery(cmd);
    }
   }
   catch (Exception e)
   {
    Console.WriteLine(e);
    throw;
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
