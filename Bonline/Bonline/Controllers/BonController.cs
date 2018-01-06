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
        public ActionResult BonKassa(Bon b)
        {
            

            return View("Kassa", b);
        }

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
        //changed name from Details to BonDetais to avoid confusion with Details, lower
        public ActionResult BonDetails(Bon bon)
        {
            bon.Date = DateTime.Now;
            bon.Description = "Boodschappen hier, " + DateTime.Now.ToString();
            bon.LocatieId = 5;

            //added the reference to the context
            _bonRepository.InsertKassa(bon);
            return RedirectToAction("Bon");
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
            Bon b = new Bon();
            b.Date = DateTime.Now;
            return View(b);
        }

        [HttpPost]
        public ActionResult Toevoegen(Bon b)
        {
            b.LocatieId = _bonRepository.GetLocId(b);
            if (b.LocatieId == 0)
            {
                _bonRepository.AddLocId(b);
            }
            TicketAuth auth = new TicketAuth();
            b.AccId = auth.Decrypt();
            
            _bonRepository.AddBon(b);
            return RedirectToAction("Bon");
        }
    }
}
