using System.Data.SqlClient;
using Bonline.Database;

namespace Bonline.Models
{
    public class Organisatie : IQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }

        public Organisatie()
        {
            Query = "SELECT * FROM dbo.Organisatie";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("ID"));
            Name = reader.GetString(reader.GetOrdinal("Naam"));
        }
    }
}