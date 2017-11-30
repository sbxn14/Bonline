using Bonline.Database;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Bonline.Models
{
    public class Bon : IQuery
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Locatie Loc { get; set; }

        public string Locatie { get; set; }
        public int LocatieId { get; set; }
        public DateTime Date { get; set; }
        public Account Acc { get; set; }

        public string Org { get; set; }

        public string Orgid { get; set; }

        public string Account {get; set;}
        public int AccId { get; set; }
        public string Query { get; set; }

        public Bon()
        {
            Query = "SELECT * FROM dbo.Bon";
        }

        public Bon(int AccId, DateTime date, string description, int locatieid)
        {
            this.AccId = AccId;
            this.Date = date;
            this.Description = description;
            this.LocatieId = locatieid;

        }

        public Bon(int Accid, DateTime date, string description, string locatie, string organisatie)
        {
            this.AccId = Accid;
            this.Date = date;
            this.Description = description;
            this.Org = organisatie;
            this.Locatie = locatie;

        }

        public Bon(string org, string loc)
        {
            this.Org = org;
            this.Locatie = loc;
        }



  

  public void Parse(SqlDataReader reader)
  {
   Id = reader.GetInt32(reader.GetOrdinal("Id"));
   Description = reader.GetString(reader.GetOrdinal("Boodschappen"));
   AccId = reader.GetInt32(reader.GetOrdinal("AccountID"));
   LocatieId = reader.GetInt32(reader.GetOrdinal("LocatieID"));
   Date = reader.GetDateTime(reader.GetOrdinal("Datum"));

   Loc = Datamanager.LocList.FirstOrDefault(x => x.Id == LocatieId);
   Acc = Datamanager.AccList.FirstOrDefault(x => x.Id == AccId);






  }
 }
}