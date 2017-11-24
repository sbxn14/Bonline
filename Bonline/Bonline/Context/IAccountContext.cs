using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonline.Models;

namespace Bonline.Context
{
 public interface IAccountContext
 {
  void Insert(Account account);
  List<Account> Select();
  void Update(Account account, string nieuwWachtwoord);
  void UpdateInactief(Account account);
 }
}
