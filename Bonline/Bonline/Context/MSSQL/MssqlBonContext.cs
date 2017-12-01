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
   public void InsertKassa(Bon b)
   {
            using (SqlConnection conn = new SqlConnection(DB.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Bon(Datum, Boodschappen, AccountID, LocatieID) VALUES(@Datum, @Boodschappen, @AccountID, @LocatieID)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Datum", b.Date);
                cmd.Parameters.AddWithValue("@Boodschappen", b.Description);
                cmd.Parameters.AddWithValue("@AccountID", b.AccId);
                cmd.Parameters.AddWithValue("@LocatieId", b.LocatieId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

   }

   public Bon GetOrgName(Bon b)
        {
           
            using (SqlConnection conn = new SqlConnection(DB.ConnectionString))
            {

                try
                {
                    conn.Open();
                    string query = "select organisatie.naam as organisatienaam, locatie.naam as locatienaam from locatie inner join organisatie on locatie.orgid = organisatie.id where locatie.id = @locid";
                    SqlCommand cmd = new SqlCommand(query, conn);


                    cmd.Parameters.AddWithValue("@locid", b.LocatieId);
                    cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // get everything from the things and make a new obj
                            string org = (string)reader["organisatienaam"];
                            string loc = (string)reader["locatienaam"];

                            b = new Bon(org, loc);
                        }
                    }
                }

                catch
                {
                   b = null;
                }

                finally
                {
                    
                    conn.Close();
                }

                return b;

            }
        }

   public void GetLocName(Bon b)
        {
            using (SqlConnection conn = new SqlConnection(DB.ConnectionString))
            {
                conn.Open();
                string query = "select locatie.naam as @locatie from bon inner join locatie on bon.locatieid = locatie.id";
                SqlCommand cmd = new SqlCommand(query, conn);
            }


        }




  public void Insert(Bon bon)
  {
   try
   {
<<<<<<< HEAD
<<<<<<< refs/remotes/origin/Kassasysteem
    conn.Open();
    string query = "INSERT INTO Bon (Boodschappen, Datum, LocatieID,) VALUES (@Boodschappen, @Datum, @LocatieID)";
    SqlCommand cmd = new SqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@Boodschappen", bon.Description);
    cmd.Parameters.AddWithValue("@Datum", bon.Date);
    cmd.Parameters.AddWithValue("@LocatieId", bon.Loc);
    cmd.ExecuteNonQuery();
    conn.Close();
=======
=======
>>>>>>> master
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
<<<<<<< HEAD
>>>>>>> Details bonnen werkt, Zoeken niet meer?
=======
>>>>>>> master
   }
  }

  public List<Bon> Select()
  {
   return DB.RunQuery(new Bon());
  }
 }
}