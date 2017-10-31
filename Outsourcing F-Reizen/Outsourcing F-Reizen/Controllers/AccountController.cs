using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Outsourcing_F_Reizen.Models;

namespace Outsourcing_F_Reizen.Controllers
{
    public class AccountController : Controller
    {

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            if (form.Count > 0)
            {
                Account acc = new Account
                {
                    Username = PasswordManager.Hash(Request.Form["Username"]),
                    Password = PasswordManager.Hash(Request.Form["Password"])
                };

                bool auth = Database.DB.LoginCheck(acc);

                if (auth == false)
                {
                    ViewBag.Message = "This is not a registered Account. Check your Username or Password.";
                    return View();
                }
                    return RedirectToAction("Index", "Home");
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