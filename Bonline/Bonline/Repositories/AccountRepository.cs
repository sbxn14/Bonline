using System;
using System.Collections.Generic;
using System.Linq;
using Bonline.Context;
using Bonline.Models;

namespace Bonline.Repositories
{
	public class AccountRepository
	{
		readonly IAccountContext _context;

		public AccountRepository(IAccountContext context)
		{
			_context = context;
		}


		public List<Account> SelectAccounts()
		{
			List<Account> accounts = new List<Account>();
			_context.Select();
			return accounts;
		}

		public void AddAccount(Account account)
		{
			_context.Insert(account);
		}

		public void UpdateAccount(Account account, string nieuwWachtwoord)
		{
			_context.Update(account, nieuwWachtwoord);
		}

		public bool LoginAccount(Account account)
		{
			int accounts = (from acc in _context.Select()
							where acc.Email.Equals(account.Email)
								&& acc.Password.Equals(account.Password)
							select acc).Count();
			if (accounts >= 1)
			{
				return true;
			}
			return false;
		}

		public int LoginId(Account account)
		{
			try
			{
				Account accountId = (from acc in _context.Select()
									 where acc.Email.Equals(account.Email)
										  && acc.Password.Equals(account.Password)
									 select acc).Single();
				return accountId.Id;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public bool CheckInactiefAccount(Account account)
		{
			try
			{
				Account accounts = (from acc in _context.Select()
									where acc.Id.Equals(account.Id)
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
			return true;

		}

		public void UpdateInactief(Account account)
		{
			_context.UpdateInactief(account);
		}

		public Account SelectAccount(int id)
		{
			Account account = (from acc in _context.Select()
							   where acc.Id.Equals(id)
							   select acc).Single();
			return account;
		}


	}
}