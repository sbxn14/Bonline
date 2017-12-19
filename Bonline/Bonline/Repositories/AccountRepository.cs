using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            List<Account> accounts = _context.Select();
            return accounts;
        }

        public void AddAccount(Account account)
        {
            _context.Insert(account);
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
            catch (Exception)
            {
                return 0;
            }
        }

        public bool CheckBestaatAccount(Account account)
        {
            try
            {
                List<Account> accounts = (from acc in _context.Select()
                                          where acc.Email.Equals(account.Email)
                                          select acc).ToList();
                if (accounts.Count != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is NullReferenceException)
                {
                    return false;
                }
            }
            return false;
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