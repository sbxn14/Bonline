using Bonline.Context.MSSQL;
using Bonline.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bonline.Repositories;
using Account = Bonline.Models.Account;

namespace Bonline.Controllers
{
 public class AccountController : Controller
 {
  private AccountRepository accountRepository = new AccountRepository(new MssqlAccountContext());

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Register(FormCollection form)
  {

   string mailregex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
   string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";

   bool isMailMatch = Regex.IsMatch(Request.Form["Email"], mailregex);
   bool isPassMatch = Regex.IsMatch(Request.Form["Password"], passregex);

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
    Email = PasswordManager.Hash(Request.Form["email"]),
    Password = PasswordManager.Hash(Request.Form["password"]),
   };
   accountRepository.AddAccount(account);
   return View("Login");
  }

  [HttpGet]
  public ActionResult Register()
  {
   return View();
  }

  [ValidateAntiForgeryToken]
  [HttpPost]
  public ActionResult Login(FormCollection form)
  {
   TicketAuth ticket = new TicketAuth();
   if (form.Count > 0)
   {
    Account acc = new Account
    {
	Email = PasswordManager.Hash(Request.Form["Email"]),
	Password = PasswordManager.Hash(Request.Form["Password"])
    };

    string id = accountRepository.LoginId(acc);

    if (!accountRepository.CheckInactiefAccount(acc))
    {
	ViewBag.Message1 = "Uw account is inactief";
	return View("Login");
    }

    if (!accountRepository.LoginAccount(acc))
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
   }
   return RedirectToAction("Bon", "Bon");
  }

  [HttpGet]
  public ActionResult Login()
  {
   return View();
  }
 }
}
