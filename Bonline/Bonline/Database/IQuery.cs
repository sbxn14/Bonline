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
	 void Parse(SqlDataReader reader);
	 string Query { get; set; }
 }
}
