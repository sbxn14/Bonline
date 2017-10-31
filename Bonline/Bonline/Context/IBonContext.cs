using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonline.Models;

namespace Bonline.Context
{
 interface IBonContext
 {
	 void Insert(Bon b);
	 List<Bon> Select();
 }
}
