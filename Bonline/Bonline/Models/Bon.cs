using Bonline.Database;
using System;
using System.Data.SqlClient;

namespace Bonline.Models
{
 public class Bon : IQuery
 {
  public int Id { get; set; }
  public string Description { get; set; }
  public Locatie Loc { get; set; }
  public int LocatieId { get; set; }
  public DateTime Date { get; set; }
  public Account Acc { get; set; }
  public int AccId { get; set; }
  public string Query { get; set; }

  public Bon()
  {
   Query = "SELECT * FROM dbo.Bon";
  }

  public void Parse(SqlDataReader reader)
  {
   Id = reader.GetInt32(reader.GetOrdinal("Id"));
   Description = reader.GetString(reader.GetOrdinal("Boodschappen"));
   AccId = reader.GetInt32(reader.GetOrdinal("AccountID"));
   LocatieId = reader.GetInt32(reader.GetOrdinal("LocatieID"));

   //locatie
   //account
  Date = reader.GetDateTime(reader.GetOrdinal("Datum"));
  }
 } 
}