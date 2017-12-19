using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Context.MSSQL;
using Bonline.Database;
using Bonline.Models;
using Bonline.Repositories;
using Bonline.ViewModels;

namespace Bonline.Controllers
{
    [Authorize]
    public class BonController : Controller
    {
        private readonly BonRepository _bonRepository = new BonRepository(new MssqlBonContext());
        private readonly ListBonEnBon _viewModel = new ListBonEnBon();
        private readonly TicketAuth _auth = new TicketAuth();

        
        [HttpGet]
        public ActionResult Bon()
        {
            Datamanager.Initialize();
            _viewModel.Bonnen = _bonRepository.SelectBonnen(_auth.Decrypt()).ToList();
            _viewModel.Organisaties = _bonRepository.GetAllOrgs();
            return View(_viewModel);
        }


        [HttpPost]
        public ActionResult BonKassa(Bon b) => View("Kassa");

        [HttpPost]
        public ActionResult Bon(string GekozenOrg)
        {
            List<Bon> gebruikerBonnen = _bonRepository.SelectBonnen(_auth.Decrypt()).ToList();
            _viewModel.Bonnen = _bonRepository.GetBonnenMetOrgNaam(GekozenOrg, gebruikerBonnen );
            _viewModel.Organisaties = _bonRepository.GetAllOrgs();
                return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Bon bon)
        {
            int id = (int)Session["AccountId"];
            if (id <= 0)
            {
                RedirectToAction("Login", "Account");
            }

            return View("Details", bon);
        }

        public ActionResult GoToKassa()
        {
            return View("Kassa");
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            try
            {
                Bon bon = _bonRepository.SelectBon(id);
                return Details(bon);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Toevoegen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Toevoegen(Bon bon)
        {
            TicketAuth auth = new TicketAuth();
            bon.AccId = auth.Decrypt();
            _bonRepository.AddBon(bon);
            return View("Bon", "Bon");
        }
    }
}
