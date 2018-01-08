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

        [HttpGet]
        public ActionResult Kassa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kassa(Bon b)
        {
            return View("Kassa", b);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //changed name from Details to BonDetais to avoid confusion with Details, lower
        public ActionResult BonDetails(Bon bon)
        {
            bon.Date = DateTime.Now;
            bon.Description = "Boodschappen hier, " + DateTime.Now.ToString();
            bon.LocatieId = 5;
            bon.imageId = 1;

            //added the reference to the context
            _bonRepository.InsertKassa(bon);
            return RedirectToAction("Bon");
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



        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            try
            {
                Bon_en_Pic vm = new Bon_en_Pic();
                vm.b = _bonRepository.SelectBon(id);
                vm.image = _bonRepository.GetImage(vm.b.imageId);
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(vm.image.Data, 0, vm.image.Data.Length);
                return View(vm);
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
            b.LocatieId = _bonRepository.GetLocId(b);
            _bonRepository.AddBon(b);
            return RedirectToAction("Bon");
        }
    }
}
