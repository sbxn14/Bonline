using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonline.Database
{
 public interface IQuery
 {
  string Query { get; set; }
  void Parse(SqlDataReader reader);

 }
}
