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
   string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$";

   bool isMailMatch = Regex.IsMatch(Request.Form["Email"], mailregex);
   bool isPassMatch = Regex.IsMatch(Request.Form["Password"], passregex);

   if (!isMailMatch)
   {
    ViewBag.Message = "Please enter a valid Emailaddress";
    return View();
   }

   if (!isPassMatch)
   {
    ViewBag.Message = "Please enter a password with atleast 1 uppercase letter, 1 lowercase letter and 1 digit";
    return View();
   }
   Account account = new Account
   {
    Email = Request.Form["email"],
    Password = Request.Form["password"],
    Admin = false,
    Inactief = false
   };
   PasswordManager.Hash(account.Email);
   PasswordManager.Hash(account.Password);
   accountRepository.AddAccount(account);
   return View();
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
   if (form.Count > 0)
   {
    Account acc = new Account
    {
	Email = Request.Form["Email"],
	Password = PasswordManager.Hash(Request.Form["Password"])
    };

    bool auth = accountRepository.LoginAccount(acc);
    bool Inactief = accountRepository.CheckInactiefAccount(acc);
    string id = accountRepository.LoginId(acc);

    if (Inactief)
    {
	ViewBag.Message = "Uw account is inactief";
	return View();
    }

    if (auth == false)
    {
	ViewBag.Message = "This is not a registered Account. Check your Email or Password.";
	return View();
    }

    if (id != null)
    {
	FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, id, DateTime.Now, DateTime.Now.AddHours(3), false, "", FormsAuthentication.FormsCookiePath);
	HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
	HttpContext.Response.Cookies.Add(c);
	return RedirectToAction("Index", "Home");
    }
   }
   return View();
  }

  [HttpGet]
  public ActionResult Login()
  {
   return View();
  }
 }
}
