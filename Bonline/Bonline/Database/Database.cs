using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bonline.Database
{
 public class Database
 {
  private string connectionstring = "";
  public List<T> RunQuery<T>(T value) where T : IQuery, new()
  {
   List<T> results = new List<T>();
   using (SqlConnection con = new SqlConnection(connectionstring))
   {
    using (SqlCommand cmd = con.CreateCommand())
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

