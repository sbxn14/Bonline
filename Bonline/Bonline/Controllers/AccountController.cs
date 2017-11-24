using Bonline.Context.MSSQL;
using Bonline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bonline.Database;
using Bonline.Repositories;
using Account = Bonline.Models.Account;

namespace Bonline.Controllers
{
 public class AccountController : Controller
 {
  private AccountRepository accountRepository = new AccountRepository(new MssqlAccountContext());

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Register(Account acc)
  {

   string mailregex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
   string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";

   bool isMailMatch = Regex.IsMatch(acc.Email, mailregex);
   bool isPassMatch = Regex.IsMatch(acc.Password, passregex);

   if (!isMailMatch)
   {
    ViewBag.Message1 = "Please enter a valid Emailaddress";
    return View();
   }

   if (!isPassMatch)
   {
    ViewBag.Message2 = "Please enter a password with atleast 1 uppercase letter, 1 lowercase letter and 1 digit and 8 characters";
    return View();
   }
   Account account = new Account
   {
    Email = acc.Email,
    Password = PasswordManager.Hash(acc.Password)
   };
   accountRepository.AddAccount(account);
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

   string id = accountRepository.LoginId(account);

   if (accountRepository.CheckInactiefAccount(account))
   {
    ViewBag.Message1 = "Uw account is inactief";
    return View("Login");
   }

   if (!accountRepository.LoginAccount(account))
   {
    ViewBag.Message2 = "This is not a registered Account. Check your Email or Password.";
    return View("Login");
   }

   if (id != null)
   {
    HttpCookie c = ticket.Encrypt(id);
    HttpContext.Response.Cookies.Add(c);
    return RedirectToAction("Bon", "Bon");
   }
   return RedirectToAction("Bon", "Bon");
  }



  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Accounts()
  {
   List<Account> accountList = DB.RunQuery(new Account());
   var model = accountList;
   return View(model);
  }

//TODO verbeteren, zodat er geen exception nodig is.
  [HttpGet]
  public ActionResult Accounts(int id = 0)
  {
   try
   {
    Account account = accountRepository.SelectAccount(id);
    account.Inactief = !account.Inactief;
    accountRepository.UpdateInactief(account);
   }
   catch (Exception e)
   {
    Console.WriteLine(e);
   }
   List<Account> accountList = DB.RunQuery(new Account());
   return View(accountList);
  }

 }
}
