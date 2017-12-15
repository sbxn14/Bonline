﻿using Bonline.Context.MSSQL;
using Bonline.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Bonline.Database;
using Bonline.Repositories;
using Account = Bonline.Models.Account;

namespace Bonline.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository = new AccountRepository(new MssqlAccountContext());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account acc)
        {
            string mailregex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";
            bool isMailMatch = false;
            bool isPassMatch = false;

            if (acc.Email == null)
            {
                isMailMatch = false;
            }
            else if (acc.Password == null)
            {
                isPassMatch = false;
            }
            else
            {
                isMailMatch = Regex.IsMatch(acc.Email, mailregex);
                isPassMatch = Regex.IsMatch(acc.Password, passregex);
            }

            if (!isMailMatch && !isPassMatch)
            {
                ViewBag.Message1 = "Please enter a valid Email and Password.";
                return View();
            }

            if (!isMailMatch)
            {
                ViewBag.Message1 = "Please enter a valid Emailaddress";
                return View();
            }
            if (!isPassMatch)
            {
                ViewBag.Message2 =
                      "Please enter a password with atleast 1 uppercase letter, 1 lowercase letter and 1 digit and 8 characters";
                return View();
            }
            acc.Password = PasswordManager.Hash(acc.Password);
            _accountRepository.AddAccount(acc);
            return View("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account account)
        {
            TicketAuth ticket = new TicketAuth();
            account.Password = PasswordManager.Hash(account.Password);
            int accId = _accountRepository.LoginId(account);
            account = _accountRepository.SelectAccount(accId);

            if (_accountRepository.CheckInactiefAccount(account))
            {
                ViewBag.Message1 = "Uw account is inactief";
                return View("Login");
            }

            if (!_accountRepository.LoginAccount(account))
            {
                ViewBag.Message2 = "This is not a registered Account. Check your Email or Password.";
                return View("Login");
            }
            HttpCookie c = ticket.Encrypt(accId.ToString());
            HttpContext.Response.Cookies.Add(c);
            if (account.Admin)
            {
                return RedirectToAction("Accounts");
            }
            return RedirectToAction("Bon", "Bon");
        }

        [HttpGet]
        public ActionResult Accounts()
        {

            try
            {
                TicketAuth auth = new TicketAuth();
                HttpCookie c = System.Web.HttpContext.Current.Request.Cookies["__RequestVerificationToken"];
                int accId = auth.Decrypt();
                Account acc = _accountRepository.SelectAccount(accId);
                if (acc.Admin)
                {
                    List<Account> accounts = _accountRepository.SelectAccounts();
                    return View(accounts);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Accounts(Account acc)
        {
            _accountRepository.UpdateInactief(acc);
            return View();
        }
    }
}
