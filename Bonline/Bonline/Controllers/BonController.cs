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
    public class BonController : Controller
    {
        private readonly BonRepository _bonRepository = new BonRepository(new MssqlBonContext());
        private readonly ListBonEnBon _viewModel = new ListBonEnBon();

        [HttpGet]
        public ActionResult Bon()
        {
            Datamanager.Initialize();
            TicketAuth auth = new TicketAuth();
            _viewModel.Bonnen = _bonRepository.SelectBonnen(auth.Decrypt()).ToList();
            _viewModel.Organisaties = _bonRepository.GetAllOrgs();
            return View(_viewModel);
        }


        [HttpPost]
        public ActionResult BonKassa(Bon b) => View("Kassa");

        [HttpPost]
        public ActionResult Bon(string GekozenOrg)
        {
            TicketAuth auth = new TicketAuth();
            List<Bon> GebruikerBonnen = _bonRepository.SelectBonnen(auth.Decrypt()).ToList();
            _viewModel.Bonnen = _bonRepository.GetBonnenMetOrgNaam(GekozenOrg, GebruikerBonnen );
            _viewModel.Organisaties = _bonRepository.GetAllOrgs();
                return View(_viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Bon bon)
        {
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
