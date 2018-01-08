using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
    public class MssqlBonContext : IBonContext
    {
        public void AddLocId(Bon b)
        {
            using (new SqlConnection(Db.ConnectionString))
            {
                string query = "INSERT INTO dbo.Locatie (Naam, OrgID, Address) VALUES (@naam, @orgid, @address)";
                SqlCommand cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@naam", "Tilburg");
                cmd.Parameters.AddWithValue("@orgid", 1);
                cmd.Parameters.AddWithValue("@address", b.Loc.Address);

                Db.RunNonQuery(cmd);
            }
        }

        public int GetLocId(Bon b)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.Locatie WHERE Address = @address";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@address", b.Loc.Address);
                    cmd.ExecuteNonQuery();
                    int locId = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locId = reader.GetInt32(reader.GetOrdinal("ID"));
                        }
                    }
                    conn.Close();

                    return locId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void InsertKassa(Bon b)
        {
            using (new SqlConnection(Db.ConnectionString))
            {
                string query =
                    "INSERT INTO Bon(Datum, Boodschappen, AccountID, LocatieID , Pic_ID) VALUES(@Datum, @Boodschappen, @AccountID, @LocatieID, @PicId)";
                SqlCommand cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Datum", b.Date);
                cmd.Parameters.AddWithValue("@Boodschappen", b.Description);
                cmd.Parameters.AddWithValue("@AccountID", b.Acc.Id);
                cmd.Parameters.AddWithValue("@LocatieId", b.Loc.Id);
                cmd.Parameters.AddWithValue("@PicId", b.imageId);
                Db.RunNonQuery(cmd);
            }
        }

        public Bon GetOrgName(Bon b)
        {
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                try
                {

                    conn.Open();
                    string query =
                        "select organisatie.naam as organisatienaam, locatie.naam as locatienaam from locatie inner join organisatie on locatie.orgid = organisatie.id where locatie.id = @locid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@locid", b.Loc.Id);
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


        public void Insert(Bon bon)
        {
            try
            {
                using (new SqlConnection(Db.ConnectionString))
                {
                    string query =
                        "INSERT INTO bon (Boodschappen, Datum, AccountID, LocatieID, Pic_ID ) VALUES (@Boodschappen, @Datum, @AccountID, @LocatieID, @PicId)";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Boodschappen", bon.Description);
                    cmd.Parameters.AddWithValue("@Datum", bon.Date);
                    cmd.Parameters.AddWithValue("@AccountID", bon.Acc.Id);
                    cmd.Parameters.AddWithValue("@LocatieId", bon.Loc.Id);
                    cmd.Parameters.AddWithValue("@PicId", bon.imageId);
                    Db.RunNonQuery(cmd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Bon> Select()
        {
            return Db.RunQuery(new Bon());
        }

        public ImageModel GetImage(int ImageId)
        {
            ImageModel image = new ImageModel();
            string constr = Database.Db.ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM tblFiles WHERE ID = @ImageId";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@ImageId", ImageId);
                using (cmd)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            image.Id = Convert.ToInt32(sdr["Id"]);
                            image.Name = sdr["Name"].ToString();
                            image.Info = sdr["Info"].ToString();
                            image.ContentType = sdr["ContentType"].ToString();
                            image.Data = (byte[])sdr["Data"];
                        }
                    }
                }
                con.Close();
            }
            return image;
        }
    }
}