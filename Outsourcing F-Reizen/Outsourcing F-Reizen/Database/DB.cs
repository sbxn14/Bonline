using System.Data.SqlClient;
using Outsourcing_F_Reizen.Models;

namespace Outsourcing_F_Reizen.Database
{
    public static class DB
    {
        private static string CS = "Data Source=DESKTOP-QQQ2UTC;Initial Catalog=Database;Integrated Security=True";
        public static bool LoginCheck(Account acc)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Accounts WHERE username=@username AND password=@password", conn);
            cmd.Parameters.AddWithValue("@username", acc.Username);
            cmd.Parameters.AddWithValue("@password", acc.Password);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                return true;
            }
            return false;
        }
    }
}
