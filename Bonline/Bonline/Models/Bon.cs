using Bonline.Database;
using System;
using System.Data.SqlClient;
using System.Linq;
using Bonline.Models;

namespace Bonline.Models
{
    public class Bon : IQuery
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Locatie Loc { get; set; }
        public DateTime Date { get; set; }
        public Account Acc { get; set; }
        public string Org { get; set; }
        public ImageModel image { get; set; }
        public int imageId { get; set; }
        public string Query { get; set; }

        public Bon()
        {
            Query = "SELECT * FROM dbo.Bon";
        }

        public Bon(int accId, DateTime date, string description, int locatieid)
        {
            Acc.Id = accId;
            Date = date;
            Description = description;
            Loc.Id = locatieid;

        }

        public Bon(int accid, DateTime date, string description, string locatie, string organisatie)
        {
            Acc.Id = accid;
            Date = date;
            Description = description;
            Org = organisatie;
            Loc.Address = locatie;

        }

        public Bon(string org, string loc)
        {
            Org = org;
            Loc.Address = loc;
        }

        public void Parse(SqlDataReader reader)
        {
            if (Acc == null)
            {
                Acc = new Account();
            }
            if (Loc == null)
            {
                Loc = new Locatie();
            }
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Description = reader.GetString(reader.GetOrdinal("Boodschappen"));
            Acc.Id = reader.GetInt32(reader.GetOrdinal("AccountID"));
            Loc.Id = reader.GetInt32(reader.GetOrdinal("LocatieID"));
            Date = reader.GetDateTime(reader.GetOrdinal("Datum"));
            imageId = reader.GetInt32(reader.GetOrdinal("Pic_ID"));

            Loc = Datamanager.LocList.FirstOrDefault(x => x.Id == Loc.Id);
            Acc = Datamanager.AccList.FirstOrDefault(x => x.Id == Acc.Id);

        }

    }
}