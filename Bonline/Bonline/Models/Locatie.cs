using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Bonline.Database;

namespace Bonline.Models
{
    public class Locatie : IQuery
    {
        public int Id { get; set; }
        public Organisatie Org { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Query { get; set; }

        public Locatie()
        {
            Query = "SELECT * FROM dbo.Locaties";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            // organisatie
            Name = reader.GetString(reader.GetOrdinal("Name"));
            Address = reader.GetString(reader.GetOrdinal("Address"));
        }
    }
}