using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Bonline.Context;
using Bonline.Models;

namespace Bonline.Repositories
{
 public class AccountRepository
 {
  IAccountContext context;

  public AccountRepository(IAccountContext context)
  {
   this.context = context;
  }


  public List<Account> SelectAccounts()
  {
   List<Account> accounts = new List<Account>();
   this.context.Select();
   return accounts;
  }

  public void AddAccount(Account account)
  {
   this.context.Insert(account);
  }

  public void DeleteAccount(Account account)
  {
   this.context.Delete(account);
  }

  public void UpdateAccount(Account account, string nieuwWachtwoord)
  {
   this.context.Update(account, nieuwWachtwoord);
  }

  public bool LoginAccount(Account account)
  {
   IEnumerable<Account> accounts = from acc in this.context.Select()
							  where acc.Email.Equals(account.Email)
							  where acc.Password.Equals(account.Password)
							  select acc;
   if (accounts.Contains(account))
   {
    return true;
   }
   else
   {
    return false;
   }
  }

  public string LoginId(Account account)
  {


   IEnumerable<Account> accounts = from acc in this.context.Select()
							where acc.Email.Equals(account.Email)
							where acc.Password.Equals(account.Password)
							select acc;
   if (accounts.Count() > 1)
   {
    //TODO duplicatie accounts, catch error
    return null;
   }
   else
   {
    return accounts.First().Id.ToString();
   }
  }

  public bool CheckInactiefAccount(Account account)
  {

   IEnumerable<Account> accounts = from acc in this.context.Select()
							where acc.Email.Equals(account.Email)
							where acc.Password.Equals(account.Password)
							select acc;
   if (accounts.Count() > 1)
   {
    //TODO duplicatie accounts, catch error
    return false;
   }
   else
   {
    return accounts.First().Inactief;
   }
  }
 }
}