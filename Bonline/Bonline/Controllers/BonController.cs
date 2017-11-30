using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Database;
using Bonline.Models;
using Bonline.Repositories;
using Bonline.Context.MSSQL;


namespace Bonline.Controllers
{
 public class BonController : Controller
 {
        BonRepository repo = new BonRepository(new MssqlBonContext());


  [HttpGet]
  public ActionResult Bon()
  {
   //Datamanager.Initialize();

   // List<Bon> BonnenList = Datamanager.BonList;
   List<Bon> BonnenList = DB.RunQuery(new Bon());
   var model = BonnenList;
   return View(model);
  }

        [HttpPost]
        public ActionResult BonKassa(Bon b) => View("Kassa");

        [HttpPost]
        public ActionResult Bondirect(Bon b)
        {

            

            DateTime Date = DateTime.Now;
            string Description = "lijst hier, " + Date.ToString();
            int LocatieId = 5;
            Bon f = new Bon(b.AccId, Date, Description, LocatieId);
            repo.InsertKassa(f);

            //repo.GetOrgName(f).Locatie, repo.GetOrgName(f).Org
            string Locatie = repo.GetOrgName(f).Locatie;
            string organisatie = repo.GetOrgName(f).Org;

            Bon G = new Bon(b.AccId, Date, Description, Locatie, organisatie);       
            
            
            return View("BonOverzicht", G);
        }


        public ActionResult GoToKassa()
        {
            return View("Kassa");
            
        }


    }


 
}