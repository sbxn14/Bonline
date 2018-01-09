using System.Data.SqlClient;

namespace Bonline.Database
{
    public interface IQuery
    {
        string Query { get; set; }
        void Parse(SqlDataReader reader);
    }
}
