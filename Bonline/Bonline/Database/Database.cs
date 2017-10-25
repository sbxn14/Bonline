using System.Collections.Generic;
using System.Data.SqlClient;

namespace Bonline.Database
{
    public class Database
    {
        private readonly string connectionstring = "";

        public List<T> RunQuery<T>(T value) where T : IQuery, new()
        {
            var results = new List<T>();
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = con.CreateCommand())
                {
                    SqlDataReader reader = null;
                    cmd.Connection.Open();
                    cmd.Prepare();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                    }
                    cmd.Connection.Close();
                }
            }
            return results;
        }
    }
}