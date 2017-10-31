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
        public DateTime Date { get; set; }
        public string Query { get; set; }

        public Bon()
        {
            Query = "SELECT * FROM dbo.Bonnen";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Description = reader.GetString(reader.GetOrdinal("Description"));
            // Locatie //
            Date = reader.GetDateTime(reader.GetOrdinal("Date"));
        }
    }
}