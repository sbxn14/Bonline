using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Bonline.Database;
using Bonline.Models;

namespace Bonline.Context.MSSQL
{
	public class MssqlBonContext : IBonContext
	{
		public void InsertKassa(Bon b)
		{
			using (new SqlConnection(Db.ConnectionString))
			{
				string query = "INSERT INTO Bon(Datum, Boodschappen, AccountID, LocatieID) VALUES(@Datum, @Boodschappen, @AccountID, @LocatieID)";
				SqlCommand cmd = new SqlCommand(query);

				cmd.Parameters.AddWithValue("@Datum", b.Date);
				cmd.Parameters.AddWithValue("@Boodschappen", b.Description);
				cmd.Parameters.AddWithValue("@AccountID", b.AccId);
				cmd.Parameters.AddWithValue("@LocatieId", b.LocatieId);
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


		public void Insert(Bon bon)
		{
			try
			{
				using (new SqlConnection(Db.ConnectionString))
				{
					string query = "INSERT INTO bon (Boodschappen, Datum, LocatieID, ) VALUES (@Boodschappen, @Datum, @LocatieID)";
					SqlCommand cmd = new SqlCommand(query);
					cmd.Parameters.AddWithValue("@Boodschappen", bon.Description);
					cmd.Parameters.AddWithValue("@Datum", bon.Date);
					cmd.Parameters.AddWithValue("@LocatieId", bon.Loc);
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
	}
}