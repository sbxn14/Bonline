using System.Collections.Generic;
using Bonline.Models;

namespace Bonline.Context
{
 public interface IAccountContext
 {
  void Insert(Account account);
  List<Account> Select();
  void UpdateInactief(Account account);
 }
}
