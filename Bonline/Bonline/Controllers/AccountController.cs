using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Bonline.Controllers
{
 public class AccountController : Controller
 {

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Register()
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
   return View();
  }

  public ActionResult Login()
  {
   return View();
  }
 }
}
