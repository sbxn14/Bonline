using Bonline.Context.MSSQL;
using Bonline.Models;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Bonline.Repositories;
using Bonline.ViewModels;

namespace Bonline.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository = new AccountRepository(new MssqlAccountContext());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account acc, string wachtwoord)
        {
            string mailregex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";
            bool isMailMatch = false;
            bool isPassMatch = false;
            if (acc.Email == null || acc.Password == null)
            {
                ViewBag.Message1 = "Vul een email en wachtwoord in.";
                return View();
            }

            if (acc.Password != wachtwoord)
            {
                ViewBag.Message1 = "Herhaling wachtwoord niet gelijk. Let op spelfouten.";
                return View();
            }
            isMailMatch = Regex.IsMatch(acc.Email, mailregex);
            isPassMatch = Regex.IsMatch(acc.Password, passregex);
            if (!isMailMatch && !isPassMatch)
            {
                ViewBag.Message1 = "Vul een email en wachtwoord in.";
                return View();
            }
            if (!isMailMatch)
            {
                ViewBag.Message1 = "Gebruik een valide email adres.";
                return View();
            }
            if (!isPassMatch)
            {
                ViewBag.Message2 = "Een wachtwoord moet ten minste 1 hoofdletter, 1 kleine letter, 1 cijfer hebben.";
                return View();
            }
            acc.Password = PasswordManager.Hash(acc.Password);
            if (_accountRepository.CheckBestaatAccount(acc))
            {
                ViewBag.Message1 = "Account met deze email bestaat al.";
                return View();
            }
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
            account = _accountRepository.SelectAccount(account);
            if (account.Id == 0)
            {
                ViewBag.Message = "Dit is geen geregistreerd account. Check of de ingevulde gegevens kloppen.";
                return View();
            }
            if (account.Inactief)
            {
                ViewBag.Message = "Uw account is inactief.";
                return View();
            }
            HttpCookie c = ticket.Encrypt(account.Id.ToString());
            HttpContext.Response.Cookies.Add(c);
            if (account.Admin)
            {
                return RedirectToAction("Accounts");
            }
            return RedirectToAction("Bon", "Bon");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Accounts(int id = 0)
        {
            ListAcc_en_Acc viewModel = new ListAcc_en_Acc();
            if (id == 0)
            {
                TicketAuth auth = new TicketAuth();
                int accId = auth.Decrypt();
                Account acc = _accountRepository.GetAccount(accId);
                if (acc.Id == 0)
                {
                    RedirectToAction("Index", "Home");
                }
                if (acc.Admin)
                {
                    viewModel.Accs = _accountRepository.SelectAccounts();
                    return View(viewModel);
                }
                return RedirectToAction("Index", "Home");
            }
            Account a = _accountRepository.GetAccount(id);
            a.Inactief = !a.Inactief;
            _accountRepository.UpdateInactief(a);
            viewModel.Accs = _accountRepository.SelectAccounts();
            return View(viewModel);
        }
    }
}
