using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Bonline.Database
{
    public static class Db
    {
        public static string ConnectionString { get; set; }

        static Db()
        {

            //connectionstring localhost   
            //ConnectionString = "Data Source=LAPT OP-GICS0PBT;Initial Catalog=BonlineDatabase.dbo;Integrated Security=True";

            //ConnectionString voor Azure database
            ConnectionString = "Server=tcp:bonline.database.windows.net,1433;Database=Bon-Line;Uid=Kassabon@bonline;Pwd=Fontys2017;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;";
        }


        public static List<T> RunQuery<T>(T value) where T : IQuery, new()
        {
            SqlDataReader reader = null;
            List<T> result = new List<T>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = value.Query;

                    try
                    {
                        cmd.Connection.Open();
                        cmd.Prepare();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            T obj = new T();
                            obj.Parse(reader);
                            result.Add(obj);
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.Write(e);
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        reader?.Close();
                    }
                    return result;
                }
            }
        }

        public static bool RunNonQuery(SqlCommand com)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = com)
                {
                    cmd.Connection = con;
                    try
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        Console.Write(e);
                        return false;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }
        }
    }
}