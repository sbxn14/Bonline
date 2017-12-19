using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
    public class MssqlAccountContext : IAccountContext
    {

        public void Insert(Account account)
        {
            using (new SqlConnection(Db.ConnectionString))
            {
                //changed query: waardes komen overeen met de db
                string query = "INSERT INTO dbo.Account (Administrator, Inactief, Email, Wachtwoord) VALUES (@Administrator, @Inactief, @Email, @Wachtwoord)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Administrator", account.Admin);
                cmd.Parameters.AddWithValue("@Inactief", account.Inactief);
                cmd.Parameters.AddWithValue("@Email", account.Email);
                cmd.Parameters.AddWithValue("@Wachtwoord", account.Password);
                Db.RunNonQuery(cmd);
            }
        }


        public List<Account> Select()
        {
            return Db.RunQuery(new Account());
        }

        public void UpdateInactief(Account account)
        {
            try
            {
                using (new SqlConnection(Db.ConnectionString))
                {
                    //changed query: waardes komen overeen met de db
                    string query = "UPDATE dbo.Account SET Inactief = @Inactief WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Inactief", account.Inactief);
                    cmd.Parameters.AddWithValue("@ID", account.Id);
                    Db.RunNonQuery(cmd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
