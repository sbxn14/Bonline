using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Bonline.Context;
using Bonline.Context.MSSQL;
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
   int accounts = (from acc in this.context.Select()
			    where acc.Email.Equals(account.Email)
					&& acc.Password.Equals(account.Password)
			    select acc).Count();
   if (accounts >= 1)
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
   string accounts = (from acc in this.context.Select()
				  where acc.Email.Equals(account.Email)
				  && acc.Password.Equals(account.Password)
				  select acc).ToString();
   return accounts;
  }

  public bool CheckInactiefAccount(Account account)
  {
   try
   {
    Account accounts = (from acc in this.context.Select()
				    where acc.Email.Equals(account.Email)
						&& acc.Password.Equals(account.Password)
				    select acc).Single();
    return accounts.Inactief;
   }
   catch (Exception ex)
   {
    if (ex is ArgumentNullException || ex is NullReferenceException)
    {
	return true;
    }
   }
  }
 }
}