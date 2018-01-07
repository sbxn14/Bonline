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

        public Account GetAccount(int accId)
        {
            try
            {
                Account accounts = (from acc in _context.Select()
                                    where acc.Id.Equals(accId)
                                    select acc).Single();
                return accounts;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is NullReferenceException)
                {
                    return new Account();
                }
            }
            return new Account();
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

        public void UpdateInactief(Account account)
        {
            _context.UpdateInactief(account);
        }

        public Account SelectAccount(Account acc)
        {
            try
            {
                Account account = (from a in _context.Select()
                                   where a.Email.Equals(acc.Email)
                                         && a.Password.Equals(acc.Password)
                                   select a).Single();
                return account;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is NullReferenceException)
                {
                    return new Account();
                }
            }
            return new Account();
        }
    }
}