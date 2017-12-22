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

        
        [HttpGet]
        public ActionResult Bon()
        {
            Datamanager.Initialize();
            TicketAuth _auth = new TicketAuth();
            _viewModel.Bonnen = _bonRepository.SelectBonnen(_auth.Decrypt()).ToList();
            _viewModel.Organisaties = _bonRepository.GetAllOrgs();
            return View(_viewModel);
        }


        [HttpPost]
        public ActionResult BonKassa(Bon b) => View("Kassa");

        [HttpPost]
        public ActionResult Bon(string GekozenOrg)
        {
            TicketAuth _auth = new TicketAuth();
            List<Bon> gebruikerBonnen = _bonRepository.SelectBonnen(_auth.Decrypt()).ToList();
            _viewModel.Bonnen = _bonRepository.GetBonnenMetOrgNaam(GekozenOrg, gebruikerBonnen );
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
                return View(bon);
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
            Acc_en_Bon viewmodel = new Acc_en_Bon();
            TicketAuth auth = new TicketAuth();
            viewmodel.Acc.Id = auth.Decrypt();
            viewmodel.B.Date = DateTime.Now;
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Toevoegen(Bon bon)
        {
            TicketAuth auth = new TicketAuth();
            bon.AccId = auth.Decrypt();
            _bonRepository.AddBon(bon);
            return View("Bon", "Bon");
        }
    }
}
