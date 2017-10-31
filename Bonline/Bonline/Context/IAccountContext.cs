using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonline.Models;

namespace Bonline.Context
{
 interface IAccountContext
 {
	 void Insert(Account account);
	 List<Account> Select();
	 void Update(int id, string NieuwWachtwoord);
	 void Delete(int id);
 }
}
