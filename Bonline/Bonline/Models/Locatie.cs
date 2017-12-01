using System.Data.SqlClient;
using System.Linq;
using Bonline.Database;

namespace Bonline.Models
{
 public class Locatie : IQuery
 {
  public int Id { get; set; }
  public Organisatie Org { get; set; }
  public int OrgId { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public string Query { get; set; }

  public Locatie()
  {
   Query = "SELECT * FROM dbo.Locatie";
  }

  public void Parse(SqlDataReader reader)
  {
   Id = reader.GetInt32(reader.GetOrdinal("Id"));
   OrgId = reader.GetInt32(reader.GetOrdinal("OrgID"));
   Name = reader.GetString(reader.GetOrdinal("Naam"));
   Address = reader.GetString(reader.GetOrdinal("Address"));


   Org = Datamanager.OrgList.FirstOrDefault(x => x.Id == OrgId);
  }
 }
}